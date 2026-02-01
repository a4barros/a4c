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
            Assert.Equal(2.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test2()
        {
            var expr = new Parser(Lexer.ProcessString("1-1"));
            Assert.Equal(0.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test3()
        {
            var expr = new Parser(Lexer.ProcessString("3*2"));
            Assert.Equal(6.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test4()
        {
            var expr = new Parser(Lexer.ProcessString("3/2"));
            Assert.Equal(1.5, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test5()
        {
            var expr = new Parser(Lexer.ProcessString("3/2 + 0.5"));
            Assert.Equal(2.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test6()
        {
            var expr = new Parser(Lexer.ProcessString("(3/2 + 0.5)^2"));
            Assert.Equal(4.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void Test7()
        {
            var expr = new Parser(Lexer.ProcessString("(3/2 + 0.5)^(5-3)"));
            Assert.Equal(4.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestPrecedence1()
        {
            var expr = new Parser(Lexer.ProcessString("1+2*3"));
            Assert.Equal(7.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestPrecedence2()
        {
            var expr = new Parser(Lexer.ProcessString("10-4/2"));
            Assert.Equal(8.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestAssociativity1()
        {
            var expr = new Parser(Lexer.ProcessString("10-3-2"));
            Assert.Equal(5.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestAssociativity2()
        {
            var expr = new Parser(Lexer.ProcessString("8/4/2"));
            Assert.Equal(1.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestParentheses1()
        {
            var expr = new Parser(Lexer.ProcessString("(1+2)*3"));
            Assert.Equal(9.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestParentheses2()
        {
            var expr = new Parser(Lexer.ProcessString("10*(2+3)"));
            Assert.Equal(50.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestParenthesesNested()
        {
            var expr = new Parser(Lexer.ProcessString("((1+2)*(3+4))"));
            Assert.Equal(21.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestUnaryMinus1()
        {
            var expr = new Parser(Lexer.ProcessString("-5"));
            Assert.Equal(-5.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestUnaryMinus2()
        {
            var expr = new Parser(Lexer.ProcessString("-(2+3)"));
            Assert.Equal(-5.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestUnaryMinus3()
        {
            var expr = new Parser(Lexer.ProcessString("--5"));
            Assert.Equal(5.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestUnaryMinus4()
        {
            var expr = new Parser(Lexer.ProcessString("-2*-3"));
            Assert.Equal(6.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestMixed1()
        {
            var expr = new Parser(Lexer.ProcessString("1 + 2 * 3 - 4 / 2"));
            Assert.Equal(5.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestMixed2()
        {
            var expr = new Parser(Lexer.ProcessString("(1 + 2) * (3 - 4 / 2)"));
            Assert.Equal(3.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestDecimalHandling()
        {
            var expr = new Parser(Lexer.ProcessString("1.5 * 2"));
            Assert.Equal(3.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestBigExpression()
        {
            var expr = new Parser(Lexer.ProcessString("-(3 + 4) * (2 - 5 / (1 + 1))"));
            Assert.Equal(3.5, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestSyntaxError1()
        {
            Assert.Throws<ParserException>(() =>
                new Parser(Lexer.ProcessString("1+")).Parse());
        }
        [Fact]
        public void TestSyntaxError2()
        {
            Assert.Throws<ParserException>(() =>
                new Parser(Lexer.ProcessString("(1+2")).Parse());
        }
        [Fact]
        public void TestSyntaxError3()
        {
            Assert.Throws<ParserException>(() =>
                new Parser(Lexer.ProcessString("*)")).Parse());
        }
        [Fact]
        public void TestSyntaxError4()
        {
            Assert.Throws<ParserException>(() =>
                new Parser(Lexer.ProcessString("1++2")).Parse());
        }
        [Fact]
        public void TestPower1()
        {
            var expr = new Parser(Lexer.ProcessString("2^3"));
            Assert.Equal(8.0, expr.Parse().Evaluate());
        }

        [Fact]
        public void TestPower2()
        {
            var expr = new Parser(Lexer.ProcessString("2^3^2"));
            Assert.Equal(512.0, expr.Parse().Evaluate());
        }

        [Fact]
        public void TestPower3()
        {
            var expr = new Parser(Lexer.ProcessString("2*3^2"));
            Assert.Equal(18.0, expr.Parse().Evaluate());
        }

        [Fact]
        public void TestPower4()
        {
            var expr = new Parser(Lexer.ProcessString("(2+1)^3"));
            Assert.Equal(27.0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestBigExpr1()
        {
            var expr = new Parser(Lexer.ProcessString("( (597355-8916456*59756)/(45952-8252686))+789 *968/(964+945)*559+  123"));
            Assert.Equal(288691.16178957152444783305458, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestBigExpr2()
        {
            var expr = new Parser(Lexer.ProcessString("( (597355-8916456*59756)/(45952-8252686^3))+789^  (78-100) *968/(964+945)*559+  123"));
            Assert.Equal(123.00000000094795416402683325, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestSqrt1()
        {
            var expr = new Parser(Lexer.ProcessString("sqrt(4)"));
            Assert.Equal(2, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestSqrt2()
        {
            var expr = new Parser(Lexer.ProcessString("-sqrt(100)+10"));
            Assert.Equal(0, expr.Parse().Evaluate());
        }
        [Fact]
        public void TestSqrt3()
        {
            var expr = new Parser(Lexer.ProcessString("sqrt(156) + sqrt(9856) - sqrt(965)"));
            Assert.Equal(80.702936030705516, expr.Parse().Evaluate());
        }
    }
}
