using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Tokens
{
    public struct Token : IEquatable<Token>
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public Token(TokenType type, string value)
            : this()
        {
            this.Type = type;
            this.Value = value;
        }

        public bool Equals(Token other)
        {
            return this.Type == other.Type && this.Value == other.Value;
        }

        public override bool Equals(object other)
        {
            if (other is Token)
                return this.Equals((Token)other);
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Type.GetHashCode();
                hash = hash * 23 + Value.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("[Token Type={0}, Value='{1}']", Type, Value);
        }
    }

    public enum TokenType
    {
        Ignore,
        OpenBrace,
        Property,
        Colon,
        Identifier,
        SemiColon,
        CloseBrace,
        AtCommand,
    }
}
