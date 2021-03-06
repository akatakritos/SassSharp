﻿using SassSharp.Ast;
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
            var builder = new SyntaxTreeBuilder(tokenizer.Tokenize(sass));
            var compiler = new AstCompiler();
            var render = new CssRenderer();

            var ast = builder.Build();

            return render.Render(compiler.Compile(ast));
        }
    }
}
