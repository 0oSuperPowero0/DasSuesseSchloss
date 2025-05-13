using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss;
public abstract class LebensObjekte
    {
        public string Name { get; protected set; }// Nur in vererbte Klasse oder gleiche Klasse
        public int HP { get; protected set; }

        public LebensObjekte(string name, int hp)
    {
        Name = name;
        HP = hp;
    }
    public abstract int Angriff(); // alle Charakter muss angreiffen können
    public virtual void SchadenNehmen(int schaden)
    {
        HP -= schaden;
        Console.WriteLine($"{Name} nimmt {schaden} Schaden!");
        if (HP <= 0)
        {
            HP = 0;
            Console.WriteLine($"{Name} wurde besiegt!");
        }
    }

    }



