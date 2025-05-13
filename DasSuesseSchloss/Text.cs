using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss;
   public class Text
    {
    public void Sprechen(string text) { 
    foreach(char c in text)
        {
            Console.WriteLine(c);
            Thread.Sleep(50);
        }
    }

    }

