using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public interface INodeVisitor<R, P>
    {
        R Visit(RootNode node, P p);
        R Visit(SelectorNode node, P p);
        R Visit(SassNode node, P p);
        R Visit(SassContainerNode node, P p);
        R Visit(DeclarationNode node, P p);
        R Visit(PropertyNode node, P p);
        R Visit(ValueNode node, P p);
        R Visit(AtCommandNode node, P p);
    }
}
