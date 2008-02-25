using System;
using System.Collections.Generic;
using System.Text;
using VXToolChain.Assist;
using System.IO;

namespace VXToolChain
{
	class Parser
	{
		public void FindAllLabels(Stream preprocessedFile, Part part)
		{
			StreamReader reader = new StreamReader(preprocessedFile);
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				if (line.Contains("."))
				{
					part.SetCurrentSection(new Section(line));
				}

				if (line.Contains(":"))
				{
					part.AddLabel(new Label(line));
				}
			}
		}

		public void GenerateCode(Stream preprocessedFile, Part part)
		{
			part.ResetSections();

			StreamReader reader = new StreamReader(preprocessedFile);
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				if (line.Contains("."))
				{
					part.SetCurrentSection(new Section(line));
				}

				if (part.GetNameOfCurrentSection() == "code")
				{
					part.AddInstruction(new Instruction(line));
				}
			}
		}
	}
}
