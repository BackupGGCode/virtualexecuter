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

        public override void CaseAPortDefinition(APortDefinition node)
        {
            if (node.GetInit() != null)
            {
                CodeSegmentElement port = new PortDeclaration(Convert.ToInt32(node.GetAdress().Text), node.GetName().Text, TypeEnvironment.GetValueOf(node.GetInit()));
                mOutputFile.AddTemplate("init", port);
            }
            base.CaseAPortDefinition(node);
        }

        public override void CaseAVariableDefinition(AVariableDefinition node)
        {
            // create the variable in the data segment
            DataSegmentElement elm = new DataSegmentElement(node.GetName().Text, TypeEnvironment.GetVxcTypeOf(node.GetTypeSpecifier().GetType()));
            mOutputFile.mDataSegment.Add(elm);

            // create initializer expression in code segment.
            PExpression pexp = node.GetInit();
            if (pexp != null)
            {
                long value = TypeEnvironment.GetValueOf(pexp);
                VariableInitializerExpression exp = new VariableInitializerExpression(value, elm.Type, elm.Identifier);
                mOutputFile.AddTemplate("init", exp);
            }
            
            base.CaseAVariableDefinition(node);
        }

        public override void CaseAFunctionDefinition(AFunctionDefinition node)
        {
            CodeSegmentElement method = new MethodDeclaration(node.GetName().Text);
            mOutputFile.AddTemplate(node.GetName().Text, method);
            base.CaseAFunctionDefinition(node);
        }

        public void Emit(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.Write(mOutputFile.Emit());
            sw.Flush();
            sw.Close();            
        }
    }
}
