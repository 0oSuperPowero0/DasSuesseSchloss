using System.Security.Cryptography.X509Certificates;
using System.IO;

using DasSuesseSchloss;
using System;
using System.Threading.Tasks.Dataflow;
using System.Threading;


Spieler spieler = new Spieler();
Boss schokoB = new Boss();
Text text = new Text();
Console.WriteLine($"Inventar: {string.Join(", ", spieler.Inventar)}");

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
        spieler.AddItem("Brokkobomb");
        Sprechen("Du hast 2 Heiltrank, Brokkobomb und Strohhalm genommen.");
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
void Verlassen()
{
    Sprechen("Möchten Sie ins Bett gehen?.");
    Console.WriteLine("1: Ja\n2: Nein");
    string verlassenAntwort = Console.ReadLine() ?? "".Trim() ?? "";

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
    else
    {
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
        Console.Clear();
        spieler.Speichern();
        Sprechen("Spiel gespeichert!");
        Console.ReadKey();

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
void Akt1()
{
    Console.WriteLine(" Willkommen in der Plätzchen Wüste!");
    bool weiterKampfen = true;
    while (weiterKampfen)
    {
        Random rnd = new Random();
        Monster zufallsMonster = rnd.Next(2) == 0
                   ? new Monster("Knoppers (Lv.1)", 35, 10, 15, 3)
                   : new Monster("Hanuta (Lv.1)", 40, 15, 20, 4);
        Kampf(zufallsMonster);
        SpielSpeichern();

        if (spieler.XP >= 30 && spieler.Level == 1)
        {
            spieler.LevelUp();
            Zelten();
            Akt2();
            return; //muss mit Akt2 fortfahren, also die weitere Codeausführung verhindern.
        }
        Console.WriteLine("\nMöchtest du weiter kämpfen?\n1: Ja\n2: Nein");
        string weiterAntwort = Console.ReadLine()?.Trim() ?? "";

        if (weiterAntwort == "1")
        {
            NeueRunde(1);
        }
        else
        {
            Sprechen("Spiel beendet.");
            
            StartMenu();
        }
        return;
    }

}

void Akt2()
{
    Console.WriteLine(" Willkommen in Pummelig Gummifeld!");
    bool weiterKamfen = true;
    while (weiterKamfen)
    {
        Random rnd = new Random();
        Monster zufallsMonster = rnd.Next(2) == 0
                   ? new Monster("Trolli (Lv.2)", 40, 15, 20, 5)
                   : new Monster("Haribo (Lv.2)", 50, 20, 25, 6);
        Kampf(zufallsMonster);

        SpielSpeichern();
        if (spieler.XP >= 50 && spieler.Level == 2)
        {
            spieler.LevelUp();
            Zelten();
            Akt3();
            return;
        }
        Console.WriteLine("\nMöchtest du weiter kämpfen?\n1: Ja\n2: Nein");
        string weiterAntwort = Console.ReadLine()?.Trim() ?? "";

        if (weiterAntwort == "1")
        {
            NeueRunde(2);
        }
        else
        {
            Sprechen("Spiel beendet.");
            
            StartMenu();
        }return;
    }

}

void Akt3()
{
    Console.WriteLine(" Willkommen in Karamell Sumpf!");
    bool weiterKamfen = true;
    
    while(weiterKamfen)
    {
        Monster karmellMonster = new Monster("Toffee (Lv.3)", 55, 30, 35, 7);

        Kampf(karmellMonster);
        SpielSpeichern();

        if (spieler.XP >= 70 && spieler.Level == 3)
        {
            spieler.LevelUp();
            Zelten();
            BossKampf();
            return; //muss mit Akt2 fortfahren, also die weitere Codeausführung verhindern.
        }
        Console.WriteLine("\nMöchtest du weiter kämpfen?\n1: Ja\n2: Nein");
        string weiterAntwort = Console.ReadLine()?.Trim() ?? "";

        if (weiterAntwort == "1")
        {
            NeueRunde(3);
        }
        else
        {
            Sprechen("Spiel beendet.");
            
            StartMenu();
        }return;
    }
}


void Kampf(Monster monster)
{
    Console.Clear();


    while (monster.HP > 0 && spieler.HP > 0) // Kampfen
    {
        Console.WriteLine("====================================");
        Console.WriteLine($" {spieler.Name} || Lv.{spieler.Level} || HP: {spieler.HP}\n\n");
        Console.WriteLine($"\n {monster.Name} || HP: {monster.HP}");// Status
        Console.WriteLine("====================================\n");

        Console.WriteLine("Dein Zug! Was möchtest du tun?\n1: Angreifen\n2: Inventar\n3: Entkommen\n");
        string waehlen = Console.ReadLine() ?? "".Trim() ?? "";// wählen die Option

        switch (waehlen)
        {
            case "1":

                Console.Clear();

                int spielerAngriff = spieler.Angriff();
                Sprechen($"\n\n\n{spieler.Name} greift {monster.Name} an!");

                monster.SchadenNehmen(ref monster.HP, spielerAngriff);
                Sprechen($"{monster.Name} bekommt -{spielerAngriff} Schaden!");
                

                if (monster.HP > 0)
                {
                    int monsterAngriff = monster.Angriff();
                    Sprechen($"\n\n\n{monster.Name} greift {spieler.Name} an!");

                    spieler.SchadenNehmen(ref spieler.HP, monsterAngriff);
                    Sprechen($"{spieler.Name} bekommt -{monsterAngriff} Schaden!");
                    Console.ReadKey();
                }
                break;

            case "2":
                bool inventarCheck1 = true;

                while (inventarCheck1)
                {
                    Console.Clear();
                    spieler.InventarAnzeigen();
                    Console.WriteLine("Wähle ein Item aus deinem Iventar:\n1: BroKKobomb\n2: Heiltrank (+50)\n3: Zurück zum Kampf"); //Inventar Zeigen und wählen Heiltrank
                    string itemWahl = Console.ReadLine() ?? "".Trim() ?? "";

                    switch (itemWahl)
                    {
                        case "1":
                            if (spieler.Inventar.Contains("Brokkobomb"))
                            {
                                Console.Clear();
                                spieler.Sprechen("Brokkobomb!!");
                                monster.SchadenNehmen(ref monster.HP, 10);
                                Sprechen($"\n\n\n{monster.Name} bekommt -10 Schaden!");
                                spieler.Inventar.Remove("Brokkobomb");
                                if (monster.HP > 0)
                                {
                                    int monsterAngriff = monster.Angriff();
                                    Sprechen($"\n\n\n{monster.Name} greift {spieler.Name} an!");
                                    spieler.SchadenNehmen(ref spieler.HP, monsterAngriff);
                                    Sprechen($"{spieler.Name} bekommt -{monsterAngriff} Schaden");
                                }
                                inventarCheck1 = false;
                            }
                            else
                            {
                                Console.Clear();
                                spieler.Sprechen("Keine Brokkobomb mehr!");// kann nicht zeigen
                                inventarCheck1 = false;
                            }
                            break;

                        case "2":

                            if (spieler.Inventar.Contains("Heiltrank"))// Copilot: dann kann es "Heiltrank" als "2" abrufen und bringen
                            {
                                spieler.Sprechen("Schluck*Schluck*");
                                spieler.Heilen();
                                inventarCheck1 = false;

                            }
                            else
                            {
                                spieler.Sprechen("Keine Heiltrank mehr!");
                                inventarCheck1 = false;
                            }
                            break;


                        case "3":

                            spieler.Sprechen("Zurück zum Kampf!");
                            inventarCheck1 = false;
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

        string[] items = { "Heiltrank", "Zucker", "Brokkobomb", "Müll" }; // nachdem Kampf Spieler automatisch Item bekommen(Inventar)
        int itemAnzahl = new Random().Next(1, 4);
        List<string> gewonnenItems = new List<string>();
        for (int i = 0; i < itemAnzahl; i++)
        {
            string itemFallen = items[new Random().Next(items.Length)];
            spieler.AddItem(itemFallen);
            gewonnenItems.Add(itemFallen);
        }
            Sprechen($"{string.Join(", ", gewonnenItems)} erhalten!");
        }

    Console.WriteLine("--------------------------------------------------------");

}
void BossKampf()
{
    Console.WriteLine("Das Süße Schloss");
    Boss schokoB = new Boss();

    while (schokoB.HP > 0 && spieler.HP > 0)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($" {spieler.Name} Lv.Full || HP: {spieler.HP}\n\n");
        Console.WriteLine($" {schokoB.Name} Lv.Full || HP: {schokoB.HP}\n");
        Console.WriteLine("====================================\n");

        Console.WriteLine("Dein Zug! Was möchtest du tun?\n1: Angrifen\n2: Inventar\n3: Entkommen");
        string waehlen = Console.ReadLine() ?? "".Trim() ?? "";

        switch (waehlen)
        {
            case "1":
                int spielerAngriff = spieler.Angriff();
                Sprechen($"\n\n\n{spieler.Name} greift {schokoB.Name} an!");
                schokoB.SchadenNehmen(ref schokoB.HP, spielerAngriff);
                Sprechen($"{schokoB.Name} bekommt -{spielerAngriff} Schaden!");
                

                if (schokoB.HP > 0)
                {
                    int bossAngriff = schokoB.Angriff();
                    Sprechen($"\n\n\n{schokoB.Name} greift {spieler.Name} an!");
                    spieler.SchadenNehmen(ref spieler.HP, bossAngriff);
                    Sprechen($"{spieler.Name} bekommt -{bossAngriff} Schaden!");
                    Console.ReadKey();
                }
                break;

            case "2":
                bool inventarCheck2 = true;

                while (inventarCheck2)
                {
                    Console.Clear();
                    spieler.InventarAnzeigen();
                    Console.WriteLine("Wähle ein Item aus deinem Iventar:\n1: BroKKobomb\n2: Heiltrank (+50)\n3: Zurück zum Kampf"); //Inventar Zeigen und wählen Heiltrank
                    string itemWahl = Console.ReadLine() ?? "".Trim() ?? "";

                    switch (itemWahl)
                    {
                        case "1":
                            if (spieler.Inventar.Contains("Brokkobomb"))
                            {
                                Console.Clear();
                                spieler.Sprechen("Brokkobomb!!");
                                schokoB.SchadenNehmen(ref schokoB.HP, 10);
                                Sprechen($"\n\n\n{schokoB.Name} bekommt -10 Schaden!");
                                spieler.Inventar.Remove("Brokkobomb");
                                if (schokoB.HP > 0)
                                {
                                    int monsterAngriff = schokoB.Angriff();
                                    Sprechen($"\n\n\n{schokoB.Name} greift {spieler.Name} an!");
                                    spieler.SchadenNehmen(ref spieler.HP, monsterAngriff);
                                    Sprechen($"{spieler.Name} bekommt -{monsterAngriff} Schaden");
                                }
                                inventarCheck2 = false;
                            }
                            else
                            {
                                Console.Clear();
                                spieler.Sprechen("Keine Brokkobomb mehr!");// kann nicht zeigen
                                inventarCheck2 = false;
                            }
                            break;

                        case "2":

                            if (spieler.Inventar.Contains("Heiltrank"))// Copilot: dann kann es "Heiltrank" als "2" abrufen und bringen
                            {
                                spieler.Sprechen("Schluck*Schluck*");
                                spieler.Heilen();
                                inventarCheck2 = false;

                            }
                            else
                            {
                                spieler.Sprechen("Keine Heiltrank mehr!");
                                inventarCheck2 = false;
                            }
                            break;


                        case "3":

                            spieler.Sprechen("Zurück zum Kampf!");
                            inventarCheck2 = false;
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

        if (schokoB.HP <= 0)
        {

            Sprechen("Du hast SchokoB besiegt und das süße Schloss gerettet!");

        }
    }
}
void Zelten() { }
void NeueRunde(int aktNummer)
{
    Console.Clear();
    Sprechen("Ein Monster erscheint!");

    Random rnd = new Random();
    Monster neuesMonster;

    if (aktNummer == 1)
    {
        neuesMonster = rnd.Next(2) == 0
            ? new Monster("Knoppers (Lv.1)", 35, 10, 15, 3)
            : new Monster("Hanuta (Lv.1)", 40, 15, 20, 4);
    }
    else if (aktNummer == 2)
    {
        neuesMonster = rnd.Next(2) == 0
            ? new Monster("Trolli (Lv.2)", 40, 15, 20, 5)
            : new Monster("Haribo (Lv.2)", 50, 20, 25, 6);
    }
    else // Akt3
    {
        neuesMonster = new Monster("Toffee (Lv.3)", 55, 30, 35, 7);
    }

    Kampf(neuesMonster);
}

void Sprechen(string spr)// nur hier außer andere Klasse

{
    foreach (char c in spr)
    {
        Console.Write(string.Join("", c));
        Thread.Sleep(25);

    }
    Console.WriteLine();

}

