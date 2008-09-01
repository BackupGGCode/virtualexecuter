using System;
using System.Collections.Generic;
using System.Text;
using VxCompiler.Environment;
using VxCompiler.CodeEmission.Instructions;

namespace VxCompiler.CodeEmission
{

    public class AssemblyFile
    {
        private int mStackSize = 255;
        private List<Instruction> mInstructions;
        private List<Declaration> mDeclarations;

        private StringBuilder mCode;
        private StringBuilder mStack;
        private StringBuilder mData;

        public AssemblyFile()
        {            
            mInstructions = new List<Instruction>();
            mDeclarations = new List<Declaration>();
            mStack = new StringBuilder();
            mData = new StringBuilder();
            mCode = new StringBuilder();
            mStack.AppendLine(".stack");
            mStack.AppendLine("stack: " + mStackSize.ToString());
            mData.AppendLine(".data");
        }

        public void Add(Instruction i)
        {
            mInstructions.Add(i);
        }

        public void Add(Declaration d)
        {
            mDeclarations.Add(d);
        }

        public string Emit()
        {
            
            foreach (Instruction i in mInstructions)
            {
                mCode.AppendLine(i.ToASM());
            }
            foreach (Declaration d in mDeclarations)
            {
                mData.AppendLine(d.Name + ": " + TypeEnvironment.SizeOfType(d.Type));
            }
            string stack = mStack.ToString();
            string data = mData.ToString();
            string code = mCode.ToString();
            return stack + data + code;
        }
    }
}
