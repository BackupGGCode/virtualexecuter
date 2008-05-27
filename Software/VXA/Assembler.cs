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

		static string sourceFileDirectory = "";

		public StreamReader Preprocessor(string sourceFileName)
		{
			sourceFileDirectory = Path.GetDirectoryName(sourceFileName);
			StreamReader sourceFile = new StreamReader(sourceFileName);

			MemoryStream ms = new MemoryStream(10000);
			StreamWriter sw = new StreamWriter(ms);
			sw.AutoFlush = true;

			string line = sourceFile.ReadLine();

			while (line != null)
			{
				if (line.Contains("//"))
				{
					line = line.Remove(line.IndexOf("//"));
				}

				if (line.Contains(";"))
				{
					line = line.Remove(line.IndexOf(";"));
				}

				line = line.Trim();

				if (line.StartsWith("#"))
				{
					string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
					string directive = parts[0].Substring(1);

					switch (directive)
					{
						case "include":
							string includeFileName = "";
							try
							{
								includeFileName = line.Substring(line.IndexOf("\"") + 1);
								includeFileName = includeFileName.Remove(includeFileName.IndexOf("\""));
							}
							catch
							{
								Informer.Instance.Error("Malformed include directive");
							}

							try
							{
								StreamReader sr = Preprocessor(sourceFileDirectory + "/" + includeFileName);
								sw.Write(sr.ReadToEnd());
							}
							catch
							{
								Informer.Instance.Error("Unable to find include file '" + includeFileName + "'");
							}
							break;

						default:
							Informer.Instance.Error("Invalid directive '" + directive + "'");
							break;
					}
				}
				else
				{
					sw.WriteLine(line);
				}

				line = sourceFile.ReadLine();
			}

			ms.Seek(0, SeekOrigin.Begin);

			return new StreamReader(ms);
		}

		public void FindLabels(StreamReader source)
		{
			string line = source.ReadLine();
			string segment = "";
			int stackAddress = 0;
			int codeAddress = 0;
			int dataAddress = 0;

			stackLabels = new Dictionary<string, int>();
			dataLabels = new Dictionary<string, int>();
			codeLabels = new Dictionary<string, int>();

			while (line != null)
			{
				line = line.Trim();

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
						Informer.Instance.Map("code", label, codeAddress);
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
						Informer.Instance.Map("data", label, dataAddress);
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
						Informer.Instance.Map("stack", label, stackAddress);
						stackAddress += size;
					}
					else
					{
						Informer.Instance.Warning("Label in unknown segment '" + segment + "' ignored");
					}
				}
				else if (line != "")
				{
					if (segment == "code")
					{
						string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
						codeAddress += instructionSet.GetSize(parts[0].Trim());
					}
				}

				line = source.ReadLine();
			}

			source.BaseStream.Seek(0, SeekOrigin.Begin);

			stackSize = stackAddress;
			dataSize = dataAddress;
			codeSize = codeAddress;
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
				else if (line.Contains(":"))
				{
					if (segment == "code")
					{
						string label = line.Remove(line.IndexOf(":")).Trim();
						Informer.Instance.List(label);
					}
				}
				else if (line != "")
				{
					if (segment == "code")
					{
						string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
						for (int iii = 0; iii < parts.Length; iii++)
						{
							parts[iii] = parts[iii].Trim();
						}

						List<byte> bytes = new List<byte>(8);

						bytes.Add((byte)instructionSet.GetOpcode(parts[0]));

						int size = instructionSet.GetSize(parts[0]);
						if (size > 1)
						{
							int constant = 0;
							try
							{
								constant = int.Parse(parts[1]);
							}
							catch
							{
								try
								{
									constant = TranslateLabel(parts[1]);
								}
								catch
								{
									Informer.Instance.Error("Missing constant in line '" + line + "'");
								}
							}
							byte[] bs = BitConverter.GetBytes(constant);
							for (int byteCount = 0; byteCount < (size - 1); byteCount++)
							{
								bytes.Add(bs[byteCount]);
							}
						}

						Informer.Instance.List(codeAddress, bytes.ToArray(), line);

						code.AddRange(bytes.ToArray());

						codeAddress += size;
					}
				}

				line = source.ReadLine();
			}
			source.BaseStream.Seek(0, SeekOrigin.Begin);

			codeSegment = code.ToArray();

			Informer.Instance.List();
			Informer.Instance.List();
			Informer.Instance.List("stack", stackSize);
			Informer.Instance.List("data", dataSize);
			Informer.Instance.List("code", codeSize);
		}

		public void GenerateExecutable(FileStream file)
		{
			file.Write(ConvertStringToByteArray("VXEXE"), 0, 5);
			file.Write(ConvertIntToByteArray(options), 0, 4);
			file.Write(ConvertIntToByteArray(codeSize), 0, 4);
			file.Write(ConvertIntToByteArray(dataSize), 0, 4);
			file.Write(ConvertIntToByteArray(stackSize), 0, 4);
			file.Write(codeSegment, 0, CodeSegment.Length);

			Informer.Instance.Message("Stack size: " + StackSize);
			Informer.Instance.Message("Data size: " + dataSize);
			Informer.Instance.Message("Code size: " + codeSize);
		}

		private byte[] ConvertStringToByteArray(string value)
		{
			byte[] bytes = new byte[value.Length];
			int i = 0;
			foreach (char c in value)
			{
				bytes[i++] = (byte)c;
			}
			return bytes;
		}

		private byte[] ConvertIntToByteArray(int value)
		{
			return BitConverter.GetBytes(value);
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
		/*
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
	 */
	}
}
