using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain.Assist
{
	class Function
	{
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private int localData;
		public int LocalData
		{
			get { return localData; }
			set { localData = value; }
		}

		private List<Label> labels;
		public List<Label> Labels
		{
			get { return labels; }
			set { labels = value; }
		}

		public Function(string name)
		{
			this.name = name;
		}

		public void AddLabel(Label label)
		{
			labels.Add(label);
		}
	}
}
