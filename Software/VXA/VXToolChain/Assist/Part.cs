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

		public string GetNameOfCurrentSection()
		{
			if (currentSection == null)
			{
				throw new Exception("No section defined yet");
			}

			return currentSection.Name;
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
		}
		
		public void AddInstruction(Instruction instruction)
		{
		
		}

		public void Save(string filename)
		{
			foreach (Section section in sections)
			{
				Console.WriteLine(section.Name);
				foreach (Label label in section.Labels)
				{
					Console.WriteLine("  " + label.Name);
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
