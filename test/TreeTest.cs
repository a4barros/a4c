using System.Diagnostics.Metrics;
using a4c;

namespace test
{
    public class TreeTest
    {
        [Fact]
        public void Test1()
        {
            var tree = new Tree(TokenFactory.CreateToken(5), null, null);
            Assert.Equal(5, tree.Evaluate());
        }
        [Fact]
        public void Test2()
        {
            var five = new Tree(TokenFactory.CreateToken(5), null, null);
            var ten = new Tree(TokenFactory.CreateToken(10), null, null);
            var tree = new Tree(TokenFactory.CreateToken(TokenTypeEnum.SUM), five, ten);
            Assert.Equal(15, tree.Evaluate());
        }
    }
}
