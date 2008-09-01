using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;

namespace VxCompiler.CodeEmission.Instructions
{
    public class Push : TypedInstruction
    {             
        private string mLocal;

        public Push(string local, PType type)
        {
            mLocal = local;
            mType = type;
        }

        public override int StackChange
        {
            get
            {
                return -GetSize();
            }
        }
       
        public override string ToASM()
        {            
            return "push" + GetPostFix() + " " + mLocal;
        }
    }
}
