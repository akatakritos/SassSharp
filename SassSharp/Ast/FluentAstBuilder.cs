using System;
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
            var builder = new FluentNodeBuilder();
            nodeBuilder(builder);

            nodes.Add(Ast.Node.Create(selector, DeclarationSet.FromList(builder.Declarations), builder.Children));

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

            public FluentNodeBuilder()
            {
                declarations = new List<Declaration>();
                children = new List<Node>();
            }

            public IList<Declaration> Declarations
            {
                get { return declarations; }
            }

            public IEnumerable<Node> Children
            {
                get { return children; }
            }

            public void Declaration(string property, string value)
            {
                declarations.Add(new Declaration(property, value));
            }

            public void Child(string selector, Action<FluentNodeBuilder> nodeBuilder)
            {
                var builder = new FluentNodeBuilder();
                nodeBuilder(builder);

                children.Add(Ast.Node.Create(selector, DeclarationSet.FromList(builder.Declarations), builder.Children));
            }
        }
    }
}
