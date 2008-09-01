using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VXC.analysis;
using VXC.node;
using VxCompiler.Environment;
using VxCompiler.CodeEmission;
using VxCompiler.CodeEmission.Instructions;

namespace VxCompiler.Phases
{
    public class CodeEmissionPhase : DepthFirstAdapter
    {
        private AssemblyFile mOutputFile;
        public RuntimeEnvironment mEnv;

        public override void CaseAModule(AModule node)
        {
            mOutputFile = new AssemblyFile();
            base.CaseAModule(node);
        }

        public override void CaseAVarDecl(AVarDecl node)
        {
            Declaration d = mEnv.CurrentScope.Lookup(node);
            mOutputFile.Add(d);
            base.CaseAVarDecl(node);
        }

        public override void CaseAFuncDecl(AFuncDecl node)
        {
            // find the scope of the func decl, and make it the
            // current scope.
            mEnv.CurrentScope = mEnv.CurrentScope.GetScope(node);
            mOutputFile.Add(new Label(node.GetName().Text));
            base.CaseAFuncDecl(node);
        }
               
        public override void OutAFuncDecl(AFuncDecl node)
        {
            mEnv.CurrentScope = mEnv.CurrentScope.Parent;
            base.OutAFuncDecl(node);
        }

        public override void OutALshiftExp(ALshiftExp node)
        {
            Declaration d = mEnv.CurrentScope.Lookup((ANamedExp)node.GetLhs());
            if (d.Decl.GetType() == typeof(APortDecl))
            {
                mOutputFile.Add(new Out());
            }
            base.OutALshiftExp(node);
        }

        public override void OutARshiftExp(ARshiftExp node)
        {
            Declaration d = mEnv.CurrentScope.Lookup((ANamedExp)node.GetLhs());
            if (d.Decl.GetType() == typeof(APortDecl))
            {
                mOutputFile.Add(new In());
            }
            base.OutARshiftExp(node);
        }


        public override void OutANamedExp(ANamedExp node)
        {
            Declaration val = mEnv.CurrentScope.Lookup(node);
            if (val.Decl.GetType() == typeof(APortDecl))
            {
                mOutputFile.Add(new Load(val.Address, val.Type));
            }
            if (val.Decl.GetType() == typeof(AVarDecl))
            {
                mOutputFile.Add(new Push(val.Name, val.Type));
            }
            base.OutANamedExp(node);
        }

        public string Emit(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            string code = mOutputFile.Emit();
            sw.Write(code);
            sw.Flush();
            sw.Close();
            return code;
        }
    }
}
