using System;
using System.Collections.Generic;
using System.Text;
using VXToolChain;
using coma;

namespace VXL
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Linker");
			Console.WriteLine("Linker version: " + VXLLinker.AssemblerVersion + " - " + VXLLinker.BuildDate);
			Console.WriteLine("Highest VX core version supported: " + VXLLinker.HighestCoreVersion);

			try
			{
				VXLLinker.Run(CommandLineParser.Run(args));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);// + "\n\n" + ex.StackTrace);
			}
		}
	}
}
