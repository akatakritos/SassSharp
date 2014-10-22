using NUnit.Framework;
using SassSharp.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Tests
{
    [TestFixture]
    public class TokenTests
    {
        [Test]
        public void TestConsidersLineNumberInComparisonTest()
        {
            var t1 = new Token(TokenType.Identifier, "string", 10, 5);
            var t2 = new Token(TokenType.Identifier, "string", 9, 5);

            Assert.AreNotEqual(t1, t2);
        }

        [Test]
        public void TestDifferentTokensGenerateDifferentHashCodes()
        {
            var t1 = new Token(TokenType.Identifier, "string", 10, 5);
            var t2 = new Token(TokenType.Identifier, "string", 9, 5);

            Assert.AreNotEqual(t1.GetHashCode(), t2.GetHashCode());
        }

        [Test]
        public void TestDefaultTokenHasDefaultLineAndColumn()
        {
            var def = new Token();

            Assert.That(def.Line, Is.EqualTo(default(int)));
            Assert.That(def.Column, Is.EqualTo(default(int)));
        }
    }
}
