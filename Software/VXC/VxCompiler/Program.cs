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
        static void Main(string[] args)
        {
            bool showAST = true;
            if (args.Length == 1)
            {
                string filename = args[0];
                string outputName = Path.ChangeExtension(filename, ".vxa");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                StreamReader sr = new StreamReader(filename);
                Lexer l = new Lexer(sr);
                Parser p = new Parser(l);
                Start start = p.Parse();

                CodeEmissionPhase emission = new CodeEmissionPhase();
                start.Apply(emission);
                emission.Emit(outputName);

                if (showAST)
                {
                    ASTDisplay disp = new ASTDisplay();
                    start.Apply(disp);
                    ASTDisplayForm form = new ASTDisplayForm();
                    form.treeView1.Nodes.Add(disp.result);
                    Application.Run(form);
                }
            }            
        }
    }
}
