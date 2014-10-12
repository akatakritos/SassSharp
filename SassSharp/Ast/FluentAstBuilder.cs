﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public class FluentAstBuilder
    {
        private IList<Node> nodes;
        public FluentAstBuilder()
        {
            nodes = new List<Node>();
        }

        public FluentAstBuilder Node(string selector, Action<FluentNodeBuilder> nodeBuilder)
        {
            nodes.Add(FluentNodeBuilder.CreateNode(selector, nodeBuilder));

            return this;
        }

        public SassSyntaxTree Build()
        {
            return new SassSyntaxTree(nodes);
        }

        public class FluentNodeBuilder
        {
            private IList<Declaration> declarations;
            private IList<Node> children;

            public FluentNodeBuilder(IList<Declaration> declarations, IList<Node> children)
            {
                this.declarations = declarations;
                this.children = children;
            }

            public void Declaration(string property, string value)
            {
                declarations.Add(new Declaration(property, value));
            }

            public void Child(string selector, Action<FluentNodeBuilder> nodeBuilder)
            {
                children.Add(FluentNodeBuilder.CreateNode(selector, nodeBuilder));
            }

            public static Node CreateNode(string selector, Action<FluentNodeBuilder> nodeBuilder)
            {
                var declarations = new List<Declaration>();
                var children = new List<Node>();
                var builder = new FluentNodeBuilder(declarations, children);

                nodeBuilder(builder);

                return Ast.Node.Create(selector, DeclarationSet.FromList(declarations), children);
            }
        }
    }
}
