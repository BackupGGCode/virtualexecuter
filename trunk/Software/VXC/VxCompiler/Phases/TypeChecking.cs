using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;
using VXC;
using VXC.analysis;
using VxCompiler.Environment;

namespace VxCompiler.Phases
{
    public class TypeMismatch: Exception
    {
        public TypeMismatch(string msg) : base(msg) {}
    }

    // assign types to all expressions.
    public class TypeChecking : DepthFirstAdapter
    {
        #region Infrastructure
       
        public Scope mGlobalScope;
        public Scope mCurrentScope;

        public TypeChecking()
        {
            mGlobalScope = new Scope();
            mCurrentScope = mGlobalScope;
        }

        #endregion
        /*
        #region Declarations 

        public override void CaseAFuncDecl(AFuncDecl node)
        {
            Scope sub = new Scope();
            mCurrentScope.AddSubScope(sub);
            mCurrentScope = sub;
            base.CaseAFuncDecl(node);
        }

        public override void OutAFuncDecl(AFuncDecl node)
        {
            mCurrentScope = mCurrentScope.Parent;
            base.OutAFuncDecl(node);
        }

        public override void CaseAPortDecl(APortDecl node)
        {
            if (mCurrentScope.TypeLookup(node.GetName().Text) != null)
            {
                throw new TypeMismatch("Illegal attempt to overload port");
            }
            mCurrentScope.AddPortType(node, node.GetType());
            base.CaseAPortDecl(node);
        }

        public override void CaseAVarDecl(AVarDecl node)
        {
            if (mCurrentScope.TypeLookup(node.GetName().Text) != null)
            {
                throw new TypeMismatch("Illegal attempt to overload variable");
            }
            mCurrentScope.AddVarType(node, node.GetType());
            base.CaseAVarDecl(node);
        }

        // Checks type of initializer against variable type.
        // TODO: handle sign...
        public override void OutAVarDecl(AVarDecl node)
        {
            PExp init = node.GetInit();
            if (init != null)
            {
                if (!mCurrentScope.Contains(init))
                {
                    throw new TypeMismatch("Unresolved variable initializer");
                }
                PType type = mCurrentScope.GetTypeOf(init);
                if (!TypeEnvironment.AssignableTo(type, node.GetType()))
                {
                    throw new TypeMismatch("Unassignable right hand side of initializer.");
                }
            }
            base.OutAVarDecl(node);
        }

        public override void OutAFloatType(AFloatType node)
        {
            base.OutAFloatType(node);
            throw new TypeMismatch("Floats are not supported yet!");
            
        }
        public override void OutAStringType(AStringType node)
        {
            base.OutAStringType(node);
            throw new TypeMismatch("Strings are not supported yet!");
            
        }

        #endregion  

        #region binary expressions
      
        public override void OutAPlusExp(APlusExp node)
        {
            PExp lhs = node.GetLhs();
            PExp rhs = node.GetRhs();
            if (!mCurrentScope.Contains(lhs) || !mCurrentScope.Contains(rhs))
            {
                throw new TypeMismatch("Plus: One of the sub-expressions did not evaluate to a type");
            }
            PType lhsType = mCurrentScope.GetTypeOf(lhs);
            PType rhsType = mCurrentScope.GetTypeOf(rhs);
            if (!TypeEnvironment.AssignableTo(lhsType, rhsType))
            {
                throw new TypeMismatch("Plus: not assignable");
            }
            if (TypeEnvironment.SizeOfType(lhsType) > TypeEnvironment.SizeOfType(rhsType))
                mCurrentScope.AddExpType(node, lhsType);
            else
                mCurrentScope.AddExpType(node, rhsType);                 
            base.OutAPlusExp(node);
        }
        public override void OutAAssignExp(AAssignExp node)
        {
            PExp lhs = node.GetLhs();
            PExp rhs = node.GetRhs();
            if (!mCurrentScope.Contains(lhs) || !mCurrentScope.Contains(rhs))
            {
                throw new TypeMismatch("Assignment: One of the sub-expressions did not evaluate to a type");
            }            
            PType lhsType = mCurrentScope.GetTypeOf(lhs);
            PType rhsType = mCurrentScope.GetTypeOf(rhs);
            if (!TypeEnvironment.AssignableTo(lhsType, rhsType))
            {
                    throw new TypeMismatch("Assignment failed: not assignable");
            }
            mCurrentScope.AddExpType(node, lhsType);            
            base.OutAAssignExp(node);
        }
 
        #endregion
        
        #region Unary Expressions
        
        public override void OutAInitExp(AInitExp node)
        {
            PExp rhs = node.GetExp();
            PType rhsType = mCurrentScope.GetTypeOf(rhs);
            if (!mCurrentScope.Contains(rhs))
            {
                throw new TypeMismatch("Init: The right hand side did not evaluate to a type");
            }
            mCurrentScope.AddExpType(node, rhsType);
            base.OutAInitExp(node);
        }    
        #endregion

        #region Primary Expressions

        public override void OutANamedExp(ANamedExp node)
        {
            string name = node.GetId().Text;
            PType type = mCurrentScope.TypeLookup(name);
            if (type == null)
            {
                throw new TypeMismatch("Unresolved named reference...");
            }
            mCurrentScope.AddExpType(node, type);            
            base.OutANamedExp(node);
        }

        public override void OutAStringConstExp(AStringConstExp node)
        {
            base.OutAStringConstExp(node);
            throw new TypeMismatch("Strings are not supported yet!");
            
        }
        public override void OutAFloatConstExp(AFloatConstExp node)
        {
            base.OutAFloatConstExp(node);
            throw new TypeMismatch("Floats are not supported yet!");
            
        }
        public override void OutAIntConstExp(AIntConstExp node)
        {
            mCurrentScope.AddExpType(node, new ASingleType());
 	        base.OutAIntConstExp(node);
        }
        public override void OutABoolConstExp(ABoolConstExp node)
        {
            mCurrentScope.AddExpType(node, new ABooleanType());
            base.OutABoolConstExp(node);
        }

        #endregion
         * */
    }
}
