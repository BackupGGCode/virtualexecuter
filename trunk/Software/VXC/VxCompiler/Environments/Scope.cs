using System;
using System.Collections.Generic;
using System.Text;
using VXC;
using VXC.node;
using VXC.analysis;

namespace VxCompiler.Environment
{
    public class ScopeFailure : Exception
    {
        public ScopeFailure(string msg) : base(msg) { }
    }

    public class Scope
    {
        #region Constructor & members

        private List<Scope> mSubScopes;
        private List<ScopeInfo> mSubScopeInfo;
        private Scope mParent;
        private Dictionary<PExp, TypedValue> mExpTypes;
        private Dictionary<PDecl, Declaration> mNamedTypes;
        private Dictionary<string, Declaration> mLookupTable;

        public Scope()
        {
            mParent = null;
            mSubScopes = new List<Scope>();
            mSubScopeInfo = new List<ScopeInfo>();
            mExpTypes = new Dictionary<PExp, TypedValue>();
            mNamedTypes = new Dictionary<PDecl, Declaration>();
            mLookupTable = new Dictionary<string, Declaration>();
        }

        #endregion

        #region Scope Tree 
             
        public Scope AddSubScope(AFuncDecl decl)
        {
            Scope child = new Scope();
            child.Parent = this;            
            mSubScopes.Add(child);
            ScopeInfo inf = new ScopeInfo(decl, child);
            mSubScopeInfo.Add(inf);
            return child;
        }

        public Scope AddSubScope(ABlockStm stm)
        {
            Scope child = new Scope();
            child.Parent = this;
            mSubScopes.Add(child);
            ScopeInfo inf = new ScopeInfo(stm, child);
            mSubScopeInfo.Add(inf);
            return child;
        }

        public Scope GetScope(AFuncDecl decl)
        {
            foreach (ScopeInfo info in mSubScopeInfo)
            {
                if (info.Type == ScopeType.Function)
                {
                    if (info.Function == decl)
                        return info.Scope;
                }
            }            
            return null;
        }

        public List<Scope> SubScopes
        {
            get { return mSubScopes; }
        }

        //public Scope this[PDecl decl]
        //{
        //    get 
        //    {
        //        if (!mSubScopes.ContainsKey(decl))
        //        {
        //            throw new Exception("No scope for that PDecl");
        //        }
        //        return mSubScopes[decl]; 
        //    }
        //}

        public Scope Parent
        {
            get { return mParent; }
            set { mParent = value; }
        }

        #endregion 

        #region Scope Contents
     
        public void Add(PExp exp, long value, PType type)
        {
            mExpTypes.Add(exp, new TypedValue(type, value));
        }
        public void Add(AVarDecl var)
        {
            Declaration tv = new Declaration(var, var.GetType(), var.GetName().Text);
            mNamedTypes.Add(var, tv);
            mLookupTable.Add(var.GetName().Text, tv);
        }
        public void Add(APortDecl port)
        {
            PExp exp = port.GetAdress();
            if (Contains(exp))
            {
                long adress = GetValueOf(exp);
                Declaration tv = new Declaration(port, port.GetType(), adress, port.GetName().Text);
                mNamedTypes.Add(port, tv);
                mLookupTable.Add(port.GetName().Text, tv);
            }
            else
            {
                throw new ScopeFailure("Unresolved port address");
            }
        }

        public void SetValueOf(PExp exp, long value)
        {
            if (mExpTypes.ContainsKey(exp))
            {
                mExpTypes[exp].Value = value;
            }
            else
            {
                throw new Exception("No such expression in direct scope");
            }
        }
        public void SetValueOf(PDecl decl, long value)
        {
            if (mNamedTypes.ContainsKey(decl))
            {
                mNamedTypes[decl].Value = value;
            }
            else
            {
                throw new Exception("No such declaration in direct scope");
            }
        }

        public bool Contains(PExp exp)
        {
            if (exp != null)
                return (mExpTypes.ContainsKey(exp));
            else
                return false;
        }
        public bool Contains(AVarDecl var)
        {
            if (var != null)
                return (mNamedTypes.ContainsKey(var));
            else
                return false;
        }
        public bool Contains(APortDecl var)
        {
            if (var != null)
                return (mNamedTypes.ContainsKey(var));
            else
                return false;
        }

        public PType GetTypeOf(PExp exp)
        {
            if (mExpTypes.ContainsKey(exp))
            {
                return mExpTypes[exp].Type;
            }
            return null;
        }
        public long GetValueOf(PExp exp)
        {
            if (mExpTypes.ContainsKey(exp))
            {
                return mExpTypes[exp].Value;
            }
            return 0;
        }

        public PType TypeLookup(string name)
        {
            return Lookup(name).Type;
        }
        public long ValueLookup(string name)
        {
            return Lookup(name).Value;
        }
  
        public Declaration Lookup(ANamedExp exp)
        {
            return Lookup(exp.GetId().Text);
        }
        public Declaration Lookup(AVarDecl decl)
        {
            return Lookup(decl.GetName().Text);
        }
        public Declaration Lookup(string name)
        {
            if (mLookupTable.ContainsKey(name))
            {
                return mLookupTable[name];
            }
            else if (Parent == null)
            {
                return null;
            }
            return Parent.Lookup(name);
        }      
  
        #endregion
    }
}
