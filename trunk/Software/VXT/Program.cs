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


		static void LoadDiscImage(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Please just type the command and the file name mkay.");
				return;
			}

			if (File.Exists(args[1]) == false)
			{
				Console.WriteLine("I simply could not find that file.");
				return;
			}

			byte[] data = File.ReadAllBytes(args[1]);
			int blockSize = 1024;
			if (data.Length < blockSize)
			{
				blockSize = data.Length;
			}

			com.DiscardInBuffer();
			com.WriteLine("load " + data.Length + " " + blockSize + " ");
			com.ReadLine();
			string[] reply = com.ReadLine().Split(' ');

			if (reply.Length != 2)
			{
				Console.WriteLine("Invalid reply.");
				return;
			}
			if (reply[0] != "A")
			{
				Console.WriteLine("Come on that image is larger than the disc. I can't do that.");
				return;
			}

			blockSize = int.Parse(reply[1]);

			Console.WriteLine("Image size: " + data.Length + " bytes. Block size: " + blockSize + " bytes.");

			DateTime start = DateTime.Now;

			int current = 0;
			int length;
			while (current < data.Length && Console.KeyAvailable == false)
			{
				if ((current + blockSize) >= data.Length)
				{
					length = data.Length - current;
				}
				else
				{
					length = blockSize;
				}
				com.Write(data, current, length);
				current += length;
				Console.Write((char)com.ReadChar());
			}

			Console.WriteLine((char)com.ReadChar());

			TimeSpan time = DateTime.Now.Subtract(start);
			if (time.Seconds == 0)
			{
				Console.WriteLine("Transfer time: " + time.ToString() + " @ > " + data.Length + " B/S.");
			}
			else
			{
				Console.WriteLine("Transfer time: " + time.ToString() + " @ " + (data.Length / time.Seconds) + " B/S.");
			}
		}


		static void Main(string[] args)
		{
			string port = "com1";
			string baudrate = "115200";
			bool localEcho = false;
			bool addLineCarriageReturn = false;

			Console.WriteLine("Virtual eXecuter Terminal by Claus Andersen");
			Console.WriteLine("Version: 1.0 - March 9th 2008");

			#region Parse command line
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
			#endregion

			if (options == null || options.Count == 0)
			{
				Console.WriteLine("Available ports:");
				foreach (string s in SerialPort.GetPortNames())
				{
					Console.WriteLine("  " + s);
				}
			}

			com = new SerialPort(port, int.Parse(baudrate));
			com.NewLine = ((char)10).ToString();
			com.Open();

			Console.WriteLine("Connected to " + port + " @ " + baudrate + " bps.");

			bool run = true;
			char[] buf = new char[1024];
			int i;

			while (run)
			{
				#region Com to con
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
				#endregion

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
							case "load":
								LoadDiscImage(cmds);
								break;
							default:
								Console.WriteLine("Unknown command");
								break;
						}
					}
					else
					{
						if (key == 13)
						{
							com.Write(new char[2] { (char)13, (char)10 }, 0, 2);
						}
						else
						{
							com.Write(new char[1] { key }, 0, 1);
						}

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
