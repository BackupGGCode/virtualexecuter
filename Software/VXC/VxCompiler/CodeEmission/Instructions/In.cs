using System;
using System.Collections.Generic;
using System.Text;

namespace VxCompiler.CodeEmission.Instructions
{
    public class In : Instruction
    {
        public override int StackChange
        {
            get
            {
                return -2;
            }
        }

        public override string ToASM()
        {
            return "in";
        }
    }
}
