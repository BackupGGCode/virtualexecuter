using System;
using System.Collections.Generic;
using System.Text;
using Coma;
using System.IO;
using VXA.VX;
using System.Xml.Serialization;

namespace VXA
{
	class Program
	{
		static bool waitWhenExiting = false;

		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Assembler by Claus Andersen");
			Console.WriteLine("Version: 0.10 - June 14th 2008");
			Console.WriteLine("");

			try
			{
				List<string> sourceFiles;
				string outputDirectory = Directory.GetCurrentDirectory();
				bool generatePreprocessorFiles = false;
				bool generateMapFiles = false;
				bool generateListFiles = false;

				#region Parse command line
				Dictionary<string, List<string>> options = CommandLineParser.Run(args);

				if (options == null || options.Count == 0 || options.ContainsKey("default") == false || options["default"].Count == 0)
				{
					Help();
					Quit();
					return;
				}

				sourceFiles = options["default"];

				if (options.ContainsKey("o") && options["o"] != null && options["o"].Count == 1)
				{
					outputDirectory = options["o"][0];
					Directory.CreateDirectory(outputDirectory);
				}

				if (options.ContainsKey("p"))
				{
					generatePreprocessorFiles = true;
				}

				if (options.ContainsKey("m"))
				{
					generateMapFiles = true;
				}

				if (options.ContainsKey("l"))
				{
					generateListFiles = true;
				}

				if (sourceFiles == null || sourceFiles.Count == 0)
				{
					Help();
					Quit();
					return;
				}

				if (options.ContainsKey("w"))
				{
					waitWhenExiting = true;
				}
				#endregion

				foreach (string sourceFile in sourceFiles)
				{
					string sourceFileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFile);
					string sourceFileDirectory = Path.GetFullPath(sourceFile).Remove(sourceFile.LastIndexOf("\\"));
					string partFile = outputDirectory + "\\" + sourceFileNameWithoutExtension + ".part";
					string preprocessorFile = outputDirectory + "\\" + sourceFileNameWithoutExtension + ".pre";
					string mapFile = outputDirectory + "\\" + sourceFileNameWithoutExtension + ".map";
					string listFile = outputDirectory + "\\" + sourceFileNameWithoutExtension + ".list";

					StreamReader sourceFileReader = new StreamReader(sourceFile);
					FileStream partFileWriter = File.Create(partFile);
					StreamWriter preprocessorFileWriter = new StreamWriter(preprocessorFile);
					preprocessorFileWriter.AutoFlush = true;
					StreamWriter mapFileWriter = new StreamWriter(mapFile);
					mapFileWriter.AutoFlush = true;
					StreamWriter listFileWriter = new StreamWriter(listFile);
					listFileWriter.AutoFlush = true;

					Preprocessor pre = new Preprocessor();

					StreamReader preprocessorReader = pre.Run(sourceFileReader, sourceFileDirectory);

					/*
										if (generateListFiles)
										{
											Informer.Instance.SetListFile(listFileName);
										}

										if (mapFileName != "")
										{
											Informer.Instance.SetMapFile(mapFileName);
										}
					*/

					Assembler asm = new Assembler();

					asm.FindLabels(preprocessorReader);
					asm.GenerateCode(preprocessorReader);
					XmlSerializer serializer = new XmlSerializer(typeof(Assembler));
					serializer.Serialize(partFileWriter, asm);

					//					asm.GenerateExecutable(outputFile);


					sourceFileReader.Close();
					partFileWriter.Close();
					preprocessorFileWriter.Close();
					mapFileWriter.Close();
					listFileWriter.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.Message);
			}
			Quit();
		}

		static void Quit()
		{
			if (waitWhenExiting)
			{
				Console.Write("\nPress a key to continue...");
				Console.ReadKey(true);
			}
		}

		static void Help()
		{
			Console.WriteLine("Available options:");
			Console.WriteLine("  o - Output directory");
			Console.WriteLine("  p - Preprocessor directory");
			Console.WriteLine("  m - Map directory");
			Console.WriteLine("  l - List directory");
			Console.WriteLine("  w - Wait on exit");
			Console.WriteLine("");
			Console.WriteLine("Usage:");
			Console.WriteLine("  vxa test.vxa -o /bin -l");
			Console.WriteLine("");
		}
	}
}
