using SassSharp.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp.Ast
{
    public class SyntaxException : Exception
    {
        public SyntaxException(Token t)
            : base(string.Format("Unexpected token {0}", t))
        {}
    }

    public class SassSyntaxTreeBuilder
    {
        private Stack<Token> tokenStack;
        private IEnumerator<Token> tokenEnumerator;

        public SassSyntaxTreeBuilder(IEnumerable<Token> tokens)
        {
            this.tokenStack = new Stack<Token>();
            this.tokenEnumerator = tokens.GetEnumerator();
        }

        private Token? nextToken()
        {
            var moved = this.tokenEnumerator.MoveNext();
            if (moved)
                return tokenEnumerator.Current;
            else
                return null;
        }

        private void pushToken(Token t)
        {
            this.tokenStack.Push(t);
        }

        private Token popToken()
        {
            var t = this.tokenStack.Pop();
            return t;
        }

        public SyntaxTree Build()
        {
            RootNode root = parseRoot();

            return new SyntaxTree(root);
        }

        RootNode parseRoot()
        {
            var children = new List<Node>();
            Token? next;
            while ((next = nextToken()) != null)
            {
                var token = next.Value;
                pushToken(token);

                if (token.Type == TokenType.OpenBrace)
                    children.Add(parseSassNode());
            }

            return new RootNode(children);
        }

        SassNode parseSassNode()
        {
            popToken(); //open brace
            var selector = popToken();
            SelectorNode sel = new SelectorNode(selector.Value);
            SassContainerNode container = parseContainerNode();
            return new SassNode(sel, container);
        }

        SassContainerNode parseContainerNode()
        {
            var children = new List<Node>();
            Token? next;
            while ((next = nextToken()) != null)
            {
                var token = next.Value;
                pushToken(token);

                if (token.Type == TokenType.SemiColon)
                    children.Add(parseDeclarationNode());

                if (token.Type == TokenType.OpenBrace)
                    children.Add(parseSassNode());

                if (token.Type == TokenType.CloseBrace)
                    break;

            }

            return new SassContainerNode(children);
        }

        DeclarationNode parseDeclarationNode()
        {
            popToken(); //semicolon
            var value = popToken();
            popToken(); //colon
            var property = popToken();

            return new DeclarationNode(property.Value, value.Value);
        }

    }
}
