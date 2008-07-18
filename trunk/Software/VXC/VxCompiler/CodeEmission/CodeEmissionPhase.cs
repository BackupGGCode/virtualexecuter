using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VXC.analysis;
using VXC.node;
using VxCompiler.Environment;

namespace VxCompiler.CodeEmission
{
    public class CodeEmissionPhase : DepthFirstAdapter
    {
        private AssemblerFile mOutputFile;

        public override void CaseASourceFile(ASourceFile node)
        {
            mOutputFile = new AssemblerFile();
 	        base.CaseASourceFile(node);
        }

        public override void CaseAVariableDefinition(AVariableDefinition node)
        {
            // create the variable in the data segment
            DataSegmentElement elm = new DataSegmentElement();
            elm.Identifier = node.GetName().Text;
            elm.Type = TypeEnvironment.GetVxcTypeOf(node.GetTypeSpecifier().GetType());
            mOutputFile.mDataSegment.Add(elm);

            // create initializer expression in code segment.
            PExpression pexp = node.GetInit();
            if (pexp != null)
            {
                long value = TypeEnvironment.GetValueOf(pexp);
                VariableInitializerExpression exp = new VariableInitializerExpression(value, elm.Type, elm.Identifier);
                mOutputFile.mCodeSegment.Add(exp);
            }
            
            base.CaseAVariableDefinition(node);
        }

        public void Emit(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.Write(mOutputFile.Emit().ToString());
            sw.Flush();
            sw.Close();            
        }
    }
}
