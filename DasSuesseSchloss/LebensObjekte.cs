using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss;
public abstract class LebensObjekte : ISprechen
{
    public string Name { get; protected set; }// Nur in vererbte Klasse oder gleiche Klasse
    public int HP { get; protected set; }

    public LebensObjekte(string name, int hp)
    {
        Name = name;
        HP = hp;
    }
    public abstract int Angriff(); // alle Charakter muss angreiffen können
    public void Sprechen(string text)// Monster und Spieler können Typing Scrpipt
    {
        
    }

    public virtual void SchadenNehmen(ref int hp, int schaden)// nur hp mit ref in out möglich
    {
        hp -= schaden;
        Sprechen($"{Name} nimmt {schaden} Schaden!");
        if (hp <= 0)
        {
            hp = 0;
            Sprechen($"{Name} wurde besiegt!");
        }
    }

}



