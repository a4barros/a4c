using System.Diagnostics.Metrics;
using a4c;

namespace test
{
    public class TreeTest
    {
        [Fact]
        public void Test1()
        {
            var tree = new NumberNode(5);
            Assert.Equal(5, tree.Evaluate());
        }
        [Fact]
        public void Test2()
        {
            var five = new NumberNode(5);
            var ten = new NumberNode(10);
            var n15 = new BinaryNode(Operation.PLUS, five, ten);
            var n150 = new BinaryNode(Operation.MUL, n15, ten);
            var n30 = new BinaryNode(Operation.DIV, n150, five);
            var m30 = new UnaryNode(Operation.NEGATIVE, n30);
            Assert.Equal(-30, m30.Evaluate());
        }
        [Fact]
        public void TestBinaryOperators()
        {
            var a = new NumberNode(8);
            var b = new NumberNode(2);

            Assert.Equal(10, new BinaryNode(Operation.PLUS, a, b).Evaluate());
            Assert.Equal(6, new BinaryNode(Operation.MINUS, a, b).Evaluate());
            Assert.Equal(16, new BinaryNode(Operation.MUL, a, b).Evaluate());
            Assert.Equal(4, new BinaryNode(Operation.DIV, a, b).Evaluate());
        }
        [Fact]
        public void TestComplexExpression()
        {
            var expr =
                new BinaryNode(
                    Operation.DIV,
                    new BinaryNode(
                        Operation.MUL,
                        new BinaryNode(
                            Operation.PLUS,
                            new NumberNode(2),
                            new NumberNode(3)
                        ),
                        new BinaryNode(
                            Operation.MINUS,
                            new NumberNode(4),
                            new NumberNode(1)
                        )
                    ),
                    new NumberNode(5)
                );

            Assert.Equal(3, expr.Evaluate());
        }
        [Fact]
        public void TestDoubleUnary()
        {
            var expr = new UnaryNode(
                Operation.NEGATIVE,
                new UnaryNode(
                    Operation.NEGATIVE,
                    new NumberNode(5)
                )
            );

            Assert.Equal(5, expr.Evaluate());
        }
        [Fact]
        public void TestDivisionByZero()
        {
            var expr = new BinaryNode(
                Operation.DIV,
                new NumberNode(5),
                new NumberNode(0)
            );

            Assert.Throws<TreeException>(() => expr.Evaluate());
        }
        [Fact]
        public void TestStressLargeTree()
        {
            INode node = new NumberNode(1);

            for (int i = 0; i < 1000; i++)
            {
                node = new BinaryNode(Operation.PLUS, node, new NumberNode(1));
            }

            Assert.Equal(1001, node.Evaluate());
        }
    }
}
