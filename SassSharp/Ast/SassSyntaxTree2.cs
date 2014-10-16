using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public class SassSyntaxTree2
    {
        private IEnumerable<RuleNode> enumerable;

        public RootNode Root { get; private set; }

        public SassSyntaxTree2(RootNode root)
        {
            this.Root = root;
        }

        public SassSyntaxTree2(IEnumerable<RuleNode> enumerable)
        {
            // TODO: Complete member initialization
            this.enumerable = enumerable;
        }
    }
}
