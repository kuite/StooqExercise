using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StooqExercise.Persisters
{
    public interface IPersister
    {
        void Save(Dictionary<string, string> values);
    }
}
