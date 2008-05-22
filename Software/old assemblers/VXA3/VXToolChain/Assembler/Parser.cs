using System;
using System.Collections.Generic;
using System.Text;
using VXToolChain.Assist;
using System.IO;

namespace VXToolChain.Assembler
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
						string[] lines = line.Trim().Split(new char[] { ':', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
						string name = lines[0].Trim('!', '&');
						
						if (line.StartsWith("!"))
						{
							part.CurrentFunctionName = name;
							part.AddFunction(new Function(line));
						}
						else if (line.StartsWith("!"))
						{
							part.CurrentFunctionName = name;
						}
						else
						{
							part.AddLabel(new Label(line, part));
						}
					}
					else if (part.GetCurrentSection().Name == "code")
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
				if (line.Length > 0)
				{
					Console.WriteLine("+++++" + line);

					if (line.Contains("."))
					{
						part.SetCurrentSection(new Section(line));
					}
					else if (line.Contains(":"))
					{
						if (line.StartsWith("!"))
						{
							string[] lines = line.Trim().Split(new char[] { ':', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
							part.CurrentFunctionName = lines[0].Trim('!', '&');
						}
					}
					else if (part.GetCurrentSection().Name == "code")
					{
						part.AddCode(instructionSet.ParseAndCreateData(line, part));
					}
				}
			}
		}
	}
}
