using QUT.Gppg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLisp
{
    internal partial class MiniLispParser
    {
        private readonly static string TYPE_ERROR = "Type Error";
        private readonly static string SYNTAX_ERROR = "Syntax Error";
        private readonly static string UNDEFINED_ERROR = "Undefined Error";
        private readonly static string REDEFINE_ERROR = "Redefined Error";

        internal interface IAST
        {
            object Evaluate();
        }

        internal abstract class AST : IAST
        {
            public AST(AbstractScanner<ValueType, LexLocation> scanner) { Scanner = scanner; }

            protected AbstractScanner<ValueType, LexLocation> Scanner;

            public abstract object Evaluate();
        }

        internal sealed class Number : AST
        {
            public int Value { get; set; }

            public Number(AbstractScanner<ValueType, LexLocation> scanner, int value) : base(scanner)
            {
                Value = value;
            }

            public override object Evaluate()
            {
                return Value;
            }
        }

        internal sealed class Bool : AST
        {
            public Bool(AbstractScanner<ValueType, LexLocation> scanner, bool value) : base(scanner)
            {
                Value = value;
            }

            public bool Value { get; set; }

            public override object Evaluate()
            {
                return Value;
            }
        }

        internal sealed class Id : AST
        {
            public Id(AbstractScanner<ValueType, LexLocation> scanner, string value) : base(scanner)
            {
                Value = value;
            }

            public string Value { get; set; }

            public override object Evaluate()
            {
                try
                {
                    var env = GetCurrentEnvironment();
                    var ret = env.LookUp(Value);
                    // TODO: Check whether is fun
                    return ret.Evaluate();
                }
                catch(Exception ex)
                {
                    Scanner.yyerror(UNDEFINED_ERROR);
                    YYAbort();
                }

                return null;
            }
        }

        internal sealed class Plus : AST
        {
            public Plus(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                int finalResult = 0;
              
                if (Values.Count < 2)
                {
                    
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                foreach (var item in Values)
                {
                    try
                    {
                        finalResult += (item.Evaluate() as int?).Value;
                    }
                    catch(Exception ex)
                    {
                        Scanner.yyerror(TYPE_ERROR);
                        YYAbort();
                    }
                }

                return finalResult;
            }
        }

        internal sealed class Minus : AST
        {
            public Minus(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                int finalResult = 0;

                if(Values.Count != 2)
                {
                    Scanner.yyerror("Syntax error");
                    YYAbort();
                }

                try
                {
                    finalResult = (Values[0].Evaluate() as int?).Value - (Values[1].Evaluate() as int?).Value;
                }
                catch(Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return finalResult;
            }
        }

        internal sealed class Multiply : AST
        {
            public Multiply(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                int finalResult = 1;

                if (Values.Count < 2)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                foreach (var item in Values)
                {
                    try
                    {
                        finalResult *= (item.Evaluate() as int?).Value;
                    }
                    catch (Exception ex)
                    {
                        Scanner.yyerror(TYPE_ERROR);
                        YYAbort();
                    }
                }

                return finalResult;
            }
        }

        internal sealed class Divide : AST
        {
            public Divide(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                int finalResult = 0;

                if (Values.Count != 2)
                {
                    Scanner.yyerror("Syntax error");
                    YYAbort();
                }

                try
                {
                    finalResult = (Values[0].Evaluate() as int?).Value / (Values[1].Evaluate() as int?).Value;
                }
                catch (Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return finalResult;
            }
        }

        internal sealed class Modulus : AST
        {
            public Modulus(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                int finalResult = 0;

                if (Values.Count != 2)
                {
                    Scanner.yyerror("Syntax error");
                    YYAbort();
                }

                try
                {
                    finalResult = (Values[0].Evaluate() as int?).Value % (Values[1].Evaluate() as int?).Value;
                }
                catch (Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return finalResult;
            }
        }

        internal sealed class Greater : AST
        {
            public Greater(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                bool finalResult = false;

                if(Values.Count != 2)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    finalResult = (Values[0].Evaluate() as int?).Value > (Values[1].Evaluate() as int?).Value;
                }
                catch(Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return finalResult;
            }
        }

        internal sealed class Smaller : AST
        {
            public Smaller(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                bool finalResult = false;

                if (Values.Count != 2)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    finalResult = (Values[0].Evaluate() as int?).Value < (Values[1].Evaluate() as int?).Value;
                }
                catch (Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return finalResult;
            }
        }

        internal sealed class Equal : AST
        {
            public Equal(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                bool finalResult = false;

                if (Values.Count != 2)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    finalResult = (Values[0].Evaluate() as int?).Value == (Values[1].Evaluate() as int?).Value;
                }
                catch (Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return finalResult;
            }
        }

        internal sealed class And : AST
        {
            public And(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                bool finalResult = true;

                if(Values.Count < 2)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    for (int i = 0; i < Values.Count; i++)
                    {
                        finalResult &= (Values[i].Evaluate() as bool?).Value;
                    }
                }
                catch(Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return finalResult;
            }
        }

        internal sealed class Or : AST
        {
            public Or(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                bool finalResult = false;

                if (Values.Count < 2)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    for (int i = 0; i < Values.Count; i++)
                    {
                        finalResult |= (Values[i].Evaluate() as bool?).Value;
                    }
                }
                catch (Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return finalResult;
            }
        }

        internal sealed class Not : AST
        {
            public Not(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                bool finalResult = true;

                if (Values.Count != 1)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    finalResult = !(Values[0].Evaluate() as bool?).Value;                    
                }
                catch (Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return finalResult;
            }
        }

        internal sealed class If : AST
        {
            public If(AbstractScanner<ValueType, LexLocation> scanner, List<IAST> values) : base(scanner)
            {
                Values = values;
            }

            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                if (Values.Count != 3)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    return (Values[0].Evaluate() as bool?).Value ? Values[1].Evaluate() : Values[2].Evaluate();
                }
                catch(Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return null;
            }
        }

        internal sealed class Define : AST
        {
            public Define(AbstractScanner<ValueType, LexLocation> scanner, string name, List<IAST> values) : base(scanner)
            {
                Name = name;
                Values = values;
            }

            public string Name { get; set; }
            public List<IAST> Values { get; set; }

            public override object Evaluate()
            {
                if (Values.Count != 1)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    var env = GetCurrentEnvironment();
                    env.Add(Name, Values[0]);
                }
                catch(Exception ex)
                {
                    Scanner.yyerror(REDEFINE_ERROR);
                    YYAbort();
                }

                return null;
            }
        }



        internal sealed class PrintNum : AST
        {
            public PrintNum(AbstractScanner<ValueType, LexLocation> scanner, IAST value) : base(scanner)
            {
                Value = value;
            }

            public IAST Value { get; set; }

            public override object Evaluate()
            {
                var ret = Value.Evaluate();
                try
                {
                    Console.WriteLine((int)ret);
                }
                catch(Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }
                return null;
            }
        }

        internal sealed class PrintBool : AST
        {
            public PrintBool(AbstractScanner<ValueType, LexLocation> scanner, IAST value) : base(scanner)
            {
                Value = value;
            }

            public IAST Value { get; set; }

            public override object Evaluate()
            {
                var ret = Value.Evaluate();
                try
                {
                    Console.WriteLine((bool)ret ? "#t" : "#f");
                }
                catch (Exception ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }
                return null;
            }
        }
    }
}
