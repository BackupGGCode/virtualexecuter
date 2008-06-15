using System;
using System.Collections.Generic;
using System.Text;

namespace VXA
{
	[Serializable]
	public class Instruction
	{
		private string mnemonic;
		public string Mnemonic
		{
			get { return mnemonic; }
			set { mnemonic = value; }
		}

		private int opcode;
		public int Opcode
		{
			get { return opcode; }
			set { opcode = value; }
		}

		private int size;
		public int Size
		{
			get { return size; }
			set { size = value; }
		}

		public Instruction()
		{ }
/*
		public Instruction(string mnemonic, int opcode, int size)
		{
			this.mnemonic = mnemonic;
			this.opcode = opcode;
			this.size = size;
		}*/
	}
}
