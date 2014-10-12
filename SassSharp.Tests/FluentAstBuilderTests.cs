using NUnit.Framework;
using SassSharp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Tests
{
    [TestFixture]
    public class FluentAstBuilderTests
    {
        [Test]
        public void TestInteface()
        {
            var ast = new FluentAstBuilder().Node("p", n => {
                n.Declaration("color", "red");
                n.Declaration("height", "12px");

                n.Child("a", c => {
                    c.Declaration("font-weight", "bold");
                });
            }).Build();


            Assert.That(ast.Children.Count(), Is.EqualTo(1));

            var node = ast.Children.First();
            Assert.That(node.Selector.Value, Is.EqualTo("p"));

            var declarations = node.Declarations.ToArray();
            Assert.That(declarations.Length, Is.EqualTo(2));
            Assert.That(declarations[0], Is.EqualTo(new Declaration("color", "red")));
            Assert.That(declarations[1], Is.EqualTo(new Declaration("height", "12px")));

            Assert.That(node.Children.Count(), Is.EqualTo(1));

            node = node.Children.First();
            Assert.That(node.Selector.Value, Is.EqualTo("a"));
            Assert.That(node.Children.Count(), Is.EqualTo(0));

            declarations = node.Declarations.ToArray();
            Assert.That(declarations.Length, Is.EqualTo(1));
            Assert.That(declarations[0], Is.EqualTo(new Declaration("font-weight", "bold")));

        }
    }

    

    
}
