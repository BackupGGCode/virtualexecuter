using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VXA
{
	class Informer
	{
		#region Singleton
		static Informer instance = null;
		static public Informer Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Informer();
				}
				return instance;
			}
		}

		private Informer()
		{ }
		#endregion

		StreamWriter listFile = null;
		StreamWriter mapFile = null;

		bool printMessages = true;
		public bool PrintMessages
		{
			get { return printMessages; }
			set { printMessages = value; }
		}

		bool printErrors = true;
		public bool PrintErrors
		{
			get { return printErrors; }
			set { printErrors = value; }
		}

		bool printWarnings = true;
		public bool PrintWarnings
		{
			get { return printWarnings; }
			set { printWarnings = value; }
		}

		public void SetListFile(string filename)
		{
			listFile = new StreamWriter(filename);
			listFile.AutoFlush = true;
		}
		public void SetMapFile(string filename)
		{
			mapFile = new StreamWriter(filename);
			mapFile.AutoFlush = true;
		}

		public void Message(string message)
		{
			if (printMessages)
			{
				Console.WriteLine(message);
			}
		}

		public void Error(string message)
		{
			if (printErrors)
			{
				Console.WriteLine("! " + message);
			}
		}

		public void Warning(string message)
		{
			if (printWarnings)
			{
				Console.WriteLine("- " + message);
			}
		}

		public void List(int address, byte[] bytes, string line)
		{
			if (listFile != null)
			{
				StringBuilder sb = new StringBuilder(80);

				sb.Append(Convert.ToString(address, 16).PadLeft(8, '0'));
				sb.Append("  ");
				int i;
				for (i = 0; i < bytes.Length; i++)
				{
					sb.Append(Convert.ToString(bytes[i], 16).PadLeft(2, '0'));
					sb.Append(" ");
				}
				for (; i < 8; i++)
				{
					sb.Append("   ");
				}
				sb.Append("    ");
				sb.Append(line);

				listFile.WriteLine(sb.ToString());
			}
		}

		public void List(string labelName)
		{
			if (listFile != null)
			{
				StringBuilder sb = new StringBuilder(80);

				sb.Append("\n");
				sb.Append("        ");
				sb.Append("  ");
				sb.Append("                        ");
				sb.Append("  ");
				sb.Append(labelName);
				sb.Append(":");

				listFile.WriteLine(sb.ToString());
			}
		}

		public void List(string segmentName, int size)
		{
			if (listFile != null)
			{
				StringBuilder sb = new StringBuilder(80);

				string s = "Totalt size of " + segmentName + " segment: ";
				sb.Append(s.PadRight(31, ' '));
				sb.Append(size.ToString().PadLeft(10, ' '));
				sb.Append(" bytes");

				listFile.WriteLine(sb.ToString());
			}
		}

		public void List()
		{
			if (listFile != null)
			{
				listFile.WriteLine("");
			}
		}

		public void Map(string segment, string labelName, int address)
		{
			if (mapFile != null)
			{
				StringBuilder sb = new StringBuilder(80);

				sb.Append(Convert.ToString(address, 16).PadLeft(8, '0'));
				sb.Append("  ");
				sb.Append(".");
				sb.Append(segment.PadRight(5, ' '));
				sb.Append("  ");
				sb.Append(labelName);

				mapFile.WriteLine(sb.ToString());
			}
		}
	}
}
