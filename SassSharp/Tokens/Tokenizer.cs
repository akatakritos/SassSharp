using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SassSharp.Tokens
{
    public class Tokenizer
    {
        public IEnumerable<Token> Tokenize(IEnumerable<char> input)
        {
            return tokenize(input);
        }

        private IEnumerable<Token> tokenize(IEnumerable<char> input)
        {
            IState currentState = new WhitespaceState();
            StringBuilder buffer = new StringBuilder(25);

            foreach(char c in input)
            {
                IState nextState = null;

                if ((nextState = currentState.GetNext(c)) != null)
                {
                    var type = currentState.GetTokenType();
                    var value = readTokenFromBuffer(buffer);

                    //Console.WriteLine("{0} : '{1}'", type, value);

                    if (type != TokenType.Ignore)
                    {
                        yield return new Token(type, value);
                    }

                    currentState = nextState;
                }

                buffer.Append(c);
            }

            var type1 = currentState.GetTokenType();
            var value1 = readTokenFromBuffer(buffer);
            yield return new Token(type1, value1);

        }

        static string readTokenFromBuffer(StringBuilder buffer)
        {
            var token = buffer.ToString();
            buffer.Clear();
            return token;
        }
    }

    interface IState
    {
        IState GetNext(char c);
        TokenType GetTokenType();
    }

    class WhitespaceState : IState
    {
        public IState GetNext(char c)
        {
            if (char.IsWhiteSpace(c))
                return null;

            if (c == '{')
                return new OpenBraceState();

            if (c == '}')
                return new CloseBraceState();

            if (c == ':')
                return new ColonState();

            if (IdentifierState.IsIdentifierChar(c))
                return new IdentifierState();

            throw new TokenizerException(c, "Whitespace");
        }

        public TokenType GetTokenType()
        {
            return TokenType.Ignore;
        }
    }

    class IdentifierState : IState
    {
        public static bool IsIdentifierChar(char c)
        {
            return char.IsLetterOrDigit(c) || c == '-' || c == '_';
        }

        public IState GetNext(char c)
        {
            if (IdentifierState.IsIdentifierChar(c))
                return null;

            if (char.IsWhiteSpace(c))
                return new WhitespaceState();

            if (c == '{')
                return new OpenBraceState();

            if (c == ':')
                return new ColonState();

            if (c == ';')
                return new SemiColonState();

            throw new TokenizerException(c, "Identifier");
        }

        public TokenType GetTokenType()
        {
            return TokenType.Identifier;
        }
    }

    class OpenBraceState : IState
    {

        public IState GetNext(char c)
        {
            if (char.IsWhiteSpace(c))
                return new WhitespaceState();

            if (IdentifierState.IsIdentifierChar(c))
                return new IdentifierState();

            throw new TokenizerException(c, "OpenBrace");

        }

        public TokenType GetTokenType()
        {
            return TokenType.OpenBrace;
        }
    }

    class ColonState : IState
    {

        public IState GetNext(char c)
        {
            if (char.IsWhiteSpace(c))
                return new WhitespaceState();

            if (IdentifierState.IsIdentifierChar(c))
                return new IdentifierState();

            throw new TokenizerException(c, "Colon");
        }

        public TokenType GetTokenType()
        {
            return TokenType.Colon;
        }
    }

    class SemiColonState : IState
    {

        public IState GetNext(char c)
        {
            if (char.IsWhiteSpace(c))
                return new WhitespaceState();

            if (IdentifierState.IsIdentifierChar(c))
                return new IdentifierState();

            throw new TokenizerException(c, "SemiColon");
        }

        public TokenType GetTokenType()
        {
            return TokenType.SemiColon;
        }
    }

    class CloseBraceState : IState
    {

        public IState GetNext(char c)
        {
            if (char.IsWhiteSpace(c))
                return new WhitespaceState();

            if (IdentifierState.IsIdentifierChar(c))
                return new IdentifierState();

            throw new TokenizerException(c, "CloseBrace");
        }

        public TokenType GetTokenType()
        {
            return TokenType.CloseBrace;
        }
    }
}
