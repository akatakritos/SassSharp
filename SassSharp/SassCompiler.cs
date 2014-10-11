using SassSharp.Ast;
using SassSharp.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class SassCompiler
    {
        public string Compile(string sass)
        {
            var tokenizer = new Tokenizer();
            var builder = new AstBuilder(tokenizer.Process(sass));
            var render = new CssRenderer();

            return render.Render(builder.Build());
        }
    }
}
