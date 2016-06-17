using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using QUT.Gppg;

namespace MiniLisp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new MiniLispParser();

            try
            {
                using(StreamReader sr = new StreamReader("Test.txt"))
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
