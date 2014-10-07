using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class Declaration
    {
        public string Property { get; private set; }
        public string Value { get; private set; }

        public Declaration(string property, string value)
        {
            Property = property;
            Value = value;
        }
    }
}
