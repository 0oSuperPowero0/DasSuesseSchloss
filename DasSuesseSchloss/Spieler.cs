using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss;


public class Spieler : LebensObjekte
{
    public int Level { get; private set; } = 1;//Lv1 beginnen
    public int XP { get; private set; } = 0;
    public List<string> Inventar { get; private set; } = new List<string>();

    public Spieler(string name) : base(name, 100) { }
    
    public override int Angriff()
    {
        Random rnd = new Random();
        return rnd.Next(20,30);
    }  
    public void AddXP(int xp) 
    {
        XP += xp;
        Console.WriteLine($"{Name} erhält {xp} XP! < Lv.{Level} XP: {XP}>");
        if (XP >= 30)
        {
            LevenUp();
        }
    }  private void LevenUp()
    {
        Level++;
        Console.WriteLine($"{Name} erreicht Level {Level}!");
    }
    public void AddItem(string item) // Nach dem Kampfen Item von Monster kriegen
    {
        Inventar.Add(item);
        Console.WriteLine($"{Name} erhält: {item}");
    }
    public void InventarAnzeigen()
    {
        Console.WriteLine($"Inventar: {string.Join(", ", Inventar)}");
    }
    public void InventarEntfernen(string item)
    {
        if (Inventar.Contains(item))
        {
            Inventar.Remove(item);
            Console.WriteLine($"{item} wurde aus dem Inventar entfernt.");
        }
        else
        {
            Console.WriteLine($"{item} ist nicht im Inventar.");
        }
    }
    public void Speichern()
    {
        File.WriteAllText("spielstand.txt", $"Level: {Level}\nXP: {XP}\nInventar: {string.Join(",", Inventar)}"); // System.IO.File erstellt ein Dateistreams!!
        Console.WriteLine("Spielstand gespeichert!");
    }
}



