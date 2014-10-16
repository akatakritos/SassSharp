using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SassSharp.Ast;

namespace SassSharp.Tests
{
    [TestFixture]
    public class CssRendererTests
    {
        private IEnumerable<Rule> sampleRules()
        {
            yield return new Rule(new Selector("p"), DeclarationSet.Create(
                new Declaration("color", "red"),
                new Declaration("font-weight", "bold")));

            yield return new Rule(new Selector("p a"), DeclarationSet.Create(
                new Declaration("font-size", "12px")));
        }

        [Test]
        public void TestCompilesSimpleNodeStructureToCss()
        {
            var renderer = new CssRenderer();

            var css = renderer.Render(sampleRules());

            Assert.That(css, Is.EqualTo("p{color:red;font-weight:bold;}p a{font-size:12px;}"));
        }
    }
}
