using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SassSharp.Tokens;

namespace SassSharp.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        [Test]
        public void TestTokenizerStreamReturnsTokens()
        {
            var tokenizer = new Tokenizer();

            var tokens = tokenizer.Tokenize(@"
                p {
                    font-size: 12px;

                    a {
                        color: red;
                    }
                }").ToArray();

            var expected = new Token[] {
                new Token(TokenType.Identifier, "p"),
                new Token(TokenType.OpenBrace, "{"),
                new Token(TokenType.Identifier, "font-size"),
                new Token(TokenType.Colon, ":"),
                new Token(TokenType.Identifier, "12px"),
                new Token(TokenType.SemiColon, ";"),
                new Token(TokenType.Identifier, "a"),
                new Token(TokenType.OpenBrace, "{"),
                new Token(TokenType.Identifier, "color"),
                new Token(TokenType.Colon, ":"),
                new Token(TokenType.Identifier, "red"),
                new Token(TokenType.SemiColon, ";"),
                new Token(TokenType.CloseBrace, "}"),
                new Token(TokenType.CloseBrace, "}")
            };

            //for (var i = 0; i < expected.Length; i++ )
            //{
            //    var ex = expected[i];
            //    var ac = tokens[i];
            //    Console.WriteLine("{0}: {1}   => {2}: {3}", ex.Type, ex.Value, ac.Type, ac.Value);
            //}

            Assert.That(tokens, Is.EqualTo(expected));
        }
    }
}
