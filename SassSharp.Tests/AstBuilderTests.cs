using System;
using NUnit.Framework;
using SassSharp;
using SassSharp.Tokens;
using SassSharp.Ast;
using System.Linq;

namespace SassSharp.Tests
{
    [TestFixture]
    public class AstBuilderTests
    {
        [Test]
        public void CreatesNodeWithSimpleDeclaration()
        {
            var tokenStream = new Token[]
            {
                new Token(TokenType.Identifier, "p"),
                new Token(TokenType.OpenBrace, "{"),
                new Token(TokenType.Identifier, "color"),
                new Token(TokenType.Colon, ":"),
                new Token(TokenType.Identifier, "red"),
                new Token(TokenType.SemiColon, ";"),
                new Token(TokenType.CloseBrace, "}")
            };

            var builder = new AstBuilder(tokenStream);
            var ast = builder.Build().Children.First();

            Assert.That(ast.Selector, Is.EqualTo(new Selector("p")));
            Assert.That(ast.Declarations.Count(), Is.EqualTo(1));
            Assert.That(ast.Declarations.First(), Is.EqualTo(new Declaration("color", "red")));
            Assert.That(ast.Children.Count(), Is.EqualTo(0));
        }

    }
}

