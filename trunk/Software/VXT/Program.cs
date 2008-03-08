using System;
using System.Collections.Generic;
using System.Text;
using Coma;
using System.IO.Ports;
using System.IO;

namespace VXT
{
	class Program
	{
		static SerialPort com;
		static bool SendFile(string[] args)
		{
			byte[] data = File.ReadAllBytes(args[1]);

			com.Write(data,0,data.Length);

			return true;
		}

		static void Main(string[] args)
		{
			string port = "";
			string baudrate = "115200";
			bool localEcho = true;
			bool addLineCarriageReturn = true;

			Dictionary<string, List<string>> options = CommandLineParser.Run(args);

			if (options.ContainsKey("p"))
			{
				port = options["p"][0];
			}
			if (options.ContainsKey("b"))
			{
				baudrate = options["b"][0];
			}
			if (options.ContainsKey("l"))
			{
				localEcho = false;
			}
			if (options.ContainsKey("L"))
			{
				localEcho = true;
			}

			if (port == "")
			{
				Console.WriteLine("No port specified");
				return;
			}

			options = null;

			com = new SerialPort(port, int.Parse(baudrate));
			com.Open();

			bool run = true;
			char[] buf = new char[1024];
			int i;

			while (run)
			{
				if (com.BytesToRead > 0)
				{
					int toRead = com.BytesToRead;
					if (toRead > buf.Length)
					{
						toRead = buf.Length;
					}
					toRead = com.Read(buf, 0, toRead);
					if (addLineCarriageReturn)
					{
						char[] newBuf = new char[toRead * 2];
						int j = 0;
						for (i = 0; i < toRead; i++)
						{
							newBuf[j++] = buf[i];
							if (buf[i] == (char)13)//&& buf[i+1] == (char)10 )
							{
								newBuf[j++] = (char)10;
							}
						}
						buf = newBuf;
						toRead = j;
					}
					Console.Write(buf, 0, toRead);
				}

				if (Console.KeyAvailable)
				{
					char key = Console.ReadKey(true).KeyChar;
					if (key == 3)
					{
						return;
					}
					else if (key == 27)
					{
						Console.Write("\nVXT>");
						string cmd = Console.ReadLine();
						string[] cmds = cmd.Split(' ');
						switch (cmds[0])
						{
							case "quit":
								return;
								break;
							case "send":
								if (SendFile(cmds))
								{
									Console.WriteLine("OK");
								}

								else
								{
									Console.WriteLine("Error");
								}
								break;
						}
					}
					else
					{
						com.Write(new char[1] { key }, 0, 1);
						if (localEcho)
						{
							Console.Write(key);
						}
					}
				}

				System.Threading.Thread.Sleep(1);
			}

			com.Close();
		}
	}
}
