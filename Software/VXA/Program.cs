using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VXToolChain;
using Coma;

namespace VXA
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Assembler");
			Console.WriteLine("Assembler version: " + VXAAssembler.AssemblerVersion + " - " + VXAAssembler.BuildDate);
			Console.WriteLine("Highest VX core version supported: " + VXAAssembler.HighestCoreVersion);

			Dictionary<string, List<string>> options = CommandLineParser.Run(args);

			try
			{
				VXAAssembler.Run(options);
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

			if(options.ContainsKey("!"))
			{
				Console.WriteLine("Press any key to continue");
				Console.ReadKey();
			}
		}
	}
}
