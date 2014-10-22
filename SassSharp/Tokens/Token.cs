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
        public int Line { get; private set; }
        public int Column { get; private set; }


        public Token(TokenType type, string value)
            : this(type, value, default(int), default(int))
        {
            this.Type = type;
            this.Value = value;
        }

        public Token(TokenType type, string value, int line, int col)
            : this()
        {
            this.Type = type;
            this.Value = value;
            this.Line = line;
            this.Column = col;
        }

        public bool Equals(Token other)
        {
            return this.Type == other.Type &&
                this.Value == other.Value &&
                this.Line == other.Line &&
                this.Column == other.Column;
        }

        public override bool Equals(object other)
        {
            if (other is Token)
                return this.Equals((Token)other);
            return false;
        }

        public override int GetHashCode()
        {
            return HashHelper.Hash(Type, Value, Line, Column);
        }

        public override string ToString()
        {
            return string.Format("[Token Type={0}, Value='{1}', Line='{2}', Column='{3}']",
                Type, Value, Line, Column);
        }
    }

}
