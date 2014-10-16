using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public class SyntaxTree
    {
        public RootNode Root { get; private set; }

        public SyntaxTree(RootNode root)
        {
            this.Root = root;
        }
    }
}
