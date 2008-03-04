using System;
using System.Collections.Generic;
using System.Text;
using VXToolChain.Assist;
using System.IO;

namespace VXToolChain
{
	class Parser
	{
		InstructionSet instructionSet = new InstructionSet();

		public void FindAllLabels(Stream preprocessedFile, Part part)
		{
			StreamReader reader = new StreamReader(preprocessedFile);
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				if (line.Length > 0)
				{
					if (line.Contains("."))
					{
						part.SetCurrentSection(new Section(line));
					}
					else if (line.Contains(":"))
					{
						part.AddLabel(new Label(line));
					}
					else if (part.GetNameOfCurrentSection() == "code")
					{
						part.AddCode(new byte[instructionSet.ParseAndGetSize(line)]);
					}
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
				else if (part.GetNameOfCurrentSection() == "code")
				{
					part.AddCode(instructionSet.ParseAndCreateData(line, null));
				}
			}
		}
	}
}
