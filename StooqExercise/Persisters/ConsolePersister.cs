using System;
using System.Collections.Generic;

namespace StooqExercise.Persisters
{
    public class ConsolePersister : IPersister
    {
        public void Save(Dictionary<string, string> values)
        {
            Console.WriteLine("Values at {0} :", DateTime.Now);
            foreach (KeyValuePair<string, string> quota in values)
            {
                Console.WriteLine("{0}: {1}", quota.Key, quota.Value);
            }
            Console.WriteLine();
        }
    }
}
