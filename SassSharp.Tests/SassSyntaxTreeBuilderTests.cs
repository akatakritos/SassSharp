using NUnit.Framework;
using SassSharp.Ast;
using SassSharp.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Tests
{
    [TestFixture]
    public class SassSyntaxTreeBuilderTests
    {
        [Test]
        public void TestBuildsSimpleTree()
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

            var builder = new SassSyntaxTreeBuilder(tokenStream);
            var root = builder.Build().Root.Children.First();

            Assert.That(root, Is.TypeOf<SassNode>());

            var sassNode = (SassNode)root;
            Assert.That(sassNode.Selector.Selector.Value, Is.EqualTo("p"));

            var declaration = (DeclarationNode)sassNode.Container.Children.First();
            Assert.That(declaration.Property.Name, Is.EqualTo("color"));
            Assert.That(declaration.Value.Value, Is.EqualTo("red"));
        }

        [Test]
        public void TestBuildsNestedTree()
        {
            var tokenStream = new Token[]
            {
                new Token(TokenType.Identifier, "p"),
                new Token(TokenType.OpenBrace, "{"),
                new Token(TokenType.Identifier, "color"),
                new Token(TokenType.Colon, ":"),
                new Token(TokenType.Identifier, "red"),
                new Token(TokenType.SemiColon, ";"),
                    new Token(TokenType.Identifier, "a"),
                    new Token(TokenType.OpenBrace, "{"),
                    new Token(TokenType.Identifier, "font-weight"),
                    new Token(TokenType.Colon, ":"),
                    new Token(TokenType.Identifier, "bold"),
                    new Token(TokenType.SemiColon, ";"),
                    new Token(TokenType.CloseBrace, "}"),
                new Token(TokenType.CloseBrace, "}")
            };

            var builder = new SassSyntaxTreeBuilder(tokenStream);
            var root = builder.Build().Root.Children.First();

            Assert.That(root, Is.TypeOf<SassNode>());

            var sassNode = (SassNode)root;
            Assert.That(sassNode.Selector.Selector.Value, Is.EqualTo("p"));

            var declaration = (DeclarationNode)sassNode.Container.Children.First();
            Assert.That(declaration.Property.Name, Is.EqualTo("color"));
            Assert.That(declaration.Value.Value, Is.EqualTo("red"));

            var nested = (SassNode)sassNode.Container.Children.ElementAt(1);
            Assert.That(nested.Selector.Selector.Value, Is.EqualTo("a"));
            declaration = (DeclarationNode)nested.Container.Children.First();
            Assert.That(declaration.Property.Name, Is.EqualTo("font-weight"));
            Assert.That(declaration.Value.Value, Is.EqualTo("bold"));
        }
    }
}
