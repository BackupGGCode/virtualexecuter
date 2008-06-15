using System;
using System.Collections.Generic;
using System.Text;

namespace VXA
{
	[Serializable]
	public class Label
	{
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		
		private int address;
		public int Address
		{
			get { return address; }
			set { address = value; }
		}

		public Label(string name, int address)
		{
			this.name = name;
			this.address = address;
		}
		
		public Label(){}
	}
}
