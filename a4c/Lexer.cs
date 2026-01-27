using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace a4c
{
    public static class Lexer
    {
        private enum LexerState
        {
            NORMAL,
            WITHIN_NUMBER,
        }
        public static TokenList ProcessString(string expressionStr)
        {
            TokenList tokens = new();
            LexerState lexerState = LexerState.NORMAL;
            string numberBuffer = "";
            foreach (char c in expressionStr + " ")
            {
                if (c == '.' && numberBuffer.Contains('.'))
                    throw new LexerException($"Invalid number {numberBuffer}{c}");

                if (char.IsDigit(c) || c == '.')
                {
                    lexerState = LexerState.WITHIN_NUMBER;
                    numberBuffer = $"{numberBuffer}{c}";
                    continue;
                }
                else
                {
                    if (lexerState == LexerState.WITHIN_NUMBER)
                    {
                        try
                        {
                            tokens.Add(TokenFactory.CreateToken(Convert.ToDecimal(numberBuffer)));
                        }
                        catch (FormatException)
                        {
                            throw new LexerException($"Invalid number {numberBuffer}{c}");
                        }
                        catch (OverflowException)
                        {
                            throw new LexerException($"Overflow on number {numberBuffer}{c}");
                        }
                        numberBuffer = "";
                        lexerState = LexerState.NORMAL;
                    }
                }
                if (lexerState == LexerState.NORMAL)
                {
                    Dictionary<char, Operation> tokenMap = new()
                    {
                        { '+', Operation.PLUS },
                        { '-', Operation.MINUS },
                        { '*', Operation.MUL },
                        { '/', Operation.DIV },
                        { '^', Operation.POW },
                        { '(', Operation.OPEN_PARENTHESIS },
                        { ')', Operation.CLOSE_PARENTHESIS },
                    };

                    if (tokenMap.ContainsKey(c))
                    {
                        tokens.Add(TokenFactory.CreateToken(tokenMap[c]));
                    }
                    else
                    {
                        if (!char.IsWhiteSpace(c)) {
                            throw new LexerException($"Invalid character {c}");
                        }
                    }
                }
            }
            return tokens;
        }
    }
    public class LexerException(string message) : Exception(message)
    {
    }
}
