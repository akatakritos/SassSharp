using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class CompilationContext
    {
        private readonly PartialsCollection partials;

        public CompilationContext()
        {
            this.partials = new PartialsCollection();
        }

        public void AddPartial(Partial p)
        {
            if (partials.HasPartial(p.Name))
                throw new InvalidOperationException("A partial with this name has already been added");

            partials.Add(p);
        }

        public interface IPartialsCollection
        {
            Partial this[string name] { get; }
            bool HasPartial(string name);
        }

        public IPartialsCollection Partials { get { return partials; } }

        class PartialsCollection : IPartialsCollection
        {
            private Dictionary<string, Partial> map = new Dictionary<string, Partial>();

            public Partial this[string name]
            {
                get { return map[name]; }
            }

            public void Add(Partial p)
            {
                map.Add(p.Name, p);
            }

            public bool HasPartial(string name)
            {
                return map.ContainsKey(name);
            }
        }
    }
}
