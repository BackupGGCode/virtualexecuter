using System;
using System.Collections.Generic;
using System.Text;
using VxCompiler.Environment;

namespace VxCompiler.CodeEmission
{
    public class AssemblerFile
    {
        //public List<CodeSegmentElement> mCodeSegment = new List<CodeSegmentElement>();
        private Dictionary<string, List<CodeSegmentElement>> mCodeSegment = new Dictionary<string, List<CodeSegmentElement>>();
        public List<DataSegmentElement> mDataSegment = new List<DataSegmentElement>();

        public void AddTemplate(string label, CodeSegmentElement elm)
        {
            if (!mCodeSegment.ContainsKey(label))
            {
                mCodeSegment.Add(label, new List<CodeSegmentElement>());
            }
            mCodeSegment[label].Add(elm);
        }

        public string Emit()
        {
            StringBuilder sb = new StringBuilder();

            StackSegment stack = new StackSegment();
            sb.AppendLine(stack.Generate());

            if (mDataSegment.Count > 0)
            {
                sb.AppendLine(DataSegmentElement.GetDataSegmentKeyword());
                foreach (DataSegmentElement data in mDataSegment)
                {
                    sb.AppendLine(data.Generate());
                }
            }
            sb.AppendLine(CodeSegmentElement.GetCodeSegmentKeyword());

            if (mCodeSegment.ContainsKey("init"))
            {
                sb.AppendLine("init:");
                foreach (CodeSegmentElement elm in mCodeSegment["init"])
                {
                    sb.AppendLine(elm.Generate());
                }
                mCodeSegment.Remove("init");
            }
            if (mCodeSegment.ContainsKey("main"))
            {
                sb.AppendLine("start:");
                sb.AppendLine(Commands.GetCommand(Command.call, "main"));
                sb.AppendLine("teminate:");
                sb.AppendLine(Commands.GetCommand(Command.exit));
                sb.AppendLine(Commands.GetCommand(Command.nop));
            }
            foreach (string label in mCodeSegment.Keys)
            {                
                foreach (CodeSegmentElement elm in mCodeSegment[label])
                {
                    sb.AppendLine(elm.Generate());
                }
            }          
            return sb.ToString();
        }
    }
}
