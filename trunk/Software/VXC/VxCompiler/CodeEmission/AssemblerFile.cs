using System;
using System.Collections.Generic;
using System.Text;
using VxCompiler.Environment;

namespace VxCompiler.CodeEmission
{
    public class AssemblerFile
    {
        public List<CodeSegmentElement> mCodeSegment = new List<CodeSegmentElement>();
        public List<DataSegmentElement> mDataSegment = new List<DataSegmentElement>();
        public StringBuilder Emit()
        {
            StringBuilder sb = new StringBuilder();
            // Do liveness analysis=?
            sb.AppendLine(".stack");
            sb.AppendLine("stack: 255");
            sb.AppendLine();

            sb.AppendLine(".data");
            foreach (DataSegmentElement data in mDataSegment)
            {
                sb.AppendLine(data.Identifier + ": " + TypeEnvironment.GetSizeOf(data.Type));
            }
            sb.AppendLine();
            sb.AppendLine(".code");
            foreach (CodeSegmentElement code in mCodeSegment)
            {
                sb.Append(code.Generate());
            }
            sb.AppendLine();
            sb.AppendLine("exit");
            return sb;
        }        
    }
}
