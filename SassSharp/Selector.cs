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
            if (this.Value.StartsWith("&"))
                return new Selector(parent.Value + this.Value.Substring(1));

            return new Selector(parent.Value + " " + this.Value);
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
