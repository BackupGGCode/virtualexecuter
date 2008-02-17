using System;
using System.Collections.Generic;
using System.Text;

namespace VXL
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Assembler");
			//Console.WriteLine("Linker version: " + VXAAssembler.AssemblerVersion + " - " + VXAAssembler.BuildDate);
			//Console.WriteLine("Highest VX core version supported: " + VXAAssembler.HighestCoreVersion);

			try
			{
				//VXAAssembler.Run(CommandLineParser.Run(args));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);// + "\n\n" + ex.StackTrace);
			}
		}
	}
}
