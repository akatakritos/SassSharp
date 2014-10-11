using SassSharp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class Transpiler
    {
        public IEnumerable<Rule> Transpile(SassSyntaxTree ast)
        {
            return transpile(ast.Children);
        }

        private IEnumerable<Rule> transpile(IEnumerable<Node> nodes)
        {
            return nodes.SelectMany((node) => transpile(node));
        }

        private IEnumerable<Rule> transpile(Node node)
        {
            yield return new Rule(node.Selector, node.Declarations);

            foreach (var child in node.Children)
            {
                foreach (var rule in transpile(child))
                {
                    yield return new Rule(
                        rule.Selector.DescendFrom(node.Selector),
                        rule.Declarations);
                }
            }
        }
    }
}
