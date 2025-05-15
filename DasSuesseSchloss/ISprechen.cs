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
            Console.Write(c);// muss ändern die horizontale Ausgabe: Console.Write = gelöst!
            Thread.Sleep(25);
        }Console.WriteLine();

    }
    // jede Klasse die ISprechen implementiert, kann bentzen Typing Script.
}
