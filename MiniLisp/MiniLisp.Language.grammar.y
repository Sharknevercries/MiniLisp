%namespace MiniLisp
%partial
%parsertype MiniLispParser
%visibility internal
%tokentype Token

%union { 
			public int Value; 
			public bool BoolValue;
			public string Str; 
			public MiniLispParser.IAST Node;
			public List<MiniLispParser.IAST> NodeList;
	   }

%start program

%token PrintNum, PrintBool
%token Lp, Rp
%token Plus, Minus, Multiply, Divide, Modulus
%token Smaller, Greater, Equal, And, Or, Not

%type <NodeList> exps
%type <Node> exp, print_stmt, def_stmt, number_op, logic_op

%token <Value> Number
%token <BoolValue> Bool
%token <Str> Str


%%

program		:	stmts
			;
stmts		:	stmts stmt
			|	stmt
			;
stmt		:	exp	{
			}
			|	print_stmt {
				$1.Evaluate();
			}
			|	def_stmt {
			}
			;
print_stmt	:	Lp PrintNum exp Rp {
				$$ = new PrintNum(Scanner, $3);
			}
			|	Lp PrintBool exp Rp {
				$$ = new PrintBool(Scanner, $3);
			}
			;
def_stmt	:	
			;
exps		:	exps exp {
				$$ = $1;
				$$.Add($2);
			}
			|	exp {
				$$ = new List<IAST>();
				$$.Add($1);
			}
			;
exp			:	Number {
				$$ = new Number($1);
			}
			|	Bool {
				$$ = new Bool($1);
			}
			|	number_op
			|	logic_op
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
%%