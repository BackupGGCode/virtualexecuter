using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;

namespace VxCompiler.CodeEmission.Instructions
{
    public class Load : TypedInstruction
    {       
        private long mValue;

        public Load(long value, PType type)
        {
            mValue = value;
            mType = type;
        }

        public override int StackChange
        {
            get
            {
                return GetSize();
            }
        }

        public override string ToASM()
        {            
            return "load" + GetPostFix() + " " + mValue;
        }
    }
}
