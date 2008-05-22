using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain.Assembler
{
	class StackLabel
	{
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private int size;
		public int Size
		{
			get { return size; }
			set { size = value; }
		}
	}
}
