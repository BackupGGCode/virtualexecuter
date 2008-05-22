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
	}
}
