using SassSharp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class CssRenderer
    {
        public CssRenderer()
        {

        }

        public string Render(SassSyntaxTree ast)
        {
            StringBuilder sb = new StringBuilder();
            CssRuleEmitter t = new CssRuleEmitter();

            foreach(var rule in t.EmitRules(ast))
            {
                sb.Append(rule.Selector);
                sb.Append('{');
                
                foreach(var declaration in rule.Declarations)
                {
                    sb.Append(declaration.Property);
                    sb.Append(':');
                    sb.Append(declaration.Value);
                    sb.Append(';');
                }
                sb.Append('}');
            }
            return sb.ToString();
        }


    }
}
