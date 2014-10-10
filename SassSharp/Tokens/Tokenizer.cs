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
        public IEnumerable<Token> Process(string input)
        {
            return process(input);
        }

        private IEnumerable<Token> process(string input)
        {
            int start = 0;
            IState currentState = new WhitespaceState();

            for (var i = 0; i < input.Length; i++)
            {
                char c = input[i];
                IState nextState = null;

                if ((nextState = currentState.GetNext(c)) != null)
                {
                    var type = currentState.GetTokenType();
                    var value = input.Substring(start, i - start);

                    //Console.WriteLine("{0} : '{1}'", type, value);

                    if (type != TokenType.Ignore)
                    {
                        yield return new Token(type, value);
                    }

                    currentState = nextState;
                    start = i;
                }

            }

            var type1 = currentState.GetTokenType();
            var value1 = input.Substring(start, input.Length - start);
            yield return new Token(type1, value1);

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
