using System;
using System.Collections.Generic;
using System.Text;

namespace VxCompiler.CodeEmission.Instructions
{
    public class Label : Instruction
    {
        private string mName;

        public Label(string name)
        {
            mName = name;
        }

        public override string ToASM()
        {
            return mName + ":";
        }
    }
}
