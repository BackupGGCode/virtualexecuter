using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VXToolChain.Assist;

namespace VXToolChain
{
	class Assembler
	{
		public static string Version = "1.1";
		public static string BuildDate = "Feb. 25. 2008";
		public static string Author = "Claus Andersen";
		public Part Assemble(string filename)
		{
			Preprocessor preprocessor = new Preprocessor();
			FileStream file = File.OpenRead(filename);
			Stream preprocessedFile = preprocessor.Preprocess(file);
			file.Close();

			Part part = new Part();
			Parser parser = new Parser();
			parser.FindAllLabels(preprocessedFile, part);
			preprocessedFile.Close();

			return part;
		}
	}
}
