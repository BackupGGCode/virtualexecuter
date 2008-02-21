using System.Collections.Generic;
using System.Windows.Forms;
using VXC.analysis;
using VXC.node;

namespace ParserTest
{
    class ASTDisplay : DepthFirstAdapter
    {
        public TreeNode result = null;
        bool isDone = false;

        readonly List<TreeNode> parents = new List<TreeNode>();    

        public override void OutStart(Start node)
        {
            result = parents[parents.Count - 1];
            isDone = true;
            base.OutStart(node);
        }

        public override void DefaultIn(Node node)
        {
            string type = node.GetType().ToString();
            type = type.Remove(0, type.LastIndexOf('.')+1);

            TreeNode thisNode = new TreeNode(type);
            parents.Add(thisNode);
            base.DefaultIn(node);
        }

        public override void DefaultOut(Node node)
        {
            if (!isDone)
            {
                TreeNode thisNode = parents[parents.Count - 1];
                parents.RemoveAt(parents.Count - 1);
                TreeNode parent = parents[parents.Count - 1];
                parent.Nodes.Add(thisNode);
            }
            base.DefaultOut(node);
        }

        public override void CaseEOF(EOF node)
        {
        }

        public override void DefaultCase(Node node)
        {           
            TreeNode thisNode = new TreeNode(((Token)node).Text);
            TreeNode parent = parents[parents.Count - 1];
            parent.Nodes.Add(thisNode);
            base.DefaultCase(node);
        }
    }
}
