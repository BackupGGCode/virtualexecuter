using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VXToolChain;
using VXToolChain.Assembler;
using VXToolChain.Assist;
using Coma;

namespace VXA
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Assembler by Claus Andersen");
			Console.WriteLine("Version: 1.0 - March 3rd 2008");

			Dictionary<string, List<string>> options = CommandLineParser.Run(args);

			if (options.ContainsKey("f") == false)
			{
				throw new Exception("No input file specified");
			}
			if (options["f"].Count == 0)
			{
				throw new Exception("No input file specified");
			}
			if (options["f"].Count > 1)
			{
				throw new Exception("Only one input file is accepted");
			}

			string fullFileName = options["f"][0];
			string fileName = Path.GetFileName(fullFileName);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullFileName);
			string fileDirectory = Path.GetDirectoryName(fullFileName);

			Assembler assembler = new Assembler();

			try
			{
				Part part = assembler.Assemble(fullFileName);

				part.Save(fileDirectory + "/" + fileNameWithoutExtension + ".part");

				if (options.ContainsKey("l"))
				{
					part.GenerateListFile(fileDirectory + "/" + fileNameWithoutExtension + ".list");
				}

				if (options.ContainsKey("m"))
				{
					part.GenerateMapFile(fileDirectory + "/" + fileNameWithoutExtension + ".map");
				}
			}
			catch (Exception ex)
			{
				if (options.ContainsKey("@"))
				{
					Console.WriteLine(ex.Message + "\n\n" + ex.StackTrace);
				}
				else
				{
					Console.WriteLine(ex.Message);
				}
			}

			if (options.ContainsKey("!"))
			{
				Console.WriteLine("Press any key to continue");
				Console.ReadKey();
			}
		}
	}
}
