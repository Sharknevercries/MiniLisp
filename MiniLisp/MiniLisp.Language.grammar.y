%namespace MiniLisp
%partial
%parsertype MiniLispParser
%visibility internal
%tokentype Token

%union { 
			public int Value; 
			public bool BoolValue;
			public string Str;
			public List<string> StrList;
			public MiniLispParser.IAST Node;
			public List<MiniLispParser.IAST> NodeList;
	   }

%start program

%token PrintNum, PrintBool
%token Lp, Rp
%token Plus, Minus, Multiply, Divide, Modulus
%token Smaller, Greater, Equal, And, Or, Not
%token Fun, Define, If

%type <NodeList> exps, def_stmts, fun_body
%type <StrList> strs
%type <Node> stmt, exp, print_stmt, def_stmt, number_op, logic_op, if_exp, fun_exp, fun_call

%token <Value> Number
%token <BoolValue> Bool
%token <Str> Str

%%

program		:	stmts
			;
stmts		:	stmts stmt
			|	stmt
			;
stmt		:	exp {
				$1.Evaluate();
			}
			|	print_stmt {
				$1.Evaluate();
			}
			|	def_stmt {
				$1.Evaluate();
			}
			;
def_stmts	:	{
				$$ = new List<IAST>();
			}
			|	def_stmts def_stmt {
				$$ = $1;
				$$.Add($2);
			}
			;
print_stmt	:	Lp PrintNum exp Rp {
				$$ = new PrintNum(Scanner, $3);
			}
			|	Lp PrintBool exp Rp {
				$$ = new PrintBool(Scanner, $3);
			}
			;
exps		:	{
				$$ = new List<IAST>();
			}
			|	exps exp {
				$$ = $1;
				$$.Add($2);
			}
			;
exp			:	Number {
				$$ = new Number(Scanner, $1);
			}
			|	Bool {
				$$ = new Bool(Scanner, $1);
			}
			|	Str {
				$$ = new Id(Scanner, $1);
			}
			|	number_op
			|	logic_op
			|	if_exp
			|	fun_exp
			|	fun_call
			;
strs		:	{
				$$ = new List<string>();
			}
			|	strs Str {
				$$ = $1;
				$$.Add($2);
			}
			;
def_stmt	:	Lp Define Str exps Rp {
				$$ = new Define(Scanner, $3, $4);
			}
			;
number_op	:	Lp Plus exps Rp {
				$$ = new Plus(Scanner, $3);
			}
			|	Lp Minus exps Rp {
				$$ = new Minus(Scanner, $3);
			}
			|	Lp Multiply exps Rp {
				$$ = new Multiply(Scanner, $3);
			}
			|	Lp Divide exps Rp {
				$$ = new Divide(Scanner, $3);
			}
			|	Lp Modulus exps Rp {
				$$ = new Modulus(Scanner, $3);
			}
			|	Lp Smaller exps Rp {
				$$ = new Smaller(Scanner, $3);
			}
			|	Lp Greater exps Rp {
				$$ = new Greater(Scanner, $3);
			}
			|	Lp Equal exps Rp {
				$$ = new Equal(Scanner, $3);
			}
			;
logic_op	:	Lp And exps Rp {
				$$ = new And(Scanner, $3);
			}
			|	Lp Or exps Rp {
				$$ = new Or(Scanner, $3);
			}
			|	Lp Not exps Rp {
				$$ = new Not(Scanner, $3);
			}
			;
if_exp		:	Lp If exps Rp {
				$$ = new If(Scanner, $3);
			}
			;
fun_exp		:	Lp Fun Lp strs Rp fun_body Rp {
				$$ = new Function(Scanner, $4, $6);
			}
			;
fun_body	:	def_stmts exp {
				$$ = $1;
				$$.Add($2);
			}
			;
fun_call	:   Lp fun_exp exps Rp {
				$$ = new FunctionCall(Scanner, $2, $3);
			}
			|	Lp Str exps Rp {
				$$ = new FunctionCall(Scanner, $2, $3);
			}
			;
%%