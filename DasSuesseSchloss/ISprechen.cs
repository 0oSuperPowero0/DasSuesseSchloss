using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss;

public interface ISprechen
{
    void Sprechen(string spr)
    {
          
        foreach (char c in spr)
        {
            Console.WriteLine(string.Join("",c));// muss ändern die horizontale Ausgabe
            Thread.Sleep(50);
        }   

    }
    // jede Klasse die ISprechen implementiert, kann bentzen Typing Script.
}
