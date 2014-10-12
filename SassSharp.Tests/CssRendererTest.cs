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
    public class CssRendererTest
    {
        public SassSyntaxTree SampleSassTree
        {
            get
            {
                return new FluentAstBuilder()
                    .Node("p", n =>
                    {
                        n.Declaration("color", "red");
                        n.Declaration("font-weight", "bold");

                        n.Child("a", c =>
                        {
                            c.Declaration("font-size", "12px");
                        });
                    }).Build();

            }
        }

        [Test]
        public void TestCompilesSimpleNodeStructureToCss()
        {
            var renderer = new CssRenderer();

            var css = renderer.Render(SampleSassTree);

            Assert.That(css, Is.EqualTo("p{color:red;font-weight:bold;}p a{font-size:12px;}"));
        }
    }
}
