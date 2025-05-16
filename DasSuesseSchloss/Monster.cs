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

    public Monster(string name, int hp, int minAngriff, int maxAngriff, int xp) : base(name, hp)// Konstruktor
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
 
}
public class Boss : LebensObjekte
{
    public Boss() : base("SchokoB", 150) { }
    public override int Angriff()
    {
        Random rnd = new Random();
        return rnd.Next(45, 50);
    }


} 
