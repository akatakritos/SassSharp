using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public class SassSyntaxTree
    {

        public SassSyntaxTree(IEnumerable<RuleNode> children)
        {
            this.Children = children;
        }
        public IEnumerable<RuleNode> Children { get; private set; }

        public static SassSyntaxTree Create(params RuleNode[] nodes)
        {
            return new SassSyntaxTree(nodes);
        }
    }
}
