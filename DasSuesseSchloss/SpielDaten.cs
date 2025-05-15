using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss
{
    internal class SpielDaten
    {
        public int HP { get; internal set; }
        public List<string> Inventar { get; internal set; }
        public int XP { get; internal set; }
        public int Level { get; internal set; }
    }
}
