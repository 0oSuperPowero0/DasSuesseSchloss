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
    public int MinAngriff { get; private set; } = 20; // Angriffkraftbereich beginnen
    public int MaxAngriff { get; private set; } = 30;

    public List<string> Inventar { get; private set; } = new List<string>();// nur Unter dem Spieler verfügbar

    public Spieler() : base("Prinzen Rolle", 100) { } // Name festgelegt
    
    public override int Angriff()
    {
        Random rnd = new Random();
        return rnd.Next(MinAngriff,MaxAngriff);
    }  
    public void AddXP(int xp) 
    {
        XP += xp;
        Console.WriteLine($"{Name} erhält {xp} XP!\n< Lv.{Level} XP: {XP}>");
        if (XP >= 30)
        {
            LevelUp();
        }
    }  private void LevelUp()
    {
        Level++;
        HP += 50;
        MinAngriff = (int)(MinAngriff * 1.5);
        MaxAngriff = (int)(MaxAngriff * 1.5);
        Console.WriteLine($"{Name} erreicht Level {Level}!\n< Lv. {Level} HP: {HP}>");
    }
    public void AddItem(string item) // Nach dem Kampfen Item von Monster kriegen
    {
        Inventar.Add(item);
        Sprechen($"{Name} erhält: {item}");
    }
    public void InventarAnzeigen()
    {
        Sprechen($"Inventar: {string.Join(" - ", Inventar)}");
    }
    public void InventarEntfernen(string item)
    {
        if (Inventar.Contains(item))
        {
            Inventar.Remove(item);
            Sprechen($"{item} wurde aus dem Inventar entfernt.");
        }
        else
        {
            Sprechen($"{item} ist nicht im Inventar.");
        }
    }
    public void Heilen()
    {
        if (Inventar.Contains("HeilTrank"))
        {
            HP += 50;
            Inventar.Remove("HeilTrank");
            Sprechen($"{Name} hat einen Heiltrank getrunken.\n + 50 HP\n < Prinzen Rollen : {HP} ");
        }
        else
        {
            Sprechen("Keine Heiltrank! Lauf weg! ");  
        }
    }
    public void Speichern()
    {
        File.WriteAllText("spielstand.txt", $"Level: {Level}\nXP: {XP}\nInventar: {string.Join(",", Inventar)}"); // System.IO.File erstellt ein Dateistreams!! recherchieren!!
        Console.WriteLine("Spielstand gespeichert!");
    }
}



