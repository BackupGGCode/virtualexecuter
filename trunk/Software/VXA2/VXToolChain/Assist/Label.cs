using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain.Assist
{
	class Label
	{
		public enum LabelType { None, Single, Double, Quad, Float };
		static private Dictionary<LabelType, int> SizeOfLabelType = new Dictionary<LabelType, int>();

		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private string functionName = "";
		public string Function
		{
			get { return functionName; }
			set { functionName = value; }
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

		private int address = 0;
		public int Address
		{
			get { return address; }
			set { address = value; }
		}

		private int offset = 0;
		public int Offset
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

		static Label()
		{
			SizeOfLabelType.Add(LabelType.None, 0);
			SizeOfLabelType.Add(LabelType.Single, 1);
			SizeOfLabelType.Add(LabelType.Double, 2);
			SizeOfLabelType.Add(LabelType.Quad, 4);
			SizeOfLabelType.Add(LabelType.Float, 4);
		}


		public Label(string text, Part part)
		{
			string sectionName = part.GetCurrentSection().Name;

			if (text.Contains(":") == false)
			{
				throw new Exception("This is not a valid label '" + text + "'");
			}

			text = text.Trim();
			string[] lines = text.Split(new char[] { ':', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

			name = lines[0].Trim('!', '&');

			if (text.StartsWith("&"))
			{
				IsLocal = true;
				functionName = part.CurrentFunctionName;
			}
			else if (text.StartsWith("!"))
			{
				isFunction = true;
				part.CurrentFunctionName = name;
			}

			if (text.EndsWith(" import"))
			{
				isImported = true;
			}
			else if (text.EndsWith(" export"))
			{
				isExported = true;
			}

			if (text.Contains(" single"))
			{
				type = LabelType.Single;
			}
			else if (text.Contains(" double"))
			{
				type = LabelType.Double;
			}
			else if (text.Contains(" quad"))
			{
				type = LabelType.Quad;
			}
			else if (text.Contains(" float"))
			{
				type = LabelType.Float;
			}

			size = SizeOfLabelType[type];

			int i = text.IndexOf('[');
			if (i > 0)
			{
				if (text.IndexOf(']') < i)
				{
					throw new Exception("Missing ']' in type declaration");
				}
				size *= int.Parse(text.Substring(i).Trim('[', ']'));
			}

			address = part.GetCurrentSection().Data.Count;

			switch (sectionName)
			{
				case "code":
					if (size > 0 && isLocal == false)
					{
						throw new Exception("Only location labels are valid in code section (label '" + name + "')");
					}
					break;
				case "const":
					if (isLocal)
					{
						throw new Exception("Can't declare a local label in constant section (label '" + name + "')");
					}
					break;
				case "gdata":
					if (isLocal)
					{
						throw new Exception("Can't declare a local label in global data section (label '" + name + "')");
					}
					break;
				case "stack":
					if (isLocal)
					{
						throw new Exception("Can't declare a local label in stack section (label '" + name + "')");
					}
					break;
			}

			if (lines.Length == 2)
			{
				lines = null;
			}
		}
	}
}
