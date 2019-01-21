using Algo.Sample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AlgoTest
{
    [TestClass]
    public class TestExpression
    {
        [TestMethod]
        public void TestToTokens()
        {
            string exp = "300+5*6-400/2";
            Queue<Expression.Token> queue = Expression.ToTokens(exp);

            Assert.AreEqual(9, queue.Count);
            Assert.AreEqual(Expression.TokenType.Number, queue.Peek().type);
            Assert.AreEqual(300.0f, queue.Dequeue().value);

            Assert.AreEqual(Expression.TokenType.Operator, queue.Peek().type);
            Assert.AreEqual('+', queue.Dequeue().op);

            Assert.AreEqual(Expression.TokenType.Number, queue.Peek().type);
            Assert.AreEqual(5.0f, queue.Dequeue().value);

            Assert.AreEqual(Expression.TokenType.Operator, queue.Peek().type);
            Assert.AreEqual('*', queue.Dequeue().op);

            Assert.AreEqual(Expression.TokenType.Number, queue.Peek().type);
            Assert.AreEqual(6.0f, queue.Dequeue().value);

            Assert.AreEqual(Expression.TokenType.Operator, queue.Peek().type);
            Assert.AreEqual('-', queue.Dequeue().op);

            Assert.AreEqual(Expression.TokenType.Number, queue.Peek().type);
            Assert.AreEqual(400.0f, queue.Dequeue().value);

            Assert.AreEqual(Expression.TokenType.Operator, queue.Peek().type);
            Assert.AreEqual('/', queue.Dequeue().op);

            Assert.AreEqual(Expression.TokenType.Number, queue.Peek().type);
            Assert.AreEqual(2.0f, queue.Dequeue().value);
        }

        [TestMethod]
        public void TestExpressionEval()
        {
            string exp = "3+5";
            float result = Expression.Eval(exp);
            Assert.AreEqual(8, result);

            string exp2 = "1+2+3+4+5+6";
            Assert.AreEqual(21, Expression.Eval(exp2));

            string exp3 = "300+5*6-400/2";
            Assert.AreEqual(130, Expression.Eval(exp3));

            string exp4 = "3.5+2.2";
            Assert.AreEqual(5.7f, Expression.Eval(exp4));
        }
    }
}
