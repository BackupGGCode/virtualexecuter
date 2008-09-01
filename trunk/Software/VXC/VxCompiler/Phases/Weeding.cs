using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;
using VXC;
using VXC.analysis;
using VxCompiler.Environment;

namespace VxCompiler.Phases
{
    public class Weeding : DepthFirstAdapter
    {
        public RuntimeEnvironment Env;

        public override void CaseAFuncDecl(AFuncDecl node)
        {
            Scope sub = Env.CurrentScope.AddSubScope(node);
            Env.CurrentScope = sub;
            base.CaseAFuncDecl(node);
        }

        public override void OutAFuncDecl(AFuncDecl node)
        {
            Env.CurrentScope = Env.CurrentScope.Parent;
            base.OutAFuncDecl(node);
        }

        public override void CaseABlockStm(ABlockStm node)
        {
            // ignore default block of functions.
            if (node.Parent().GetType() != typeof(AFormalsAndBody))
            {
                Scope sub = Env.CurrentScope.AddSubScope(node);
                Env.CurrentScope = sub;
            }
            base.CaseABlockStm(node);
        }

        public override void OutABlockStm(ABlockStm node)
        {
             // ignore default block of functions.
            if (node.Parent().GetType() != typeof(AFormalsAndBody))
            {
                Env.CurrentScope = Env.CurrentScope.Parent;
            }
            base.OutABlockStm(node);
        }


        public override void CaseAVarDecl(AVarDecl node)
        {
            // check: variable initializer is a InitExp
            PExp exp = node.GetInit();
            if (exp != null)
            {
                if (exp.GetType() != typeof(AInitExp))
                {
                    Error.Fatal(ErrorType.InvalidInitializer, node.GetName());
                }
            }
            base.CaseAVarDecl(node);
        }

        public override void CaseAPortDecl(APortDecl node)
        {
            // Check: adress is a integer constant
            if (node.GetAdress().GetType() != typeof(AIntConstExp))
            {
                Error.Fatal(ErrorType.InvalidAdress, node.GetName());
            }

            // check: variable initializer is a InitExp
            PExp exp = node.GetInit();
            if (exp != null)
            {
                if (exp.GetType() != typeof(AInitExp))
                {
                    Error.Fatal(ErrorType.InvalidInitializer, node.GetName());
                }
            }
            base.CaseAPortDecl(node);
        }

        public override void OutAIntConstExp(AIntConstExp node)
        {
            // check: valid 32 bit integer
            // annotate: type and value in current scope.
            long result = RuntimeEnvironment.ConvertToLong(node.GetValue());
            PType type = RuntimeEnvironment.GetSmallestFit(result);
            Env.CurrentScope.Add(node, result, type);
            base.OutAIntConstExp(node);
        }

        public override void OutAVoidType(AVoidType node)
        {
            // check: void type must be a return types.
            if (node.Parent().GetType() != typeof(AFuncDecl))
            {
                Error.Fatal(ErrorType.VoidTypeNotReturnType, node.GetToken());
            }
            base.OutAVoidType(node);
        }
    }
}
