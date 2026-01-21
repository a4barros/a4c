using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace a4c
{
    public class Tree
    {
        private Tree? left;
        private Tree? right;
        private IToken token;
        public Tree(IToken token, Tree? left, Tree? right)
        {
            this.left = left;
            this.right = right;
            this.token = token;
        }
        public float Evaluate()
        {
            if (this.token.IsNumeric())
            {
                return this.token.GetValue();
            }
            if (this.left == null || this.right == null)
            {
                throw new TreeException($"On {this} either left or right is null");
            }
            if (this.token.GetTokenType() == TokenTypeEnum.SUM)
            {
                return this.left.Evaluate() + this.right.Evaluate();
            }
            else if (this.token.GetTokenType() == TokenTypeEnum.MINUS)
            {
                return this.left.Evaluate() - this.right.Evaluate();
            }
            else if (this.token.GetTokenType() == TokenTypeEnum.MUL)
            {
                return this.left.Evaluate() * this.right.Evaluate();
            }
            else if (this.token.GetTokenType() == TokenTypeEnum.DIV)
            {
                var right = this.right.Evaluate();
                if (right == 0)
                {
                    throw new TreeException($"On {this} division by zero");
                }
                return this.left.Evaluate() / this.right.Evaluate();
            }
            return 0;
        }
    }

    public class TreeException(string message) : Exception(message)
    {
    }
}
