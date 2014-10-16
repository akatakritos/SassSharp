using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Tokens
{
    public enum TokenType
    {
        Ignore,
        OpenBrace,
        Property,
        Colon,
        Identifier,
        SemiColon,
        CloseBrace,
    }
}
