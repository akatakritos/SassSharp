using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SassSharp.Ast;
using SassSharp.Visitors;

namespace SassSharp
{
    public class AstCompiler
    {
        public IEnumerable<Rule> Compile(SyntaxTree ast)
        {
            var rules = new List<Rule>();

            var visitor = new CssVisitor(rule => {
                rules.Add(rule);
            });

            visitor.Visit(ast.Root, new CssVisitorParams());

            rules.Reverse(); //they are emitted in backwards order due to recursion
            return rules;
        }

        struct CssVisitorParams
        {
            public RuleBuilder RuleBuilder { get; set; }
            public Selector CurrentSelector { get; set; }
        }

        class RuleBuilder
        {
            private Selector selector;
            private List<Declaration> declarations = new List<Declaration>();

            public RuleBuilder(Selector selector)
            {
                this.selector = selector;
            }

            public void AddDeclaration(string property, string value)
            {
                declarations.Add(new Declaration(property, value));
            }

            public Rule ToRule()
            {
                return new Rule(selector, DeclarationSet.FromList(declarations));
            }
        }

        class CssVisitor : NopVisitor<string, CssVisitorParams>
        {
            private Action<Rule> onRuleEmitted;
            public CssVisitor(Action<Rule> onRuleEmitted)
            {
                this.onRuleEmitted = onRuleEmitted;
            }

            private void emit(Rule rule)
            {
                onRuleEmitted(rule);
            }

            public override string Visit(RootNode node, CssVisitorParams p)
            {
                foreach (var n in node.Children)
                    n.Accept(this, new CssVisitorParams());

                return null;
            }

            public override string Visit(SassNode node, CssVisitorParams p)
            {
                var selector = new Selector(node.Selector.Accept(this, p));
                node.Container.Accept(this, new CssVisitorParams
                {
                    CurrentSelector = selector.DescendFrom(p.CurrentSelector),
                    RuleBuilder = new RuleBuilder(selector.DescendFrom(p.CurrentSelector))
                });

                return null;
            }

            public override string Visit(SelectorNode node, CssVisitorParams p)
            {
                return node.Selector.Value;
            }

            public override string Visit(SassContainerNode node, CssVisitorParams p)
            {
                foreach(var n in node.Children)
                {
                    n.Accept(this, new CssVisitorParams
                    {
                        CurrentSelector = p.CurrentSelector,
                        RuleBuilder = p.RuleBuilder
                    });
                }

                var rule = p.RuleBuilder.ToRule();
                if(rule.Declarations.Any())
                    emit(rule);

                return null;
            }

            public override string Visit(DeclarationNode node, CssVisitorParams p)
            {
                var property = node.Property.Accept(this, p);
                var value = node.Value.Accept(this, p);

                p.RuleBuilder.AddDeclaration(property, value);

                return null;
            }

            public override string Visit(PropertyNode node, CssVisitorParams p)
            {
                return node.Name;
            }

            public override string Visit(ValueNode node, CssVisitorParams p)
            {
                return node.Value;
            }
        }
    }
}
