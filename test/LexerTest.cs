using System.Diagnostics.Metrics;
using a4c;

namespace test
{
    public class LexerTest
    {
        [Fact]
        public void Test1()
        {
            var tokens = Lexer.ProcessString("1+23 * 0/5  -9");
            var t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(1, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.False(t?.IsNumeric());
            Assert.Equal(Operation.PLUS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(23, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.False(t?.IsNumeric());
            Assert.Equal(Operation.MUL, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(0, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.False(t?.IsNumeric());
            Assert.Equal(Operation.DIV, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(5, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.False(t?.IsNumeric());
            Assert.Equal(Operation.MINUS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(9, ((NumericToken)t).GetNumericalValue());
        }
        [Fact]
        public void Test2()
        {
            var tokens = Lexer.ProcessString("123 456");
            var t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(123, ((NumericToken)t).GetNumericalValue());
            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(456, ((NumericToken)t).GetNumericalValue());
        }
        [Fact]
        public void Test3()
        {
            var tokens = Lexer.ProcessString("123 456 +");
            var t = tokens.Consume();
            t = tokens.Consume();
            t = tokens.Consume();
            Assert.False(t?.IsNumeric());
            Assert.Equal(Operation.PLUS, t?.GetOp());
        }
        [Fact]
        public void Test4()
        {
            var tokens = Lexer.ProcessString("123 456+ 5.6 .5");
            var t = tokens.Consume();
            t = tokens.Consume();
            t = tokens.Consume();
            Assert.False(t?.IsNumeric());
            Assert.Equal(Operation.PLUS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(5.6, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(0.5, ((NumericToken)t).GetNumericalValue());
        }
        [Fact]
        public void Test5()
        {
            Assert.Throws<LexerException>(() => Lexer.ProcessString("."));
        }

        [Fact]
        public void TestParentheses()
        {
            var tokens = Lexer.ProcessString("(1+2)*(3-4)");

            var t = tokens.Consume();
            Assert.Equal(Operation.OPEN_PARENTHESIS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(1, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.Equal(Operation.PLUS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(2, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.Equal(Operation.CLOSE_PARENTHESIS, t?.GetOp());

            t = tokens.Consume();
            Assert.Equal(Operation.MUL, t?.GetOp());

            t = tokens.Consume();
            Assert.Equal(Operation.OPEN_PARENTHESIS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(3, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.Equal(Operation.MINUS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(4, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.Equal(Operation.CLOSE_PARENTHESIS, t?.GetOp());
        }
        [Fact]
        public void TestWhitespaceHandling()
        {
            var tokens = Lexer.ProcessString(" \t\n  12   +\n  3\t");

            var t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(12, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.Equal(Operation.PLUS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(3, ((NumericToken)t).GetNumericalValue());
        }
        [Fact]
        public void TestUnaryMinusTokenization()
        {
            var tokens = Lexer.ProcessString("-5 + -3");

            var t = tokens.Consume();
            Assert.Equal(Operation.MINUS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(5, ((NumericToken)t).GetNumericalValue());

            t = tokens.Consume();
            Assert.Equal(Operation.PLUS, t?.GetOp());

            t = tokens.Consume();
            Assert.Equal(Operation.MINUS, t?.GetOp());

            t = tokens.Consume();
            Assert.True(t?.IsNumeric());
            Assert.NotNull(t);
            Assert.Equal(3, ((NumericToken)t).GetNumericalValue());
        }
        [Fact]
        public void TestInvalidNumbers()
        {
            Assert.Throws<LexerException>(() => Lexer.ProcessString("1..2"));
            Assert.Throws<LexerException>(() => Lexer.ProcessString("..5"));
            Assert.Throws<LexerException>(() => Lexer.ProcessString("5.3.1"));
        }
        [Fact]
        public void TestInvalidCharacters()
        {
            Assert.Throws<LexerException>(() => Lexer.ProcessString("2 + $"));
            Assert.Throws<LexerException>(() => Lexer.ProcessString("3 & 4"));
            Assert.Throws<LexerException>(() => Lexer.ProcessString("@"));
        }
        [Fact]
        public void TestFunctionName()
        {
            var tokens = Lexer.ProcessString("5 +    sqrt(5)");

            tokens.Consume();
            tokens.Consume();
            var t = tokens.Consume();
            Assert.Equal(Operation.FUNCTION, t?.GetOp());
            Assert.Equal("SQRT()", t?.ToString());
            t = tokens.Consume();
            Assert.Equal(Operation.OPEN_PARENTHESIS, t?.GetOp());
            t = tokens.Consume();
            Assert.NotNull(t);
            Assert.Equal(5, ((NumericToken)t).GetNumericalValue());

        }
        [Fact]
        public void TestConstant1()
        {
            var tokens = Lexer.ProcessString("5 +  pi");

            tokens.Consume();
            tokens.Consume();
            var t = tokens.Consume();
            Assert.Equal(Operation.CONSTANT, t?.GetOp());
            Assert.Equal("PI", t?.ToString());
        }
        [Fact]
        public void TestConstant2()
        {
            var tokens = Lexer.ProcessString("5 + 5 - e - pi");

            tokens.Consume();
            tokens.Consume();
            tokens.Consume();
            tokens.Consume();
            var t = tokens.Consume();
            Assert.Equal(Operation.CONSTANT, t?.GetOp());
            Assert.Equal("E", t?.ToString());
        }
        [Fact]
        public void TestConstant3()
        {
            Assert.Throws<TokenException>(() => Lexer.ProcessString("5 + 5 - a - pi"));
        }
        [Fact]
        public void TestConstant4()
        {
            Assert.Throws<TokenException>(() => Lexer.ProcessString("pie"));
        }
        [Fact]
        public void TestConstant5()
        {
            var tokens = Lexer.ProcessString("Pi");

            var t = tokens.Consume();
            Assert.Equal(Operation.CONSTANT, t?.GetOp());
            Assert.Equal("PI", t?.ToString());
        }
    }
}
