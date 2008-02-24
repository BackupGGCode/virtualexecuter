using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain.Assist
{
	class Label
	{
		public enum LabelType { None, Single, Double, Quad, Float };

		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private bool isLocal = false;
		public bool IsLocal
		{
			get { return isLocal; }
			set { isLocal = value; }
		}

		private bool isFunction = false;
		public bool IsFunction
		{
			get { return isFunction; }
			set { isFunction = value; }
		}

		private bool isExported = false;
		public bool IsExported
		{
			get { return isExported; }
			set { isExported = value; }
		}

		private bool isImported = false;
		public bool IsImported
		{
			get { return isImported; }
			set { isImported = value; }
		}

		private UInt32 address = 0;
		public UInt32 Address
		{
			get { return address; }
			set { address = value; }
		}

		private UInt32 offset = 0;
		public UInt32 Offset
		{
			get { return offset; }
			set { offset = value; }
		}

		private int size = 0;
		public int Size
		{
			get { return size; }
			set { size = value; }
		}

		private int count = 0;
		public int Count
		{
			get { return count; }
			set { count = value; }
		}

		private LabelType type = LabelType.None;
		public LabelType Type
		{
			get { return type; }
			set { type = value; }
		}

		public Label(string text)
		{
			if (text.Contains(":") == false)
			{
				throw new Exception("This is not a valid label '" + text + "'");
			}

			text = text.Trim();
			string[] line = text.Split(new char[] { ':', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

			name = line[0].Trim('!', '&');

			if (text.StartsWith("&"))
			{
				IsLocal = true;
			}
			if (text.StartsWith("!"))
			{
				isFunction = true;
			}
			if (text.EndsWith("import"))
			{
				isImported = true;
			}
			if (text.EndsWith("export"))
			{
				isExported = true;
			}

			if (line.Length == 2)
			{
				line = null;
			}
		}
	}
}
