using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain.Assist
{
	class InstructionSet
	{
		private List<Instruction> instructions = new List<Instruction>();

		public InstructionSet()
		{
			int opcode = 0;
			// Arithmetic
			instructions.Add(new Instruction("adds", opcode++));
			instructions.Add(new Instruction("addd", opcode++));
			instructions.Add(new Instruction("addq", opcode++));
			instructions.Add(new Instruction("addf", opcode++));
			instructions.Add(new Instruction("addsc", opcode++));
			instructions.Add(new Instruction("adddc", opcode++));
			instructions.Add(new Instruction("addqc", opcode++));
			instructions.Add(new Instruction("addfc", opcode++));
			instructions.Add(new Instruction("subs", opcode++));
			instructions.Add(new Instruction("subd", opcode++));
			instructions.Add(new Instruction("subq", opcode++));
			instructions.Add(new Instruction("subf", opcode++));
			instructions.Add(new Instruction("subsc", opcode++));
			instructions.Add(new Instruction("subdc", opcode++));
			instructions.Add(new Instruction("subqc", opcode++));
			instructions.Add(new Instruction("subfc", opcode++));
			instructions.Add(new Instruction("muls", opcode++));
			instructions.Add(new Instruction("muld", opcode++));
			instructions.Add(new Instruction("mulq", opcode++));
			instructions.Add(new Instruction("mulf", opcode++));
			instructions.Add(new Instruction("mulsc", opcode++));
			instructions.Add(new Instruction("muldc", opcode++));
			instructions.Add(new Instruction("mulqc", opcode++));
			instructions.Add(new Instruction("mulfc", opcode++));
			instructions.Add(new Instruction("incs", opcode++));
			instructions.Add(new Instruction("incd", opcode++));
			instructions.Add(new Instruction("incq", opcode++));
			instructions.Add(new Instruction("incf", opcode++));
			instructions.Add(new Instruction("decs", opcode++));
			instructions.Add(new Instruction("decd", opcode++));
			instructions.Add(new Instruction("decq", opcode++));
			instructions.Add(new Instruction("decf", opcode++));
			// Transfer
			instructions.Add(new Instruction("loadls", opcode++, Instruction.ConstantType.Single, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("loadd", opcode++, Instruction.ConstantType.Double, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("loadq", opcode++, Instruction.ConstantType.Quad, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("loadf", opcode++, Instruction.ConstantType.Float, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("copys", opcode++));
			instructions.Add(new Instruction("copyd", opcode++));
			instructions.Add(new Instruction("copyq", opcode++));
			instructions.Add(new Instruction("copyf", opcode++));
			instructions.Add(new Instruction("swaps", opcode++));
			instructions.Add(new Instruction("swapd", opcode++));
			instructions.Add(new Instruction("swapq", opcode++));
			instructions.Add(new Instruction("swapf", opcode++));
			instructions.Add(new Instruction("cars", opcode++));
			instructions.Add(new Instruction("carc", opcode++));
			// Logical
			instructions.Add(new Instruction("ands", opcode++));
			instructions.Add(new Instruction("andd", opcode++));
			instructions.Add(new Instruction("andq", opcode++));
			instructions.Add(new Instruction("ors", opcode++));
			instructions.Add(new Instruction("ord", opcode++));
			instructions.Add(new Instruction("orq", opcode++));
			instructions.Add(new Instruction("xors", opcode++));
			instructions.Add(new Instruction("xord", opcode++));
			instructions.Add(new Instruction("xorq", opcode++));
			instructions.Add(new Instruction("coms", opcode++));
			instructions.Add(new Instruction("comd", opcode++));
			instructions.Add(new Instruction("comq", opcode++));
			instructions.Add(new Instruction("negs", opcode++));
			instructions.Add(new Instruction("negd", opcode++));
			instructions.Add(new Instruction("negq", opcode++));
			instructions.Add(new Instruction("shfls", opcode++));
			instructions.Add(new Instruction("shfld", opcode++));
			instructions.Add(new Instruction("shflq", opcode++));
			instructions.Add(new Instruction("shfrs", opcode++));
			instructions.Add(new Instruction("shfrd", opcode++));
			instructions.Add(new Instruction("shfrq", opcode++));
			instructions.Add(new Instruction("rotls", opcode++));
			instructions.Add(new Instruction("rotld", opcode++));
			instructions.Add(new Instruction("rotlq", opcode++));
			instructions.Add(new Instruction("rotrs", opcode++));
			instructions.Add(new Instruction("rotrd", opcode++));
			instructions.Add(new Instruction("rotrq", opcode++));
			// Stack
			instructions.Add(new Instruction("pushs", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushd", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushq", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushf", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushcs", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushcd", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushcq", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushcf", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pops", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("popd", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("popq", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("popf", opcode++, Instruction.ConstantType.Double));
			// Branches
			int temp = opcode;
			instructions.Add(new Instruction("jmp", opcode++));
			instructions.Add(new Instruction("jmpa", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("jmpz", opcode++));
			instructions.Add(new Instruction("jmpnz", opcode++));
			instructions.Add(new Instruction("jmpc", opcode++));
			instructions.Add(new Instruction("jmpnc", opcode++));
			instructions.Add(new Instruction("jmpo", opcode++));
			instructions.Add(new Instruction("jmpno", opcode++));
			instructions.Add(new Instruction("jmpn", opcode++));
			instructions.Add(new Instruction("jmpnn", opcode++));
			instructions.Add(new Instruction("jmps", opcode++));
			instructions.Add(new Instruction("jmpns", opcode++));
			instructions.Add(new Instruction("call", opcode++));
			instructions.Add(new Instruction("callc", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("ret", temp));			// alias for a jump
			// IO
			instructions.Add(new Instruction("byo", opcode++));
			instructions.Add(new Instruction("byi", opcode++));
			instructions.Add(new Instruction("bio", opcode++));
			instructions.Add(new Instruction("bii", opcode++));
			// Misc
			instructions.Add(new Instruction("wait", opcode++));
		}

		public byte[] ParseAndCreateData(string line, VXAProgram program)
		{
			if (line.Contains(".") || line.Contains(":"))
			{
				return new byte[0];
			}

			string[] parts = line.Split(new char[] { ' ', '\t' });
			if (parts.Length < 1)
			{
				return new byte[0];
			}

			foreach (Instruction instruction in instructions)
			{
				if (instruction.Mnemonic == parts[0])
				{
					return instruction.GetData(parts, program);
				}
			}

			throw new Exception("Unknown mnemonic '" + parts[0] + "'");
		}

		public int ParseAndGetSize(string line)
		{
			if (line.Contains(".") || line.Contains(":"))
			{
				return 0;
			}

			string[] parts = line.Split(new char[] { ' ', '\t' });
			if (parts.Length < 1)
			{
				return 0;
			}

			foreach (Instruction instruction in instructions)
			{
				if (instruction.Mnemonic == parts[0])
				{
					return instruction.Size;
				}
			}

			throw new Exception("Unknown mnemonic '" + parts[0] + "'");
		}
	}
}
