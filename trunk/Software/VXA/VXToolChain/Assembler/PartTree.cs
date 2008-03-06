using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain.Assembler
{
	class PartTree
	{
		private List<StackLabel> stackLabels = new List<StackLabel>();
		public List<StackLabel> StackLabels
		{
			get { return stackLabels; }
			set { stackLabels = value; }
		}


	}
}
