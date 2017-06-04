using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Timers;
using StooqExercise.Persisters;

namespace StooqExercise
{
    public class StooqProcessor
    {
        private const string Url = "https://static.stooq.com/pp/g.js";

        private readonly IPersister persister;

        private Dictionary<string, string> quotationsValues;

        public StooqProcessor(IPersister persister)
        {
            this.persister = persister;
        }

        public void Update(object sender, ElapsedEventArgs args)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var sourceText = client.DownloadString(Url);
                    quotationsValues = ParseQuotations(sourceText);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while updating: {0}", e.Message);
            }
        }

        public void Save(object sender, ElapsedEventArgs args)
        {
            try
            {
                persister.Save(quotationsValues);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while saving: {0}", e.Message);
            }

        }

        private Dictionary<string, string> ParseQuotations(string source)
        {
            var quotations = new Dictionary<string, string>();
            var pattern = "onclick=pp_m_\\(this\\)>(.*?)</a></td><td id=pp_v>(.*?)</td>";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(source);

            while (m.Success)
            {
                quotations.Add(m.Groups[1].Value, m.Groups[2].Value);
                m = m.NextMatch();
            }

            return quotations;
        }
    }
}
