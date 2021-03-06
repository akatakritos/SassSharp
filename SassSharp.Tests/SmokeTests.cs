﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Tests
{
    [TestFixture]
    public class SmokeTests
    {
        [Test]
        public void TestRendersSimpleSass()
        {
            var sass = @"
                    p {
                        font-size:12px;
                        color: red;

                        a {
                            font-weight : bold;
                        }
                    }";

            var css = new SassCompiler().Compile(sass);

            Assert.That(css, Is.EqualTo("p{font-size:12px;color:red;}p a{font-weight:bold;}"));
        }

        [Test]
        public void SmokeTestDeepNesting()
        {
            var sass = @"
                body {
                    article {
                        p {
                            a {
                                color: blue;
                            }
                        }
                    }
                }";

            var css = new SassCompiler().Compile(sass);
            Assert.That(css, Is.EqualTo("body article p a{color:blue;}"));
        }
    }
}
