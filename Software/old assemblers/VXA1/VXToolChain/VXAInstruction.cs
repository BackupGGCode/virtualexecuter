using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain
{
	class VXAInstruction
	{
		public enum ConstantType { Single, Double, Quad, Float };

		private string _mnemonic;
		public string Mnemonic
		{
			get { return _mnemonic; }
		}
		private byte _opcode;
		public byte Opcode
		{
			get { return _opcode; }
		}
		private int _size;
		public int Size
		{
			get { return _size; }
		}
		private List<ConstantType> _constants = new List<ConstantType>();
		public List<ConstantType> Constants
		{
			get { return _constants; }
			set { _constants = value; }
		}

		public VXAInstruction(string mnemonic, int opcode, params ConstantType[] constants)
		{
			_mnemonic = mnemonic;
			_opcode = (byte)opcode;
			_constants.AddRange(constants);
			_size = 0;
			foreach (ConstantType c in _constants)
			{
				if (c == ConstantType.Single)
				{
					_size += 1;
				}
				else if (c == ConstantType.Double)
				{
					_size += 2;
				}
				else if (c == ConstantType.Quad)
				{
					_size += 4;
				}
				else if (c == ConstantType.Float)
				{
					_size += 4;
				}
				else
				{
					throw new Exception("ConstantType '" + c.ToString() + "' not implemented in VXAInstruction");
				}
			}
		}

		public byte[] GetData(string[] parts, VXAProgram program)
		{
			long l;
			for (int j = 1; j < parts.Length; j++)
			{
				if (long.TryParse(parts[j], out l) == false)
				{
					parts[j] = program.GetLabelAddress(parts[j]).ToString();
				}
			}

			List<byte> data = new List<byte>();

			data.Add(Opcode);

			int i = 1;
			try
			{
				foreach (ConstantType constant in _constants)
				{
					Console.WriteLine(parts[i]);
					if (constant == ConstantType.Single)
					{
						if (parts[i].Contains("-"))
						{
							data.Add((byte)sbyte.Parse(parts[i]));
						}
						else
						{
							data.Add(byte.Parse(parts[i]));
						}
					}
					else if (constant == ConstantType.Double)
					{
						ushort us;
						if (parts[i].Contains("-"))
						{
							us = ((ushort)short.Parse(parts[i]));
						}
						else
						{
							us = ushort.Parse(parts[i]);
						}
						data.Add((byte)(us & 0xff));
						data.Add((byte)(us >> 8));
					}
					else if (constant == ConstantType.Quad)
					{
						uint ui;
						if (parts[i].Contains("-"))
						{
							ui = ((uint)int.Parse(parts[i]));
						}
						else
						{
							ui = uint.Parse(parts[i]);
						}
						data.Add((byte)(ui & 0xff));
						data.Add((byte)((ui >> 8) & 0xff));
						data.Add((byte)((ui >> 16) & 0xff));
						data.Add((byte)((ui >> 24) & 0xff));
					}
					else if (constant == ConstantType.Float)
					{
						float f = float.Parse(parts[i]);

						data.Add((byte)(((uint)f) & 0xff));
						data.Add((byte)((((uint)f) >> 8) & 0xff));
						data.Add((byte)((((uint)f) >> 16) & 0xff));
						data.Add((byte)((((uint)f) >> 24) & 0xff));
					}
					i++;
				}
			}
			catch (Exception ex)
			{
				string s = "";
				foreach (string ss in parts)
				{
					s += ss + " ";
				}
				throw new Exception("Constants in line '" + s + "'is not valid");
			}

			return data.ToArray();
		}
	}
}
