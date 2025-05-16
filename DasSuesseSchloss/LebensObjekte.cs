using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss;
public abstract class LebensObjekte : ICharakter
{
    public string Name { get; protected set; }// Nur in vererbte Klasse oder gleiche Klasse
    public int HP { get; set; }

    public LebensObjekte(string name, int hp)
    {
        Name = name;
        HP = hp;
    }  
    public abstract int Angriff(); // alle Charakter muss angreiffen können

    public virtual void SchadenNehmen(int schaden)// nur hp mit ref in out möglich
    {
        HP -= schaden;
        Sprechen($"{Name} nimmt {schaden} Schaden!");
        if (HP <= 0)
        {
            HP = 0;
            Sprechen($"{Name} wurde besiegt!");
        }
    }
    public void Sprechen(string text)// Monster und Spieler können Typing Scrpipt
    {
        foreach(char c in text)
        {
            Console.Write(string.Join("",c));
            Thread.Sleep(25);
        }
        Console.WriteLine();
        
    }
  

}



