using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLisp
{
    internal partial class MiniLispScanner
    {
		public override void yyerror(string format, params object[] args)
		{
			base.yyerror(format, args);
			Console.WriteLine(format, args);
			Console.WriteLine();
		}
    }
}
