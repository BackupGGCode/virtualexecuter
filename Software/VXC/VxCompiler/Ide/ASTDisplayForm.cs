using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VxCompiler.Environment;

namespace VxCompiler
{
    public partial class ASTDisplayForm : Form
    {
        public RuntimeEnvironment env;

        public ASTDisplayForm()
        {
            InitializeComponent();
        }

        public void showCode(string code)
        {
            richTextBox1.Text = code;
        }
        public void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            object node = e.Node.Tag;
            if (node != null)
            {
                object o = ShellMaker.MakeShell(node, env);
                if (o != null)
                    propertyGrid1.SelectedObject = o;
                else
                    propertyGrid1.SelectedObject = e.Node.Tag;
                
            }
        }
    }
}