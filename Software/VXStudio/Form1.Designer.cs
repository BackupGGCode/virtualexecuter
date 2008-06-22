namespace VXStudio
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generatelistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generatemapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainerTopBottom = new System.Windows.Forms.SplitContainer();
			this.splitContainerLeftRight = new System.Windows.Forms.SplitContainer();
			this.treeViewProjectFiles = new System.Windows.Forms.TreeView();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.splitContainerTopBottom.Panel1.SuspendLayout();
			this.splitContainerTopBottom.Panel2.SuspendLayout();
			this.splitContainerTopBottom.SuspendLayout();
			this.splitContainerLeftRight.Panel1.SuspendLayout();
			this.splitContainerLeftRight.Panel2.SuspendLayout();
			this.splitContainerLeftRight.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 394);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(611, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabelStatus
			// 
			this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
			this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem,
            this.projectToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(611, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// filesToolStripMenuItem
			// 
			this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveallToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
			this.filesToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.filesToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click_1);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveallToolStripMenuItem
			// 
			this.saveallToolStripMenuItem.Name = "saveallToolStripMenuItem";
			this.saveallToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.saveallToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.saveallToolStripMenuItem.Text = "Save &all";
			this.saveallToolStripMenuItem.Click += new System.EventHandler(this.saveallToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// projectToolStripMenuItem
			// 
			this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildToolStripMenuItem,
            this.generatelistToolStripMenuItem,
            this.generatemapToolStripMenuItem});
			this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
			this.projectToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
			this.projectToolStripMenuItem.Text = "&Project";
			// 
			// buildToolStripMenuItem
			// 
			this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
			this.buildToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.buildToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.buildToolStripMenuItem.Text = "&Build";
			this.buildToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
			// 
			// generatelistToolStripMenuItem
			// 
			this.generatelistToolStripMenuItem.CheckOnClick = true;
			this.generatelistToolStripMenuItem.Name = "generatelistToolStripMenuItem";
			this.generatelistToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.generatelistToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.generatelistToolStripMenuItem.Text = "Generate .&list";
			this.generatelistToolStripMenuItem.Click += new System.EventHandler(this.generatelistToolStripMenuItem_Click);
			// 
			// generatemapToolStripMenuItem
			// 
			this.generatemapToolStripMenuItem.CheckOnClick = true;
			this.generatemapToolStripMenuItem.Name = "generatemapToolStripMenuItem";
			this.generatemapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
			this.generatemapToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.generatemapToolStripMenuItem.Text = "Generate .&map";
			this.generatemapToolStripMenuItem.Click += new System.EventHandler(this.generatemapToolStripMenuItem_Click);
			// 
			// splitContainerTopBottom
			// 
			this.splitContainerTopBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerTopBottom.Location = new System.Drawing.Point(0, 24);
			this.splitContainerTopBottom.Name = "splitContainerTopBottom";
			this.splitContainerTopBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerTopBottom.Panel1
			// 
			this.splitContainerTopBottom.Panel1.Controls.Add(this.splitContainerLeftRight);
			// 
			// splitContainerTopBottom.Panel2
			// 
			this.splitContainerTopBottom.Panel2.Controls.Add(this.richTextBoxOutput);
			this.splitContainerTopBottom.Size = new System.Drawing.Size(611, 370);
			this.splitContainerTopBottom.SplitterDistance = 249;
			this.splitContainerTopBottom.TabIndex = 2;
			this.splitContainerTopBottom.TabStop = false;
			// 
			// splitContainerLeftRight
			// 
			this.splitContainerLeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerLeftRight.Location = new System.Drawing.Point(0, 0);
			this.splitContainerLeftRight.Name = "splitContainerLeftRight";
			// 
			// splitContainerLeftRight.Panel1
			// 
			this.splitContainerLeftRight.Panel1.Controls.Add(this.treeViewProjectFiles);
			this.splitContainerLeftRight.Panel1Collapsed = true;
			// 
			// splitContainerLeftRight.Panel2
			// 
			this.splitContainerLeftRight.Panel2.Controls.Add(this.tabControl1);
			this.splitContainerLeftRight.Size = new System.Drawing.Size(611, 249);
			this.splitContainerLeftRight.SplitterDistance = 159;
			this.splitContainerLeftRight.TabIndex = 0;
			this.splitContainerLeftRight.TabStop = false;
			// 
			// treeViewProjectFiles
			// 
			this.treeViewProjectFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewProjectFiles.Location = new System.Drawing.Point(0, 0);
			this.treeViewProjectFiles.Name = "treeViewProjectFiles";
			this.treeViewProjectFiles.Size = new System.Drawing.Size(159, 249);
			this.treeViewProjectFiles.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(611, 249);
			this.tabControl1.TabIndex = 0;
			// 
			// richTextBoxOutput
			// 
			this.richTextBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxOutput.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBoxOutput.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxOutput.Name = "richTextBoxOutput";
			this.richTextBoxOutput.ReadOnly = true;
			this.richTextBoxOutput.Size = new System.Drawing.Size(611, 117);
			this.richTextBoxOutput.TabIndex = 0;
			this.richTextBoxOutput.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(611, 416);
			this.Controls.Add(this.splitContainerTopBottom);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "VXStudio";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainerTopBottom.Panel1.ResumeLayout(false);
			this.splitContainerTopBottom.Panel2.ResumeLayout(false);
			this.splitContainerTopBottom.ResumeLayout(false);
			this.splitContainerLeftRight.Panel1.ResumeLayout(false);
			this.splitContainerLeftRight.Panel2.ResumeLayout(false);
			this.splitContainerLeftRight.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.SplitContainer splitContainerTopBottom;
		private System.Windows.Forms.SplitContainer splitContainerLeftRight;
		private System.Windows.Forms.TreeView treeViewProjectFiles;
		private System.Windows.Forms.RichTextBox richTextBoxOutput;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
		private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ToolStripMenuItem generatelistToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generatemapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveallToolStripMenuItem;
	}
}

