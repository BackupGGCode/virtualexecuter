using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;
using VxCompiler.Environment;

namespace VxCompiler
{
    public static class ShellMaker
    {
        public static object MakeShell(object node, RuntimeEnvironment env)
        {
            if (node.GetType() == typeof(AFuncDecl))
            {
                return new FunctionShell((AFuncDecl)node);
            }
            if (node.GetType() == typeof(AVarDecl))
            {
                return new VariableShell((AVarDecl)node, env);
            }
            if (IsExpression(node))
            {
                return new ExpressionShell((PExp)node, env);
            }
            return null;
        }

        public static bool IsExpression(object node)
        {
            Type t = node.GetType();
            return (t == typeof(AAssignExp) ||
                    t == typeof(ANamedExp) ||
                    t == typeof(APlusExp) ||
                    t == typeof(AIntConstExp));
        }
    }

    public class Shell
    {
        private long mValue;

        public long Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
        private string mName;

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

    }

    public class VariableShell : Shell
    {
        public VariableShell(AVarDecl decl, RuntimeEnvironment env)
        {
            Name = decl.GetName().ToString();

            //if (decl.GetInit() != null && env.GetGlobalScope().ContainsExpression(decl.GetInit()))
            //{
            //    Value = env.GetGlobalScope().GetValueOfExpression(decl.GetInit());
            //}
        }

    }

    public class ExpressionShell : Shell
    {

        public ExpressionShell(PExp exp, RuntimeEnvironment env)
        {
            long value;
            if (env.GlobalSearch(exp, out value))
            {
                Value = value;
            }
        }
    }

    public class FunctionShell : Shell
    {
        private string mReturns;
        public string ReturnType
        {
            get { return mReturns; }
            set { mReturns = value; }
        }

        public FunctionShell(AFuncDecl decl)
        {
            ReturnType = TypeEnvironment.GetTypename(decl.GetType());
            Name = decl.GetName().ToString();
        }
    }
}
