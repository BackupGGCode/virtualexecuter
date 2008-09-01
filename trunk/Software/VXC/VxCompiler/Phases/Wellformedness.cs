using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;
using VXC;
using VXC.analysis;

namespace VxCompiler.Phases
{
    public class Wellformedness : DepthFirstAdapter
    {
        public override void OutAVarDecl(AVarDecl node)
        {
            if (node.GetInit() != null)
            {
                // initializer must be an assigment. Handled by grammar
                
            }
            base.OutAVarDecl(node);
        }
    }
}
