using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class DeclarationSet : IEnumerable<Declaration>
    {
        private IList<Declaration> declarations;

        public DeclarationSet()
        {
            declarations = new List<Declaration>();
        }

        public static DeclarationSet Create(params Declaration[] declarations)
        {
            var set = new DeclarationSet();
            
            foreach(var d in declarations)
            {
                set.declarations.Add(d);
            }

            return set;
        }

        IEnumerator<Declaration> IEnumerable<Declaration>.GetEnumerator()
        {
            return declarations.GetEnumerator();    
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)declarations).GetEnumerator();
        }
    }
}
