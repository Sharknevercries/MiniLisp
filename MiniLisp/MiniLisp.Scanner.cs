using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLisp
{
    internal partial class MiniLispScanner
    {
        public void GetNumber()
        {
            yylval.Value = int.Parse(yytext);
        }

        public void GetId()
        {
            yylval.Str = yytext;
        }

        public void GetBool()
        {
            yylval.BoolValue = yytext[1] == 't';
        }

        public void PrintCacheToken(string message)
        {
            //Console.WriteLine("Catch {0}", message);
        }

        public override void yyerror(string format, params object[] args)
        {
            base.yyerror(format, args);
            Console.WriteLine(format, args);
        }        
    }    
}
