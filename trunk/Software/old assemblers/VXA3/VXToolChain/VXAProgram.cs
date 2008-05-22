using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VXToolChain.Assist;

namespace VXToolChain
{
	class VXAProgram
	{
		private List<VXASection> _sections = new List<VXASection>();
		public List<VXASection> Sections
		{
			get { return _sections; }
			set { _sections = value; }
		}

		private InstructionSet instructionSet = new InstructionSet();


		public void AddNewLabel(string sectionName, string labelName, params string[] arguments)
		{
			if (arguments.Length == 1 && arguments[0] == "")
			{
				arguments = new string[0];
			}

			switch (sectionName)
			{
				case "code":
					if (arguments.Length != 0)
					{
						throw new Exception("Code section labels can not take arguments");
					}
					AddLabel(sectionName, labelName);
					break;
				case "const":
					if (arguments.Length != 2)
					{
						throw new Exception("Const section labels must have type and value specified");
					}
					AddLabel(sectionName, labelName, arguments[0], "1", arguments[1]);
					break;
				case "data":
					if (arguments.Length < 1 || arguments.Length > 2)
					{
						throw new Exception("Data section labels must have at least their type specified and may have a count specified as well");
					}
					if (arguments.Length == 1)
					{
						AddLabel(sectionName, labelName, arguments[0]);
					}
					else
					{
						AddLabel(sectionName, labelName, arguments[0], arguments[1]);
					}
					break;
				case "stack":
					if (arguments.Length != 2)
					{
						throw new Exception("Stack section labels must have at least their type specified and may have a count specified as well");
					}
					AddLabel(sectionName, labelName, arguments[0], arguments[1]);
					break;
				default:
					throw new Exception("Invalid section '" + sectionName + "'");
					break;
			}
		}

		private void AddLabel(string sectionName, string name)
		{
			AddLabel(sectionName, name, VXALabel.LabelType.None.ToString(), "1", "");
		}
		private void AddLabel(string sectionName, string name, string type)
		{
			AddLabel(sectionName, name, type, "1", "");
		}
		private void AddLabel(string sectionName, string name, string type, string count)
		{
			AddLabel(sectionName, name, type, count, "");
		}
		private void AddLabel(string sectionName, string name, string type, string count, string value)
		{
			VXASection existingSection = null;

			foreach (VXASection sec in _sections)
			{
				if (sec.LabelExists(name))
				{
					if (sec.Name == sectionName)
					{
						throw new Exception("Duplicate label '" + name + "' found twice in section " + sec.Name);
					}
					else
					{
						throw new Exception("Duplicate label '" + name + "' found in both the " + sec.Name + " and the " + sectionName + " sections");
					}
				}
				if (sec.Name == sectionName)
				{
					existingSection = sec;
				}
			}

			if (existingSection == null)
			{
				if (sectionName == "code" || sectionName == "const" || sectionName == "stack" || sectionName == "data")
				{
					existingSection = new VXASection(sectionName);
				}
				else
				{
					throw new Exception("Invalid section '" + sectionName + "'");
				}

				_sections.Add(existingSection);
			}

			VXALabel.LabelType labelType;
			try
			{
				labelType = (VXALabel.LabelType)Enum.Parse(typeof(VXALabel.LabelType), type, true);
			}
			catch (Exception ex)
			{
				throw new Exception("Invalid label type '" + type + "'");
			}

			int c;
			if (int.TryParse(count, out c) == false)
			{
				throw new Exception("Invalid count '" + count + "' for label '" + name + "'");
			}
			existingSection.AddLabel(name, labelType, c, value);
		}

		public void AddInstruction(string line, VXAProgram program)
		{
			AddSectionData("code", instructionSet.ParseAndCreateData(line, program));
		}

		public byte[] GetSectionData(string sectionName)
		{
			foreach (VXASection section in _sections)
			{
				if (section.Name == sectionName)
				{
					return section.Data.ToArray();
				}
			}
			return new byte[0];
		}
		public void AddSectionData(string sectionName, byte[] data)
		{
			foreach (VXASection section in _sections)
			{
				if (section.Name == sectionName)
				{
					section.AddData(data);
				}
			}
		}

		public int GetSectionSize(string sectionName)
		{
			foreach (VXASection section in _sections)
			{
				if (section.Name == sectionName)
				{
					return section.Size;
				}
			}
			return 0;
		}
		public void AddToSectionSize(string sectionName, string line)
		{
			foreach (VXASection section in _sections)
			{
				if (section.Name == sectionName)
				{
					//section.Size += instructionSet.ParseAndGetSize(line);
				}
			}
		}
		/*
		public void AddInstruction(string line)
		{
			AddSectionData("code", instructionSet.ParseAndCreateData(line));
		}
		*/

		public int GetLabelAddress(string labelName)
		{
			foreach (VXASection section in _sections)
			{
				foreach (VXALabel label in section.Labels)
				{
					if (label.Name == labelName)
					{
						return label.Address;
					}
				}
			}

			throw new Exception("Label '" + labelName + "' not found");
		}
	}
}
