using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public static class AstDump
    {
        public static string Dump(SassSyntaxTree ast)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("AST");

            forEachSpecialLast(ast.Children, (n, isLast) =>
            {
                dumpNode(sb, n, prefix: "", isLastLine: isLast);
            });

            return sb.ToString();
        }

        private static void dumpNode(StringBuilder sb, 
            RuleNode n, string prefix = "", bool isLastLine = false)
        {
            sb.Append(prefix).AppendFormat("{0}-- Rule: '{1}'", isLastLine ? '`' : '|', n.Selector.Value).AppendLine();
            sb.Append(prefix).AppendFormat("{0}   |-- Declarations", isLastLine ? ' ' : '|').AppendLine();

            forEachSpecialLast(n.Declarations, (d, isLast) =>
            {
                sb.Append(prefix);
                sb.AppendFormat("{0}   |   {1}-- {2}: {3}",
                    isLastLine ? ' ' : '|',
                    isLast ? '`' : '|', d.Property, d.Value).AppendLine();
            });

            sb.Append(prefix).AppendFormat("{0}   `-- Children", isLastLine ? ' ' : '|').AppendLine();

            forEachSpecialLast(n.Children, (child, isLast) =>
            {
                dumpNode(sb, child, prefix + "|       ", isLast);
            });
        }

        private static void forEachSpecialLast<T>(IEnumerable<T> collection, Action<T, bool> action)
        {
            bool first = true;
            T prev = default(T);
            foreach(var t in collection)
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
    }
}
