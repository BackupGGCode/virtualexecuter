using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;

namespace VxCompiler.Environment
{
    public class TypedValue
    {
        private long mValue;
        private PType mType;
         
        public TypedValue(PType type)
        {
            mType = type;
        }

        public TypedValue(PType type, long value)
        {
            mType = type;
            mValue = value;
        }

        public TypedValue(long value)
        {
            mValue = value;
        }

        public PType Type
        {
            get { return mType; }
            set { mType = value; }
        }

        public long Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
	
    }
}
