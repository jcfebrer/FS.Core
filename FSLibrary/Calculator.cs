#region

using FSException;
using System;
using System.Collections;

#endregion

namespace FSLibrary
{

    /// <summary>
    /// Calculadora científica
    /// </summary>
    public class Calculator
    {
        private const string ALPHA = "_ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string DIGITS = "#0123456789";

        private readonly string[] m_funcs =
        {
            "sin", "cos", "tan", "arcsin", "arccos", "arctan", "sqrt", "max", "min",
            "floor", "ceiling", "log", "log10", "ln", "round", "abs", "neg", "pos"
        };

        private readonly Stack m_stack = new Stack();
        private string m_colstring;
        private ArrayList m_operators;
        private int[,] m_State;
        private ArrayList m_tokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="Calculator"/> class.
        /// </summary>
        public Calculator()
        {
            init();
        }

        private void init_operators()
        {
            DBSymbol op = null;

            m_operators = new ArrayList();

            op = new DBSymbol();
            op.Token = "-";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL1;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "+";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL1;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "*";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL2;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "/";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL2;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = @"\";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL2;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "%";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL2;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "^";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL3;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "!";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL5;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "&";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL5;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "-";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL4;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "+";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL4;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = "(";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL5;
            m_operators.Add(op);

            op = new DBSymbol();
            op.Token = ")";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL0;
            m_operators.Add(op);

            m_operators.Sort(op);
        }


        /// <summary>
        /// Evalua la expresión
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public double evaluate(string expression)
        {
            Queue symbols = null;

            try
            {
                if (NumberUtils.IsNumeric(expression)) return double.Parse(expression);

                calc_scan(expression, ref symbols);

                return level0(ref symbols);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil("Invalid expression.", ex);
            }
        }


        private double calc_op(DBSymbol op, double operand1, double operand2)
        {
            switch (op.Token.ToLower())
            {
                case "&":
                    return 5;

                case "^":
                    return Math.Pow(operand1, operand2);

                case "+":

                    switch (op.PrecedenceLevel)
                    {
                        case PRECEDENCE.LEVEL1:
                            return operand2 + operand1;
                        case PRECEDENCE.LEVEL4:
                            return operand1;
                    }


                    break;
                case "-":
                    switch (op.PrecedenceLevel)
                    {
                        case PRECEDENCE.LEVEL1:
                            return operand1 - operand2;
                        case PRECEDENCE.LEVEL4:
                            return -1 * operand1;
                    }


                    break;
                case "*":
                    return operand2 * operand1;

                case "/":
                    return operand1 / operand2;

                case @"\":
                    return Convert.ToInt64(operand1) / Convert.ToInt64(operand2);

                case "%":
                    return operand1 % operand2;

                case "!":
                    var i = 0;
                    double res = 1;

                    if (operand1 > 1)
                        for (i = Convert.ToInt32(operand1); i >= 1; i += -1)
                            res = res * i;
                    return res;
            }


            return 0;
        }

        private double calc_op(DBSymbol op, double operand1)
        {
            return calc_op(op, operand1, 0);
        }


        private double calc_function(string func, ArrayList args)
        {
            switch (func.ToLower())
            {
                case "cos":
                    return Math.Cos(Convert.ToDouble(args[1]));

                case "sin":
                    return Math.Sin(Convert.ToDouble(args[1]));

                case "tan":
                    return Math.Tan(Convert.ToDouble(args[1]));

                case "floor":
                    return Math.Floor(Convert.ToDouble(args[1]));

                case "ceiling":
                    return Math.Ceiling(Convert.ToDouble(args[1]));

                case "max":
                    return Math.Max(Convert.ToDouble(args[1]), Convert.ToDouble(args[2]));

                case "min":
                    return Math.Min(Convert.ToDouble(args[1]), Convert.ToDouble(args[2]));

                case "arcsin":
                    return Math.Asin(Convert.ToDouble(args[1]));


                case "arccos":
                    return Math.Acos(Convert.ToDouble(args[1]));

                case "arctan":
                    return Math.Atan(Convert.ToDouble(args[1]));


                case "sqrt":
                    return Math.Sqrt(Convert.ToDouble(args[1]));

                case "log":
                    return Math.Log10(Convert.ToDouble(args[1]));


                case "log10":
                    return Math.Log10(Convert.ToDouble(args[1]));


                case "abs":
                    return Math.Abs(Convert.ToDouble(args[1]));


                case "round":
                    return Math.Round(Convert.ToDouble(args[1]));

                case "ln":
                    return Math.Log(Convert.ToDouble(args[1]));

                case "neg":
                    return -1 * Convert.ToDouble(args[1]);

                case "pos":
                    return +1 * Convert.ToDouble(args[1]);
            }


            return 0;
        }


        private double identifier(string token)
        {
            switch (token.ToLower())
            {
                case "e":
                    return Math.E;
                case "pi":
                    return Math.PI;
            }

            return 0;
        }


        private bool is_operator(string token, PRECEDENCE level, ref DBSymbol operatorIdent)
        {
            try
            {
                var op = new DBSymbol();
                op.Token = token;
                op.PrecedenceLevel = level;
                op.tag = "test";

                var ir = m_operators.BinarySearch(op, op);

                if (ir > -1)
                {
                    operatorIdent = (DBSymbol) m_operators[ir];
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool is_operator(string token)
        {
            return is_operator(token, (PRECEDENCE) (-1));
        }

        private bool is_operator(string token, PRECEDENCE level)
        {
            DBSymbol transTemp0 = null;
            return is_operator(token, level, ref transTemp0);
        }


        private bool is_function(string token)
        {
            try
            {
                var lr = Array.BinarySearch(m_funcs, token.ToLower());

                return lr > -1;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Calculates the scan.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="symbols">The symbols.</param>
        /// <returns></returns>
        public bool calc_scan(string line, ref Queue symbols)
        {
            var sym = new DBSymbol();
            var sp = 0;
            var cp = 0;
            var col = 0;
            var lex_state = 0;
            var cc = char.MinValue;

            symbols = new Queue();

            line = line + " ";

            sp = 0;
            cp = 0;
            lex_state = 1;


            while (cp <= line.Length - 1)
            {
                cc = line[cp];

                col = m_colstring.IndexOf(cc) + 3;

                var selectVal = col;
                if (selectVal == 2)
                {
                    if (ALPHA.IndexOf(char.ToUpper(cc)) > 0)
                        col = 1;
                    else if (DIGITS.IndexOf(char.ToUpper(cc)) > 0)
                        col = 2;
                    else
                        col = 6;
                }
                else if (selectVal > 5)
                {
                    col = 7;
                }


                lex_state = m_State[lex_state - 1, col - 1];

                switch (lex_state)
                {
                    case 3:
                    {
                        sym.Token = line.Substring(sp, cp - sp);
                        if (is_function(sym.Token))
                            sym.Cls = TOKENCLASS.KEYWORD;
                        else
                            sym.Cls = TOKENCLASS.IDENTIFIER;

                        symbols.Enqueue(sym);

                        lex_state = 1;
                        cp = cp - 1;
                    }
                        break;
                    case 5:
                    {
                        sym.Token = line.Substring(sp, cp - sp);
                        sym.Cls = TOKENCLASS.NUMBER;

                        symbols.Enqueue(sym);

                        lex_state = 1;
                        cp = cp - 1;
                    }
                        break;
                    case 6:
                    {
                        sym.Token = line.Substring(sp, cp - sp + 1);
                        sym.Cls = TOKENCLASS.PUNCTUATION;

                        symbols.Enqueue(sym);

                        lex_state = 1;
                    }
                        break;
                    case 7:
                    {
                        sym.Token = line.Substring(sp, cp - sp + 1);
                        sym.Cls = TOKENCLASS.OPERATOR;

                        symbols.Enqueue(sym);

                        lex_state = 1;
                    }
                        break;
                }


                cp += 1;
                if (lex_state == 1) sp = cp;
            }

            return true;
        }


        private void init()
        {
            int[,] state =
            {
                {2, 4, 1, 1, 4, 6, 7}, {2, 3, 3, 3, 3, 3, 3}, {1, 1, 1, 1, 1, 1, 1},
                {2, 4, 5, 5, 4, 5, 5},
                {1, 1, 1, 1, 1, 1, 1}, {1, 1, 1, 1, 1, 1, 1}, {1, 1, 1, 1, 1, 1, 1}
            };

            init_operators();


            m_State = state;
            m_colstring = Convert.ToChar(9) + " " + ".()";
            foreach (DBSymbol op in m_operators) m_colstring = m_colstring + op.Token;


            Array.Sort(m_funcs);
            m_tokens = new ArrayList();
        }

        #region Nested type: DBSymbol

        private class DBSymbol : IComparer
        {
            #region Delegates

            public delegate int compare_function(object x, object y);

            #endregion

            public TOKENCLASS Cls;
            public PRECEDENCE PrecedenceLevel;
            public string tag;
            public string Token;

            // interface methods implemented by compare

            #region IComparer Members

            int IComparer.Compare(object x, object y)
            {
                return compare(x, y);
            }

            #endregion

            public virtual int compare(object x, object y)
            {
                DBSymbol asym = null, bsym = null;
                asym = (DBSymbol) x;
                bsym = (DBSymbol) y;


                if (double.Parse(asym.Token) > double.Parse(bsym.Token)) return 1;

                if (double.Parse(asym.Token) < double.Parse(bsym.Token)) return -1;

                if ((Convert.ToDouble(asym.PrecedenceLevel) == -1) |
                    (Convert.ToDouble(bsym.PrecedenceLevel) == -1)) return 0;

                if (Convert.ToInt64(asym.PrecedenceLevel) > Convert.ToInt64(bsym.PrecedenceLevel)) return 1;

                if (Convert.ToInt64(asym.PrecedenceLevel) < Convert.ToInt64(bsym.PrecedenceLevel)) return -1;

                return 0;
            }
        }

        #endregion

        #region Nested type: PRECEDENCE

        private enum PRECEDENCE
        {
            NONE = 0,
            LEVEL0 = 1,
            LEVEL1 = 2,
            LEVEL2 = 3,
            LEVEL3 = 4,
            LEVEL4 = 5,
            LEVEL5 = 6
        }

        #endregion

        #region Nested type: TOKENCLASS

        private enum TOKENCLASS
        {
            KEYWORD = 1,
            IDENTIFIER = 2,
            NUMBER = 3,
            OPERATOR = 4,
            PUNCTUATION = 5
        }

        #endregion

        #region '"Recusrsive Descent Parsing Functions"' 

        private double level0(ref Queue tokens)
        {
            return level1(ref tokens);
        }


        private double level1_prime(ref Queue tokens, double result)
        {
            DBSymbol symbol = null;
            DBSymbol operatorIdent = null;

            if (tokens.Count > 0)
                symbol = (DBSymbol) tokens.Peek();
            else
                return result;

            if (is_operator(symbol.Token, PRECEDENCE.LEVEL1, ref operatorIdent))
            {
                tokens.Dequeue();
                result = calc_op(operatorIdent, result, level2(ref tokens));
                result = level1_prime(ref tokens, result);
            }


            return result;
        }


        private double level1(ref Queue tokens)
        {
            return level1_prime(ref tokens, level2(ref tokens));
        }


        private double level2(ref Queue tokens)
        {
            return level2_prime(ref tokens, level3(ref tokens));
        }


        private double level2_prime(ref Queue tokens, double result)
        {
            DBSymbol symbol = null;
            DBSymbol operatorIdent = null;

            if (tokens.Count > 0)
                symbol = (DBSymbol) tokens.Peek();
            else
                return result;


            if (is_operator(symbol.Token, PRECEDENCE.LEVEL2, ref operatorIdent))
            {
                tokens.Dequeue();
                result = calc_op(operatorIdent, result, level3(ref tokens));
                result = level2_prime(ref tokens, result);
            }

            return result;
        }


        private double level3(ref Queue tokens)
        {
            return level3_prime(ref tokens, level4(ref tokens));
        }


        private double level3_prime(ref Queue tokens, double result)
        {
            DBSymbol symbol = null;
            DBSymbol operatorIdent = null;

            if (tokens.Count > 0)
                symbol = (DBSymbol) tokens.Peek();
            else
                return result;


            if (is_operator(symbol.Token, PRECEDENCE.LEVEL3, ref operatorIdent))
            {
                tokens.Dequeue();
                result = calc_op(operatorIdent, result, level4(ref tokens));
                result = level3_prime(ref tokens, result);
            }


            return result;
        }


        private double level4(ref Queue tokens)
        {
            return level4_prime(ref tokens);
        }


        private double level4_prime(ref Queue tokens)
        {
            DBSymbol symbol = null;
            DBSymbol operatorIdent = null;

            if (tokens.Count > 0)
                symbol = (DBSymbol) tokens.Peek();
            else
                throw new ExceptionUtil("Invalid expression.");


            if (is_operator(symbol.Token, PRECEDENCE.LEVEL4, ref operatorIdent))
            {
                tokens.Dequeue();
                return calc_op(operatorIdent, level5(tokens), 0);
            }

            return level5(tokens);
        }


        private double level5(Queue tokens)
        {
            return level5_prime(tokens, level6(ref tokens));
        }


        private double level5_prime(Queue tokens, double result)
        {
            DBSymbol symbol = null;
            DBSymbol operatorIdent = null;

            if (tokens.Count > 0)
                symbol = (DBSymbol) tokens.Peek();
            else
                return result;


            if (is_operator(symbol.Token, PRECEDENCE.LEVEL5, ref operatorIdent))
            {
                tokens.Dequeue();
                return calc_op(operatorIdent, result, 0);
            }

            return result;
        }


        private double level6(ref Queue tokens)
        {
            DBSymbol symbol = null;

            if (tokens.Count > 0)
                symbol = (DBSymbol) tokens.Peek();
            else
                throw new ExceptionUtil("Invalid expression.");

            double val = 0;


            if (symbol.Token == "(")
            {
                tokens.Dequeue();
                val = level0(ref tokens);

                symbol = (DBSymbol) tokens.Dequeue();
                if (symbol.Token != ")") throw new ExceptionUtil("Invalid expression.");

                return val;
            }

            switch (symbol.Cls)
            {
                case TOKENCLASS.IDENTIFIER:
                    tokens.Dequeue();
                    return identifier(symbol.Token);

                case TOKENCLASS.KEYWORD:
                    tokens.Dequeue();
                    return calc_function(symbol.Token, arguments(tokens));
                case TOKENCLASS.NUMBER:

                    tokens.Dequeue();
                    m_stack.Push(double.Parse(symbol.Token));
                    return double.Parse(symbol.Token);

                default:
                    throw new ExceptionUtil("Invalid expression.");
            }
        }


        private ArrayList arguments(Queue tokens)
        {
            DBSymbol symbol = null;
            var args = new ArrayList();

            if (tokens.Count > 0)
                symbol = (DBSymbol) tokens.Peek();
            else
                throw new ExceptionUtil("Invalid expression.");

            if (symbol.Token == "(")
            {
                tokens.Dequeue();
                args.Add(level0(ref tokens));

                symbol = (DBSymbol) tokens.Dequeue();
                while (symbol.Token != ")")
                {
                    if (symbol.Token == ",")
                        args.Add(level0(ref tokens));
                    else
                        throw new ExceptionUtil("Invalid expression.");
                    symbol = (DBSymbol) tokens.Dequeue();
                }

                return args;
            }

            throw new ExceptionUtil("Invalid expression.");
        }

        #endregion
    }
}