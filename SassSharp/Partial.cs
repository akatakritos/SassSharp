using SassSharp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class Partial
    {
        public string Name { get; private set; }
        public SyntaxTree Ast { get; private set; }

        public Partial(string name, SyntaxTree ast)
        {
            Name = name;
            Ast = ast;
        }
    }
}
