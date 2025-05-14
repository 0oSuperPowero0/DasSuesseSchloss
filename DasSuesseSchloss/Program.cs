using System.Security.Cryptography.X509Certificates;
using System.IO;

using DasSuesseSchloss;
using System;
using System.Threading.Tasks.Dataflow;
//
//  Console.SetWindowSize(100, 50);
//  Console.SetBufferSize(100, 50);
// Konsole Fenster Größe


Spieler spieler = new Spieler();
Boss schokoB = new Boss();
Text text = new Text();


Console.WriteLine("1.Starten");
Console.WriteLine("2.Daten laden");
Console.WriteLine("3.Verlasen");
string antwort = (Console.ReadLine() ?? "").Trim();
Console.Clear();

if (antwort == "1")
{
  Sprechen(text.AddSkript("einleitung"));
    Console.ReadKey();
    Console.Clear();
   Sprechen("hallo?");  
  Console.ReadKey();
  Console.Clear();
  Akt1(); // !! endlose Schleife

}
else if (antwort == "2")
{
    DatenLaden();// wie aufbauen?
}
else if (antwort == "3")
{
    Sprechen("Möchten Sie ins Bett gehen?.");
    Environment.Exit(0);// Beenden
}
else
{ Console.WriteLine("Wählen Sie 1, 2 oder 3"); }

void Sprechen(string spr)// nur hier außer andere Klasse
{
    foreach (char c in spr)
    {
        Console.WriteLine(string.Join("",c));
        Thread.Sleep(50);
        Console.WriteLine();
    }
    
}

void Akt1()
{
    Console.WriteLine(" Willkommen in der Plätzchen Wüste!");
    Kampf(new List<MonsterGruppe>
            {
                new MonsterGruppe("Knoppers", 35, 10, 15, 1, 3, 5, "Plätzchen Wüste"),
                new MonsterGruppe("Hanuta", 40, 15, 20, 1, 2, 6, "Plätzchen Wüste")
            });

    SpielSpeichern();
    Console.Clear();
    Akt2();
}

void Akt2()
{
    Console.WriteLine(" Willkommen in Pummelig Gummifeld!");
    Kampf(new List<MonsterGruppe>
            {
                new MonsterGruppe("Trolli", 40, 20, 25, 1, 3, 8, "Pummelig Gummifeld"),
                new MonsterGruppe("Haribo", 50, 25, 40, 1, 2, 10, "Pummelig Gummifeld")
            });

    SpielSpeichern();
    Console.Clear();
    Akt3();
}

void Akt3()
{
    Console.WriteLine(" Willkommen in Karamell Sumpf!");
    Kampf(new List<MonsterGruppe>
            {
                new MonsterGruppe("Toffee", 60, 35, 45, 1, 3, 20, "Karamell Sumpf")
            });
    SpielSpeichern();

}

void Bossbegegnen()
{
    Boss schokoB = new Boss();
    
}
void Kampf(List<MonsterGruppe> monsterGruppen)
{
    foreach (var gruppe in monsterGruppen)
    {
        Console.WriteLine(gruppe.Region);
        foreach (var monster in gruppe.MonsterListe)
        {
            Console.WriteLine($" {monster.Name} <HP: {monster.HP}>");

            while (monster.HP > 0 && spieler.HP > 0) // Kampfen
            {
                Console.WriteLine("Dein Zug! Was möchtest du tun?\n1: Angriff\n2: Inventar\n3: Entkommen");
                string waehlen = Console.ReadLine() ?? "".Trim();// wählen die Option

                int hp = monster.HP;

                if (waehlen == "1")
                {
                    int spielerAngriff = spieler.Angriff();
                    SchadenNehmen(ref hp, spielerAngriff);
                }
                else if (waehlen == "2")
                {
                    bool inventarCheck = true;

                    while (inventarCheck)
                    {
                        spieler.InventarAnzeigen();
                        Console.WriteLine("Wähle ein Item aus deinem Iventar:\n1: Zucker (unverwendbar)\n2: Heiltrank (+50)\n3: Zurück zum Kampf"); //Inventar Zeigen und wählen Heiltrank
                        string itemWahl = Console.ReadLine() ?? "".Trim();

                        if (itemWahl == "1")
                        {
                            spieler.Sprechen("Zucker ist nicht verwendbar!");
                        }
                        else if (itemWahl == "2")
                        {
                            if (spieler.Inventar.Contains("Heiltrank"))// Copilot: dann kann es "Heiltrank" als "2" abrufen und bringen
                            {
                                spieler.Heilen();
                            }
                            else
                            {
                                spieler.Sprechen("Keine Heiltrank mehr!");
                            }

                        }
                        else if (itemWahl == "3")
                        {
                            spieler.Sprechen("Zurück zum Kampf!");
                            inventarCheck = false;
                        }
                        else
                        {
                            spieler.Sprechen("Wähle richitig!");
                        }
                    }                    
                }
                else if (waehlen == "3")
                {
                    spieler.Sprechen("Lauf weg!");
                    break;
                }
                else { Sprechen("Wähle ricitig!"); }

            }
            if (monster.HP <= 0)
            {
                Sprechen($"{monster.Name} wurde besiegt!");
                spieler.AddXP(monster.XP);
                string[] items = { "Heiltrank", "Zucker" }; // nachdem Kampf Spieler automatisch Item bekommen(Inventar)
                string itemFallen = items[new Random().Next(items.Length)];
                spieler.AddItem(itemFallen);
            }


        }
        Console.WriteLine("--------------------------------------------------------");
    }
}
void SpielSpeichern() {
    Console.WriteLine("Speichern?\n1: Ja\n2: Nein\n3: Exit");
        string speichernAntwort = Console.ReadLine()?? "".Trim()??"";
    if (speichernAntwort == "1")
    {
        spieler.Speichern();
        Sprechen("Spiel gespeichert!");
    }
    else if (speichernAntwort == "2")
    {
        Sprechen("Weiter geht's");
    }
    else if (speichernAntwort == "3")
    {
        Environment.Exit(0); // testen
    }
    else return;
}
void DatenLaden()//copilot suchen
{
    string speicherPfad = "spielstand.txt";
    if (File.Exists(speicherPfad))
    {
        string[] gespeicherteDaten = File.ReadAllLines(speicherPfad);
        foreach (string zeile in gespeicherteDaten)
        {
            Console.WriteLine(zeile);
        }
        Console.WriteLine("Weiter spielen?\n1: Ja\n2: Nein)");
        string weiterAntwort = Console.ReadLine() ?? "".Trim() ?? "";
        if (weiterAntwort == "1")
        {
            Console.Clear();
            Akt1();
        }
        else
        {
            Sprechen("Spiel beendet.");
        }

    }
    else { Console.WriteLine("Leer"); }
}

void SchadenNehmen(ref int hp, int spielerAngriff) { }
Console.WriteLine($"Inventar: {string.Join(", ", spieler.Inventar)}");


// jede Stufe wird zufällige Auswahl von Monster erschienen
//Random rnd = new Random();
//var stufe1 = new List<MonsterGruppe>
//{
//    new MonsterGruppe("Knoppers", 35,10,15,1,3, 5, "Plätzchen Wüste"),//name, HP, minAngriff, maxAngriff, minMonster, maxMonster+1, xp, region
//    new MonsterGruppe("Hanuta", 40,15,20,1,2, 6, "Plätzchen Wüste")
//};//!!!
//var ersteStufe = stufe1[rnd.Next(stufe1.Count)]; // Stufe in Monster
//
//var stufe2 = new List<MonsterGruppe>
//{
//    new MonsterGruppe("Trolli", 40,20,25,1,3, 8, "Pummelig Gummifeld"),
//    new MonsterGruppe("Haribo", 50,25,40,1,2, 10, "Pummelig Gummifeld")
//};
//var zweiteStufe = stufe2[rnd.Next(stufe2.Count)];
//
//// Nur ein Typ Monster
//var stufe3 = new MonsterGruppe("Toffee", 60, 35, 45, 1, 3, 20, "Karamell Sumpf");
//
//var monsterGruppen = new List<MonsterGruppe>
//{
//    ersteStufe,zweiteStufe,stufe3
//};