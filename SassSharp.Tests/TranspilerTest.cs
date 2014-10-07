using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SassSharp;
using SassSharp.Ast;
using System.Linq;

namespace SassSharp.Tests
{
    [TestClass]
    public class TranspilerTest
    {
        public Node TestData
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

                return rootNode;

            }
        }
        [TestMethod]
        public void TestCanTranspileSimpleData()
        {
            var t = new Transpiler();

            var results = t.Transpile(TestData);

            Assert.IsInstanceOfType(results, typeof(IEnumerable<Rule>));
        }

        [TestMethod]
        public void TestTranspiledSelectorsAreFlattened()
        {
            var t = new Transpiler();

            var results = t.Transpile(TestData).ToArray();

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual<string>("p", results[0].Selector);
            Assert.AreEqual<string>("p a", results[1].Selector);
        }

        [TestMethod]
        public void TestTranspiledSelectorsRetainDeclarations()
        {
            var t = new Transpiler();

            var results = t.Transpile(TestData).ToArray();

            var declarations = results[0].Declarations.ToArray();
            Assert.AreEqual(2, declarations.Length);
            Assert.AreEqual(new Declaration("color", "red"), declarations[0]);
            Assert.AreEqual(new Declaration("font-weight", "bold"), declarations[1]);

            declarations = results[1].Declarations.ToArray();
            Assert.AreEqual(1, declarations.Length);
            Assert.AreEqual(new Declaration("font-size", "12px"), declarations[0]);
        }
    }
}
