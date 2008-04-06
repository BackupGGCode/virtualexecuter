using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VXToolChain.Assist
{
	class Part
	{



		private List<Section> sections = new List<Section>();
		public List<Section> Sections
		{
			get { return sections; }
			set { sections = value; }
		}
		private Section currentSection = null;

		public void SetCurrentSection(Section section)
		{
			if (currentSection != null)
			{
				if (currentSection.Name == section.Name)
				{
					return;
				}
			}

			foreach (Section sec in sections)
			{
				if (sec.Name == section.Name)
				{
					currentSection = sec;
					return;
				}
			}

			sections.Add(section);
			currentSection = section;
		}

		public Section GetCurrentSection()
		{
			if (currentSection == null)
			{
				throw new Exception("No section defined yet");
			}

			return currentSection;
		}

		private List<Function> functions = new List<Function>();
		public List<Function> Functions
		{
			get { return functions; }
			set { functions = value; }
		}

		private string currentFunctionName = "";
		public string CurrentFunctionName
		{
			get { return currentFunctionName; }
			set { currentFunctionName = value; }
		}


		public void AddFunction(Function function)
		{
			functions.Add(function);
		}


		public void ResetSections()
		{
			foreach (Section section in sections)
			{
				section.Data.Clear();
			}
			//			sections = new List<Section>();
			currentSection = null;
			currentFunctionName = "";
		}

		public void AddLabel(Label label)
		{
			if (currentSection == null)
			{
				throw new Exception("Label definition out side a section");
			}

			currentSection.Labels.Add(label);
			if (label.IsLocal == false)
			{
				currentSection.AddData(new byte[label.Size]);
			}
			if (label.IsFunction)
			{
				functions.Add(new Function(label.Name));
				this.currentFunctionName = label.Name;
			}

			//			Console.WriteLine("Added label '" + label.Name + "'");
		}

		public void AddCode(byte[] data)
		{
			foreach (Section section in sections)
			{
				if (section.Name == "code")
				{
					section.Data.AddRange(data);
				}
			}
		}

		public int GetLabelAddress(string labelName)
		{
			foreach (Section section in sections)
			{
				foreach (Label label in section.Labels)
				{
					if (label.Name == labelName)
					{
						if (label.IsLocal)
						{
							if (label.Function == currentFunctionName)
							{
								return label.Address;
							}
							else
							{
								throw new Exception("Label '" + labelName + "' in function '" + currentFunctionName + "' is out of scope");
							}
						}
						else
						{
							return label.Address;
						}
					}
				}
			}

			throw new Exception("Label '" + labelName + "' not found");
		}

		public void Save(string filename)
		{
			foreach (Section section in sections)
			{
				Console.WriteLine(section.Name + " - " + section.Data.Count);
				foreach (Label label in section.Labels)
				{
					Console.WriteLine("  " + label.Name + "-" + label.Address);
				}
			}

			foreach (Section section in sections)
			{
				if (section.Name == "code")
				{
					foreach (byte b in section.Data)
					{
						Console.WriteLine("--" + b.ToString());
					}
				}
			}
		}

		public void Load(string filename)
		{ }

		public void GenerateMapFile(string filename)
		{
			StreamWriter f = File.CreateText(filename);
			f.WriteLine("blabla");
			f.Close();
		}

		public void GenerateListFile(string filename)
		{
			StreamWriter f = File.CreateText(filename);
			f.WriteLine("blabla");
			f.Close();
		}
	}
}
