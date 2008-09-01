using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;

namespace VxCompiler.CodeEmission.Instructions
{
    public class MalformedInstruction : Exception
    {
        public MalformedInstruction(string msg) : base(msg) { }
    }

    public class Instruction
    {
        public Instruction() {}

        public virtual int StackChange
        {
            get { return 0; }
        }

        public virtual int LocalAccess
        {
            get { return -1; }
        }

        public virtual string ToASM() 
        {
            return "";
        }       
    }
}
