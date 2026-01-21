using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace a4c
{
    internal static class Lexer
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
                    numberBuffer = numberBuffer * 10 + (c - '0');
                    continue;
                }
                else
                {
                    lexerState = LexerStateEnum.NORMAL;
                    if (numberBuffer != 0)
                    {
                        tokens.Add(TokenFactory.CreateToken(numberBuffer));
                        numberBuffer = 0;
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

                    tokens.Add(TokenFactory.CreateToken(tokenMap[c]));
                }
            }
            return tokens;
        }
    }
}
