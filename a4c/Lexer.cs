using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace a4c
{
    public static class Lexer
    {
        private enum LexerStateEnum
        {
            NORMAL,
            WITHIN_NUMBER,
        }
        public static TokenList ProcessString(string expressionStr)
        {
            TokenList tokens = new();
            LexerStateEnum lexerState = LexerStateEnum.NORMAL;
            int numberBuffer = 0;
            foreach (char c in expressionStr + " ")
            {
                if (char.IsDigit(c))
                {
                    lexerState = LexerStateEnum.WITHIN_NUMBER;
                    numberBuffer = AddDigit(numberBuffer, c);
                    continue;
                }
                else
                {
                    if (lexerState == LexerStateEnum.WITHIN_NUMBER)
                    {
                        tokens.Add(TokenFactory.CreateToken(numberBuffer));
                        numberBuffer = 0;
                        lexerState = LexerStateEnum.NORMAL;
                    }
                }
                if (lexerState == LexerStateEnum.NORMAL)
                {
                    Dictionary<char, TokenTypeEnum> tokenMap = new()
                    {
                        { '+', TokenTypeEnum.SUM },
                        { '-', TokenTypeEnum.MINUS },
                        { '*', TokenTypeEnum.MUL },
                        { '/', TokenTypeEnum.DIV },
                        { '(', TokenTypeEnum.OPEN_PARENTHESIS },
                        { ')', TokenTypeEnum.CLOSE_PARENTHESIS },
                    };

                    if (tokenMap.ContainsKey(c))
                    {
                        tokens.Add(TokenFactory.CreateToken(tokenMap[c]));
                    }
                    else
                    {
                        if (!char.IsWhiteSpace(c)) {
                            throw new Exception($"Invalid character {c}");
                        }
                    }
                }
            }
            return tokens;
        }
        private static int AddDigit(int currentNumber, int digit)
        {
            return currentNumber * 10 + (digit - '0');
        }
    }
}
