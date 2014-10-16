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

            var builder = new SassSyntaxTreeBuilder();
            var root = builder.Build(tokenStream).Root.Children.First();

            Assert.That(root, Is.TypeOf<SassNode>());

            var sassNode = (SassNode)root;
            Assert.That(sassNode.Selector.Selector.Value, Is.EqualTo("p"));

            var declaration = (DeclarationNode)sassNode.Container.Children.First();
            Assert.That(declaration.Property.Name, Is.EqualTo("color"));
            Assert.That(declaration.Value.Value, Is.EqualTo("red"));
        }
    }
}
