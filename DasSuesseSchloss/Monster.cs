using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DasSuesseSchloss;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;

namespace DasSuesseSchloss;



public class Monster : LebensObjekte
{    
    private int MinAngriff; // Angriffkraftbereich
    private int MaxAngriff;
    public int XP { get; private set; }// Eigenschaft
   
    public Monster(string name, int hp, int minAngriff, int maxAngriff,int xp):base(name, hp)//Konstruktor
    {
        this.MinAngriff = minAngriff; //zur besseren Lesbarkeit
        this.MaxAngriff = maxAngriff;
        XP = xp;
    }
    public override int Angriff()
    {
        Random rnd = new Random();
        return rnd.Next(MinAngriff, MaxAngriff);
    }
    public override string ToString() // Monster erscheint
    {
        return $"{Name} <HP: {HP}>";
    }
   
}
public class Boss : LebensObjekte
{
    public Boss() : base("SchokoB", 300) { }
    public override int Angriff()
    {
        Random rnd = new Random();
        return rnd.Next(45, 60);
    }
    public void Dialog()
    {

        Sprechen("hey");
    }

}
public class MonsterGruppe // separate Monstergruppe erstellen und kontrollieren
{
    public List<Monster> MonsterListe { get; private set; } 
    public string Region { get; private set; } //Monster Stufe als Region trennen!!
    public MonsterGruppe(string name, int hp, int minAngriff, int maxAngriff, int minMonster, int maxMonster, int xp, string region)
    {
        Region = region;
        MonsterListe = new List<Monster>();
        Random rnd = new Random();
        int monsterAnzahl = rnd.Next(minMonster, maxMonster + 1); // rnd.Next(==, +1)
        for (int i = 0; i < monsterAnzahl; i++)
        {
            MonsterListe.Add(new Monster(name, hp, minAngriff, maxAngriff, xp));            
        }
    }        
}

// Toppers 1~2 Monsters + Hanuta Monster
//public class KnoppersUndHanuta
//{
//    public List<Monster> monsterliste { get; private set; }
//
//    public KnoppersUndHanuta()
//    {
//        monsterliste = new List<Monster>();
//        Random rnd = new Random();
//
//        int KnoppersUndHanutaAnzahl = rnd.Next(1, 3);
//        for (int i = 0; i < KnoppersUndHanutaAnzahl; i++)
//        {
//            monsterliste.Add(new Monster("Knoppers", 40));
//        }
//        monsterliste.Add(new Monster("Hanuta", 50));
//
//    }
//    public int Angriff()
//    {
//    
//        Random rnd = new Random();
//        return rnd.Next(1, 10);
//    }
//}
//class TrolliUndHaribo
//{
//    public List<Monster> monsterliste { get; private set; }
//    public TrolliUndHaribo()
//    {
//
//        monsterliste = new List<Monster>();
//        Random rnd = new Random();
//
//        int trolliAnzahl = rnd.Next(1, 3);
//        for (int i = 0; i < trolliAnzahl; i++)
//        {
//            monsterliste.Add(new Monster("Trolli", 40));
//        }
//        monsterliste.Add(new Monster("Haribo", 50));
//
//    }
//}
//class Toffee
//{
//    public List<Monster> monsterliste { get; private set; }
//    public Toffee()
//    {
//        monsterliste = new List<Monster>();
//        Random rnd = new Random();
//
//        int toffeeAnzahl = rnd.Next(1, 4); // random 1~3
//        for (int i = 0; i < toffeeAnzahl; i++)
//        {
//            monsterliste.Add(new Monster("Toffee", 50));
//        }
//    }
//}
