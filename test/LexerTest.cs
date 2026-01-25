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
            Assert.True(t.IsNumeric());
            Assert.Equal(1, t.GetValue());

            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(Operation.PLUS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(23, t.GetValue());

            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(Operation.MUL, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(0, t.GetValue());

            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(Operation.DIV, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(5, t.GetValue());

            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(Operation.MINUS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(9, t.GetValue());
        }
        [Fact]
        public void Test2()
        {
            var tokens = Lexer.ProcessString("123 456");
            var t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(123, t.GetValue());
            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(456, t.GetValue());
        }
        [Fact]
        public void Test3()
        {
            var tokens = Lexer.ProcessString("123 456 +");
            var t = tokens.Consume();
            t = tokens.Consume();
            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(Operation.PLUS, t.GetOp());
        }
        [Fact]
        public void Test4()
        {
            var tokens = Lexer.ProcessString("123 456+ 5.6 .5");
            var t = tokens.Consume();
            t = tokens.Consume();
            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(Operation.PLUS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(5.6m, t.GetValue());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(0.5m, t.GetValue());
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
            Assert.Equal(Operation.OPEN_PARENTHESIS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(1, t.GetValue());

            t = tokens.Consume();
            Assert.Equal(Operation.PLUS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(2, t.GetValue());

            t = tokens.Consume();
            Assert.Equal(Operation.CLOSE_PARENTHESIS, t.GetOp());

            t = tokens.Consume();
            Assert.Equal(Operation.MUL, t.GetOp());

            t = tokens.Consume();
            Assert.Equal(Operation.OPEN_PARENTHESIS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(3, t.GetValue());

            t = tokens.Consume();
            Assert.Equal(Operation.MINUS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(4, t.GetValue());

            t = tokens.Consume();
            Assert.Equal(Operation.CLOSE_PARENTHESIS, t.GetOp());
        }
        [Fact]
        public void TestWhitespaceHandling()
        {
            var tokens = Lexer.ProcessString(" \t\n  12   +\n  3\t");

            var t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(12, t.GetValue());

            t = tokens.Consume();
            Assert.Equal(Operation.PLUS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(3, t.GetValue());
        }
        [Fact]
        public void TestUnaryMinusTokenization()
        {
            var tokens = Lexer.ProcessString("-5 + -3");

            var t = tokens.Consume();
            Assert.Equal(Operation.MINUS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(5, t.GetValue());

            t = tokens.Consume();
            Assert.Equal(Operation.PLUS, t.GetOp());

            t = tokens.Consume();
            Assert.Equal(Operation.MINUS, t.GetOp());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(3, t.GetValue());
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
            Assert.Throws<LexerException>(() => Lexer.ProcessString("2 + a"));
            Assert.Throws<LexerException>(() => Lexer.ProcessString("3 & 4"));
            Assert.Throws<LexerException>(() => Lexer.ProcessString("@"));
        }
    }
}
