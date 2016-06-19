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
        private static MiniLispParser parser;
        private static StringWriter standardOut;

        [TestInitialize]
        public void Initialize()
        {
            parser = new MiniLispParser();
            standardOut = new StringWriter();
            Console.SetOut(standardOut);
        }

        [TestMethod]
        public void TestFile01_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/01_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("Syntax Error{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile01_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/01_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("Syntax Error{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile02_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/02_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("1{0}2{0}3{0}4{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile02_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/02_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("0{0}-123{0}456{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile03_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/03_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("133{0}2{0}-1{0}-256{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile03_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/03_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("1{0}0{0}9{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile04_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/04_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("#t{0}#f{0}#f{0}#t{0}#t{0}#f{0}#f{0}#t{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile04_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/04_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("#t{0}#t{0}#f{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile05_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/05_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("1{0}2{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile05_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/05_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("6{0}1{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile06_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/06_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("1{0}6{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile06_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/06_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("26{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile07_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/07_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("4{0}9{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile07_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/07_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("610{0}0{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile08_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/08_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("91{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFile08_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/08_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("3{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFileb1_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/b1_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("2{0}6{0}24{0}3628800{0}1{0}2{0}5{0}55{0}6765{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFileb1_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/b1_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("4{0}2{0}27{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFileb2_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/b2_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("Type Error{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFileb2_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/b2_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("Type Error{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFileb3_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/b3_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("25{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFileb3_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/b3_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("9{0}8{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFileb4_1()
        {
            using (StreamReader sr = new StreamReader("./testcase/b4_1.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("11{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }

        [TestMethod]
        public void TestFileb4_2()
        {
            using (StreamReader sr = new StreamReader("./testcase/b4_2.lsp"))
            {
                parser.Parse(sr.ReadToEnd());

                string expected = string.Format("9{0}", Environment.NewLine);
                Assert.AreEqual(expected, standardOut.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var parser = new MiniLispParser();

            try
            {
                using(StreamReader sr = new StreamReader("./testcase/b1_2.lsp"))
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
