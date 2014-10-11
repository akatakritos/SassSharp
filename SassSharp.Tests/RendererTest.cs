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
    public class RendererTest
    {
        public SassSyntaxTree TestData
        {
            get
            {
                var rootNode = Node.Create("p",
                    DeclarationSet.Create(
                        new Declaration("color", "red"),
                        new Declaration("font-weight", "bold")
                    ),
                    Node.Create("a",
                        DeclarationSet.Create(
                            new Declaration("font-size", "12px")
                        )
                    )
                );

                return new SassSyntaxTree(new Node[] { rootNode });
            }
        }

        [Test]
        public void TestCompilesSimpleNodeStructureToCss()
        {
            var renderer = new Renderer();

            var css = renderer.Render(TestData);

            Assert.That(css, Is.EqualTo("p{color:red;font-weight:bold;}p a{font-size:12px;}"));
        }
    }
}
