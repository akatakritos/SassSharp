using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SassSharp;
using SassSharp.Tests.Fixtures;

namespace SassSharp.Tests
{
    [TestFixture]
    public class FileEnumeratorTests
    {
        [Test]
        public void TestYieldsCharactersFromTheFile()
        {
            FileEnumerator f = new FileEnumerator(Fixture.PathFor("alphabet.txt"));

            var alphabet = f.GetChars().Take(8).ToArray();

            Assert.That(alphabet, Is.EqualTo(new char[]{
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'
            }));
        }

        [Test]
        public void TestYieldsNewLineCharactersAndDoesntThrowThemAway()
        {
            FileEnumerator f = new FileEnumerator(Fixture.PathFor("alphabet-vertical.txt"));

            var newlineCount = f.GetChars().Count(c => c == '\n');

            Assert.That(newlineCount, Is.EqualTo(26));
        }

        [Test]
        public void TestHandlesUTF8Files()
        {
            FileEnumerator f = new FileEnumerator(Fixture.PathFor("alphabet-utf8.txt"));

            var count = f.GetChars().Count();

            Assert.That(count, Is.EqualTo(26));
        }

    }

    
}
