using System;
using SassSharp;
using SassSharp.Ast;
using NUnit.Framework;

namespace SassSharp.Tests
{
    [TestFixture]
    public class SassTreeDumperTests
    {
        public SassTreeDumperTests()
        {
            var tree = new FluentAstBuilder2().SassNode("p", n =>
                {
                    n.Declaration("font-weight", "bold");
                    n.Declaration("color", "red");

                    n.SassNode("a", a =>
                        {
                            a.Declaration("width", "120px");
                        });
                }).ToTree();

        }
    }
}

