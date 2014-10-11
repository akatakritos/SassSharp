using System;
using System.Collections.Generic;
using SassSharp;
using SassSharp.Tokens;

namespace SassSharp.Ast
{
    public class AstBuilder
    {
        private IEnumerable<Token> tokens;
        private Stack<Token> tokenStack;
        private Stack<NodeBuilder> nodeStack;

        private NodeBuilder CurrentNode
        {
            get { return nodeStack.Peek(); }
        }

        public AstBuilder(IEnumerable<Token> tokens)
        {
            this.tokens = tokens;
            this.tokenStack = new Stack<Token>();
            this.nodeStack = new Stack<NodeBuilder>();
            this.nodeStack.Push(new NodeBuilder("")); //root node
        }

        public Node Build()
        {
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
                if (token.Type == TokenType.OpenBrace)
                {
                    nodeStack.Push(new NodeBuilder(tokenStack.Pop().Value));
                    continue;
                }

                if (token.Type == TokenType.SemiColon)
                {
                    var value = tokenStack.Pop().Value;
                    tokenStack.Pop(); // colon
                    var property = tokenStack.Pop().Value;

                    CurrentNode.AddDeclaration(new Declaration(property, value));
                    continue;
                }

                if (token.Type == TokenType.CloseBrace)
                {
                    var child = nodeStack.Pop();
                    CurrentNode.AddChild(child.ToNode());
                    continue;
                }

                tokenStack.Push(token);


            }

            return CurrentNode.ToNode();
        }
    }
}

