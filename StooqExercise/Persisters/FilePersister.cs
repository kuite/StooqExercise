using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StooqExercise.Persisters
{
    public class FilePersister : IPersister
    {
        private readonly string filePath;

        public FilePersister()
        {
            filePath = AppDomain.CurrentDomain.BaseDirectory + "Data.txt";
        }

        public FilePersister(string filePath)
        {
            this.filePath = filePath;
        }

        public void Save(Dictionary<string, string> values)
        {
            if (!File.Exists(filePath))
            {
                var tempStream = File.Create(filePath);
                tempStream.Close();
            }

            string[] readText = File.ReadAllLines(filePath);
            bool changed = false;

            for (int i = 1; i < 7; i++)
            {
                var value = values.Values.ElementAt(values.Count - i);
                var savedRawVal = readText.ElementAt(readText.Length - i);
                var savedVal = savedRawVal.Substring(savedRawVal.IndexOf(':') + 1).Trim();
                if (value != savedVal)
                {
                    changed = true;
                    break;
                }
            }

            if (!changed)
            {
                return;
            }

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine("Values at {0} :", DateTime.Now);
                foreach (KeyValuePair<string, string> quota in values)
                {
                    sw.WriteLine("{0}: {1}", quota.Key, quota.Value);
                }
            }
        }
    }
}
