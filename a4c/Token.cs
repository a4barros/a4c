using System;
using System.Collections.Generic;
using System.Text;

namespace a4c
{

    public interface IToken
    {
        public bool IsNumeric();
        public Operation GetOperation();
        public decimal GetValue();
        public string ToString();
    }
    internal class NumericToken : IToken
    {
        private readonly decimal Value;
        public NumericToken(decimal value)
        {
            this.Value = value;
        }

        public bool IsNumeric()
        {
            return true;
        }

        public Operation GetOperation()
        {
            return Operation.NUMBER;
        }

        public decimal GetValue()
        {
            return Value;
        }
        public override string ToString()
        {
            return $"<NUMBER {GetValue()}>";
        }
    }
    internal class OperationToken : IToken
    {
        private Operation TokenType;
        public OperationToken(Operation tokenType)
        {
            TokenType = tokenType;
        }
        public bool IsNumeric()
        {
            return false;
        }

        public Operation GetOperation()
        {
            return TokenType;
        }

        public decimal GetValue()
        {
            return 0;
        }
        public override string ToString()
        {
            return $"<{GetOperation()}>";
        }
    }
    public static class TokenFactory
    {
        public static IToken CreateToken(decimal value)
        {
            return new NumericToken(value);
        }
        public static IToken CreateToken(Operation tokenType)
        {
            return new OperationToken(tokenType);
        }
    }
    public class TokenList
    {
        private List<IToken> tokens = new List<IToken>();
        public TokenList()
        {
        }
        public void Add(IToken token)
        {
            tokens.Add(token);
        }
        public IToken Consume()
        {
            var t = tokens[0];
            tokens.RemoveAt(0);
            return t;
        }
        public override string ToString()
        {
            return String.Join(" ", tokens);
        }
    }
}
