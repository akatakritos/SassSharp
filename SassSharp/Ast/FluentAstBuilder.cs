using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public class FluentAstBuilder
    {
        private IList<RuleNode> nodes;
        public FluentAstBuilder()
        {
            nodes = new List<RuleNode>();
        }

        public FluentAstBuilder Node(string selector, Action<FluentNodeBuilder> setup)
        {
            nodes.Add(FluentNodeBuilder.CreateNode(selector, setup));

            return this;
        }

        public SassSyntaxTree Build()
        {
            return new SassSyntaxTree(nodes);
        }

        public class FluentNodeBuilder
        {
            private IList<Declaration> declarations;
            private IList<RuleNode> children;

            public FluentNodeBuilder(IList<Declaration> declarations, IList<RuleNode> children)
            {
                this.declarations = declarations;
                this.children = children;
            }

            public void Declaration(string property, string value)
            {
                declarations.Add(new Declaration(property, value));
            }

            public void Child(string selector, Action<FluentNodeBuilder> setup)
            {
                children.Add(FluentNodeBuilder.CreateNode(selector, setup));
            }

            public static RuleNode CreateNode(string selector, Action<FluentNodeBuilder> setup)
            {
                var declarations = new List<Declaration>();
                var children = new List<RuleNode>();
                var builder = new FluentNodeBuilder(declarations, children);

                setup(builder);

                return Ast.RuleNode.Create(selector, DeclarationSet.FromList(declarations), children);
            }
        }
    }
}
