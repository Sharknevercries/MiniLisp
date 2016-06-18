using QUT.Gppg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLisp
{
    internal partial class MiniLispParser
    {
        private static List<Environment> _envrionments;
        private static int _top = 0;

        internal static void InitializeEnvironment()
        {
            _envrionments = new List<Environment>();
            AddEnvironment(new Environment()); 
        }

        internal static Environment GetCurrentEnvironment()
        {
            return _envrionments[_top - 1];
        }

        internal static void AddEnvironment(Environment env)
        {
            _envrionments.Add(env);
            _top++;
        }

        internal static void PopEnvironment()
        {
            _envrionments.RemoveAt(--_top);
        }

        internal class Environment
        {
            public Environment()
            {
                DefineTable = new Dictionary<string, IAST>();
            }

            private Dictionary<string, IAST> DefineTable;

            public IAST LookUp(string key)
            {
                return DefineTable[key];                
            }

            public void Add(string key, IAST value)
            {
                DefineTable.Add(key, value);
            }
        }
    }
}
