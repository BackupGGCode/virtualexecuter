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

		private bool IsSectionName(string line)
		{
			return line.Contains(".");
		}
		private string ParseSectionName(string line)
		{
			if (IsSectionName(line) == false)
			{
				return "";
			}

			string[] parts = line.Trim().Split(' ', '\t', '\n');
			return parts[0].Trim('.');
		}
		private bool IsStackLabel(string line)
		{
			if (line.Contains(".") || line.Contains(":") == false)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		private string ParseStackLabel(string line)
		{
			if (line.Contains(".") == false)
			{
				return "";
			}

			string[] parts = line.Trim().Split(' ', '\t', '\n');
			return parts[0].Trim('.');
		}

		public void FirstPass(Stream preprocessedFile, Part part)
		{
			StreamReader reader = new StreamReader(preprocessedFile);
			string line;
			string currentSectionName = "";

			while ((line = reader.ReadLine()) != null)
			{
				if (line != "")
				{
					if (IsSectionName(line))
					{
						currentSectionName = ParseSectionName(line);
					}

					switch (currentSectionName)
					{
						case "code":
							break;
						case "const":
							break;
						case "stack":
							if (IsStackLabel(line) == false)
							{
								throw new Exception("Invalid statement in stack section '" + line + "'");
							}


							break;
						case "data":
							break;
					}

					/*
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
					 */
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
