using System;
using System.Collections.Generic;
using System.Text;
using VXC;
using VXC.analysis;
using VXC.node;
using VxCompiler.Environment;

namespace VxCompiler.Phases
{
    public class Environments : DepthFirstAdapter
    {               
        public RuntimeEnvironment mEnv;
       
        #region Declarations

        public override void CaseAPortDecl(APortDecl node)
        {
            mEnv.CurrentScope.Add(node);
            base.CaseAPortDecl(node);
        }
        public override void OutAPortDecl(APortDecl node)
        {
            // check stuff....
            base.OutAPortDecl(node);
        }

        public override void CaseAVarDecl(AVarDecl node)
        {
            // add variable to scope
            mEnv.CurrentScope.Add(node);
            base.CaseAVarDecl(node);
        }      
        public override void OutAVarDecl(AVarDecl node)
        {
            // check init condition if any.
            if (node.GetInit() != null)
            {
                if (mEnv.CurrentScope.Contains(node.GetInit()))
                {
                    long value = mEnv.CurrentScope.GetValueOf(node.GetInit());
                    mEnv.CurrentScope.SetValueOf(node, value);
                }
                else
                {
                    throw new Exception("Unresolveable initcondition");
                }
            }           
            base.OutAVarDecl(node);
        }
        
        public override void CaseAFuncDecl(AFuncDecl node)
        {
            // find the scope of the func decl, and make it the
            // current scope.
            mEnv.CurrentScope = mEnv.CurrentScope.GetScope(node);
            base.CaseAFuncDecl(node);
        }
        public override void OutAFuncDecl(AFuncDecl node)
        {
            mEnv.CurrentScope = mEnv.CurrentScope.Parent;
            base.OutAFuncDecl(node);
        }

        #endregion

        /*
        
        #endregion

        
        #endregion

        #region Expressions
        public override void OutAInitExp(AInitExp node)
        {
            PExp rhs = node.GetExp();
            if (!mCurrentScope.ContainsExpression(rhs))
            {
             throw new Exception("Unresolveable initializer");
            }
            long value = mCurrentScope.GetValueOfExpression(rhs);
            // sub expression elimination
            mCurrentScope.RemoveExpression(rhs);
            node.RemoveChild(rhs);
            // replace by constant.
            PExp @const = new AIntConstExp(new TIntegerLiteral(value.ToString()));
            node.ReplaceBy(@const);
            mCurrentScope.AddExpression(@const, value);         
            base.OutAInitExp(node);
        }

        public override void OutAModExp(AModExp node)
        {
            EvaluateBinaryExp(node, binop.Mod);
            base.OutAModExp(node);
        }
        public override void OutAMultiplyExp(AMultiplyExp node)
        {
            EvaluateBinaryExp(node, binop.Mult);
            base.OutAMultiplyExp(node);
        }
        public override void OutADivideExp(ADivideExp node)
        {
            EvaluateBinaryExp(node, binop.Div);
            base.OutADivideExp(node);
        }
        public override void OutAPlusExp(APlusExp node)
        {
            EvaluateBinaryExp(node, binop.Plus);            
            base.OutAPlusExp(node);
        }
        public override void OutAAssignExp(AAssignExp node)
        {
            EvaluateBinaryExp(node, binop.Plus);                  
            base.OutAAssignExp(node);
        }
        public override void OutAIntConstExp(AIntConstExp node)
        {
            mCurrentScope.AddExpression(node, ConvertValueType(node.ToString()));
 	        base.OutAIntConstExp(node);
        }
        #endregion

        #region utilities

        public enum binop
        {
            Plus, Mult, Div, Mod
        }
        public void EvaluateBinaryExp(PExp node, binop op)
        {
            long value_lhs = 0; 
            long value_rhs = 0;
            PExp rhs = GetRhs(node);
            PExp lhs = GetLhs(node);
            bool connectR = false;
            bool connectL = false;
            if (mCurrentScope.ContainsExpression(rhs))
            {
                value_rhs = mCurrentScope.GetValueOfExpression(rhs);
                connectR = true;
            }
            else if (mEnv.NameLookup(rhs, out value_rhs))
            {
                connectR = true;
            }
            if (connectR)
            {
                if (mCurrentScope.ContainsExpression(lhs))
                {
                    value_lhs = mCurrentScope.GetValueOfExpression(lhs);
                    connectL = true;
                }
                else if (mEnv.NameLookup(lhs, out value_lhs))
                {
                    connectL = true;
                }
            }
            if (connectL)
            {
                long result = BinExpVal(value_lhs, value_rhs, op);
                mCurrentScope.RemoveExpression(lhs);
                mCurrentScope.RemoveExpression(rhs);
                mCurrentScope.AddExpression(node, result);

            }
        }
        public long BinExpVal(long a, long b, binop op)
        {
            switch (op)
            {
                case binop.Plus:
                    return a + b;
                case binop.Mult:
                    return a * b;
                case binop.Div:
                    return a / b;
                case binop.Mod:
                    return a % b;
            }
            return 0;
        }
        public PExp GetLhs(PExp node)
        {
            if (node.GetType() == typeof(APlusExp))
            {
                return ((APlusExp)node).GetLhs();
            }
            if (node.GetType() == typeof(ALshiftExp))
            {
                return ((ALshiftExp)node).GetLhs();
            }
            if (node.GetType() == typeof(ARshiftExp))
            {
                return ((ARshiftExp)node).GetLhs();
            }
            if (node.GetType() == typeof(AMultiplyExp))
            {
                return ((AMultiplyExp)node).GetLhs();
            }
            if (node.GetType() == typeof(ADivideExp))
            {
                return ((ADivideExp)node).GetLhs();
            }
            if (node.GetType() == typeof(AModExp))
            {
                return ((AModExp)node).GetLhs();
            }         
            if (node.GetType() == typeof(AAssignExp))
            {
                return ((AAssignExp)node).GetLhs();
            }
            return null;
        }
        public PExp GetRhs(PExp node)
        {
            if (node.GetType() == typeof(APlusExp))
            {
                return ((APlusExp)node).GetRhs();
            }
            if (node.GetType() == typeof(ALshiftExp))
            {
                return ((ALshiftExp)node).GetRhs();
            }
            if (node.GetType() == typeof(ARshiftExp))
            {
                return ((ARshiftExp)node).GetRhs();
            }
            if (node.GetType() == typeof(AMultiplyExp))
            {
                return ((AMultiplyExp)node).GetRhs();
            }
            if (node.GetType() == typeof(ADivideExp))
            {
                return ((ADivideExp)node).GetRhs();
            }
            if (node.GetType() == typeof(AModExp))
            {
                return ((AModExp)node).GetRhs();
            }
            if (node.GetType() == typeof(AAssignExp))
            {
                return ((AAssignExp)node).GetRhs();
            }
            return null;            
        }       
        public long ConvertValueType(string text)
        {
            return Convert.ToInt64(text);
        }

        #endregion 
        */
    }
}
