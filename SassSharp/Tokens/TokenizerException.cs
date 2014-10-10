using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SassSharp.Tokens
{
    public class TokenizerException : Exception
    {
        public TokenizerException(char c, string state)
            : base(string.Format("Invalid character '{0}' in state {1}", c, state))
        {
        }
    }
}
