using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace a4c
{
    public interface ITree
    {
        public decimal Evaluate();
    }
    public class BinaryNode : ITree
    {
        private ITree left;
        private ITree right;
        private Operation operation;
        public BinaryNode(Operation operation, ITree left, ITree right)
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
                        var right = this.right.Evaluate();
                        if (right == 0)
                        {
                            throw new TreeException($"On {this} division by zero");
                        }
                        return left.Evaluate() / this.right.Evaluate();
                    }
                default:
                    throw new TreeException($"Invalid operation on {this}");
            }
        }
        public override string ToString()
        {
            return $"{operation}({left} {right})";
        }
    }

    public class UnaryNode : ITree
    {
        private ITree node;
        private Operation operation;
        public UnaryNode(Operation operation, ITree node)
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

    public class NumberNode : ITree
    {
        private decimal value;

        public NumberNode(decimal value)
        {
            this.value = value;
        }

        public decimal Evaluate()
        {
            return value; 
        }
    }
    public class TreeException(string message) : Exception(message);
}
