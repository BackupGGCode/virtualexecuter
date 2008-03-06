using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VXToolChain
{
	class VXAPreprocessor
	{
		public static MemoryStream Run(Stream inputStream)
		{
			StreamReader reader = new StreamReader(inputStream);
			MemoryStream outputStream = new MemoryStream();
			StreamWriter writer = new StreamWriter(outputStream);
			string line = reader.ReadLine();

			while (line != null)
			{
				int i = line.IndexOf(';');
				if (i >= 0)
				{
					line = line.Remove(i);
				}
				line = line.Trim();
				//if (line != "")
				//{
					writer.WriteLine(line);
				//}
				line = reader.ReadLine();
			}

			reader.Close();
			writer.Flush();
			return outputStream;
		}
	}
}
