using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;
using VxCompiler.Environment;

namespace VxCompiler.CodeEmission.Instructions
{
    public class TypedInstruction : Instruction
    {
        protected PType mType;

        public PType Type
        {
            get { return mType; }
            set { mType = value; }
        }

        public int GetSize()
        {
            return TypeEnvironment.SizeOfType(mType);
        }

        protected virtual char GetPostFix()
        {
            char postFix;
            if (mType.GetType() == typeof(ASingleType))
            {
                postFix = 's';
            }
            else if (mType.GetType() == typeof(ADoubleType))
            {
                postFix = 'd';
            }
            else if (mType.GetType() == typeof(AQuadType))
            {
                postFix = 'q';
            }
            else
            {
                throw new MalformedInstruction("No such type");
            }
            return postFix;
        }
    }
}
