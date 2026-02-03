using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace a4c
{
    public interface INode
    {
        public double Evaluate();
        public string ToString();
    }
    public class BinaryNode : INode
    {
        private readonly INode left;
        private readonly INode right;
        private readonly Operation operation;
        public BinaryNode(Operation operation, INode left, INode right)
        {
            this.left = left;
            this.right = right;
            this.operation = operation;
        }
        public double Evaluate()
        {
            switch (operation)
            {
                case Operation.PLUS:
                    return left.Evaluate() + right.Evaluate();
                case Operation.MINUS:
                    return left.Evaluate() - right.Evaluate();
                case Operation.MUL:
                    return left.Evaluate() * right.Evaluate();
                case Operation.DIV:
                {
                    var r = right.Evaluate();
                    if (r == 0)
                        throw new TreeException($"On {this} division by zero");

                    return left.Evaluate() / r;
                }
                case Operation.POW:
                    return Math.Pow(left.Evaluate(), right.Evaluate());
                default:
                    throw new TreeException($"Invalid operation on {this}");
            }
        }
        public override string ToString()
        {
            return $"({left} {operation} {right})";
        }
    }

    public class UnaryNode : INode
    {
        private readonly INode node;
        private readonly Operation operation;
        public UnaryNode(Operation operation, INode node)
        {
            this.node = node;
            this.operation = operation;
        }
        public double Evaluate()
        {
            return operation switch
            {
                Operation.NEGATIVE => -node.Evaluate(),
                _ => throw new TreeException($"Invalid operation at {this}"),
            };
        }
        public override string ToString()
        {
            return $"{operation}({node})";
        }
    }

    public class NumberNode : INode
    {
        private readonly double value;

        public NumberNode(double value)
        {
            this.value = value;
        }

        public double Evaluate()
        {
            return value; 
        }
        public override string ToString() => value.ToString();

    }
    public class FunctionNode : INode
    {
        private readonly IToken FunctionName;
        private readonly INode Argument;
        public FunctionNode(IToken functionName, INode argument)
        {
            this.FunctionName = functionName;
            this.Argument = argument;
        }

         double INode.Evaluate()
        {
            return FunctionName.GetFunctionName() switch
            {
                Function.SQRT => Math.Sqrt(Argument.Evaluate()),
                Function.SIN => Math.Sin(Argument.Evaluate()),
                Function.COS => Math.Cos(Argument.Evaluate()),
                Function.TAN => Math.Tan(Argument.Evaluate()),
                _ => throw new TreeException($"Unknown function {this}"),
            };
        }

        string INode.ToString()
        {
            return $"{FunctionName}({Argument})";
        }
    }
    public class ConstantNode : INode
    {
        private readonly Constant ConstantName;
        public ConstantNode(Constant constantName)
        {
            ConstantName = constantName;
        }
        public double Evaluate()
        {
            return ConstantName switch
            {
                Constant.PI => Math.PI,
                Constant.E => Math.E,
                _ => throw new TreeException($"Unknown constant {this}"),
            };
        }
        string INode.ToString()
        {
            return $"{ConstantName}";
        }
    }
    public class TreeException(string message) : Exception(message);
}
