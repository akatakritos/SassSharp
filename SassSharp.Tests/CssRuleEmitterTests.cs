using System;
using NUnit.Framework;
using System.Collections.Generic;
using SassSharp;
using SassSharp.Ast;
using System.Linq;

namespace SassSharp.Tests
{
    [TestFixture]
    public class CssRuleEmitterTests
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
        public void TestCanTranspileSimpleData()
        {
            var t = new CssRuleEmitter();

            var results = t.EmitRules(TestData);

            Assert.That(results, Is.InstanceOf<IEnumerable<Rule>>());
        }

        [Test]
        public void TestTranspiledSelectorsAreFlattened()
        {
            var t = new CssRuleEmitter();

            var results = t.EmitRules(TestData).ToArray();

            Assert.That(results.Length, Is.EqualTo(2));
            Assert.That(results[0].Selector.Value, Is.EqualTo("p"));
            Assert.That(results[1].Selector.Value, Is.EqualTo("p a"));
        }

        [Test]
        public void TestTranspiledSelectorsRetainDeclarations()
        {
            var t = new CssRuleEmitter();

            var results = t.EmitRules(TestData).ToArray();

            var declarations = results[0].Declarations.ToArray();
            Assert.That(declarations.Length, Is.EqualTo(2));
            Assert.That(declarations[0], Is.EqualTo(new Declaration("color", "red")));
            Assert.That(declarations[1], Is.EqualTo(new Declaration("font-weight", "bold")));

            declarations = results[1].Declarations.ToArray();
            Assert.That(declarations.Length, Is.EqualTo(1));
            Assert.That(declarations[0], Is.EqualTo(new Declaration("font-size", "12px")));
        }
    }
}
