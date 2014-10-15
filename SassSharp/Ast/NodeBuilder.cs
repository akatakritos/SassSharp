using System;
using System.Collections.Generic;
using SassSharp.Tokens;
using SassSharp;

namespace SassSharp.Ast
{
    public class NodeBuilder
    {
        private List<Declaration> declarations;
        private List<RuleNode> children;
        private string selector;

        public NodeBuilder(string selector)
        {
            this.selector = selector;
            this.declarations = new List<Declaration>();
            this.children = new List<RuleNode>();
        }

        public void AddDeclaration(Declaration item)
        {
            declarations.Add(item);
        }

        public void AddChild(RuleNode child)
        {
            children.Add(child);
        }

        public RuleNode ToNode()
        {
            var node = RuleNode.Create(this.selector,
                           DeclarationSet.FromList(this.declarations),
                           this.children);
            return node;
        }
    }
}

