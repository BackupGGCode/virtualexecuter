using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using VXC.lexer;
using VXC.parser;
using VXC.node;
using System.IO;

namespace ParserTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StreamReader sr = new StreamReader("../../../Grammar/program.vxc");
            Lexer l = new Lexer(sr);
            Parser p = new Parser(l);
            Start start = p.Parse();
        
            ASTDisplay disp = new ASTDisplay();
            start.Apply(disp);
            ASTDisplayForm form = new ASTDisplayForm();
            form.treeView1.Nodes.Add(disp.result);

            Application.Run(form);
        }
    }
}
