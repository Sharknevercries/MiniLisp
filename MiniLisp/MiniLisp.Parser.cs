using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniLisp
{
    internal partial class MiniLispParser
    {
        public MiniLispParser() : base(null) { }

        public void Parse(string s)
        {
            byte[] inputBuffer = System.Text.Encoding.Default.GetBytes(s);
            MemoryStream stream = new MemoryStream(inputBuffer);
            this.Scanner = new MiniLispScanner(stream);
            this.Parse();
        }
    }
}
