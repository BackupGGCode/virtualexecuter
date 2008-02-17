using System;
using System.Collections.Generic;
using System.Text;

namespace coma
{
	class CommandLineParser
	{
		public static Dictionary<string, List<string>> Run(string[] arguments)
		{
			Dictionary<string, List<string>> options = new Dictionary<string, List<string>>();

			int i = 0;
			while (i < arguments.Length)
			{
				string option = arguments[i];

				if (option.StartsWith("-") == false)
				{
					throw new Exception("Command line options must start with '-'");
				}
				if (option.Length != 2)
				{
					throw new Exception("Command line options must be on the form '-x' where the x represents the option letter");
				}

				List<string> list = new List<string>();

				i++;
				if (i < arguments.Length)
				{
					while (i < arguments.Length && arguments[i].StartsWith("-") == false)
					{
						list.Add(arguments[i++]);
					}
				}

				options.Add(option.Substring(1), list);
			}

			return options;
		}
	}
}
