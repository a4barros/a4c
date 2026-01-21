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
            Assert.Equal(TokenTypeEnum.SUM, t.GetTokenType());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(23, t.GetValue());

            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(TokenTypeEnum.MUL, t.GetTokenType());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(0, t.GetValue());

            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(TokenTypeEnum.DIV, t.GetTokenType());

            t = tokens.Consume();
            Assert.True(t.IsNumeric());
            Assert.Equal(5, t.GetValue());

            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(TokenTypeEnum.MINUS, t.GetTokenType());

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
            Assert.Equal(TokenTypeEnum.SUM, t.GetTokenType());
        }
        [Fact]
        public void Test4()
        {
            var tokens = Lexer.ProcessString("123 456+");
            var t = tokens.Consume();
            t = tokens.Consume();
            t = tokens.Consume();
            Assert.False(t.IsNumeric());
            Assert.Equal(TokenTypeEnum.SUM, t.GetTokenType());
        }
    }
}
