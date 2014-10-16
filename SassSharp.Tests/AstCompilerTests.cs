using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SassSharp.Ast;

namespace SassSharp.Tests
{
    [TestFixture]
    public class AstCompilerTests
    {
        private SassSyntaxTree2 simpleAst()
        {
            return new FluentAstBuilder2()
                .SassNode("a", a =>
                {
                    a.Declaration("color", "red");
                    a.Declaration("font-size", "12px");
                }).ToTree();
        }

        private SassSyntaxTree2 complexAst()
        {
            return new FluentAstBuilder2()
                .SassNode("div", d =>
                {
                    d.Declaration("font-size", "12px");
                    d.Declaration("color", "red");

                    d.SassNode("p", p =>
                    {
                        p.Declaration("margin", "0");

                        p.SassNode("a", a =>
                        {
                            a.Declaration("font-weight", "bold");
                        });
                    });

                    d.Declaration("height", "64em");
                }).ToTree();
        }

        [Test]
        public void TestCompilesSimpleAstDownToSingleRule()
        {
            var ast = simpleAst();
            var compiler = new AstCompiler();

            var results = compiler.Compile(ast).ToArray();

            Assert.That(results.Length, Is.EqualTo(1));
           
            
        }

        [Test]
        public void TestCompileSimpleAstToCorrectSelector()
        {
            var ast = simpleAst();
            var compiler = new AstCompiler();

            var results = compiler.Compile(ast).ToArray();

            Assert.That(results[0].Selector.Value, Is.EqualTo("a"));
        }

        [Test]
        public void TestCompileSimpleAstToCorrectDeclarations()
        {
            var ast = simpleAst();
            var compiler = new AstCompiler();

            var declarations = compiler.Compile(ast).First().Declarations.ToArray();

            Assert.That(declarations.Length, Is.EqualTo(2));
            Assert.That(declarations[0], Is.EqualTo(new Declaration("color", "red")));
            Assert.That(declarations[1], Is.EqualTo(new Declaration("font-size", "12px")));
        }

        [Test]
        public void TestCompilesComplexAstToThreeRules()
        {
            var ast = complexAst();
            var compiler = new AstCompiler();

            var rules = compiler.Compile(ast).ToArray();

            Assert.That(rules.Length, Is.EqualTo(3));
        }

        [Test]
        public void TestCompilesComplexAstToCorrectSelectors()
        {
            var ast = complexAst();
            var compiler = new AstCompiler();

            var rules = compiler.Compile(ast).ToArray();

            Assert.That(rules[0].Selector.Value, Is.EqualTo("div"));
            Assert.That(rules[1].Selector.Value, Is.EqualTo("div p"));
            Assert.That(rules[2].Selector.Value, Is.EqualTo("div p a"));
        }

        [Test]
        public void TestCompilesComplexAstToCorrectDeclarations()
        {
            var ast = complexAst();
            var compiler = new AstCompiler();

            var rules = compiler.Compile(ast).ToArray();
            var declarations0 = rules[0].Declarations.ToArray();
            var declarations1 = rules[1].Declarations.ToArray();
            var declarations2 = rules[2].Declarations.ToArray();

            Assert.That(declarations0[0], Is.EqualTo(new Declaration("font-size", "12px")));
            Assert.That(declarations0[1], Is.EqualTo(new Declaration("color", "red")));
            Assert.That(declarations0[2], Is.EqualTo(new Declaration("height", "64em")));

            Assert.That(declarations1[0], Is.EqualTo(new Declaration("margin", "0")));

            Assert.That(declarations2[0], Is.EqualTo(new Declaration("font-weight", "bold")));
        }

        [Test]
        public void TestSassNodeWithoutDeclarationsDoesntGetYielded()
        {
            var ast = new FluentAstBuilder2().SassNode("a", a =>
            {
            }).ToTree();

            var compiler = new AstCompiler();

            var results = compiler.Compile(ast);
            Assert.That(results.Count(), Is.EqualTo(0));
        }
    }
}
