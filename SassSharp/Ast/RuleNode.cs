using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    


    public class RuleNode : Node
    {
        public Selector Selector { get; private set; }
        public DeclarationSet Declarations { get; private set; }
        public IEnumerable<RuleNode> Children { get; private set; }

        public static RuleNode Create(string selector, DeclarationSet declaraations, params RuleNode[] children)
        {
            return new RuleNode
            {
                Selector = new Selector(selector),
                Declarations = declaraations,
                Children = children
            };
        }

        public static RuleNode Create(string selector, DeclarationSet declaraations)
        {
            return new RuleNode
            {
                Selector = new Selector(selector),
                Declarations = declaraations,
                Children = new RuleNode[] { }
            };
        }

        public static RuleNode Create(string selector, DeclarationSet declarations, IEnumerable<RuleNode> children)
        {
            return new RuleNode
            {
                Selector = new Selector(selector),
                Declarations = declarations,
                Children = children
            };
        }

        //this is temporary, this whole class is going away
        public override R Accept<R, P>(INodeVisitor<R, P> visitor, P p)
        {
            return visitor.Visit(new RootNode(null), p);
        }
    }
}
