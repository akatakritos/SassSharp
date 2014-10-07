using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public struct Selector : IEquatable<Selector>
    {
        public string Value { get; private set; }

        public Selector(string selector)
            : this()
        {
            Value = selector;
        }

        public Selector DescendFrom(Selector parent)
        {
            return new Selector(parent.Value + " " + this.Value);
        }

        public static implicit operator String(Selector s)
        {
            return s.Value;
        }

        public static implicit operator Selector(string s)
        {
            return new Selector(s);
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(Selector other)
        {
            if (other == null)
                return false;

            return this.Value == other.Value;
        }

        public override bool Equals(object other)
        {
            if (other is Selector)
                return this.Equals((Selector)other);
            else
                return false;
        }
    }
}
