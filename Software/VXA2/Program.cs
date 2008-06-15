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
				string preprocessorOutputDirectory = Directory.GetCurrentDirectory();
				string mapOutputDirectory = Directory.GetCurrentDirectory();
				string listOutputDirectory = Directory.GetCurrentDirectory();
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
					preprocessorOutputDirectory = options["o"][0];
					mapOutputDirectory = options["o"][0];
					listOutputDirectory = options["o"][0];
				}

				if (options.ContainsKey("p"))
				{
					generatePreprocessorFiles = true;

					if (options["p"] != null && options["p"].Count == 1)
					{
						preprocessorOutputDirectory = options["p"][0];
					}
					Directory.CreateDirectory(preprocessorOutputDirectory);
				}

				if (options.ContainsKey("m"))
				{
					generateMapFiles = true;

					if (options["m"] != null && options["m"].Count == 1)
					{
						mapOutputDirectory = options["m"][0];
					}
					Directory.CreateDirectory(mapOutputDirectory);
				}

				if (options.ContainsKey("l"))
				{
					generateListFiles = true;

					if (options["l"] != null && options["l"].Count == 1)
					{
						listOutputDirectory = options["l"][0];
					}
					Directory.CreateDirectory(listOutputDirectory);
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
					string sourceFileName = Path.GetFileNameWithoutExtension(sourceFile);
					int index = sourceFile.LastIndexOf("\\");
					string sourceFileDirectory = "";
					if (index > -1)
					{
						sourceFileDirectory = sourceFile.Remove(index);
					}

					StreamReader sourceFileReader = new StreamReader(sourceFile);
					FileStream partFile = File.Create(sourceFileName + ".part");

					Console.WriteLine("sf: " + sourceFileName);
					Console.WriteLine("sfd: " + sourceFileDirectory);

					Preprocessor preprocessor = new Preprocessor();
					Assembler asm = new Assembler();

					sourceFileReader = preprocessor.Run(sourceFileReader, sourceFileDirectory);
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

					asm.FindLabels(sourceFileReader);
					asm.GenerateCode(sourceFileReader);
					XmlSerializer serializer = new XmlSerializer(typeof(Assembler));
					serializer.Serialize(partFile, asm);
					
					//					asm.GenerateExecutable(outputFile);

					sourceFileReader.Close();
					partFile.Close();
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
