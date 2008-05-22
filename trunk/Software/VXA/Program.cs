using System;
using System.Collections.Generic;
using System.Text;
using Coma;
using System.IO;

namespace VXA
{
	class Program
	{
		static bool waitWhenExiting = true;
		static string sourceFileName = "";
		static StreamReader sourceFile = null;
		static string outputFileName = "";
		static StreamWriter outputFile = null;

		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Assembler by Claus Andersen");
			Console.WriteLine("Version: 0.01 - May 21th 2008");

			try
			{
				#region Parse command line
				Dictionary<string, List<string>> options = CommandLineParser.Run(args);

				if (options.ContainsKey("f"))
				{
					sourceFileName = options["f"][0];
					Console.WriteLine(sourceFile);
					if (File.Exists(sourceFileName) == false)
					{
						Console.WriteLine("The specified source file does not exist");
						Quit();
						return;
					}
					outputFileName = Path.ChangeExtension(sourceFileName, ".vxx");
				}

				if (options == null || options.Count == 0)
				{
					Help();
					Quit();
					return;
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
				#endregion

				sourceFile = new StreamReader(sourceFileName);
				outputFile = new StreamWriter(outputFileName);

				Assembler asm = new Assembler();
				asm.FindLabels(sourceFile);
				asm.GenerateCode(sourceFile);
				asm.GenerateExecutable(outputFile);

				sourceFile.Close();
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
