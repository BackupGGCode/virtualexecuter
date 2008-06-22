using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace VXStudio
{
	public partial class Form1 : Form
	{
		private string vxaPath = @"C:\Documents and Settings\coma\My Documents\Projects\VirtualeXecuter\Software\VXA\bin\Release\VXA.exe";
		private Dictionary<string, string> buildOptions = new Dictionary<string, string>();

		public Form1()
		{
			InitializeComponent();
			buildOptions.Add("l", "false");
			buildOptions.Add("m", "false");
		}

		#region Menues
		#region File
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				FileStream fs = File.Create(dialog.FileName);
				fs.Close();
				OpenTabPage(dialog.FileName);
				Status("File created");
			}
		}
		private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				OpenTabPage(dialog.FileName);
				Status("File opened");
			}
		}
		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TabPage tp = (TabPage)tabControl1.SelectedTab;
			RichTextBox rtb = (RichTextBox)tp.Tag;
			if (tp.Text.EndsWith("*"))
			{
				string filename = (string)rtb.Tag;
				rtb.SaveFile(filename, RichTextBoxStreamType.PlainText);
				tp.Text = Path.GetFileName(filename);
				Status("File saved");
			}
			else
			{
				Status("Nothing to save");
			}
		}
		private void saveallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool saved = false;
			foreach (TabPage tp in tabControl1.TabPages)
			{
				RichTextBox rtb = (RichTextBox)tp.Tag;
				if (tp.Text.EndsWith("*"))
				{
					string filename = (string)rtb.Tag;
					rtb.SaveFile(filename, RichTextBoxStreamType.PlainText);
					tp.Text = Path.GetFileName(filename);
					saved = true;
				}
			}

			if (saved)
			{
				Status("Files saved");
			}
			else
			{
				Status("Nothing to save");
			}
		}
		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TabPage tp = (TabPage)tabControl1.SelectedTab;
			RichTextBox rtb = (RichTextBox)tp.Tag;
			if (tp.Text.EndsWith("*"))
			{
				DialogResult result = MessageBox.Show("File is modifed. Save before closing?", "VXStudio", MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Cancel)
				{
					return;
				}
				if (result == DialogResult.Yes)
				{
					string filename = (string)rtb.Tag;
					rtb.SaveFile(filename, RichTextBoxStreamType.PlainText);
					tp.Text = Path.GetFileName(filename);
					Status("File saved");
				}
			}
			tabControl1.TabPages.Remove(tp);
		}
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion

		#region Project
		private void buildToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveallToolStripMenuItem_Click(null, null);
			Application.DoEvents();
			richTextBoxOutput.Clear();
			TabPage tp = (TabPage)tabControl1.SelectedTab;
			if (tp != null)
			{
				RichTextBox rtb = (RichTextBox)tp.Tag;
				BuildFile((string)rtb.Tag);
			}
		}
		private void generatelistToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (buildOptions["l"] == "true")
			{
				buildOptions["l"] = "false";
			}
			else
			{
				buildOptions["l"] = "true";
			}
		}
		private void generatemapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (buildOptions["m"] == "true")
			{
				buildOptions["m"] = "false";
			}
			else
			{
				buildOptions["m"] = "true";
			}
		}
		#endregion
		#endregion

		#region Helpers
		private void OpenTabPage(string filename)
		{
			try
			{
				RichTextBox rtb = new RichTextBox();
				rtb.LoadFile(filename, RichTextBoxStreamType.PlainText);
				rtb.Tag = filename;
				rtb.DetectUrls = false;
				rtb.AcceptsTab = true;
				rtb.Font = new Font(new FontFamily("courier new"), 9.75f);
				TabPage tp = new TabPage(Path.GetFileName(filename));
				tp.Tag = rtb;
				tp.Controls.Add(rtb);
				rtb.Dock = DockStyle.Fill;
				rtb.TextChanged += new EventHandler(rtb_TextChanged);
				tabControl1.TabPages.Add(tp);
				tabControl1.SelectTab(tp);
				rtb.Focus();
			}
			catch
			{
				MessageBox.Show("Unable to open file");
			}
		}
		private void rtb_TextChanged(object sender, EventArgs e)
		{
			RichTextBox rtb = (RichTextBox)sender;
			TabPage tp = (TabPage)rtb.Parent;

			if (tp.Text.EndsWith("*") == false)
			{
				string filename = (string)rtb.Tag;
				tp.Text = Path.GetFileName(filename) + "*";
			}
		}
		private void Status(string text)
		{
			statusStrip1.Items["toolStripStatusLabelStatus"].Text = text;
		}
		private void BuildFile(string filename)
		{
			string outputFilename = Path.ChangeExtension(filename, ".vxx");
			string arguments = "\"" + filename + "\" ";

			foreach (string key in buildOptions.Keys)
			{
				if (buildOptions[key] == "true")
				{
					arguments += "-" + key + " ";
				}
			}

			try
			{
				Process p = new Process();
				p.EnableRaisingEvents = false;
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.RedirectStandardError = true;
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.FileName = vxaPath;
				p.StartInfo.Arguments = arguments;// "c:\\test.vxa";
				p.Start();
				p.WaitForExit(10000);
				richTextBoxOutput.AppendText(p.StandardOutput.ReadToEnd());
				if (p.HasExited)
				{
					Status("Build completed");
				}
				else
				{
					Status("Build timed out");
				}
			}
			catch
			{
				Status("Build failed");
			}
		}
		#endregion
	}
}