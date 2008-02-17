using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VXToolChain
{
	class VXAAssembler
	{
		public static string BuildDate = "Feb 5. 2008";
		public static string AssemblerVersion = "1.0.0";
		public const int HighestCoreVersion = 1;

		public static void Run(Dictionary<string, List<string>> options)
		{
			int targetCoreVersion = HighestCoreVersion;

			if (options.ContainsKey("s") == false)
			{
				throw new Exception("No source file specified");
			}

			if (options.ContainsKey("v"))
			{
				if (int.TryParse(options["v"][0], out targetCoreVersion) == false)
				{
					throw new Exception("Invalid target core version");
				}
			}

			string fullFileName = options["s"][0];
			string fileNameWithoutExtension = GetPathAndFilenameWithoutExtension(fullFileName);

			Console.WriteLine("Running pre processor...");
			FileStream sourceFile = File.OpenRead(fullFileName);
			MemoryStream preProcessed = VXAPreprocessor.Run(sourceFile);
			sourceFile.Close();
			preProcessed.Seek(0, SeekOrigin.Begin);

			#region Generate pre processor output file if -p option is set
			if (options.ContainsKey("p"))
			{
				Console.WriteLine("Writing pre processor output to file...");
				FileStream preProcessorFile = File.Create(fileNameWithoutExtension + ".pre");
				preProcessed.WriteTo(preProcessorFile);
				preProcessorFile.Close();
				preProcessed.Seek(0, SeekOrigin.Begin);
			}
			#endregion

			Console.WriteLine("Mapping labels...");
			VXAProgram program = FindAllLabels(preProcessed);


			#region Generate map file
			if (options.ContainsKey("m"))
			{
				Console.WriteLine("Writing map file...");
				FileStream mapFile = File.Create(fileNameWithoutExtension + ".map");
				StreamWriter mapWriter = new StreamWriter(mapFile);
				foreach (VXASection section in program.Sections)
				{
					mapWriter.WriteLine("Section: " + section.Name);
					Console.WriteLine("  Section: " + section.Name);
					foreach (VXALabel label in section.Labels)
					{
						mapWriter.Write("  " + label.Name + " " + label.Type + " " + label.Count + " @ " + label.Address + "\n");
						Console.WriteLine("    " + label.Name + " " + label.Type + " " + label.Count + " @ " + label.Address);
					}
				}
				mapWriter.Close();
				mapFile.Close();
			}
			#endregion

			preProcessed.Seek(0, SeekOrigin.Begin);

			Console.WriteLine("Producing binary data...");
			GenerateProgramOpcodes(preProcessed, program);

			int codeSize = program.GetSectionSize("code");
			int constSize = program.GetSectionSize("const");
			int dataSize = program.GetSectionSize("data");
			int stackSize = program.GetSectionSize("stack");
			int loadSize = codeSize + constSize;
			int inMemorySize = codeSize + constSize + dataSize + stackSize;


			#region Generate executable file
			Console.WriteLine("Writing executable file...");
			FileStream exeFile = File.Create(fileNameWithoutExtension + ".vxx");

			exeFile.WriteByte((byte)'V');		// Tag
			exeFile.WriteByte((byte)'X');
			exeFile.WriteByte((byte)'E');
			exeFile.WriteByte((byte)'X');
			exeFile.WriteByte((byte)'E');
			exeFile.WriteByte((byte)targetCoreVersion);		// Core version
			exeFile.WriteByte((byte)(codeSize >> 8));			// Code size
			exeFile.WriteByte((byte)(codeSize & 0xff));
			exeFile.WriteByte((byte)(constSize >> 8));		// Const size
			exeFile.WriteByte((byte)(constSize & 0xff));
			exeFile.WriteByte((byte)(dataSize >> 8));			// Data size
			exeFile.WriteByte((byte)(dataSize & 0xff));
			exeFile.WriteByte((byte)(stackSize >> 8));		// Stack size
			exeFile.WriteByte((byte)(stackSize & 0xff));

			exeFile.Write(program.GetSectionData("code"), 0, program.GetSectionSize("code"));
			exeFile.Write(program.GetSectionData("const"), 0, program.GetSectionSize("const"));

			exeFile.Close();
			#endregion

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Code size:      " + codeSize);
			Console.WriteLine("Const size:     " + constSize);
			Console.WriteLine("Data size:      " + dataSize);
			Console.WriteLine("Stack size:     " + stackSize);
			Console.WriteLine("---------------------");
			Console.WriteLine("Load size:      " + loadSize);
			Console.WriteLine("In-memory size: " + inMemorySize);
		}

		private static VXAProgram FindAllLabels(Stream s)
		{
			s.Seek(0, SeekOrigin.Begin);
			VXAProgram program = new VXAProgram();
			StreamReader reader = new StreamReader(s);
			string currentSectionName = "";
			int lineCounter = 1;
			string line = reader.ReadLine();

			while (line != null)
			{
				if (line.Contains("."))
				{
					currentSectionName = line.Substring(line.IndexOf('.') + 1);
				}
				else if (line.Contains(":"))
				{
					string labelName = line.Remove(line.IndexOf(':'));
					string restOfLine = "";

					if (line.EndsWith(":") == false)
					{
						restOfLine = line.Substring(line.IndexOf(':') + 1).Trim();
					}

					if (currentSectionName == "")
					{
						throw new Exception("Label definition for label '" + labelName + "' on line " + lineCounter + " appeared before a section was defined");
					}

					string[] arguments = restOfLine.Split(new char[] { ' ', '\t' });
					program.AddNewLabel(currentSectionName, labelName, arguments);
				}
				else
				{
					if (currentSectionName == "code")
					{
						program.AddToSectionSize(currentSectionName, line);
					}
				}

				line = reader.ReadLine();
				lineCounter++;
			}

			//reader.Close();

			return program;
		}

		private static void GenerateProgramOpcodes(Stream s, VXAProgram program)
		{
			StreamReader reader = new StreamReader(s);
			string currentSectionName = "";
			string line = reader.ReadLine();

			while (line != null)
			{
				if (line.Contains("."))
				{
					currentSectionName = line.Substring(line.IndexOf('.') + 1);
				}
				else if (line.Contains(":") == false && line != "")
				{
					if (currentSectionName == "code")
					{
						program.AddInstruction(line, program);
					}
				}
				line = reader.ReadLine();
			}

			reader.Close();
		}

		private static string GetPathAndFilenameWithoutExtension(string path)
		{
			if (Path.IsPathRooted(path))
			{
				return Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path);
			}
			else
			{
				return Path.GetFileNameWithoutExtension(path);
			}
		}
	}
}
