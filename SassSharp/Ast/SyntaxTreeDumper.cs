using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public static class SyntaxTreeDumper
    {
        public static string Dump(SyntaxTree ast)
        {
            StringBuilder sb = new StringBuilder();
            var visitor = new SassSyntaxTreeDumperVisitor();

            //walk the tree
            visitor.Visit(ast.Root, new SassTreeDumperParams
            {
                IsLast = true,
                Prefix = "",
                Builder = sb
            });

            return sb.ToString();
        }

        struct SassTreeDumperParams
        {
            public bool IsLast { get; set; }
            public StringBuilder Builder { get; set; }
            public string Prefix { get; set; }
        }

        class SassSyntaxTreeDumperVisitor : INodeVisitor<StringBuilder, SassTreeDumperParams>
        {
            public StringBuilder Visit(RootNode node, SassTreeDumperParams p)
            {
                p.Builder.AppendLine("Root");
                forEachSpecialLast(node.Children, (n, isLast) =>
                {
                    n.Accept(this, new SassTreeDumperParams
                    {
                        IsLast = isLast,
                        Prefix = "",
                        Builder = p.Builder
                    });
                });

                return p.Builder;
            }


            public StringBuilder Visit(SelectorNode node, SassTreeDumperParams p)
            {
                return p.Builder
                    .Append(p.Prefix)
                    .AppendFormat("{0}-- Selector: '{1}'", junction(p.IsLast), node.Selector.Value)
                    .AppendLine();
            }

            public StringBuilder Visit(SassNode node, SassTreeDumperParams p)
            {
                p.Builder
                    .Append(p.Prefix)
                    .AppendFormat("{0}-- Sass", junction(p.IsLast))
                    .AppendLine();

                node.Selector.Accept(this, new SassTreeDumperParams
                {
                    IsLast = false,
                    Prefix = indent(p.Prefix, p.IsLast),
                    Builder = p.Builder
                });

                node.Container.Accept(this, new SassTreeDumperParams
                {
                    IsLast = true,
                    Prefix = p.Prefix + "    ",
                    Builder = p.Builder
                });

                return p.Builder;
            }

            public StringBuilder Visit(SassContainerNode node, SassTreeDumperParams p)
            {
                p.Builder
                    .Append(p.Prefix)
                    .AppendFormat("{0}-- Container", junction(p.IsLast))
                    .AppendLine();

                forEachSpecialLast(node.Children, (n, isLast) =>
                {
                    n.Accept(this, new SassTreeDumperParams
                    {
                        IsLast = isLast,
                        Prefix = indent(p.Prefix, p.IsLast),
                        Builder = p.Builder
                    });
                });

                return p.Builder;
            }

            public StringBuilder Visit(DeclarationNode node, SassTreeDumperParams p)
            {
                p.Builder
                    .Append(p.Prefix)
                    .AppendFormat("{0}-- Declaration", junction(p.IsLast))
                    .AppendLine();

                node.Property.Accept(this, new SassTreeDumperParams
                {
                    IsLast = false,
                    Prefix = indent(p.Prefix, p.IsLast),
                    Builder = p.Builder
                });

                node.Value.Accept(this, new SassTreeDumperParams
                {
                    IsLast = true,
                    Prefix = indent(p.Prefix, p.IsLast),
                    Builder = p.Builder
                });

                return p.Builder;
            }

            public StringBuilder Visit(PropertyNode node, SassTreeDumperParams p)
            {
                return p.Builder
                    .Append(p.Prefix)
                    .AppendFormat("{0}-- Property: '{1}'", junction(p.IsLast), node.Name)
                    .AppendLine();
            }

            public StringBuilder Visit(ValueNode node, SassTreeDumperParams p)
            {
                return p.Builder
                    .Append(p.Prefix)
                    .AppendFormat("{0}-- Value: '{1}'", junction(p.IsLast), node.Value)
                    .AppendLine();
            }

            private static void forEachSpecialLast<T>(IEnumerable<T> collection, Action<T, bool> action)
            {
                bool first = true;
                T prev = default(T);
                foreach (var t in collection)
                {
                    if (first)
                        first = false;
                    else
                        action(prev, false);

                    prev = t;
                }

                if (!first) //havent iterated - empty collection
                    action(prev, true);
            }

            private static string indent(string prefix, bool islast)
            {
                return prefix + string.Format("{0}   ", islast ? ' ' : '|');
            }

            private static char junction(bool isLast)
            {
                return isLast ? '`' : '|';
            }
        }
    }

    
    
}
