using System;
using System.Collections.Generic;
using System.Text;
using Coma;
using System.IO;

namespace VXA
{
	class Program
	{
		static bool waitWhenExiting = false;
		static string sourceFileName = "";
		static string outputFileName = "";
		static FileStream outputFile = null;
		static string listFileName = "";
		static string mapFileName = "";

		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Assembler by Claus Andersen");
			Console.WriteLine("Version: 0.02 - May 27th 2008");
			Console.WriteLine("");

			try
			{
				#region Parse command line
				Dictionary<string, List<string>> options = CommandLineParser.Run(args);

				if (options == null || options.Count == 0)
				{
					Help();
					Quit();
					return;
				}

				if (options.ContainsKey("default"))
				{
					sourceFileName = options["default"][0];
					if (File.Exists(sourceFileName) == false)
					{
						Console.WriteLine("The specified source file does not exist");
						Quit();
						return;
					}
					outputFileName = Path.ChangeExtension(sourceFileName, ".vxx");
				}

				if (options.ContainsKey("o"))
				{
					outputFileName = options["o"][0];
				}

				if (options.ContainsKey("l"))
				{
					if (options["l"].Count == 0)
					{
						listFileName = Path.ChangeExtension(sourceFileName, ".lst");
					}
					else if (options["l"].Count == 1)
					{
						listFileName = options["l"][0];
					}
					else
					{
						Console.WriteLine("More than one list file was specified");
					}
				}

				if (options.ContainsKey("m"))
				{
					if (options["m"].Count == 0)
					{
						mapFileName = Path.ChangeExtension(sourceFileName, ".map");
					}
					else if (options["m"].Count == 1)
					{
						mapFileName = options["m"][0];
					}
					else
					{
						Console.WriteLine("More than one map file was specified");
					}
				}

				if (sourceFileName == "")
				{
					Help();
					Quit();
					return;
				}

				if (options.ContainsKey("w"))
				{
					waitWhenExiting = false;
				}
				if (options.ContainsKey("W"))
				{
					waitWhenExiting = true;
				}
				#endregion

				outputFile = File.Create(outputFileName);

				if (listFileName != "")
				{
					Informer.Instance.SetListFile(listFileName);
				}

				if (mapFileName != "")
				{
					Informer.Instance.SetMapFile(mapFileName);
				}

				Assembler asm = new Assembler();

				StreamReader preprocessedSourceFile = asm.Preprocessor(sourceFileName);

				asm.FindLabels(preprocessedSourceFile);
				asm.GenerateCode(preprocessedSourceFile);
				asm.GenerateExecutable(outputFile);

				preprocessedSourceFile.Close();
				outputFile.Close();
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
			Console.WriteLine("  f - Source file.");
			Console.WriteLine("");
			Console.WriteLine("Usage:");
			Console.WriteLine("  vxa -f test.vxa");
			Console.WriteLine("");
		}
	}
}
