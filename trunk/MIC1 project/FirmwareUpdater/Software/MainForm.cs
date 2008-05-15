using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace Firmware_Updater
{
	public partial class MainForm : Form
	{
		#region GUI shit
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		public MainForm()
		{
			InitializeComponent();
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			UpdatePortList();
			UpdateBaudrateList();
			UpdateNotifyIconText();
		}
		#endregion

		#region Private helpers
		void UpdatePortList()
		{
			comboBoxPortName.Items.Clear();
			comboBoxPortName.Items.AddRange(SerialPort.GetPortNames());
			comboBoxPortName.Text = comboBoxPortName.Items[comboBoxPortName.Items.Count - 1].ToString();
		}
		void UpdateBaudrateList()
		{
			comboBoxBaudrate.Items.Clear();
			comboBoxBaudrate.Items.Add("115200");
			comboBoxBaudrate.Items.Add("57600");
			comboBoxBaudrate.Items.Add("19200");
			comboBoxBaudrate.Items.Add("9600");
			comboBoxBaudrate.Text = comboBoxBaudrate.Items[0].ToString();
		}
		void Browse()
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textBoxFileName.Text = openFileDialog1.FileName.Replace('\\', '/');
				UpdateNotifyIconText();
			}
		}
		void print(string s)
		{
			richTextBoxMessages.AppendText(s + "\n");
			Application.DoEvents();
		}
		private void richTextBoxMessages_Resize(object sender, EventArgs e)
		{
			richTextBoxMessages.ScrollToCaret();
		}
		private void richTextBoxMessages_TextChanged(object sender, EventArgs e)
		{
			richTextBoxMessages.ScrollToCaret();
		}
		void Quit()
		{
			Application.Exit();
			notifyIconProgram.Visible = false;
		}
		#endregion

		#region Target interaction
		bool UpdateTarget()
		{
			SerialPort port = null;

			byte[] memory = null;
			int size = 0;
			bool reply = true;

			try
			{
				port = new SerialPort(comboBoxPortName.Text, int.Parse(comboBoxBaudrate.Text));
				port.ReadTimeout = 500;
				port.Open();
				if (!port.IsOpen)
				{
					throw new Exception("Couldn't open port!");
				}
				port.Write(new char[] { '*' }, 0, 1);		// send ESC to enter bootloader
				print("Port opened");


				if (textBoxFileName.Text.EndsWith(".gen", true, System.Globalization.CultureInfo.CurrentUICulture))
				{
					//					ReadFileGeneric(textBoxFileName.Text);
				}
				else if (textBoxFileName.Text.EndsWith(".hex", true, System.Globalization.CultureInfo.CurrentUICulture))
				{
					ReadFileHex(textBoxFileName.Text, ref memory, ref size);
				}
				else if (textBoxFileName.Text.EndsWith(".a90", true, System.Globalization.CultureInfo.CurrentUICulture))
				{
					ReadFileHex(textBoxFileName.Text, ref memory, ref size);
				}
				else
				{
					throw new Exception("Please select a valid file first");
				}
				print("File read");


				int pageSize = port.ReadByte();
				pageSize += port.ReadByte() * 256;
				int pageCount = port.ReadByte();
				pageCount += port.ReadByte() * 256;

				//print("Target flash size:" + (pageCount * pageSize * 2).ToString() + " bytes");


				print("Programming...");
				int pagesToProgram = size / (pageSize * 2);
				if ((pagesToProgram * pageSize * 2) < size)
				{
					pagesToProgram++;
				}

				progressBar1.Maximum = pagesToProgram;

				progressBar1.Value = 0;

				int p;
				byte[] pageNumber = new byte[3];
				for (int currentPage = 0; currentPage < pagesToProgram; currentPage++)
				{
					p = currentPage * pageSize * 2;
					pageNumber[0] = (byte)(p & 0xff);
					pageNumber[1] = (byte)((p >> 8) & 0xff);
					pageNumber[2] = (byte)((p >> 16) & 0xff);
					port.Write(pageNumber, 0, 3);
					port.Write(memory, currentPage * pageSize * 2, pageSize * 2);

					if (port.ReadByte() != (byte)'A')
					{
						throw new Exception("Acknowledge error from target!");
					}

					progressBar1.Value = currentPage;
				}
				progressBar1.Value = 0;
				print("Done :)");
			}
			catch (Exception ex)
			{
				print(ex.Message);
				reply = false;
			}

			if (port != null)
			{
				if (port.IsOpen)
				{
					port.Close();
					print("Port closed");
				}
			}
			return reply;
		}
		#endregion

		#region File parsers
		byte ReadHexByte(string s, int offset)
		{
			return byte.Parse(s.Substring(offset, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
		}
		ushort ReadHexShort(string s, int offset)
		{
			return ushort.Parse(s.Substring(offset, 4), System.Globalization.NumberStyles.AllowHexSpecifier);
		}
		/*
		void ReadFileGeneric(string filename)
		{
			memMap = new byte[flashSize];
			for (int i = 0; i < memMap.Length; i++)
				memMap[i] = 0xff;
			memLength = -1;
			FileStream fs = new FileStream(filename, FileMode.Open);
			TextReader tr = new StreamReader(fs);
			string s;
			int a, d;
			while ((s = tr.ReadLine()) != null)
			{
				a = int.Parse(s.Substring(0, 6), System.Globalization.NumberStyles.AllowHexSpecifier);
				d = int.Parse(s.Substring(7, 4), System.Globalization.NumberStyles.AllowHexSpecifier);
				if ((2 * a) > memLength)
					memLength = 2 * a;
				memMap[2 * a] = Convert.ToByte(d & 0xff);
				memMap[2 * a + 1] = Convert.ToByte(d >> 8);
			}
			tr.Close();
			fs.Close();
			if (memLength >= flashSize)
				throw new Exception("Program is larger than flash!");
		}
		 * */
		void ReadFileHex(string filename, ref byte[] mem, ref int size)
		{
			mem = new byte[256 * 1024];		// currently the biggest AVR has 256 kB flash
			for (int i = 0; i < mem.Length; i++)
			{
				mem[i] = 0xff;
			}
			size = 0;
			FileStream fs = new FileStream(filename, FileMode.Open);
			TextReader tr = new StreamReader(fs);
			string s;
			int byteCount, address, recordType;

			while ((s = tr.ReadLine()) != null)
			{
				if (s[0] != ':')
				{
					throw new Exception("Error in file or not a hex file!");
				}
				byteCount = ReadHexByte(s, 1);
				address = ReadHexShort(s, 3);
				recordType = ReadHexByte(s, 7);
				switch (recordType)
				{
					case 0:
						int i;
						for (i = 0; i < byteCount; i++)
						{
							mem[address++] = ReadHexByte(s, 9 + (i * 2));
						}
						if (size < address)
						{
							size = address;
						}
						break;
					case 1:
						break;
					//default:
					//	throw new Exception("Unsupported hex record type");
				}
			}
			tr.Close();
			fs.Close();
		}
		#endregion

		#region Buttons
		private void buttonRescan_Click(object sender, EventArgs e)
		{
			UpdatePortList();
		}
		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			Browse();
		}
		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			UpdateTarget();
		}
		private void buttonQuit_Click(object sender, EventArgs e)
		{
			Quit();
		}
		#endregion

		#region Notify icon stuff
		void MainFormResize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				Hide();
				notifyIconProgram.Visible = true;
				if (notifyIconProgram.Tag == null)
				{
					notifyIconProgram.Tag = "!";
					notifyIconProgram.ShowBalloonTip(2);
				}
			}
		}
		void NotifyIcon1MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Show();
				WindowState = FormWindowState.Normal;
				notifyIconProgram.Visible = false;
			}
		}
		void UpdateNotifyIconText()
		{
			string s = "Firmware Updater\nPort: " + comboBoxPortName.Text + "\nFile: " + textBoxFileName.Text.Substring(textBoxFileName.Text.LastIndexOf('/') + 1);
			if (s.Length <= 64)
			{
				notifyIconProgram.Text = s;
			}
			else
			{
				notifyIconProgram.Text = s.Substring(0, 60) + "...";
			}
		}
		private void comboBoxPortName_TextUpdate(object sender, EventArgs e)
		{
			UpdateNotifyIconText();
		}
		void BrowseToolStripMenuItemClick(object sender, EventArgs e)
		{
			Browse();
		}
		void UpdateToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (UpdateTarget())
			{
				notifyIconProgram.ShowBalloonTip(2, "Update", "Target updated :)", ToolTipIcon.None);
			}
			else
			{
				notifyIconProgram.ShowBalloonTip(2, "Update", "Could NOT update target :(", ToolTipIcon.Error);
			}
		}
		void QuitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Quit();
		}
		#endregion
	}
}
