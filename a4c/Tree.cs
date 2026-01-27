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
        public decimal Evaluate();
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
        public decimal Evaluate()
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
                    return (decimal)Math.Pow((double)left.Evaluate(), (double)right.Evaluate());
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
        public decimal Evaluate()
        {
            switch (operation)
            {
                case Operation.NEGATIVE:
                    return -node.Evaluate();
                default:
                    throw new TreeException($"Invalid operation at {this}");
            }
        }
        public override string ToString()
        {
            return $"{operation}({node})";
        }
    }

    public class NumberNode : INode
    {
        private readonly decimal value;

        public NumberNode(decimal value)
        {
            this.value = value;
        }

        public decimal Evaluate()
        {
            return value; 
        }
        public override string ToString() => value.ToString();

    }
    public class TreeException(string message) : Exception(message);
}
