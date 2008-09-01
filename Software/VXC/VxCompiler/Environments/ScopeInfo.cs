using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;

namespace VxCompiler.Environment
{
    public enum ScopeType
    {
        Function, Block, Global
    }

    public class ScopeInfo
    {
        private Scope mScope;
        private PDecl mFunction;
        private PStm mBlock;
        private ScopeType mType;

        public ScopeInfo(AFuncDecl decl, Scope scope)
        {
            mFunction = decl;
            mBlock = null;
            mType = ScopeType.Function;
            mScope = scope;
        }

        public ScopeInfo(ABlockStm stm, Scope scope)
        {
            mBlock = stm;
            mFunction = null;
            mType = ScopeType.Block;
            mScope = scope;
        }

        public ScopeInfo(Scope scope)
        {
            mBlock = null;
            mFunction = null;
            mType = ScopeType.Global;
            mScope = scope;
        }

        public ScopeType Type
        {
            get { return mType; }
        }
	
        public PStm Block
        {
            get { return mBlock; }
        }
	
        public PDecl Function
        {
            get { return mFunction; }
        }
	
        public Scope Scope
        {
            get { return mScope; }
        }
	
    }
}
