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
    public class SassSyntaxTreeDumperTests
    {
        [Test]
        public void TestDumpsTheTree()
        {
            var ast = createTree();
            var expected = createExpected();

            var s = SassSyntaxTreeDumper.Dump(ast);

            Assert.That(s, Is.EqualTo(expected));
        }

        public SyntaxTree createTree()
        {
            return new FluentAstBuilder2().SassNode("p", p =>
            {
                p.Declaration("color", "red");
                p.Declaration("width", "100px");

                p.SassNode("a", a =>
                {
                    a.Declaration("font-weight", "bold");
                });
            }).ToTree();
        }

        public string createExpected()
        {
            StringBuilder sb = new StringBuilder();
            sb
            .AppendLine("Root")
            .AppendLine("`-- Sass")
            .AppendLine("    |-- Selector: 'p'")
            .AppendLine("    `-- Container")
            .AppendLine("        |-- Declaration")
            .AppendLine("        |   |-- Property: 'color'")
            .AppendLine("        |   `-- Value: 'red'")
            .AppendLine("        |-- Declaration")
            .AppendLine("        |   |-- Property: 'width'")
            .AppendLine("        |   `-- Value: '100px'")
            .AppendLine("        `-- Sass")
            .AppendLine("            |-- Selector: 'a'")
            .AppendLine("            `-- Container")
            .AppendLine("                `-- Declaration")
            .AppendLine("                    |-- Property: 'font-weight'")
            .AppendLine("                    `-- Value: 'bold'");

            return sb.ToString();
        }
    }
}
