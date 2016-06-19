using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using QUT.Gppg;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiniLisp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new MiniLispParser();

            try
            {
                using(StreamReader sr = new StreamReader("./testcase/b4_2.lsp"))
                {
                    parser.Parse(sr.ReadToEnd());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
