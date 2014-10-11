using System;
using System.Collections.Generic;
using SassSharp.Tokens;
using SassSharp;

namespace SassSharp.Ast
{
    public class NodeBuilder
    {
        private List<Declaration> declarations;
        private List<Node> children;
        private string selector;

        public NodeBuilder(string selector)
        {
            this.selector = selector;
            this.declarations = new List<Declaration>();
            this.children = new List<Node>();
        }

        public void AddDeclaration(Declaration item)
        {
            declarations.Add(item);
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }

        public Node ToNode()
        {
            var node = Node.Create(this.selector,
                           DeclarationSet.FromList(this.declarations),
                           this.children);
            return node;
        }
    }
}

