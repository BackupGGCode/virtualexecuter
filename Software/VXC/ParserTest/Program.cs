using System;
using System.IO;
using System.Windows.Forms;
using VXC.lexer;
using VXC.node;
using VXC.parser;
using VxCompiler.CodeEmission;

namespace VxCompiler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string arg1 = "../../../Grammar/program.vxc";
            string outputName = Path.ChangeExtension(arg1, ".vxa");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StreamReader sr = new StreamReader(arg1);
            Lexer l = new Lexer(sr);
            Parser p = new Parser(l);
            Start start = p.Parse();
        
            ASTDisplay disp = new ASTDisplay();
            start.Apply(disp);
            ASTDisplayForm form = new ASTDisplayForm();
            form.treeView1.Nodes.Add(disp.result);       

            CodeEmissionPhase emission = new CodeEmissionPhase();
            start.Apply(emission);
            emission.Emit(outputName);

            //Application.Run(form);
        }
    }
}
