using System;
using SassSharp;
using SassSharp.Ast;
using System.Collections.Generic;
using System.Linq;


namespace SassSharp.Ast
{
    public class FluentAstBuilder2
    {
        private List<Node> children = new List<Node>();
        public FluentAstBuilder2()
        {
        }

        public FluentAstBuilder2 SassNode(string selector, Action<FluentSassNodeBuilder> build)
        {
            var f = new FluentSassNodeBuilder(selector);
            build(f);
            children.Add(f.ToNode());
            return this;
        }

        public SassSyntaxTree2 Build()
        {
            return new SassSyntaxTree2(new RootNode(children));
        }
    }

    public class FluentSassNodeBuilder
    {
        private string selector;
        private List<Node> children;
        public FluentSassNodeBuilder(string selector)
        {
            this.selector = selector;
            this.children = new List<Node>();
        }

        public SassNode ToNode()
        {
            return new SassNode(new SelectorNode(selector), new SassContainerNode(children));
        }

        public FluentSassNodeBuilder Declaration(string property, string value)
        {
            children.Add(new DeclarationNode(property, value));
            return this;
        }

        public FluentSassNodeBuilder SassNode(string selector, Action<FluentSassNodeBuilder> build)
        {
            var f = new FluentSassNodeBuilder(selector);
            build(f);
            children.Add(f.ToNode());
            return this;
        }

    }
}

