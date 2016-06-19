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

        private static int ConvertToInt(object value)
        {
            return (int)((value is Number) ? (value as Number).Value : value);
        }

        private static bool ConvertToBool(object value)
        {
            return (bool)((value is Bool) ? (value as Bool).Value : value);
        }

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
                return this;
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
                return this;
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
                    return (LookUp(Value) as IAST).Evaluate();
                }
                catch (KeyNotFoundException ex)
                {
                    Scanner.yyerror(UNDEFINED_ERROR);
                    YYAbort();
                }
                catch (NullReferenceException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
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
                        finalResult += ConvertToInt(item.Evaluate());
                    }
                    catch (InvalidCastException ex)
                    {
                        Scanner.yyerror(TYPE_ERROR);
                        YYAbort();
                    }
                }

                return new Number(Scanner, finalResult);
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

                if (Values.Count != 2)
                {
                    Scanner.yyerror("Syntax error");
                    YYAbort();
                }

                try
                {
                    finalResult = ConvertToInt(Values[0].Evaluate()) - ConvertToInt(Values[1].Evaluate());
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return new Number(Scanner, finalResult);
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
                        finalResult *= ConvertToInt(item.Evaluate());
                    }
                    catch (InvalidCastException ex)
                    {
                        Scanner.yyerror(TYPE_ERROR);
                        YYAbort();
                    }
                }

                return new Number(Scanner, finalResult);
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
                    finalResult = ConvertToInt(Values[0].Evaluate()) / ConvertToInt(Values[1].Evaluate());
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return new Number(Scanner, finalResult);
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
                    finalResult = ConvertToInt(Values[0].Evaluate()) % ConvertToInt(Values[1].Evaluate());
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return new Number(Scanner, finalResult);
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

                if (Values.Count != 2)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    finalResult = ConvertToInt(Values[0].Evaluate()) > ConvertToInt(Values[1].Evaluate());
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return new Bool(Scanner, finalResult);
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
                    finalResult = ConvertToInt(Values[0].Evaluate()) < ConvertToInt(Values[1].Evaluate());
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return new Bool(Scanner, finalResult);
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
                bool finalResult = true;

                if (Values.Count < 2)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    for (int i = 0; i < Values.Count - 1; i++)
                    {
                        finalResult &= ConvertToInt(Values[0].Evaluate()) == ConvertToInt(Values[1].Evaluate());
                    }
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return new Bool(Scanner, finalResult);
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

                if (Values.Count < 2)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                try
                {
                    foreach (var item in Values)
                    {
                        finalResult &= ConvertToBool(item.Evaluate());
                    }
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return new Bool(Scanner, finalResult);
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
                    foreach (var item in Values)
                    {
                        finalResult |= ConvertToBool(item.Evaluate());
                    }
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return new Bool(Scanner, finalResult);
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
                    finalResult = !ConvertToBool(Values[0].Evaluate());
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }

                return new Bool(Scanner, finalResult);
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
                    return ConvertToBool(Values[0].Evaluate()) ? Values[1].Evaluate() : Values[2].Evaluate();
                }
                catch (InvalidCastException ex)
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
                    Add(Name, Values[0]);
                }
                catch (ArgumentException ex)
                {
                    Scanner.yyerror(REDEFINE_ERROR);
                    YYAbort();
                }

                return null;
            }
        }

        internal sealed class Function : AST
        {
            public Function(AbstractScanner<ValueType, LexLocation> scanner, List<string> param, List<IAST> body) : base(scanner)
            {
                Param = param;
                Body = body;
                LocalEnvironment = new Dictionary<string, IAST>();
            }

            public Dictionary<string, IAST> LocalEnvironment;
            public List<string> Param { get; set; }
            public List<IAST> Body { get; set; }

            public override object Evaluate()
            {
                try
                {
                    foreach(var item in LocalEnvironment.Keys)
                    {
                        Add(item, LocalEnvironment[item] as IAST);
                    }                    

                    for (int i = 0; i < Body.Count - 1; i++)
                    {
                        Body[i].Evaluate();
                    }
                }
                catch(NullReferenceException ex)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }

                if (Body[Body.Count - 1] is Function)
                {
                    var currentEnvironment = GetCurrentEnvironment();
                    var innerFunction = Body[Body.Count - 1] as Function;
                    foreach (var item in Param)
                    {
                        innerFunction.LocalEnvironment.Add(item, currentEnvironment.LookUp(item) as IAST);
                    }
                    return Body[Body.Count - 1];
                }
                else
                    return Body[Body.Count - 1].Evaluate();
            }
        }

        internal sealed class FunctionCall : AST
        {
            public FunctionCall(AbstractScanner<ValueType, LexLocation> scanner, IAST function, List<IAST> param) : base(scanner)
            {
                Function = function;
                Param = param;
                FunctionName = null;
            }

            public FunctionCall(AbstractScanner<ValueType, LexLocation> scanner, string functionName, List<IAST> param) : base(scanner)
            {
                Param = param;
                FunctionName = functionName;
            }

            public string FunctionName;
            public IAST Function { get; set; }
            public List<IAST> Param { get; set; }

            public override object Evaluate()
            {
                if (FunctionName != null)
                    Function = LookUp(FunctionName) as IAST;
                
                /*
                if (Param.Count != func.Param.Count)
                {
                    Scanner.yyerror(SYNTAX_ERROR);
                    YYAbort();
                }
                */

                var env = new Environment();

                if(Function is FunctionCall)
                {
                    Function = Function.Evaluate() as IAST;
                }

                var func = Function as Function;

                for (int i = 0; i < Param.Count; i++)
                {
                    var key = func.Param[i];
                    IAST value = null;
                    if (Param[i] is Function)
                    {
                        value = Param[i];
                    }
                    else
                    {
                        value = Param[i].Evaluate() as IAST;
                    }                    
                    env.Add(key, value);
                }

                PushEnvironment(env);

                if (FunctionName != null)
                    Add(FunctionName, Function);

                var ret = func.Evaluate();

                PopEnvironment();

                return ret;
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
                try
                {
                    Console.WriteLine(ConvertToInt(Value.Evaluate()));
                }
                catch (InvalidCastException ex)
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
                    Console.WriteLine(ConvertToBool(Value.Evaluate()) ? "#t" : "#f");
                }
                catch (InvalidCastException ex)
                {
                    Scanner.yyerror(TYPE_ERROR);
                    YYAbort();
                }
                return null;
            }
        }
    }
}
