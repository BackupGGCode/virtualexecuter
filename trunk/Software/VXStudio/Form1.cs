using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VXStudio
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
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
				OpenFile(dialog.FileName);
				Status("File created");
			}
		}
		private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				OpenFile(dialog.FileName);
				Status("File saved");
			}
		}
		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
		private void saveallToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion

		#region Project
		private void buildToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
		#endregion
		#endregion

		#region Helpers
		private void OpenFile(string filename)
		{
			try
			{
				RichTextBox rtb = new RichTextBox();
				rtb.LoadFile(filename, RichTextBoxStreamType.PlainText);
				rtb.Tag = filename;
				TabPage tp = new TabPage(Path.GetFileName(filename));
				tp.Controls.Add(rtb);
				rtb.Dock = DockStyle.Fill;
				rtb.TextChanged += new EventHandler(rtb_TextChanged);
				tabControl1.TabPages.Add(tp);
				tabControl1.SelectTab(tp);
			}
			catch
			{
				MessageBox.Show("Unable to open file");
			}
		}

		void rtb_TextChanged(object sender, EventArgs e)
		{
			RichTextBox rtb = (RichTextBox)sender;
			string filename = (string)rtb.Tag;


		}
		private void Status(string text)
		{
			statusStrip1.Items["toolStripStatusLabelStatus"].Text = text;
		}
		#endregion
	}
}