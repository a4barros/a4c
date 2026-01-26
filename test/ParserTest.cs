using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using a4c;

namespace test
{
    public class ParserTest
    {
        [Fact]
        public void Test1()
        {
            var expr = new Parser(Lexer.ProcessString("1+1"));
            Assert.Equal(2.0m, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test2()
        {
            var expr = new Parser(Lexer.ProcessString("1-1"));
            Assert.Equal(0.0m, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test3()
        {
            var expr = new Parser(Lexer.ProcessString("3*2"));
            Assert.Equal(6.0m, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test4()
        {
            var expr = new Parser(Lexer.ProcessString("3/2"));
            Assert.Equal(1.5m, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test5()
        {
            var expr = new Parser(Lexer.ProcessString("3/2 + 0.5"));
            Assert.Equal(2.0m, expr.Parse().Evaluate());
        }
    }
}
