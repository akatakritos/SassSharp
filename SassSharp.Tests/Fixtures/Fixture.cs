using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Tests.Fixtures
{
    public static class Fixture
    {
        public static string PathFor(string filename)
        {
            return Path.Combine("../../Fixtures", filename);
        }
    }
}
