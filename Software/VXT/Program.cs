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
		static string port = null;
		static string baudrate = "115200";
		static bool localEcho = false;
		static bool sendNewLineAsCarriageReturnPlusLineFeed = true;
		static bool appendLineFeedToCarriageReturnOnIncoming = true;
		static bool encodeAll = false;
		static bool encodeControlCharacters = false;
		static bool saveToFile = false;
		static string logFileName = "log.txt";
		static StreamWriter logFile = null;
		static bool waitWhenExiting = false;

		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Terminal by Claus Andersen");
			Console.WriteLine("Version: 1.13 - October 11th 2008");

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
				if (options.ContainsKey("o"))
				{
					sendNewLineAsCarriageReturnPlusLineFeed = false;
				}
				if (options.ContainsKey("i"))
				{
					appendLineFeedToCarriageReturnOnIncoming = false;
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

				if (port == null || port == "")
				{
					Console.WriteLine("No port specified");
					Help();
					Console.WriteLine("Available ports:");
					foreach (string s in SerialPort.GetPortNames())
					{
						Console.WriteLine("  " + s);
					}
					Quit();
					return;
				}

				com = new SerialPort(port, int.Parse(baudrate));
				com.NewLine = ((char)10).ToString();
				com.ReadTimeout = 500;
				com.Open();

				Console.WriteLine("Connected to " + port + " @ " + baudrate + " bps.");

				const int BLOCK_SIZE = 1024;
				bool run = true;
				char[] buf = new char[BLOCK_SIZE];
				byte[] byteBuf = new byte[BLOCK_SIZE];
				char[] outBuffer;

				while (run)
				{
					#region Port to console
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
									if (appendLineFeedToCarriageReturnOnIncoming)
									{
										outBuffer[j++] = (char)13;
										outBuffer[j++] = (char)10;
									}
									else
									{
										outBuffer[j++] = (char)13;
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

						//Console.Write(outBuffer, 0, j);
						for (int k = 0; k < j; k++)
						{
							if (outBuffer[k] == 12)
							{
								Console.Clear();
							}
							else
							{
								Console.Write(outBuffer[k]);

							}
						}

						if (saveToFile)
						{
							logFile.Write(outBuffer, 0, j);
							logFile.Flush();
						}
					}
					#endregion

					#region Console to port
					if (Console.KeyAvailable)
					{
						ConsoleKeyInfo ki = Console.ReadKey(true);

						if (ki.Key == ConsoleKey.Escape && ki.Modifiers == ConsoleModifiers.Shift)
						{
							Console.Write("\nVXT>");
							string cmd = Console.ReadLine();
							string[] cmds = cmd.Split(' ');
							switch (cmds[0])
							{
								case "quit":
									run = false;
									break;
								case "load":
									LoadDiscImage(cmds);
									break;
								case "run":
									StreamAndRunPvxProgram(cmds);
									break;
								case "":
									break;
								default:
									Console.WriteLine("Unknown command");
									break;
							}
						}
						else
						{
							char key;
							switch (ki.Key)
							{
								case ConsoleKey.UpArrow:
									key = (char)17;
									break;
								case ConsoleKey.DownArrow:
									key = (char)18;
									break;
								case ConsoleKey.LeftArrow:
									key = (char)19;
									break;
								case ConsoleKey.RightArrow:
									key = (char)20;
									break;
								case ConsoleKey.Delete:
									key = (char)127;
									break;
								case ConsoleKey.Home:
									key = (char)2;
									break;
								case ConsoleKey.End:
									key = (char)3;
									break;
								default:
									key = ki.KeyChar;
									break;
							}

							if (key == 3)
							{
								run = false;
							}
							else
							{
								if (key == 13)
								{
									if (sendNewLineAsCarriageReturnPlusLineFeed)
									{
										com.Write(new char[2] { (char)13, (char)10 }, 0, 2);
									}
									else
									{
										com.Write(new char[1] { (char)13 }, 0, 1);
									}
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
					}
					#endregion

					System.Threading.Thread.Sleep(1);
				}

				if (com != null)
				{
					com.Close();
				}
			}
			catch (Exception ex)
			{
				com = null;
				if (ex.Message != "")
				{
					Console.WriteLine(ex.Message);
				}
			}

			Quit();
		}

		static void Quit()
		{
			if (saveToFile)
			{
				logFile.Close();
			}
			if (waitWhenExiting)
			{
				Console.Write("\nPress a key to continue...");
				Console.ReadKey(true);
			}
		}

		static void Help()
		{
			Console.WriteLine("Available options:");
			Console.WriteLine("  p - Set port name.");
			Console.WriteLine("  b - Set baudrate (default is 115200 bps).");
			Console.WriteLine("  l - Enable local echo of all typed characters.");
			Console.WriteLine("  e - Enable partial encoding. Control characters will be shown as hex values.");
			Console.WriteLine("  E - Enable full encoding. All characters will be shown as hex values.");
			Console.WriteLine("  f - Enable logging. If no file name is specified '" + logFileName + "' is used.");
			Console.WriteLine("  o - Don't send full new line (cr + lf) on 'Enter'. Just send cr.");
			Console.WriteLine("  i - Don't append lf to cr when a cr is recevied.");
			Console.WriteLine("  w - Wait for a key press when exiting (use if run directly from Windows).");
			Console.WriteLine("");
			Console.WriteLine("Usage:");
			Console.WriteLine("  vxt -p com1 -i -b 9600");
			Console.WriteLine("");
			Console.WriteLine("To run special functions press shift + ESC and enter function name and arguments.");
			Console.WriteLine("To abort special function mode just press Enter.");
			Console.WriteLine("  load - Load a disc image to a VX machine.");
			Console.WriteLine("  run  - Send and run a PVX program.");
			Console.WriteLine("  quit - Quit VXT.");
			Console.WriteLine("");
			Console.WriteLine("Pressing Ctrl + C has the same effect as running the quit command.");
			Console.WriteLine("");
		}

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
				if ((current + blockSize) > data.Length)
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
					Thread.Sleep(1);
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

		static void StreamAndRunPvxProgram(string[] args)
		{
			char reply;

			if ((args.Length != 2) && (args.Length != 3))
			{
				Console.WriteLine("Please just type the command and the file name mkay.");
				return;
			}

			if (File.Exists(args[1]) == false)
			{
				Console.WriteLine("Program not found.");
				return;
			}

			byte[] data = File.ReadAllBytes(args[1]);

			int blockSize = 128;
			if (data.Length < blockSize)
			{
				blockSize = data.Length;
			}

			if (args.Length == 2)		// Send command line
			{
				com.WriteLine("run " + args[1]);
			}
			else
			{
				com.WriteLine("run " + args[1] + " " + args[2]);
			}
			com.ReadLine(); // dump echo

			Console.Write("Transfering program...");

			int headerSize = 6 + (5 * 4);
			com.Write(data, 0, headerSize);	// Send header

			reply = (char)com.ReadChar();

			//Console.WriteLine("!!!!!" + (int)reply + "!!!!!");

			if (reply == 'H')
			{
				Console.WriteLine("error!\nInvalid file tag.");
				return;
			}
			else if (reply == 'S')
			{
				Console.WriteLine("error!\nNot enough program memory.");
				return;
			}
			else if (reply == 'I')
			{
				Console.WriteLine("error!\nNot enough core memory.");
				return;
			}
			else if (reply != 'A')
			{
				Console.WriteLine("error!\nInvalid reply");
				return;
			}

			int current = headerSize;
			int length = 128;

			while (current < data.Length && Console.KeyAvailable == false && reply == 'A')
			{
				if ((current + blockSize) > data.Length)
				{
					length = data.Length - current;
				}
				com.Write(data, current, length);
				current += length;
				reply = (char)com.ReadChar();
			}

			if (current != data.Length)
			{
				Console.WriteLine("error!");
				return;
			}

			Console.WriteLine("done.");
		}
	}
}
