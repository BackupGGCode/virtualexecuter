using System;
using System.Collections.Generic;
using System.Text;

namespace VXA
{
	class CommandLineParser
	{
		public static Dictionary<string, string> Run(string[] arguments)
		{
			Dictionary<string, string> options = new Dictionary<string, string>();

			int i = 0;
			while (i < arguments.Length)
			{
				if (arguments[i].StartsWith("-") == false)
				{
					throw new Exception("Command line options must start with '-'");
				}
				if (arguments[i].Length != 2)
				{
					throw new Exception("Command line options must be on the form '-x' where the x represents the option letter");
				}

				if ((i + 1) < arguments.Length)
				{
					if (arguments[i + 1].StartsWith("-") == false)
					{
						options.Add(arguments[i].Substring(1), arguments[i + 1]);
						i += 2;
					}
					else
					{
						options.Add(arguments[i].Substring(1), "");
						i += 1;
					}
				}
				else
				{
					options.Add(arguments[i].Substring(1), "");
					i += 1;
				}

			}
			return options;
		}
	}
}
