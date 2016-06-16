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

%{

%}

%%

{Number}		{	}


%%