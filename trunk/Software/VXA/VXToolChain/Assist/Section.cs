using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain.Assist
{
	class Section
	{
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private List<Label> labels = new List<Label>();
		public List<Label> Labels
		{
			get { return labels; }
			set { labels = value; }
		}

		private List<Function> functions = new List<Function>();
		public List<Function> Functions
		{
			get { return functions; }
			set { functions = value; }
		}

		private List<byte> data = new List<byte>();
		public List<byte> Data
		{
			get { return data; }
			set { data = value; }
		}

		public Section(string text)
		{
			if (text.Contains(".") == false)
			{
				throw new Exception("This is not a valid section '" + text + "'");
			}

			string line = text;
			int i = line.IndexOfAny(new char[] { ' ', '\t', '\n' });

			if (i >= 0)
			{
				line = line.Remove(i);
			}
			i = text.IndexOf('.');
			if (i >= 0)
			{
				line = line.Substring(i + 1);
			}
			name = line;
		}

		public void AddData(byte[] dataBytes)
		{
			data.AddRange(dataBytes);
		}
	}
}
