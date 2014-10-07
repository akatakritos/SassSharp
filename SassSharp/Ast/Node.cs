using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public class Node
    {
        public Selector Selector { get; private set; }
        public DeclarationSet Declarations { get; private set; }
        public IEnumerable<Node> Children { get; private set; }

        public static Node Create(string selector, DeclarationSet declaraations, params Node[] children)
        {
            return new Node
            {
                Selector = new Selector(selector),
                Declarations = declaraations,
                Children = children
            };
        }

        public static Node Create(string selector, DeclarationSet declaraations)
        {
            return new Node
            {
                Selector = new Selector(selector),
                Declarations = declaraations,
                Children = new Node[] { }
            };
        }
    }
}
