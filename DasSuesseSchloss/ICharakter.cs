using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss;

public interface ICharakter
{
    string Name { get; }
    int HP { get; set; }
    int Angriff();
    void SchadenNehmen(int schaden);
    void Sprechen(string text);

}
