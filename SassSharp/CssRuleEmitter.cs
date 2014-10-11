using SassSharp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class CssRuleEmitter
    {
        public IEnumerable<Rule> EmitRules(SassSyntaxTree ast)
        {
            return emit(ast.Children);
        }

        private IEnumerable<Rule> emit(IEnumerable<Node> nodes)
        {
            return nodes.SelectMany((node) => emit(node));
        }

        private IEnumerable<Rule> emit(Node node)
        {
            yield return new Rule(node.Selector, node.Declarations);

            foreach (var child in node.Children)
            {
                foreach (var rule in emit(child))
                {
                    yield return new Rule(
                        rule.Selector.DescendFrom(node.Selector),
                        rule.Declarations);
                }
            }
        }
    }
}
