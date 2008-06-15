using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace VXA
{
	class InstructionSet
	{
		#region Singleton
		static InstructionSet instance = null;
		static public InstructionSet Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new InstructionSet();
				}
				return instance;
			}
		}

		private InstructionSet()
		{
			instructions = new List<Instruction>(256);
			XmlSerializer serializer = new XmlSerializer(typeof(List<Instruction>));
			string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			if (exeDir.StartsWith("file"))
			{
				exeDir = exeDir.Substring(6);
			}
			FileStream file = new FileStream(exeDir + "/InstructionSet.xml", FileMode.Open);
			instructions = (List<Instruction>)serializer.Deserialize(file);
			//serializer.Serialize(file, instructions);
			file.Close();
		}
		#endregion

		private List<Instruction> instructions;

		public int GetSize(string mnemonic)
		{
			foreach (Instruction i in instructions)
			{
				if (i.Mnemonic == mnemonic)
				{
					return i.Size;
				}
			}
			throw new Exception("Unknown instruction '" + mnemonic + "'");
		}

		public int GetOpcode(string mnemonic)
		{
			foreach (Instruction i in instructions)
			{
				if (i.Mnemonic == mnemonic)
				{
					return i.Opcode;
				}
			}
			throw new Exception("Unknown instruction '" + mnemonic + "'");
		}
	}
}

