using System;
using System.Collections.Generic;
using System.Text;
using VxCompiler.Environment;

namespace VxCompiler.CodeEmission
{
    public interface CodeSegmentElement
    {
        string Generate();
    }

    public class VariableInitializerExpression : CodeSegmentElement
    {
        private long mValue;
        private VxcType mType;
        private string mName;

        public VariableInitializerExpression(long value, VxcType type, string name)
        {
            mValue = value;
            mType = type;
            mName = name;
        }

        public string Generate()
        {
            StringBuilder template = new StringBuilder();
            template.AppendLine(Commands.GetCommand(Command.load, mType, mValue.ToString()));
            template.AppendLine(Commands.GetCommand(Command.pop, mType, mName));
            return template.ToString();
        }
    }
}
