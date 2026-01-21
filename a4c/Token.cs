using System;
using System.Collections.Generic;
using System.Text;

namespace a4c
{
    public enum TokenTypeEnum
    {
        SUM, MINUS, MUL, DIV, OPEN_PARENTHESIS, CLOSE_PARENTHESIS, NUMBER
    }
    public interface IToken
    {
        public bool IsNumeric();
        public TokenTypeEnum GetTokenType();
        public int GetValue();
        public string ToString();
    }
    internal class NumericToken : IToken
    {
        private readonly int Value;
        public NumericToken(int value)
        {
            this.Value = value;
        }

        public bool IsNumeric()
        {
            return true;
        }

        public TokenTypeEnum GetTokenType()
        {
            return TokenTypeEnum.NUMBER;
        }

        public int GetValue()
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
        private TokenTypeEnum TokenType;
        public OperationToken(TokenTypeEnum tokenType)
        {
            TokenType = tokenType;
        }
        public bool IsNumeric()
        {
            return false;
        }

        public TokenTypeEnum GetTokenType()
        {
            return TokenType;
        }

        public int GetValue()
        {
            return 0;
        }
        public override string ToString()
        {
            return $"<{GetTokenType()}>";
        }
    }
    public static class TokenFactory
    {
        public static IToken CreateToken(int value)
        {
            return new NumericToken(value);
        }
        public static IToken CreateToken(TokenTypeEnum tokenType)
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
