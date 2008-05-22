using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VXA
{
	class Assembler
	{
		Dictionary<string, int> stackLabels = null;
		public Dictionary<string, int> StackLabels
		{
			get { return stackLabels; }
			set { stackLabels = value; }
		}
		Dictionary<string, int> dataLabels = null;
		public Dictionary<string, int> DataLabels
		{
			get { return dataLabels; }
			set { dataLabels = value; }
		}
		Dictionary<string, int> codeLabels = null;
		public Dictionary<string, int> CodeLabels
		{
			get { return codeLabels; }
			set { codeLabels = value; }
		}

		private int stackSize = 0;
		public int StackSize
		{
			get { return stackSize; }
		}
		private int dataSize = 0;
		public int DataSize
		{
			get { return dataSize; }
		}
		private int codeSize = 0;
		public int CodeSize
		{
			get { return codeSize; }
		}

		private byte[] codeSegment;
		public byte[] CodeSegment
		{
			get { return codeSegment; }
			set { codeSegment = value; }
		}

		InstructionSet instructionSet = InstructionSet.Instance;

		int options = 0;

		public void FindLabels(StreamReader source)
		{
			string line = source.ReadLine();
			int lineNumber = 1;
			string segment = "";
			int stackAddress = 0;
			int codeAddress = 0;
			int dataAddress = 0;

			stackLabels = new Dictionary<string, int>();
			dataLabels = new Dictionary<string, int>();
			codeLabels = new Dictionary<string, int>();

			while (line != null)
			{
				if (line.Contains("."))
				{
					segment = line.Substring(line.IndexOf(".") + 1);
				}
				else if (line.Contains(":"))
				{
					string label = line.Remove(line.IndexOf(":")).Trim();

					if (segment == "code")
					{
						codeLabels.Add(label, codeAddress);
					}
					else if (segment == "data")
					{
						string sizeString = line.Substring(line.IndexOf(":") + 1).Trim();
						int size = 1;
						if (sizeString != "")
						{
							size = int.Parse(sizeString);
						}
						dataLabels.Add(label, dataAddress);
						dataAddress += size;
					}
					else if (segment == "stack")
					{
						string sizeString = line.Substring(line.IndexOf(":") + 1).Trim();
						int size = 1;
						if (sizeString != "")
						{
							size = int.Parse(sizeString);
						}
						stackLabels.Add(label, stackAddress);
						stackAddress += size;
					}
					else
					{
						Informer.Instance.Warning("Label in unknown segment '" + segment + "' ignored (line " + lineNumber + ")");
					}
				}
				else if (line != "")
				{
					if (segment == "code")
					{
						string[] parts = line.Split(' ');
						codeAddress += instructionSet.GetSize(parts[0].Trim());
					}
				}

				line = source.ReadLine();
			}
			source.BaseStream.Seek(0, SeekOrigin.Begin);

			stackSize = stackAddress;
			dataSize = dataAddress;
			codeSize = codeAddress;

			Console.WriteLine("Found the following data segment labels:");
			foreach (string s in dataLabels.Keys)
			{
				Console.WriteLine("  " + s + " @ " + dataLabels[s]);
			}

			Console.WriteLine("\nFound the following code segment labels:");
			foreach (string s in codeLabels.Keys)
			{
				Console.WriteLine("  " + s + " @ " + codeLabels[s]);
			}
		}

		public void GenerateCode(StreamReader source)
		{
			string line = source.ReadLine();
			string segment = "";
			int codeAddress = 0;

			List<byte> code = new List<byte>(10000);

			while (line != null)
			{
				if (line.Contains("."))
				{
					segment = line.Substring(line.IndexOf(".") + 1);
				}
				else if (line != "" && line.Contains(":") == false)
				{
					if (segment == "code")
					{
						string[] parts = line.Split(' ');

						code.Add((byte)instructionSet.GetOpcode(parts[0]));

						if (instructionSet.GetSize(parts[0].Trim()) > 1)
						{
							int constant;
							try
							{
								constant = int.Parse(parts[1]);
							}
							catch
							{
								constant = TranslateLabel(parts[1]);
							}
							byte[] bytes = BitConverter.GetBytes(constant);
							for (int byteCount = 0; byteCount < instructionSet.GetSize(parts[0].Trim()) - 1; byteCount++)
							{
								code.Add(bytes[byteCount]);
							}
						}

						codeAddress += instructionSet.GetSize(parts[0].Trim());
					}
				}

				line = source.ReadLine();
			}
			source.BaseStream.Seek(0, SeekOrigin.Begin);

			codeSegment = code.ToArray();

			foreach (byte b in code)
			{
				Console.WriteLine(b);
			}
		}

		public void GenerateExecutable(StreamWriter file)
		{
			file.Write("VXEXE");
			file.Write(ConvertIntToCharArray(options));
			file.Write(ConvertIntToCharArray(codeSize));
			file.Write(ConvertIntToCharArray(dataSize));
			file.Write(ConvertIntToCharArray(stackSize));
			file.Write(ConvertByteArrayToCharArray(codeSegment));
		}

		private char[] ConvertIntToCharArray(int value)
		{
			return ConvertByteArrayToCharArray(BitConverter.GetBytes(value));
		}

		private char[] ConvertByteArrayToCharArray(byte[] bytes)
		{
			char[] chars = new char[bytes.Length];
			int i = 0;
			foreach (byte b in bytes)
			{
				chars[i++] = (char)b;
			}
			return chars;
		}

		private int TranslateLabel(string name)
		{
			if (dataLabels.ContainsKey(name))
			{
				return dataLabels[name];
			}
			else if (codeLabels.ContainsKey(name))
			{
				return codeLabels[name];
			}
			else
			{
				throw new Exception("Label '" + name + "' not found");
			}
		}
		private int TranslateDataLabel(string name)
		{
			if (dataLabels.ContainsKey(name))
			{
				return dataLabels[name];
			}
			else
			{
				throw new Exception("Data label '" + name + "' not found");
			}
		}
		private int TranslateCodeLabel(string name)
		{
			if (codeLabels.ContainsKey(name))
			{
				return codeLabels[name];
			}
			else
			{
				throw new Exception("Code label '" + name + "' not found");
			}
		}
	}
}
