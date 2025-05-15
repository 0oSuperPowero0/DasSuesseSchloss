using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
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
    }
    private void LevelUp()
    {        
    }

    public void LevelUp(Action naechst)//hp und Angriffkraftbereich in der Mothode gerade ändern
    {
        Level++;
        int neueMaxHP = Level * 50 + 100; 
        HP = Math.Min(HP + 50, neueMaxHP); //HP += 50;
        MinAngriff = (int)(MinAngriff * 1.5);
        MaxAngriff = (int)(MaxAngriff * 1.5);
        Console.WriteLine($"{Name} erreicht Level {Level}!\n< Lv. {Level} HP: {HP}>");

        SetGameProgress(naechst);
     
    }
    public void SetGameProgress(Action naechst)
    {
        naechst.Invoke(); // Callback!
    }
    public void AddItem(string item) // Nach dem Kampfen Item von Monster kriegen
    {
        Inventar.Add(item);
        Sprechen($"{Name} erhält: {item}");
    }
    public void InventarAnzeigen()
    {
        Console.WriteLine($"Inventar: {string.Join(" - ", Inventar)}");
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
        int neueMaxHP = Level * 50 + 100; // wenn LevelUp wird, stieg MaxHP
        if (Inventar.Contains("Heiltrank"))
        {
            HP = Math.Min(HP + 50, neueMaxHP);//HP += 50;
            Inventar.Remove("Heiltrank");
            Sprechen($"{Name} hat einen Heiltrank getrunken.\n + 50 HP\n < Prinzen Rollen : {HP} ");
        }
        else
        {
            Sprechen("Keine Heiltrank! Lauf weg! ");  
        }
    }
    public void Speichern()
    {
        string daten = $"Level:{Level}\nHP:{HP}\nXP:{XP}\nInventar:{string.Join(",", Inventar)}";//teilen
        File.WriteAllText("spielstand.txt", daten); // System.IO.File erstellt ein Dateistreams!! recherchieren!!
        Sprechen("Spielstand gespeichert!");
    }
    public void Laden()
    {
        string speicherPfad = "spielstand.txt";

        if (File.Exists(speicherPfad))
        {
            string[] daten = File.ReadAllLines(speicherPfad); // lesen File

            foreach (string zeile in daten)
            {
                string[] teile = zeile.Split(':'); // Datentrennung

                if (teile.Length == 2)
                {
                    switch (teile[0])
                    {
                        case "Level":
                            Level = int.Parse(teile[1]);
                            break;
                        case "HP":
                            HP = int.Parse(teile[1]);
                            break;
                        case "XP":
                            XP = int.Parse(teile[1]);
                            break;
                        case "Inventar":
                            Inventar = new List<string>(teile[1].Split(',')); // Iventar
                            break;
                    }
                }
            }

            Sprechen("Spielstand geladen!");
            Console.WriteLine($"Level: {Level} HP: {HP} XP: {XP}\nInventar: \n-{string.Join("\n-", Inventar)}");
        }
        else
        {
            Sprechen("Kein gespeicherter Spielstand gefunden!");
        }
    }
}



