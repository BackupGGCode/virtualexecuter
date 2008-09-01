using System;
using System.IO;
using System.Windows.Forms;
using VXC.lexer;
using VXC.node;
using VXC.parser;
using VxCompiler.Phases;
using VxCompiler.Environment;

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
                // prepare
                string filename = args[0];
                string outputName = Path.ChangeExtension(filename, ".vxa");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // parse source file
                StreamReader sr = new StreamReader(filename);
                Lexer l = new Lexer(sr);
                Parser p = new Parser(l);
                Start start = p.Parse();

                RuntimeEnvironment env = new RuntimeEnvironment();
                try
                {
                    // Apply compiler phases
                    Weeding weeder = new Weeding();
                    weeder.Env = env;
                    start.Apply(weeder);

                    Environments envs = new Environments();
                    envs.mEnv = env;
                    start.Apply(envs);

                    //TypeChecking buildTEnv = new TypeChecking();
                    //start.Apply(buildTEnv);

                    //EnvironmentBuilding buildEnv = new EnvironmentBuilding();
                    //start.Apply(buildEnv);

                    CodeEmissionPhase emission = new CodeEmissionPhase();
                    emission.mEnv = env;
                    start.Apply(emission);
                    string code = emission.Emit(outputName);

                    if (showAST)
                    {
                        ASTDisplay disp = new ASTDisplay();
                        start.Apply(disp);
                        ASTDisplayForm form = new ASTDisplayForm();
                        form.env = env;
                        form.treeView1.Nodes.Add(disp.result);
                        form.showCode(code);
                        Application.Run(form);
                    }
                }
                catch (OutOfMemoryException e)
                {
                    MessageBox.Show(e.Message);
                }

            }            
        }
    }
}
