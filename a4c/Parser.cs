using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Xml.Linq;

namespace a4c
{
    // <expr>   ::= <term> + <term>
    //           |  <term> - <term>

    // <term>   ::= <power> * <power>
    //           |  <power> / <power>
    //           |  <power>

    // <power>  ::= <factor> ^ <factor>
    //           |  <factor>

    // <factor> ::= NUMBER
    //           |  ( <expr> )
    //           |  - <factor>

    public class Parser
    {
        private TokenList tokenList;
        public Parser(TokenList tokenList)
        {
            this.tokenList = tokenList;
        }
        public INode Parse()
        {
            return ParseExpr();
        }
        private INode ParseExpr()
        {
            // <expr>   ::= <term> + <term>
            //           |  <term> - <term>
            var node = ParseTerm();
            var nextToken = tokenList.LookNext();

            while ((nextToken = tokenList.LookNext()) != null && nextToken.IsEither(Operation.PLUS, Operation.MINUS))
            {
                var operation = tokenList.Consume() ?? throw new ParserException("No more tokens to consume.");
                var right = ParseTerm();
                node = new BinaryNode(operation.GetOp(), node, right);
            }
            return node;
        }
        private INode ParseTerm()
        {
            // <term>   ::= <power> * <power>
            //           |  <power> / <power>
            //           |  <power>
            var node = ParsePower();
            var nextToken = tokenList.LookNext();

            while ((nextToken = tokenList.LookNext()) != null && nextToken.IsEither(Operation.MUL, Operation.DIV))
            { 
                var operation = tokenList.Consume() ?? throw new ParserException("No more tokens to consume.");
                var right = ParsePower();
                node = new BinaryNode(operation.GetOp(), node, right);
            }
            return node;
        }
        private INode ParsePower()
        {
            // <power>  ::= <factor> ^ <factor>
            //           |  <factor>
            var node = ParseFactor();
            var nextToken = tokenList.LookNext();
            while ((nextToken = tokenList.LookNext()) != null && nextToken.Is(Operation.POW))
            {
                var operation = tokenList.Consume() ?? throw new ParserException("No more tokens to consume.");
                var right = ParsePower();
                node = new BinaryNode(operation.GetOp(), node, right);
            }
            return node;
        }
        private INode ParseFactor()
        {
            // <factor> ::= NUMBER
            //           |  ( <expr> )
            //           |  - <factor>
            var nextToken = tokenList.LookNext();
            if (nextToken?.GetOp() == Operation.NUMBER)
            {
                var numberToken = tokenList.Consume() ?? throw new ParserException("No more tokens to consume.");
                var value = numberToken.GetValue();

                return new NumberNode(value);
            }
            else if (nextToken?.GetOp() == Operation.OPEN_PARENTHESIS)
            {
                tokenList.Consume();
                var node = ParseExpr();
                nextToken = tokenList.LookNext();
                if (nextToken?.GetOp() != Operation.CLOSE_PARENTHESIS)
                {
                    throw new ParserException($"Parenthesis not closed on {node}");
                }
                tokenList.Consume();
                return node;
            }
            else if (nextToken?.GetOp() == Operation.MINUS)
            {
                tokenList.Consume();
                var operand = ParseFactor();
                return new UnaryNode(Operation.NEGATIVE, operand);
            }
            else
            {
                throw new ParserException(
                    $"Unexpected '{nextToken}'. Expected number, '(', or '-'."
                );
            }
        }
    }
    public class ParserException(string message) : Exception(message);

}
