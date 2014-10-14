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
    public class AstDumpTest
    {
        [Test]
        public void TestDumpAnAsciiTree()
        {
            var ast = new FluentAstBuilder()
            .Node("p", p =>
            {
                p.Declaration("color", "red");
                p.Declaration("font-size", "12px");

                p.Child("a", a =>
                {
                    a.Declaration("font-weight", "bold");
                });
            })
            .Node("h1", h => {
                h.Declaration("font-size", "24px");
            }).Build();

            var tree = AstDump.Dump(ast);
            Assert.That(tree, Is.EqualTo(
@"AST
|-- Rule: 'p'
|   |-- Declarations
|   |   |-- color: red
|   |   `-- font-size: 12px
|   `-- Children
|       `-- Rule: 'a'
|           |-- Declarations
|           |   `-- font-weight: bold
|           `-- Children
`-- Rule: 'h1'
    |-- Declarations
    |   `-- font-size: 24px
    `-- Children
"));
        }
    }
}
