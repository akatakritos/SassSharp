using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class Rule
    {
        public Selector Selector { get; private set; }
        public DeclarationSet Declarations { get; private set; }

        public Rule(Selector selector, DeclarationSet declarations)
        {
            Selector = selector;
            Declarations = declarations;
        }
    }
}
