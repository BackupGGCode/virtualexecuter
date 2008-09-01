using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;

namespace VxCompiler.Environment
{
    public class Declaration
    {
        private long mValue;
        private long mAddress;
        private string mName;        
        private PDecl mDecl;
        private PType mType;        

        public PType Type
        {
            get { return mType; }
        }
	
        public Declaration(PDecl decl, PType type, string name)
        {
            mDecl = decl;
            mType = type;
            mName = name;
        }

        public Declaration(PDecl decl, PType type, long address, string name)
        {
            mDecl = decl;
            mType = type;
            mAddress = address;
            mName = name;
        }

        public PDecl Decl
        {
            get { return mDecl; }
        }
	
        public long Value
        {
            get { return mValue; }
            set { mValue = value; }
        }

        public long Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
	
    }
}
