using SassSharp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Visitors
{
    internal class NopVisitor<R,P> : INodeVisitor<R,P>
    {
        public virtual R Visit(RootNode node, P p)
        {
            return default(R);
        }

        public virtual R Visit(SelectorNode node, P p)
        {
            return default(R);
        }

        public virtual R Visit(SassNode node, P p)
        {
            return default(R);
        }

        public virtual R Visit(SassContainerNode node, P p)
        {
            return default(R);
        }

        public virtual R Visit(DeclarationNode node, P p)
        {
            return default(R);
        }

        public virtual R Visit(PropertyNode node, P p)
        {
            return default(R);
        }

        public virtual R Visit(ValueNode node, P p)
        {
            return default(R);
        }

        public virtual R Visit(AtCommandNode node, P p)
        {
            return default(R);
        }
    }
}
