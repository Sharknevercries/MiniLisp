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
    [TestClass]
    public class ParserTest
    {
        private MiniLispParser parser;

        [ClassInitialize]
        public void Initialize()
        {
            parser = new MiniLispParser();
        }

        [TestInitialize]
        public void InitializeTest()
        {
            StreamWriter standardOut = new StreamWriter(Console.OpenStandardOutput());
            standardOut.AutoFlush = true;
            Console.SetOut(standardOut);
        }

        [TestMethod]
        public void TestFile01_1()
        {
            using (StreamReader sr = new StreamReader("Test.txt"))
            {
                parser.Parse(sr.ReadToEnd());
            }
            Assert.AreEqual<int>(1, 1);
        }
    }

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
