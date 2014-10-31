using NUnit.Framework;
using SassSharp.Ast;
using SassSharp.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SassSharp;

namespace SassSharp.Tests
{

    [TestFixture]
    public class CompilationContextTests
    {
        [Test]
        public void TestCanAddPartialsToCompilation()
        {
            var context = new CompilationContext();
            Partial p = new Partial("partial", new SyntaxTree(null));

            context.AddPartial(p);

            Assert.That(context.Partials["partial"], Is.EqualTo(p));
        }

        [Test]
        public void TestCantAddPartialWithSameNameTwice()
        {
            var context = new CompilationContext();
            var p = new Partial("partial", new SyntaxTree(null));
            var p2 = new Partial(p.Name, p.Ast); //same name

            context.AddPartial(p);

            Assert.Throws<InvalidOperationException>(() =>
            {
                context.AddPartial(p2);
            });

        }
    }
}
