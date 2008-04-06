using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain
{
	class VXALabel
	{
		public enum LabelType { None, Single, Double, Quad, Float };
		private Dictionary<LabelType, int> SizeOfLabelType = new Dictionary<LabelType, int>();

		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private int _address;
		public int Address
		{
			get { return _address; }
			set { _address = value; }
		}

		private LabelType _type;
		public LabelType Type
		{
			get { return _type; }
			set { _type = value; }
		}

		private int _count;
		public int Count
		{
			get { return _count; }
			set { _count = value; }
		}

		private string _value;
		public byte[] GetValue()
		{
			byte[] data;
			switch (_type)
			{/*
				case LabelType.Single:
					data = new byte[1];
					int i;
					if (int.TryParse(value, out i) == false)
					{
						throw new Exception("Invalid value '" + value + "' for label of type " + _type.ToString());
					}
					break;
					
				case LabelType.Double:
					data = new byte[2];
					int i;
					if (int.TryParse(value, out i) == false)
					{
						throw new Exception("Invalid value '" + value + "' for label of type " + _type.ToString());
					}
					break;
					
				case LabelType.Quad:
					data = new byte[4];
					int i;
					if (int.TryParse(value, out i) == false)
					{
						throw new Exception("Invalid value '" + value + "' for label of type " + _type.ToString());
					}
					break;
					
				case LabelType.Float:
					data = new byte[1];
					if (byte.TryParse(value, data[0]) == false)
					{
						throw new Exception("Invalid value '" + value + "' type for label");
					}
					break;
					*/
				default:
					data = new byte[0];
					break;
			}
			return data;
		}

		public int GetSize()
		{
			return SizeOfLabelType[_type] * Count;
		}

		public VXALabel()
		{
			InitializeSizeOfLabelTypeList();
		}

		public VXALabel(string name, int address, LabelType type, int count, string value)
		{
			InitializeSizeOfLabelTypeList();
			_name = name;
			_address = address;
			_type = type;
			_count = count;
			_value = value;
		}

		private void InitializeSizeOfLabelTypeList()
		{
			SizeOfLabelType.Add(LabelType.None, 0);
			SizeOfLabelType.Add(LabelType.Single, 1);
			SizeOfLabelType.Add(LabelType.Double, 2);
			SizeOfLabelType.Add(LabelType.Quad, 4);
			SizeOfLabelType.Add(LabelType.Float, 4);
		}
	}
}
