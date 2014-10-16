using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public class SassSyntaxTree2
    {
        public RootNode Root { get; private set; }

        public SassSyntaxTree2(RootNode root)
        {
            this.Root = root;
        }
    }
}
