using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DasSuesseSchloss;


public class Spieler : LebensObjekte
{
    public int Level { get; private set; } = 1;//Lv1 beginnen
    public int XP { get; private set; } = 0;
    public int MinAngriff { get; private set; } = 20; // Angriffkraftbereich beginnen
    public int MaxAngriff { get; private set; } = 30;

    public List<string> Inventar { get; private set; } = new List<string>();// Null

    public Spieler() : base("Prinzen Rolle", 100) { } // Name festgelegt

    public override int Angriff()
    {
        Random rnd = new Random();
        return rnd.Next(MinAngriff, MaxAngriff);
    }
    public void AddXP(int xp)
    {
        XP += xp;
        Console.WriteLine($"{Name} erhält {xp} XP!\n< Lv.{Level} XP: {XP}>");
        while ((Level == 1 && XP >= 30) || (Level == 2 && XP >= 50) || (Level == 3 && XP >= 70))
        {
            LevelUp();
        }
    }


    public void LevelUp()//hp und Angriffkraftbereich in der Mothode gerade ändernAction naechst
    {
        Level++;
        int neueMaxHP = Level * 50 + 100;
        HP = Math.Min(HP + 50, neueMaxHP); //HP += 50;
        MinAngriff = (int)(MinAngriff * 1.5);
        MaxAngriff = (int)(MaxAngriff * 1.5);
        Console.WriteLine($"{Name} erreicht Level {Level}!\n< Lv. {Level} HP: {HP}>");


    }
    public void Zelten()
    {        
        Console.Clear();
        Sprechen("Schalf gut!");

        HP = Level * 50 + 100; // voll HP
        Sprechen($"{Name} +{HP} HP");
        Console.ReadKey();
    }
    
    public void AddItem(string item) // Nach dem Kampfen Item von Monster kriegen
    {
        Inventar.Add(item);        
    }
    public void InventarAnzeigen()
    {
        if (Inventar.Count == 0)
        {
            Sprechen("Inventar ist leer.");
            return;
        }

        Dictionary<string, int> inventarAnzahl = new Dictionary<string, int>();
        foreach (string item in Inventar)
        {
            if (inventarAnzahl.ContainsKey(item))//Dictionary<TKey, TValue> -> false oder true
            {
                inventarAnzahl[item]++;
            }
            else
            {
                inventarAnzahl[item] = 1;
            }
            Console.Clear();
            Console.WriteLine("Inventar:");
            foreach (var itemTyp in inventarAnzahl)
            {
                //Key ->Ein eindeutiger Wert, der Daten identifiziert, Value ->Die mit diesem Schlüssel verknüpften Daten sind die tatsächlich zu speichernden Informationen.
                Console.WriteLine($"- {itemTyp.Key} ({itemTyp.Value})");
            }
        }

        //Console.WriteLine($"Inventar: \n-{string.Join("\n-", Inventar)}");

    }
    public void Heilen()
    {
        int neueMaxHP = Level * 50 + 100; // wenn LevelUp wird, stieg MaxHP
        if (Inventar.Contains("Heiltrank"))
        {            
            int geheilteHP = Math.Min(HP + 50, neueMaxHP);
            int gewonnenHP = geheilteHP - HP;
            HP = geheilteHP; //innerhalb der maximalen HP des Spielers
            Inventar.Remove("Heiltrank");

        }
        else
        {
            Sprechen("Keine Heiltrank! Lauf weg! ");
        }
    }
    public void Speichern()
    {
        try
        {
            var daten = new SpielDaten
            {
                Level = Level,
                HP = HP,
                XP = XP,
                Inventar = Inventar
            };

            string json = JsonSerializer.Serialize(daten, new JsonSerializerOptions { WriteIndented = true });
            if (string.IsNullOrWhiteSpace(json))
            {
                Sprechen("Fehler: Speicherprozess erzeugt leere Daten!");
                return;
            }

            File.WriteAllText("spielstand.json", json);// truncated and overwritten
            Sprechen("Spielstand gespeichert!");
            Console.ReadKey();
            Console.Clear();
        }
        catch (Exception e) { Sprechen($"Fehler beim Speichern: {e.Message}"); }
    }
    public void Laden()
    {
        string speicherPfad = "spielstand.json";

        if (!File.Exists(speicherPfad))
        {
            Sprechen("Fehler: Keine gespeicherte Datei vorhanden!");
            return;
        }
        try
        {
            string json = File.ReadAllText(speicherPfad); // lesen File
            if (string.IsNullOrWhiteSpace(json))
            {
                Sprechen("Fehler: Gespeicherte Datei ist leer oder ungültig!");
                return;
            }

            var daten = JsonSerializer.Deserialize<SpielDaten>(json);
            if (daten == null)
            {
                Sprechen("Fehler: Gespeicherte Daten konnten nicht gelesen werden!");
                return;
            }

            Level = daten.Level > 0 ? daten.Level : 1;
            HP = daten.HP > 0 ? daten.HP : 100;
            XP = daten.XP >= 0 ? daten.XP : 0;
            Inventar = daten.Inventar ?? new List<string>();

            Sprechen("Spielstand geladen!");
            Console.ReadKey();
            Console.Clear();

            Dictionary<string, int> inventarAnzahl = new Dictionary<string, int>();

            foreach (string item in Inventar)
            {
                if (inventarAnzahl.ContainsKey(item))
                {
                    inventarAnzahl[item]++;
                }
                else
                {
                    inventarAnzahl[item] = 1;
                }
            }
            Console.WriteLine("Inventar:");
            foreach (var itemTyp in inventarAnzahl)
            {
                Console.WriteLine($"- {itemTyp.Key} ({itemTyp.Value})");
            }
            //Console.WriteLine($"Level: {Level} HP: {HP} XP: {XP}\nInventar: \n-{string.Join("\n-", Inventar ?? new List<string>())}");

        }
        catch (Exception e)
        {
            Sprechen($"Fehler beim Laden: {e.Message}");
        }
    }


}




