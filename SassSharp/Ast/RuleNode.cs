﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public interface INodeVisitor<R, P>
    {
        R Visit(RootNode node, P p);
        R Visit(RuleNode node, P p);
        R Visit(SelectorNode node, P p);
        R Visit(SassNode node, P p);
        R Visit(SassContainerNode node, P p);
        R Visit(DeclarationNode node, P p);
        R Visit(PropertyNode node, P p);
        R Visit(ValueNode node, P p);
    }

    public abstract class Node
    {
        public abstract R Accept<R, P>(INodeVisitor<R,P> visitor, P p);
    }

    public class RootNode : Node
    {
        public IEnumerable<Node> Children { get; private set; }

        public override R Accept<R, P>(INodeVisitor<R, P> visitor, P p)
        {
            return visitor.Visit(this, p);
        }
    }


    public class SelectorNode : Node
    {
        public Selector Selector { get; private set; }

        public override R Accept<R,P>(INodeVisitor<R,P> visitor, P p)
        {
            return visitor.Visit(this, p);
        }
    }

    public class SassNode : Node
    {
        public SelectorNode Selector { get; private set; }
        public SassContainerNode Container { get; private set; }
        public override R Accept<R, P>(INodeVisitor<R, P> visitor, P p)
        {
            return visitor.Visit(this, p);
        }
    }

    public class SassContainerNode : Node
    {
        public IEnumerable<Node> Children { get; private set; }

        public override R Accept<R,P>(INodeVisitor<R,P> visitor, P p)
        {
            return visitor.Visit(this, p);
        }
    }

    public class DeclarationNode : Node
    {
        public PropertyNode Property { get; private set; }
        public ValueNode Value { get; private set; }

        public override R Accept<R, P>(INodeVisitor<R, P> visitor, P p)
        {
            return visitor.Visit(this, p);
        }
    }

    public class PropertyNode : Node
    {
        public string Name { get; private set; }

        public override R Accept<R, P>(INodeVisitor<R, P> visitor, P p)
        {
            return visitor.Visit(this, p);
        }
    }

    public class ValueNode : Node
    {
        public string Value { get; private set; }

        public override R Accept<R, P>(INodeVisitor<R, P> visitor, P p)
        {
            return visitor.Visit(this, p);
        }
    }


    public class RuleNode : Node
    {
        public Selector Selector { get; private set; }
        public DeclarationSet Declarations { get; private set; }
        public IEnumerable<RuleNode> Children { get; private set; }

        public static RuleNode Create(string selector, DeclarationSet declaraations, params RuleNode[] children)
        {
            return new RuleNode
            {
                Selector = new Selector(selector),
                Declarations = declaraations,
                Children = children
            };
        }

        public static RuleNode Create(string selector, DeclarationSet declaraations)
        {
            return new RuleNode
            {
                Selector = new Selector(selector),
                Declarations = declaraations,
                Children = new RuleNode[] { }
            };
        }

        public static RuleNode Create(string selector, DeclarationSet declarations, IEnumerable<RuleNode> children)
        {
            return new RuleNode
            {
                Selector = new Selector(selector),
                Declarations = declarations,
                Children = children
            };
        }

        public override R Accept<R, P>(INodeVisitor<R, P> visitor, P p)
        {
            return visitor.Visit(this, p);
        }
    }
}