using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain
{
	class VXAInstructionSet
	{
		private List<VXAInstruction> instructions = new List<VXAInstruction>();

		public VXAInstructionSet()
		{
			int opcode = 0;
			// Arithmetic
			instructions.Add(new VXAInstruction("adds", opcode++));
			instructions.Add(new VXAInstruction("addd", opcode++));
			instructions.Add(new VXAInstruction("addq", opcode++));
			instructions.Add(new VXAInstruction("addf", opcode++));
			instructions.Add(new VXAInstruction("addsc", opcode++));
			instructions.Add(new VXAInstruction("adddc", opcode++));
			instructions.Add(new VXAInstruction("addqc", opcode++));
			instructions.Add(new VXAInstruction("addfc", opcode++));
			instructions.Add(new VXAInstruction("subs", opcode++));
			instructions.Add(new VXAInstruction("subd", opcode++));
			instructions.Add(new VXAInstruction("subq", opcode++));
			instructions.Add(new VXAInstruction("subf", opcode++));
			instructions.Add(new VXAInstruction("subsc", opcode++));
			instructions.Add(new VXAInstruction("subdc", opcode++));
			instructions.Add(new VXAInstruction("subqc", opcode++));
			instructions.Add(new VXAInstruction("subfc", opcode++));
			instructions.Add(new VXAInstruction("muls", opcode++));
			instructions.Add(new VXAInstruction("muld", opcode++));
			instructions.Add(new VXAInstruction("mulq", opcode++));
			instructions.Add(new VXAInstruction("mulf", opcode++));
			instructions.Add(new VXAInstruction("mulsc", opcode++));
			instructions.Add(new VXAInstruction("muldc", opcode++));
			instructions.Add(new VXAInstruction("mulqc", opcode++));
			instructions.Add(new VXAInstruction("mulfc", opcode++));
			instructions.Add(new VXAInstruction("incs", opcode++));
			instructions.Add(new VXAInstruction("incd", opcode++));
			instructions.Add(new VXAInstruction("incq", opcode++));
			instructions.Add(new VXAInstruction("incf", opcode++));
			instructions.Add(new VXAInstruction("decs", opcode++));
			instructions.Add(new VXAInstruction("decd", opcode++));
			instructions.Add(new VXAInstruction("decq", opcode++));
			instructions.Add(new VXAInstruction("decf", opcode++));
			// Transfer
			instructions.Add(new VXAInstruction("loads", opcode++, VXAInstruction.ConstantType.Single, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("loadd", opcode++, VXAInstruction.ConstantType.Double, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("loadq", opcode++, VXAInstruction.ConstantType.Quad, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("loadf", opcode++, VXAInstruction.ConstantType.Float, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("copys", opcode++));
			instructions.Add(new VXAInstruction("copyd", opcode++));
			instructions.Add(new VXAInstruction("copyq", opcode++));
			instructions.Add(new VXAInstruction("copyf", opcode++));
			instructions.Add(new VXAInstruction("swaps", opcode++));
			instructions.Add(new VXAInstruction("swapd", opcode++));
			instructions.Add(new VXAInstruction("swapq", opcode++));
			instructions.Add(new VXAInstruction("swapf", opcode++));
			instructions.Add(new VXAInstruction("cars", opcode++));
			instructions.Add(new VXAInstruction("carc", opcode++));
			// Logical
			instructions.Add(new VXAInstruction("ands", opcode++));
			instructions.Add(new VXAInstruction("andd", opcode++));
			instructions.Add(new VXAInstruction("andq", opcode++));
			instructions.Add(new VXAInstruction("ors", opcode++));
			instructions.Add(new VXAInstruction("ord", opcode++));
			instructions.Add(new VXAInstruction("orq", opcode++));
			instructions.Add(new VXAInstruction("xors", opcode++));
			instructions.Add(new VXAInstruction("xord", opcode++));
			instructions.Add(new VXAInstruction("xorq", opcode++));
			instructions.Add(new VXAInstruction("coms", opcode++));
			instructions.Add(new VXAInstruction("comd", opcode++));
			instructions.Add(new VXAInstruction("comq", opcode++));
			instructions.Add(new VXAInstruction("negs", opcode++));
			instructions.Add(new VXAInstruction("negd", opcode++));
			instructions.Add(new VXAInstruction("negq", opcode++));
			instructions.Add(new VXAInstruction("shfls", opcode++));
			instructions.Add(new VXAInstruction("shfld", opcode++));
			instructions.Add(new VXAInstruction("shflq", opcode++));
			instructions.Add(new VXAInstruction("shfrs", opcode++));
			instructions.Add(new VXAInstruction("shfrd", opcode++));
			instructions.Add(new VXAInstruction("shfrq", opcode++));
			instructions.Add(new VXAInstruction("rotls", opcode++));
			instructions.Add(new VXAInstruction("rotld", opcode++));
			instructions.Add(new VXAInstruction("rotlq", opcode++));
			instructions.Add(new VXAInstruction("rotrs", opcode++));
			instructions.Add(new VXAInstruction("rotrd", opcode++));
			instructions.Add(new VXAInstruction("rotrq", opcode++));
			// Stack
			instructions.Add(new VXAInstruction("pushs", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("pushd", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("pushq", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("pushf", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("pushcs", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("pushcd", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("pushcq", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("pushcf", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("pops", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("popd", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("popq", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("popf", opcode++, VXAInstruction.ConstantType.Double));
			// Branches
			int temp = opcode;
			instructions.Add(new VXAInstruction("jmp", opcode++));
			instructions.Add(new VXAInstruction("jmpa", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("jmpz", opcode++));
			instructions.Add(new VXAInstruction("jmpnz", opcode++));
			instructions.Add(new VXAInstruction("jmpc", opcode++));
			instructions.Add(new VXAInstruction("jmpnc", opcode++));
			instructions.Add(new VXAInstruction("jmpo", opcode++));
			instructions.Add(new VXAInstruction("jmpno", opcode++));
			instructions.Add(new VXAInstruction("jmpn", opcode++));
			instructions.Add(new VXAInstruction("jmpnn", opcode++));
			instructions.Add(new VXAInstruction("jmps", opcode++));
			instructions.Add(new VXAInstruction("jmpns", opcode++));
			instructions.Add(new VXAInstruction("call", opcode++));
			instructions.Add(new VXAInstruction("callc", opcode++, VXAInstruction.ConstantType.Double));
			instructions.Add(new VXAInstruction("ret", temp));			// alias for a jump
			// IO
			instructions.Add(new VXAInstruction("byo", opcode++));
			instructions.Add(new VXAInstruction("byi", opcode++));
			instructions.Add(new VXAInstruction("bio", opcode++));
			instructions.Add(new VXAInstruction("bii", opcode++));
			// Misc
			instructions.Add(new VXAInstruction("wait", opcode++));

			Console.WriteLine("Number of instructions in instruction set: " + instructions.Count);
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

			foreach (VXAInstruction instruction in instructions)
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

			foreach (VXAInstruction instruction in instructions)
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
