using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;
using VXC;
using VXC.analysis;
using VxCompiler.Environment;

namespace VxCompiler.Phases
{
    public class Liveness: DepthFirstAdapter
    {
        public int stackSize = 0;

        public override void CaseAVarDecl(AVarDecl node)
        {
            base.CaseAVarDecl(node);
        }

        public override void CaseAFuncDecl(AFuncDecl node)
        {
            base.CaseAFuncDecl(node);
        }
    }
}
