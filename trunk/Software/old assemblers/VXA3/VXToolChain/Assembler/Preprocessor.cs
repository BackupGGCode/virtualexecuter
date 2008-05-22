using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VXToolChain.Assembler
{
	class Preprocessor
	{
		public Stream Preprocess(Stream file)
		{
			MemoryStream output = new MemoryStream();
			StreamWriter writer = new StreamWriter(output);
			StreamReader reader = new StreamReader(file);

			string line;
			while ((line = reader.ReadLine()) != null)
			{
				if (line.Contains(";"))
				{
					line = line.Remove(line.IndexOf(";"));
				}

				writer.WriteLine(line.Trim());
			}

			writer.Flush();

			file.Seek(0, SeekOrigin.Begin);
			output.Seek(0, SeekOrigin.Begin);

			return output;
		}
	}
}
