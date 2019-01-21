using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algo.Sample
{
    public class Expression
    {
        public delegate float OperatorHandler(params float[] vals);

        public enum TokenType
        {
            Operator,
            Number,
        }

        public class Token
        {
            public TokenType type;
            public char op;
            public float value;
        }

        public class Operator
        {
            //优先级
            public int priority;

            //期望的参数个数
            public int desiredParaCount;

            //操作符
            public char op;

            //计算方法
            public OperatorHandler handler;
        }

        public static class Operators
        {
            //+
            public static float Sum(params float[] vals)
            {
                return vals[0] + vals[1];
            }

            //-
            public static float Sub(params float[] vals)
            {
                return vals[0] - vals[1];
            }

            //*
            public static float Multi(params float[] vals)
            {
                return vals[0] * vals[1];
            }

            // 除法
            public static float Div(params float[] vals)
            {
                return vals[0] / vals[1];
            }
        }

        private static Dictionary<char, Operator> operators;

        static Expression()
        {
            operators = new Dictionary<char, Operator>();
            RegisteOperator('+', 5, 2, Operators.Sum);
            RegisteOperator('-', 5, 2, Operators.Sub);
            RegisteOperator('*', 4, 2, Operators.Multi);
            RegisteOperator('/', 4, 2, Operators.Div);
        }

        private static void RegisteOperator(char op, int priority, int paramCount, OperatorHandler handler)
        {
            Operator o = new Operator();
            o.op = op;
            o.priority = priority;
            o.desiredParaCount = paramCount;
            o.handler = handler;

            operators[op] = o;
        }

        //评估表达式结果
        public static float Eval(string expression)
        {
            float result = 0;
            Queue<Token> tokens = ToTokens(expression);
            Stack<Token> operandsS = new Stack<Token>();     //操作数栈
            Stack<Token> operatorsS = new Stack<Token>();    //操作符栈

            while (tokens.Count > 0)
            {
                var next = tokens.Dequeue();
                if (next.type == TokenType.Number)
                {
                    operandsS.Push(next);
                }
                else
                {
                    bool pushed = false;
                    while (operatorsS.Count > 0)
                    {
                        var top = operatorsS.Peek();
                        Operator topOp = operators[top.op];
                        Operator nextOp = operators[next.op];
                        if (nextOp.priority < topOp.priority)
                        {
                            //栈顶的优先级高于当前操作符优先级
                            pushed = true;
                            operatorsS.Push(next);
                            break;
                        }
                        else
                        {
                            operatorsS.Pop();
                            float[] vals = new float[topOp.desiredParaCount];
                            //从0-i，对应参数从左-右
                            for (int i = topOp.desiredParaCount - 1; i >= 0; i--)
                            {
                                if (operandsS.Count > 0)
                                    vals[i] = operandsS.Pop().value;
                            }
                            operandsS.Push(new Token()
                            {
                                type = TokenType.Number,
                                value = topOp.handler(vals)
                            });
                        }
                    }
                    if (!pushed) operatorsS.Push(next);
                }
            }

            while (operatorsS.Count > 0)
            {
                var next = operatorsS.Pop();
                var nextOp = operators[next.op];
                float[] vals = new float[nextOp.desiredParaCount];
                for (int i = nextOp.desiredParaCount - 1; i >= 0; i--)
                {
                    if (operandsS.Count > 0)
                        vals[i] = operandsS.Pop().value;
                }
                operandsS.Push(new Token()
                {
                    type = TokenType.Number,
                    value = nextOp.handler(vals)
                });
            }

            result = operandsS.Pop().value;

            return result;
        }

        public static Queue<Token> ToTokens(string expression)
        {
            Queue<Token> tokens = new Queue<Token>();
            StringBuilder builder = null;

            using (StringReader sr = new StringReader(expression))
            {
                int intChar = sr.Read();
                while (intChar != -1)
                {
                    char ch = Convert.ToChar(intChar);
                    bool chIsOperator = false;

                    if (ch == '+') { chIsOperator = true; }
                    else if (ch == '-') { chIsOperator = true; }
                    else if (ch == '*') { chIsOperator = true; }
                    else if (ch == '/') { chIsOperator = true; }
                    else
                    {//简化处理，除操作符之外其他都认为是操作数
                        chIsOperator = false;
                    }

                    if (chIsOperator)
                    {//当前字符是操作符
                        if (builder != null)
                        {
                            tokens.Enqueue(new Token()
                            {
                                type = TokenType.Number,
                                value = Convert.ToSingle(builder.ToString())
                            });
                            builder = null;
                        }
                        tokens.Enqueue(new Token()
                        {
                            type = TokenType.Operator,
                            op = ch
                        });
                    }
                    else
                    {//当前字符是操作数
                        if (builder == null)
                            builder = new StringBuilder();
                        builder.Append(ch);
                    }
                    intChar = sr.Read();
                }

                //扫描结束，将操作数存到队列中
                if (builder != null)
                    tokens.Enqueue(new Token()
                    {
                        type = TokenType.Number,
                        value = Convert.ToSingle(builder.ToString())
                    });
            }

            return tokens;
        }
    }
}
