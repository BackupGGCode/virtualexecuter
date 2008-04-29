using System;
using System.Collections.Generic;
using System.Text;
using Coma;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace VXT
{
	class Program
	{
		static SerialPort com;
		static string port = "com1";
		static string baudrate = "115200";
		static bool localEcho = false;
		static bool fullNewLinesOnIncoming = false;
		static bool fullNewLinesOnOutgoing = false;
		static bool encodeAll = false;
		static bool encodeControlCharacters = false;
		static bool saveToFile = false;
		static string logFileName = "log.txt";
		static StreamWriter logFile = null;
		static bool waitWhenExiting = false;

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
				//com.Write(data, current, length);
				for (int aaa = 0; aaa < length; aaa++)
				{
					com.Write(data, current++, 1);
					//					Thread.Sleep(1);
				}
				//current += length;
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

			Console.WriteLine("Virtual eXecuter Terminal by Claus Andersen");
			Console.WriteLine("Version: 1.11 - April 29th 2008");

			try
			{
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
					localEcho = true;
				}
				if (options.ContainsKey("e"))
				{
					encodeControlCharacters = true;
				}
				if (options.ContainsKey("E"))
				{
					encodeAll = true;
				}
				if (options.ContainsKey("f"))
				{
					saveToFile = true;
					if (options["f"].Count == 0)
					{

					}
					else if (options["f"].Count == 1)
					{
						logFileName = options["f"][0];
					}
					else
					{
						Console.WriteLine("Only one log file name may be specified");
					}
					Console.WriteLine("Logging to file '" + logFileName + "'");
					logFile = new StreamWriter(logFileName);
				}
				if (options.ContainsKey("w"))
				{
					waitWhenExiting = true;
				}
				#endregion

				if (options == null || options.Count == 0)
				{
					Help();
					Console.WriteLine("Available ports:");
					foreach (string s in SerialPort.GetPortNames())
					{
						Console.WriteLine("  " + s);
					}
					Quit();
					return;
				}

				if (port == "")
				{
					Console.WriteLine("No port specified");
					return;
				}

				com = new SerialPort(port, int.Parse(baudrate));
				com.NewLine = ((char)10).ToString();
				com.Open();

				Console.WriteLine("Connected to " + port + " @ " + baudrate + " bps.");

				const int BLOCK_SIZE = 1024;
				bool run = true;
				char[] buf = new char[BLOCK_SIZE];
				byte[] byteBuf = new byte[BLOCK_SIZE];
				char[] outBuffer;

				try
				{
					while (run)
					{
						#region Com to con
						if (com.BytesToRead > 0)
						{
							int toRead = com.BytesToRead;
							if (toRead > BLOCK_SIZE)
							{
								toRead = BLOCK_SIZE;
							}
							outBuffer = new char[10 * toRead];

							int j = 0;

							if (encodeAll)
							{
								toRead = com.Read(byteBuf, 0, toRead);

								for (int i = 0; i < toRead; i++)
								{
									string s = Convert.ToString(byteBuf[i], 16);
									if (s.Length == 1)
									{
										outBuffer[j++] = '0';
									}
									foreach (char c in s)
									{
										outBuffer[j++] = c;
									}
									outBuffer[j++] = ' ';
								}
							}
							else
							{
								toRead = com.Read(buf, 0, toRead);

								for (int i = 0; i < toRead; i++)
								{
									if (buf[i] == (char)13)
									{
										outBuffer[j++] = buf[i];

										if (fullNewLinesOnIncoming)
										{
											outBuffer[j++] = (char)10;
										}
									}
									else if (buf[i] < 32)
									{
										if (encodeControlCharacters)
										{
											outBuffer[j++] = '<';
											string s = Convert.ToString((byte)buf[i], 16);
											if (s.Length == 1)
											{
												outBuffer[j++] = '0';
											}
											foreach (char c in s)
											{
												outBuffer[j++] = c;
											}
											outBuffer[j++] = '>';
										}
										else
										{
											outBuffer[j++] = buf[i];
										}
									}
									else
									{
										outBuffer[j++] = buf[i];
									}
								}
							}

							Console.Write(outBuffer, 0, j);
							if (saveToFile)
							{
								logFile.Write(outBuffer, 0, j);
								logFile.Flush();
							}
						}
						#endregion

						#region Con to com
						if (Console.KeyAvailable)
						{
							char key = Console.ReadKey(true).KeyChar;
							if (key == 3)
							{
								Quit();
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
										if (saveToFile)
										{
											logFile.Close();
										}
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
						#endregion

						System.Threading.Thread.Sleep(1);
					}

					com.Close();
					if (saveToFile)
					{
						logFile.Close();
					}
				}
				catch (Exception ex)
				{
					if (ex.Message != "")
					{
						Console.WriteLine(ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		static void Quit()
		{
			if (saveToFile)
			{
				logFile.Close();
			}
			if (waitWhenExiting)
			{
				Console.ReadKey(true);
			}
		}

		static void Help()
		{
			Console.WriteLine("Available options:");
			Console.WriteLine("  p - Set port name.\n      'vxt -p com1'");
			Console.WriteLine("  b - Set baudrate (default is 115200 bps).\n      'vxt -p com1 -b 9600'");
			Console.WriteLine("  l - Enable local echo of all typed characters.\n      'vxt -p com1 -l'");
			Console.WriteLine("  e - Enable partial encoding. Control characters will be shown as hex values.\n      'vxt -p com1 -e'");
			Console.WriteLine("  E - Enable full encoding. All characters will be shown as hex values.\n      'vxt -p com1 -E'");
			Console.WriteLine("  f - Enable logging. If no file name is specified the default name will be used.\n      'vxt -p com1 -f logfile.txt'");
			Console.WriteLine("");
			Console.WriteLine("To run special functions hit ESC and enter function name and arguments.");
			Console.WriteLine("  load - Load a disc image to a VX machine.");
			Console.WriteLine("  quit - Quit VXT.");
			Console.WriteLine("");
			Console.WriteLine("To close the terminal either hit Ctrl + C or use the special function 'quit'.");
			Console.WriteLine("");
		}
	}
}
