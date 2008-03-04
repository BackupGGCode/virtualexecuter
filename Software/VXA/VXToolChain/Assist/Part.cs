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

		public void ResetSections()
		{
			sections = new List<Section>();
			currentSection = null;
		}

		public void AddLabel(Label label)
		{
			if (currentSection == null)
			{
				throw new Exception("Label definition out side a section");
			}

			currentSection.Labels.Add(label);
			currentSection.AddData(new byte[label.Size]);
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

		public uint GetLabelAddress(string labelName)
		{
			foreach (Section section in sections)
			{
				foreach (Label label in section.Labels)
				{
					if (label.Name == labelName)
					{
						return label.Address;
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
