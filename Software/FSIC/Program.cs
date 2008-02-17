using System;
using System.Collections.Generic;
using System.Text;
using Coma.CommandLine;

namespace FSIC
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter FileStore Image Creator");
			//Console.WriteLine("FileStore version: " + VXAAssembler.AssemblerVersion + " - " + VXAAssembler.BuildDate);

			Dictionary<string, string> options = CommandLineParser.Run(args);

			try
			{
				//VXAAssembler.Run(options);
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
		}
	}
}
