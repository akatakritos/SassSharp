using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public struct Declaration : IEquatable<Declaration>
    {
        public string Property { get; private set; }
        public string Value { get; private set; }

        public Declaration(string property, string value)
            : this()
        {
            Property = property;
            Value = value;
        }

        public override string ToString()
        {
            return Property + ":" + Value;
        }

        public override int GetHashCode()
        {
            return HashHelper.Hash(Property, Value);
        }


        public bool Equals(Declaration other)
        {
            return this.Property == other.Property && this.Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Declaration)
                return this.Equals((Declaration)obj);
            else
                return false;
        }
    }
}
