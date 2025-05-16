using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss
{
    public class SpielDaten
    {
        public int Level { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }
        public List<string> Inventar { get; set; } = new List<string>();
    }
}
