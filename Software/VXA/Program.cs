using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VXToolChain;
using coma;

namespace VXA
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Assembler");
			Console.WriteLine("Assembler version: " + VXAAssembler.AssemblerVersion + " - " + VXAAssembler.BuildDate);
			Console.WriteLine("Highest VX core version supported: " + VXAAssembler.HighestCoreVersion);

			try
			{
				VXAAssembler.Run(CommandLineParser.Run(args));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);// + "\n\n" + ex.StackTrace);
			}

		}
	}
}
