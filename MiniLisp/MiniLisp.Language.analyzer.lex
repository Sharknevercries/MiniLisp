%namespace MiniLisp
%scannertype MiniLispScanner
%visibility internal
%tokentype Token

%option stack, minimize, parser, verbose, persistbuffer, noembedbuffers 

Seperator		[\t\n\r ]
Letter			[a-z]
Digit			[0-9]

Number			0|[1-9]{Digit}*|-[1-9]{Digit}*
Id				{Letter}({Letter}|{Digit}|-)*
Bool			#[tf]

Lp				\(
Rp				\)
Plus			"+"
Minus			"-"
Mutiply		    "*"
Divide			"/"
Modulus		    mod
Smaller			"<"
Greater		    ">"
Equal			"="
And				"and"
Or				"or"
Not				"not"
If				"if"
Define			"define"
Fun				"fun"
PrintNum		"print-num"
PrintBool		"print-bool"

%{

%}

%%

{Number}		{	PrintCacheToken(yytext);	GetNumber();	return (int)Token.Number;	}
{Plus}			{	PrintCacheToken(yytext);	return (int)Token.Plus;	}
{Minus}			{	PrintCacheToken(yytext);	return (int)Token.Minus;	}
{Mutiply}		{	PrintCacheToken(yytext);	return (int)Token.Multiply;	}
{Divide}		{	PrintCacheToken(yytext);	return (int)Token.Divide;	}
{Modulus}		{	PrintCacheToken(yytext);	return (int)Token.Modulus;	}
{Bool}			{	PrintCacheToken(yytext);	GetBoolean();	return (int)Token.Bool;	}
{Smaller}		{	PrintCacheToken(yytext);	return (int)Token.Smaller;	}
{Greater}		{	PrintCacheToken(yytext);	return (int)Token.Greater;	}
{Equal}			{	PrintCacheToken(yytext);	return (int)Token.Equal;	}
{And}			{	PrintCacheToken(yytext);	return (int)Token.And;	}
{Or}			{	PrintCacheToken(yytext);	return (int)Token.Or;	}
{Not}			{	PrintCacheToken(yytext);	return (int)Token.Not;	}

{PrintNum}		{	PrintCacheToken(yytext);	return (int)Token.PrintNum;	}
{PrintBool}		{	PrintCacheToken(yytext);	return (int)Token.PrintBool;	}
{Lp}			{	return (int)Token.Lp;	}
{Rp}			{	return (int)Token.Rp;	}


{Seperator}		{	}
.				{	}


%%