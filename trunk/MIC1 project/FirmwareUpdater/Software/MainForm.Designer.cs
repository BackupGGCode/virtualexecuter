/*
 * Created by SharpDevelop.
 * User: Coma
 * Date: 17-05-2007
 * Time: 16:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Firmware_Updater
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.comboBoxPortName = new System.Windows.Forms.ComboBox();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.textBoxFileName = new System.Windows.Forms.TextBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.buttonRescan = new System.Windows.Forms.Button();
			this.buttonQuit = new System.Windows.Forms.Button();
			this.notifyIconProgram = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.browseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.comboBoxBaudrate = new System.Windows.Forms.ComboBox();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxPortName
			// 
			this.comboBoxPortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxPortName.FormattingEnabled = true;
			this.comboBoxPortName.Location = new System.Drawing.Point(12, 12);
			this.comboBoxPortName.Name = "comboBoxPortName";
			this.comboBoxPortName.Size = new System.Drawing.Size(244, 21);
			this.comboBoxPortName.TabIndex = 0;
			this.comboBoxPortName.TextUpdate += new System.EventHandler(this.comboBoxPortName_TextUpdate);
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowse.Location = new System.Drawing.Point(262, 64);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(60, 23);
			this.buttonBrowse.TabIndex = 1;
			this.buttonBrowse.Text = "Browse...";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUpdate.Location = new System.Drawing.Point(262, 93);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(60, 23);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Text = "Update";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// textBoxFileName
			// 
			this.textBoxFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFileName.Location = new System.Drawing.Point(12, 66);
			this.textBoxFileName.Name = "textBoxFileName";
			this.textBoxFileName.Size = new System.Drawing.Size(244, 20);
			this.textBoxFileName.TabIndex = 3;
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(12, 93);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(244, 23);
			this.progressBar1.Step = 1;
			this.progressBar1.TabIndex = 4;
			// 
			// richTextBoxMessages
			// 
			this.richTextBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBoxMessages.Location = new System.Drawing.Point(12, 122);
			this.richTextBoxMessages.Name = "richTextBoxMessages";
			this.richTextBoxMessages.Size = new System.Drawing.Size(310, 63);
			this.richTextBoxMessages.TabIndex = 5;
			this.richTextBoxMessages.Text = "";
			this.richTextBoxMessages.Resize += new System.EventHandler(this.richTextBoxMessages_Resize);
			this.richTextBoxMessages.TextChanged += new System.EventHandler(this.richTextBoxMessages_TextChanged);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "Generic files|*.gen|Hex files|*.hex;*.a90|Binary files|*.bin|All files|*.*";
			this.openFileDialog1.FilterIndex = 2;
			this.openFileDialog1.Title = "Select source file...";
			// 
			// buttonRescan
			// 
			this.buttonRescan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRescan.Location = new System.Drawing.Point(262, 10);
			this.buttonRescan.Name = "buttonRescan";
			this.buttonRescan.Size = new System.Drawing.Size(60, 23);
			this.buttonRescan.TabIndex = 6;
			this.buttonRescan.Text = "Rescan";
			this.buttonRescan.UseVisualStyleBackColor = true;
			this.buttonRescan.Click += new System.EventHandler(this.buttonRescan_Click);
			// 
			// buttonQuit
			// 
			this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonQuit.Location = new System.Drawing.Point(245, 191);
			this.buttonQuit.Name = "buttonQuit";
			this.buttonQuit.Size = new System.Drawing.Size(75, 23);
			this.buttonQuit.TabIndex = 8;
			this.buttonQuit.Text = "Quit";
			this.buttonQuit.UseVisualStyleBackColor = true;
			this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
			// 
			// notifyIconProgram
			// 
			this.notifyIconProgram.BalloonTipText = "I\'m right here if you need me";
			this.notifyIconProgram.BalloonTipTitle = "Hello";
			this.notifyIconProgram.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIconProgram.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconProgram.Icon")));
			this.notifyIconProgram.Text = "Firmware Updater";
			this.notifyIconProgram.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1MouseClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.quitToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(121, 70);
			// 
			// browseToolStripMenuItem
			// 
			this.browseToolStripMenuItem.Name = "browseToolStripMenuItem";
			this.browseToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.browseToolStripMenuItem.Text = "Browse";
			this.browseToolStripMenuItem.Click += new System.EventHandler(this.BrowseToolStripMenuItemClick);
			// 
			// updateToolStripMenuItem
			// 
			this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
			this.updateToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.updateToolStripMenuItem.Text = "Update";
			this.updateToolStripMenuItem.Click += new System.EventHandler(this.UpdateToolStripMenuItemClick);
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.quitToolStripMenuItem.Text = "Quit";
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItemClick);
			// 
			// comboBoxBaudrate
			// 
			this.comboBoxBaudrate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxBaudrate.FormattingEnabled = true;
			this.comboBoxBaudrate.Location = new System.Drawing.Point(12, 39);
			this.comboBoxBaudrate.Name = "comboBoxBaudrate";
			this.comboBoxBaudrate.Size = new System.Drawing.Size(244, 21);
			this.comboBoxBaudrate.TabIndex = 9;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(332, 226);
			this.Controls.Add(this.comboBoxBaudrate);
			this.Controls.Add(this.buttonQuit);
			this.Controls.Add(this.buttonRescan);
			this.Controls.Add(this.richTextBoxMessages);
			this.Controls.Add(this.textBoxFileName);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.comboBoxPortName);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.buttonBrowse);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(235, 223);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Firmware Updater";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.Resize += new System.EventHandler(this.MainFormResize);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem browseToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.NotifyIcon notifyIconProgram;
		private System.Windows.Forms.Button buttonQuit;
		private System.Windows.Forms.Button buttonRescan;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.RichTextBox richTextBoxMessages;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TextBox textBoxFileName;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.ComboBox comboBoxPortName;
		private System.Windows.Forms.ComboBox comboBoxBaudrate;
	}
}
