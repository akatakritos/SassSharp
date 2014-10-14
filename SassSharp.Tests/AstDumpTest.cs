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

            // Use this rather than string literal because git checks out
            // with \n instead of \r\n in ci
            string expectedTree = new StringBuilder()
                .AppendLine("AST")
                .AppendLine("|-- Rule: 'p'")
                .AppendLine("|   |-- Declarations")
                .AppendLine("|   |   |-- color: red")
                .AppendLine("|   |   `-- font-size: 12px")
                .AppendLine("|   `-- Children")
                .AppendLine("|       `-- Rule: 'a'")
                .AppendLine("|           |-- Declarations")
                .AppendLine("|           |   `-- font-weight: bold")
                .AppendLine("|           `-- Children")
                .AppendLine("`-- Rule: 'h1'")
                .AppendLine("    |-- Declarations")
                .AppendLine("    |   `-- font-size: 24px")
                .AppendLine("    `-- Children").ToString();

            Assert.That(tree, Is.EqualTo(expectedTree));
        }
    }
}
