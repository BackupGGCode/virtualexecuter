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

    public class PortDeclaration : CodeSegmentElement
    {
        private int mAddress;        
        private string mName;
        private long mValue;

        public PortDeclaration(int adress, string name, long value)
        {
            mAddress = adress;
            mName = name;
            mValue = value;
        }

        public string Generate()
        {
            StringBuilder template = new StringBuilder();
            // ports are 8 bit wide !
            mValue = mValue & 0xFF;
            template.AppendLine(Commands.GetCommand(Command.load, VxcType.usingle, mValue.ToString()));
            template.AppendLine(Commands.GetCommand(Command.load, VxcType.usingle, mAddress.ToString()));
            template.AppendLine(Commands.GetCommand(Command.@out));
            return template.ToString();
        }
    }
}
