using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using SassSharp.Tokens;

namespace SassSharp.Tests.Contraints
{
    internal class TokensMatchRegardlessOfPositionConstraint : Constraint
    {
        private Token[] expected;
        private TokenDifference difference;
        private int differentIndex;

        public TokensMatchRegardlessOfPositionConstraint(Token[] expected)
        {
            this.expected = expected;
        }

        public override bool Matches(object actual)
        {
            base.actual = actual;

            return assertSameTokenStreamRegardlessOfPosition(expected, (Token[])actual);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WriteExpectedValue(expected);
        }

        public override void WriteMessageTo(MessageWriter writer)
        {
            if (difference == TokenDifference.StreamLength)
            {
                writer.WriteLine("Token streams are different lengths: Expected {0}, Got {1}",
                    expected.Length, ((Token[])actual).Length);
            } else if (difference == TokenDifference.Type)
            {
                writer.WriteLine("Tokens differ by Type at index {0}: Expected '{1}', Got '{2}'",
                    differentIndex, expected[differentIndex].Type, ((Token[])actual)[differentIndex].Type);
            }
            else if (difference == TokenDifference.Value)
            {
                writer.WriteLine("Tokens differ by Value at index {0}: Expected '{1}', Got '{2}'",
                    differentIndex, expected[differentIndex].Value, ((Token[])actual)[differentIndex].Value);
            }
        }

        private bool assertSameTokenStreamRegardlessOfPosition(Token[] expected, Token[] actual)
        {
            if (expected.Length != actual.Length)
            {
                difference = TokenDifference.StreamLength;
                differentIndex = 0;
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                var ac = actual[i];
                var ex = expected[i];

                if (ex.Type != ac.Type)
                {
                    difference = TokenDifference.Type;
                    differentIndex = i;
                    return false;
                }

                if (ex.Value != ac.Value)
                {
                    difference = TokenDifference.Value;
                    differentIndex = i;
                    return false;
                }

            }

            return true;
        }

        private enum TokenDifference
        {
            StreamLength,
            Type,
            Value
        }
    }
}
