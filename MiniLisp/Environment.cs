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
            _top = 0;
            PushEnvironment(new Environment());
        }

        internal static Environment GetCurrentEnvironment()
        {
            return _envrionments[_top - 1];
        }

        internal static void PushEnvironment(Environment env)
        {
            _envrionments.Add(env);
            _top++;
        }

        internal static void PopEnvironment()
        {
            _envrionments.RemoveAt(--_top);
        }

        internal static object LookUp(string key)
        {
            object item = null;
            for (int i = _envrionments.Count - 1; i >= 0 && item == null; i--)
            {
                try
                {
                    item = _envrionments[i].LookUp(key);
                }
                catch(Exception ex)
                {
                      
                }
            }

            if (item == null)
                throw new KeyNotFoundException();

            return item;
        }

        internal static void Add(string key, IAST value)
        {
            var env = GetCurrentEnvironment();
            env.Add(key, value);
        }

        internal class Environment
        {
            public Environment()
            {
                DefineTable = new Dictionary<string, object>();
            }

            public Dictionary<string, object> DefineTable;

            public object LookUp(string key)
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
