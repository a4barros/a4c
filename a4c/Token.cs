using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace a4c
{

    public interface IToken
    {
        public bool IsNumeric();
        public Operation GetOp();
        public Function GetFunctionName();
        public bool IsEither(Operation op1, Operation op2)
        {
            return GetOp() == op1 || GetOp() == op2;
        }
        public bool Is(Operation op)
        {
            return GetOp() == op;
        }
        public  double GetNumericalValue();
        public string ToString();
    }
    internal class NumericToken : IToken
    {
        private readonly double Value;
        public NumericToken(double value)
        {
            this.Value = value;
        }

        public bool IsNumeric()
        {
            return true;
        }

        public Operation GetOp()
        {
            return Operation.NUMBER;
        }
        public Function GetFunctionName()
        {
            return Function.None;
        }
        public double GetNumericalValue()
        {
            return Value;
        }
        public override string ToString()
        {
            return $"{GetNumericalValue()}>";
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

        public Operation GetOp()
        {
            return TokenType;
        }
        public Function GetFunctionName()
        {
            return Function.None;
        }

        public double GetNumericalValue()
        {
            return 0;
        }
        public override string ToString()
        {
            return $"{GetOp()}";
        }
    }
    public class FunctionToken : IToken
    {
        private readonly Function FunctionName;
        public FunctionToken(Function value)
        {
            this.FunctionName = value;
        }

        public bool IsNumeric()
        {
            return false;
        }

        public Operation GetOp()
        {
            return Operation.FUNCTION;
        }
        public Function GetFunctionName()
        {
            return FunctionName;
        }

        public double GetNumericalValue()
        {
            return 0;
        }
        public override string ToString()
        {
            return $"{FunctionName}()";
        }
    }
    public class ConstantToken : IToken
    {
        private readonly Constant ConstantName;
        public ConstantToken(Constant constantName)
        {
            this.ConstantName = constantName;
        }

        public bool IsNumeric()
        {
            return false;
        }

        public Operation GetOp()
        {
            return Operation.CONSTANT;
        }
        public Constant GetFunctionName()
        {
            return ConstantName;
        }

        public double GetNumericalValue()
        {
            return 0;
        }
        public override string ToString()
        {
            return $"{ConstantName}";
        }

        Function IToken.GetFunctionName()
        {
            return Function.None;
        }
        public Constant GetConstantName()
        {
            return ConstantName; 
        }
    }
    public static class TokenFactory
    {
        public static IToken CreateToken(double value)
        {
            return new NumericToken(value);
        }
        public static IToken CreateToken(Operation tokenType)
        {
            return new OperationToken(tokenType);
        }
        public static IToken CreateToken(string functionOrConstantName)
        {
            var isFunction = Enum.TryParse(functionOrConstantName, true, out Function token);
            if (isFunction)
            {
                return new FunctionToken(token);
            }

            var isConstant = Enum.TryParse(functionOrConstantName, true, out Constant constant);
            if (isConstant)
            {
                return new ConstantToken(constant);
            }
            else
            {
                throw new TokenException($"Was expecting a function or constant got {functionOrConstantName}"); 
            }
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
        public IToken? Consume()
        {
            if (tokens.Count == 0)
                return null;
            var t = tokens[0];
            tokens.RemoveAt(0);
            return t;
        }
        public IToken? LookNext()
        {
            if (tokens.Count == 0)
                return null;
            return tokens[0];
        }
        public override string ToString()
        {
            return String.Join(" ", tokens);
        }
    }
    public class TokenException(string message) : Exception(message) { }
}
