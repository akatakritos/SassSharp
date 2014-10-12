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

        [Test]
        public void TestAmpersandGetsParentSelector()
        {
            var t = new CssRuleEmitter();

            var ast = new FluentAstBuilder()
                .Node("p", n =>
                {
                    n.Declaration("color", "red");

                    n.Child("&.blue", c =>
                    {
                        c.Declaration("color", "blue");
                    });

                }).Build();

            var css = t.EmitRules(ast).ToArray();
            Assert.That(css.Length, Is.EqualTo(2));
            Assert.That(css[0].Selector.Value, Is.EqualTo("p"));
            Assert.That(css[1].Selector.Value, Is.EqualTo("p.blue"));
        }

        [Test]
        public void EmptyRulesArentEmitted()
        {
            var ast = new FluentAstBuilder().Node("p", p =>
            {

            }).Build();

            var css = new CssRuleEmitter().EmitRules(ast).ToArray();

            Assert.That(css.Length, Is.EqualTo(0));
        }

        [Test]
        public void TestMultipleLevelsOfNestingCreatesCorrectSelector()
        {
            var ast = new FluentAstBuilder()
            .Node("article", a =>
            {
                a.Child("p", p =>
                {
                    p.Child("span", s =>
                    {
                        s.Declaration("font-weight", "bold");
                    });
                });
            }).Build();

            var css = new CssRuleEmitter().EmitRules(ast).ToArray();


            Assert.That(css.Length, Is.EqualTo(1));
            Assert.That(css[0].Selector.Value, Is.EqualTo("article p span"));
            Assert.That(css[0].Declarations.Single(), Is.EqualTo(new Declaration("font-weight", "bold")));
        }
    }
}
