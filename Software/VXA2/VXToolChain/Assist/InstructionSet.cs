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
			int opcode = 1;

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

			instructions.Add(new Instruction("divs", opcode++));
			instructions.Add(new Instruction("divd", opcode++));
			instructions.Add(new Instruction("divq", opcode++));
			instructions.Add(new Instruction("divf", opcode++));

			instructions.Add(new Instruction("incs", opcode++));
			instructions.Add(new Instruction("incd", opcode++));
			instructions.Add(new Instruction("incq", opcode++));
			instructions.Add(new Instruction("incf", opcode++));

			instructions.Add(new Instruction("decs", opcode++));
			instructions.Add(new Instruction("decd", opcode++));
			instructions.Add(new Instruction("decq", opcode++));
			instructions.Add(new Instruction("decf", opcode++));

			instructions.Add(new Instruction("cmps", opcode++));
			instructions.Add(new Instruction("cmpd", opcode++));
			instructions.Add(new Instruction("cmpq", opcode++));
			instructions.Add(new Instruction("cmpf", opcode++));

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

			instructions.Add(new Instruction("cars", opcode++));
			instructions.Add(new Instruction("carc", opcode++));

			// Transfer
			instructions.Add(new Instruction("loadls", opcode++, Instruction.ConstantType.Double, Instruction.ConstantType.Single));
			instructions.Add(new Instruction("loadld", opcode++, Instruction.ConstantType.Double, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("loadlq", opcode++, Instruction.ConstantType.Double, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("loadlf", opcode++, Instruction.ConstantType.Double, Instruction.ConstantType.Float));

			instructions.Add(new Instruction("loadgs", opcode++, Instruction.ConstantType.Quad, Instruction.ConstantType.Single));
			instructions.Add(new Instruction("loadgd", opcode++, Instruction.ConstantType.Quad, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("loadgq", opcode++, Instruction.ConstantType.Quad, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("loadgf", opcode++, Instruction.ConstantType.Quad, Instruction.ConstantType.Float));

			// Stack
			instructions.Add(new Instruction("pushs", opcode++, Instruction.ConstantType.Single));
			instructions.Add(new Instruction("pushd", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushq", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("pushf", opcode++, Instruction.ConstantType.Float));

			instructions.Add(new Instruction("pushls", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushld", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushlq", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("pushlf", opcode++, Instruction.ConstantType.Double));

			instructions.Add(new Instruction("pushgs", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("pushgd", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("pushgq", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("pushgf", opcode++, Instruction.ConstantType.Quad));

			instructions.Add(new Instruction("pushcs", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("pushcd", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("pushcq", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("pushcf", opcode++, Instruction.ConstantType.Quad));

			instructions.Add(new Instruction("popls", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("popld", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("poplq", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("poplf", opcode++, Instruction.ConstantType.Double));

			instructions.Add(new Instruction("popgs", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("popgd", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("popgq", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("popgf", opcode++, Instruction.ConstantType.Quad));

			// Branches
			instructions.Add(new Instruction("jmp", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("jmpz", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("jmpnz", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("jmpc", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("jmpnc", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("jmpn", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("jmpnn", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("jmpp", opcode++, Instruction.ConstantType.Double));
			instructions.Add(new Instruction("jmpnp", opcode++, Instruction.ConstantType.Double));

			instructions.Add(new Instruction("call", opcode++, Instruction.ConstantType.Quad));
			instructions.Add(new Instruction("icall", opcode++));
			instructions.Add(new Instruction("ret", opcode++));

			// IO
			instructions.Add(new Instruction("byo", opcode++));
			instructions.Add(new Instruction("byi", opcode++));
			instructions.Add(new Instruction("bio", opcode++));
			instructions.Add(new Instruction("bii", opcode++));

			// Misc
			instructions.Add(new Instruction("wait", opcode++));
		}

		public byte[] ParseAndCreateData(string line, Part part)
		{
			string[] parts = line.Split(new char[] { ' ', '\t' });

			foreach (Instruction instruction in instructions)
			{
				if (instruction.Mnemonic == parts[0])
				{
					return instruction.GetData(parts, part);
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
			if (parts.Length == 0)
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
