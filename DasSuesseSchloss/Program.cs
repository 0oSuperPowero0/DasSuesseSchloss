using System.Security.Cryptography.X509Certificates;
using System.IO;

using DasSuesseSchloss;
using System;
using System.Threading.Tasks.Dataflow;


Spieler spieler = new Spieler();
Boss schokoB = new Boss();
Text text = new Text();

StartMenu();
void StartMenu()
{   
    Console.Clear();
    Console.WriteLine("1:Starten");
    Console.WriteLine("2:Daten laden");
    Console.WriteLine("3:Verlasen");
    string antwort = (Console.ReadLine() ?? "").Trim();
    Console.Clear();

    if (antwort == "1")
    {
        Sprechen(text.AddSkript("einleitung"));
        Console.ReadKey();
        Console.Clear();
        spieler.AddItem("Heiltrank");
        spieler.AddItem("Heiltrank");
        spieler.AddItem("Strohhalm");
        Sprechen("Du hast 2 Heiltrank, und Strohhalm genommen.");
        Console.ReadKey();
        Console.Clear();
        Akt1();

    }
    else if (antwort == "2")
    {
        DatenLaden();// wie aufbauen?
    }
    else if (antwort == "3")
    {        
        Verlassen();
    }
    else
    { Console.WriteLine("Wählen Sie 1, 2 oder 3"); }
}
void Verlassen() {
    Sprechen("Möchten Sie ins Bett gehen?.");
    Console.WriteLine("1: Ja\n2: Nein");
    string verlassenAntwort=Console.ReadLine()?? "".Trim() ??"";

    if (verlassenAntwort == "1")
    {
        Console.Clear();
        Sprechen("Adios");
        Environment.Exit(0);
    }
    else if (verlassenAntwort == "2")
    {
        Console.Clear();
        StartMenu();
    }
    else {
        Sprechen("Wählen Sie 1 oder 2");
        Verlassen();
    }
}
void SpielSpeichern()
{
    Console.WriteLine("Speichern?\n1: Ja\n2: Nein\n3: Exit");
    string speichernAntwort = Console.ReadLine() ?? "".Trim() ?? "";
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

spieler.Laden(); // speicherten Daten laden

    Console.WriteLine("Weiter spielen?\n1: Ja\n2: Nein");
    string weiterAntwort = Console.ReadLine() ?? "".Trim() ?? "";

    if (weiterAntwort == "1")
    {
        Console.Clear();

        if (spieler.Level == 1) Akt1();
        else if (spieler.Level == 2) Akt2();
        else if (spieler.Level == 3) Akt3();
        else BossKampf();
    }
    else
    {
        Sprechen("Zurück zum Start Menü.");
        Console.ReadKey();
        StartMenu();
    }   
}
void Sprechen(string spr)// nur hier außer andere Klasse

{
    foreach (char c in spr)
    {
        Console.Write(string.Join("", c));
        Thread.Sleep(50);

    }
    Console.WriteLine();

}

void Akt1()
{
    Console.WriteLine(" Willkommen in der Plätzchen Wüste!");
    Random rnd = new Random();
    Monster zufallsMonster = rnd.Next(2) == 0
               ? new Monster("Knoppers (Lv.1)", 35, 10, 15, 3)
               : new Monster("Hanuta (Lv.1)", 40, 15, 20, 4);
    Kampf(zufallsMonster);

    SpielSpeichern();
    if (spieler.Level == 1)
    {
        spieler.LevelUp(Akt2);
    }
    //Console.Clear();
}

void Akt2()
{
    Console.WriteLine(" Willkommen in Pummelig Gummifeld!");
    Random rnd = new Random();
    Monster zufallsMonster = rnd.Next(2) == 0
               ? new Monster("Trolli (Lv.2)", 30, 10, 15, 5)
               : new Monster("Haribo (Lv.2)", 35, 15, 20, 6);
    Kampf(zufallsMonster);

    SpielSpeichern();
    if (spieler.Level == 2)
    {
        spieler.LevelUp(Akt3);
    }
    //Console.Clear();
}

void Akt3()
{
    Console.WriteLine(" Willkommen in Karamell Sumpf!");

    Monster karmellMonster = new Monster("Toffee (Lv.3)", 55, 20, 30, 7);

    Kampf(karmellMonster);
    SpielSpeichern();

    if (spieler.Level == 3)
    {
        spieler.LevelUp(BossKampf);
    }


    //Console.Clear();

}



void Kampf(Monster monster)
{
    Console.Clear();


    while (monster.HP > 0 && spieler.HP > 0) // Kampfen
    {
        Console.WriteLine("=============================");
        Console.WriteLine($" > {spieler.Name} (Lv.{spieler.Level}) < HP: {spieler.HP}");
        Console.WriteLine($" > {monster.Name} < HP: {monster.HP}");// Status
        Console.WriteLine("=============================\n");

        Console.WriteLine("Dein Zug! Was möchtest du tun?\n1: Angriff\n2: Inventar\n3: Entkommen");
        string waehlen = Console.ReadLine() ?? "".Trim() ?? "";// wählen die Option

        switch (waehlen)
        {
            case "1":
                int spielerAngriff = spieler.Angriff();
                Sprechen($"{spieler.Name} greift {monster.Name} an!");

                monster.SchadenNehmen(ref monster.HP, spielerAngriff);
                Sprechen($"{monster.Name} bekommt -{spielerAngriff} Schaden!");

                if (monster.HP > 0)
                {
                    int monsterAngriff = monster.Angriff();
                    Sprechen($"{monster.Name} greift {spieler.Name} an!");

                    spieler.SchadenNehmen(ref spieler.HP, monsterAngriff);
                    Sprechen($"{spieler.Name} bekommt -{monsterAngriff} Schaden!");
                }
                break;

            case "2":
                bool inventarCheck = true;

                while (inventarCheck)
                {
                    spieler.InventarAnzeigen();
                    Console.WriteLine("Wähle ein Item aus deinem Iventar:\n1: Zucker (unverwendbar)\n2: Heiltrank (+50)\n3: Zurück zum Kampf"); //Inventar Zeigen und wählen Heiltrank
                    string itemWahl = Console.ReadLine() ?? "".Trim() ?? "";

                    switch (itemWahl)
                    {
                        case "1":

                            spieler.Sprechen("Zucker ist nicht verwendbar!");
                            break;

                        case "2":

                            if (spieler.Inventar.Contains("Heiltrank"))// Copilot: dann kann es "Heiltrank" als "2" abrufen und bringen
                            {

                                spieler.Sprechen("Schluck*Schluck*");
                                spieler.Heilen();
                            }
                            else
                            {
                                spieler.Sprechen("Keine Heiltrank mehr!");
                            }
                            break;


                        case "3":

                            spieler.Sprechen("Zurück zum Kampf!");
                            inventarCheck = false;
                            break;
                        default:
                            spieler.Sprechen("Wähle richitig!");
                            break;
                    }
                }
                break;

            case "3":

                spieler.Sprechen("Lauf weg!");
                StartMenu();
                break;

            default:
                Sprechen("Wähle ricitig!");
                break;
        }

        Console.Clear();

    }
    if (monster.HP <= 0)
    {
        Sprechen($"{monster.Name} wurde besiegt!");
        spieler.AddXP(monster.XP);
        Sprechen($" +{monster.XP} <XP: {spieler.XP}>");

        string[] items = { "Heiltrank", "Zucker", "Müll" }; // nachdem Kampf Spieler automatisch Item bekommen(Inventar)
        string itemFallen = items[new Random().Next(items.Length)];
        spieler.AddItem(itemFallen);
    }




    Console.WriteLine("--------------------------------------------------------");

}
void BossKampf()
{
    Console.WriteLine("Das Süße Schloss");
    Boss schokoB = new Boss();

    while (schokoB.HP > 0 && spieler.HP > 0)
    {
        Console.WriteLine("=============================");
        Console.WriteLine($" > {spieler.Name} < HP: {spieler.HP}");
        Console.WriteLine($" > {schokoB.Name} < HP: {schokoB.HP}");
        Console.WriteLine("=============================\n");

        Console.WriteLine("Dein Zug! Was möchtest du tun?\n1: Angriff\n2: Inventar\n3: Entkommen");
        string waehlen = Console.ReadLine() ?? "".Trim() ?? "";

        switch (waehlen)
        {
            case "1":
                int spielerAngriff = spieler.Angriff();
                Sprechen($"{spieler.Name} greift {schokoB.Name} an!");
                schokoB.SchadenNehmen(ref schokoB.HP, spielerAngriff);
                Sprechen($"{schokoB.Name} bekommt -{spielerAngriff} Schaden!");

                if (schokoB.HP > 0)
                {
                    int bossAngriff = schokoB.Angriff();
                    Sprechen($"{schokoB.Name} greift {spieler.Name} an!");
                    spieler.SchadenNehmen(ref spieler.HP, bossAngriff);
                    Sprechen($"{spieler.Name} bekommt -{bossAngriff} Schaden!");
                }
                break;

            case "2":
                spieler.InventarAnzeigen();
                Console.WriteLine("Wähle ein Item:\n1: Heiltrank (+50)\n2: Zurück zum Kampf");
                string itemWahl = Console.ReadLine() ?? "".Trim() ?? "";

                switch (itemWahl)
                {
                    case "1":
                        if (spieler.Inventar.Contains("Heiltrank"))
                        {
                            spieler.Sprechen("Schluck*Schluck*");
                            spieler.Heilen();
                        }
                        else
                        {
                            spieler.Sprechen("Keine Heiltrank mehr!");
                        }
                        break;
                    case "2":
                        spieler.Sprechen("Zurück zum Kampf!");
                        break;
                    default:
                        spieler.Sprechen("Wähle richtig!");
                        break;
                }
                break;

            case "3":
                spieler.Sprechen("Flucht ist nicht möglich!");
                break;

            default:
                Sprechen("Wähle richtig!");
                break;
        }

        Console.Clear();
    }
    if (schokoB.HP <= 0)
    {

        Sprechen("Du hast SchokoB besiegt und das süße Schloss gerettet!");

    }
}


void SchadenNehmen(ref int hp, int spielerAngriff) { }
Console.WriteLine($"Inventar: {string.Join(", ", spieler.Inventar)}");
