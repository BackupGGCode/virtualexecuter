using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain
{
	class VXASection
	{
		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public int Size
		{
			get { return _data.Count; }
		}

		private List<VXALabel> _labels = new List<VXALabel>();
		public List<VXALabel> Labels
		{
			get { return _labels; }
			set { _labels = value; }
		}

		private List<byte> _data = new List<byte>();
		public List<byte> Data
		{
			get { return _data; }
			set { _data = value; }
		}

		public VXASection(string name)
		{
			_name = name;
		}

		public bool LabelExists(string name)
		{
			foreach (VXALabel label in _labels)
			{
				if (label.Name == name)
				{
					return true;
				}
			}
			return false;
		}

		public void AddLabel(string name, VXALabel.LabelType type, int count, string value)
		{
			VXALabel label = new VXALabel(name, Size, type, count, value);
			_labels.Add(label);
			AddData(new byte[label.GetSize()]);
		}

		public void AddData(byte[] data)
		{
			_data.AddRange(data);
		}
	}
}
