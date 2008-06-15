using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VXA.VX
{
	class Preprocessor
	{
		public StreamReader Run(StreamReader sourceFile, string workingDirectory)
		{
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
								StreamReader includeFile = new StreamReader(workingDirectory + "/" + includeFileName);
								StreamReader sr = Run(includeFile, workingDirectory);
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
	}
}
