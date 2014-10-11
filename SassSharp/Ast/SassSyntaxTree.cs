using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public class SassSyntaxTree
    {

        public SassSyntaxTree(IEnumerable<Node> children)
        {
            this.Children = children;
        }
        public IEnumerable<Node> Children { get; private set; }
    }
}
