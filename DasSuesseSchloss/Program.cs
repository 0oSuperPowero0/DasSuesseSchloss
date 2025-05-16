using System.Security.Cryptography.X509Certificates;
using System.IO;

using DasSuesseSchloss;
using System;
using System.Threading.Tasks.Dataflow;
using System.Threading;
using System.Xml.Linq;
using System.Net.Security;

AsciiArt charakter = new AsciiArt();
Spieler spieler = new Spieler();
Boss schokoB = new Boss();
Text text = new Text();
Console.WriteLine($"Inventar: {string.Join(", ", spieler.Inventar ?? new List<string>())}");//Null


StartMenu();

void StartMenu()
{

    Console.Clear();
    Header();
    
    Console.WriteLine("1:Starten");
    Console.WriteLine("2:Daten laden");
    Console.WriteLine("3:Verlasen");
    string antwort = (Console.ReadLine() ?? "").Trim();
    Console.Clear();

    if (antwort == "1")
    {

        Console.Clear();
        Header();

        charakter.Charakter(2);
        Console.WriteLine("Eines Tages wurde das glückliche und süße Sweet Land von einer Katastrophe heimgesucht.\r\nBaron Choco, ein Verräter aus Sweetland, hat den König von Sweet verraten und alle Höflinge im hohen Süßigkeitenturm von Sweet Castle eingesperrt.\r\nDie Prinzessin, die nur knapp entkommen war, kam, um den faulen Prinzen aus dem Nachbarland zu finden!!!\r\n");       
        spieler.AddItem("Heiltrank");
        spieler.AddItem("Heiltrank");
        spieler.AddItem("Strohhalm");
        spieler.AddItem("Brokkobomb");
        Sprechen("Ich brauche deine Hilfe!! Hier sind einige Sachen für Sie!");
        Console.Clear();
        charakter.Charakter(2);
        Sprechen("Du hast 2 Heiltrank, Brokkobomb und Strohhalm genommen.");
        Console.ReadKey();
        Console.Clear();
        charakter.Charakter(9);
        Sprechen("Ümhähä. Süßi! kommt mit mir züm Schlöss!!");
        Console.ReadKey();
        Console.Clear();
        charakter.Charakter(11);
        Sprechen("Neeeeeeeeiiin!!");
        Console.ReadKey();
        Console.Clear();
        Akt1();

    }
    else if (antwort == "2")
    {
        Header();
        DatenLaden();
    }
    else if (antwort == "3")
    {
        Header();
        Verlassen();
    }
    else
    {
        Header();
        
        Sprechen("\nWählen Sie 1, 2 oder 3");
        StartMenu();
    }
}
void Verlassen()
{
    Header();
    Sprechen("\nMöchten Sie ins Bett gehen?.");
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

    if (spieler.Level <= 0 || spieler.HP <= 0)
    {
        Sprechen("Fehler");
        StartMenu();
        return;
    }

    Console.WriteLine("Weiter spielen?\n1: Ja\n2: Nein");
    string weiterAntwort = Console.ReadLine() ?? "".Trim() ?? "";

    if (weiterAntwort == "1")
    {
        Console.Clear();
        switch (spieler.Level)
        {
            case 1:
                Akt1();
                break;
            case 2:
                Akt2();
                break;
            case 3:
                Akt3();
                break;
            default:
                BossKampf();
                break;
        }
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
    charakter.Charakter(5);
    Console.ReadKey();
    bool weiterKampfen = true;


    while (weiterKampfen)
    {
        if (spieler.Level >= 2)// Lv2 -> Akt2
        {
            spieler.Zelten();
            Akt2();
            return; //muss mit Akt2 fortfahren, also die weitere Codeausführung verhindern.
        }
        Random rnd = new Random();
        Monster zufallsMonster = rnd.Next(2) == 0
            ? new Monster("Knoppers (Lv.1)", 35, 10, 15, 20)
            : new Monster("Hanuta (Lv.1)", 40, 15, 20, 20);

        Console.Clear();
        Sprechen("Ein knuspriges Monster erscheint!");
        Console.ReadKey();
        Kampf(zufallsMonster);
        SpielSpeichern();

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
           return;
       }
        if (spieler.XP >= 30 && spieler.Level == 1)
        { spieler.LevelUp(); }

    }

}

void Akt2()
{
    
    Console.WriteLine(" Willkommen in Pummelig Gummifeld!");
    Console.ReadKey();
    bool weiterKamfen = true;
    while (weiterKamfen)
    {
        if (spieler.Level >= 3)
        {
            spieler.Zelten();
            Akt3();
            return;
        }
        Random rnd = new Random();
        Monster zufallsMonster = rnd.Next(2) == 0
            ? new Monster("Trolli (Lv.2)", 40, 15, 20, 30)
            : new Monster("Haribo (Lv.2)", 50, 20, 25, 30);

        Console.Clear();
        Sprechen("Ein pummeliges Monster erscheint!");
        Console.ReadKey();
        Kampf(zufallsMonster);


        SpielSpeichern();
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
            return;
        }
        if(spieler.XP >= 50 && spieler.Level ==2)
         { spieler.LevelUp(); }
    }

}

void Akt3()
{
    
    Console.WriteLine(" Willkommen in Karamell Sumpf!");
    Console.ReadKey();
    bool weiterKamfen = true;

    while (weiterKamfen)
    {
        if (spieler.Level >= 4)
        {
            spieler.Zelten();
            BossKampf();
            return; //muss mit Akt2 fortfahren, also die weitere Codeausführung verhindern.
        }
        Monster karmellMonster = new Monster("Toffee (Lv.3)", 55, 30, 35, 50);

        Console.Clear();
        Sprechen("Ein klebriges Monster erscheint!");
        Console.ReadKey();
        Kampf(karmellMonster);
        SpielSpeichern();

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
            return;
        }
        if (spieler.XP >= 60 && spieler.Level == 3)
        { spieler.LevelUp(); }
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
                Sprechen($"{spieler.Name} greift {monster.Name} an!");
                monster.SchadenNehmen(spielerAngriff);
                Sprechen($"{monster.Name} bekommt -{spielerAngriff} Schaden!");
                Console.ReadKey();


                if (monster.HP > 0)
                {
                    Console.Clear();
                    int monsterAngriff = monster.Angriff();
                    Sprechen($"{monster.Name} greift {spieler.Name} an!");
                    spieler.SchadenNehmen(monsterAngriff);
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
                                monster.SchadenNehmen( 20);
                                Sprechen($"\n\n\nBrokkobomb!!!\n{schokoB.Name} bekommt -10 Schaden!");
                                spieler.Inventar.Remove("Brokkobomb");
                                Console.ReadKey();
                                if (monster.HP > 0)
                                {
                                    Console.Clear();
                                    int monsterAngriff = monster.Angriff();
                                    Sprechen($"\n\n\n{monster.Name} greift {spieler.Name} an!");                                    
                                    spieler.SchadenNehmen( monsterAngriff);
                                    Sprechen($"{spieler.Name} bekommt -{monsterAngriff} Schaden");
                                    Console.ReadKey();
                                }
                                inventarCheck1 = false;
                            }
                            else
                            {
                                Console.Clear();
                                Sprechen("Keine Brokkobomb mehr!");// kann nicht zeigen
                                Console.ReadKey();
                                inventarCheck1 = false;
                            }
                            break;

                        case "2":

                            if (spieler.Inventar.Contains("Heiltrank"))// Copilot: dann kann es "Heiltrank" als "2" abrufen und bringen
                            {
                                spieler.Heilen();
                                Console.Clear();
                                Sprechen("Schluck*Schluck");
                                Sprechen($"{spieler.Name} hat einen Heiltrank getrunken.\n + 50 HP\n<{spieler.Name} : {spieler.HP}>");
                                Console.ReadKey();
                                inventarCheck1 = false;

                            }
                            else
                            {
                                Sprechen("Keine Heiltrank mehr!");
                                Console.ReadKey();
                                inventarCheck1 = false;
                            }
                            break;


                        case "3":

                            Sprechen("Zurück zum Kampf!");
                            Console.ReadKey();
                            inventarCheck1 = false;
                            break;
                        default:
                            Sprechen("Wähle richitig!");
                            Console.ReadKey();
                            break;
                    }

                }
                break;

            case "3":

                spieler.Sprechen("Lauf weg!");
                Console.ReadKey();
                StartMenu();
                break;

            default:
                Sprechen("Wähle ricitig!");
                Console.ReadKey();
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
    Console.ReadKey();

}
void BossKampf()
{
    Console.WriteLine("Das Süße Schloss");
    Header();
    charakter.Charakter(9);
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
                Console.Clear();
                int spielerAngriff = spieler.Angriff();
                Sprechen($"{spieler.Name} greift {schokoB.Name} an!");                
                schokoB.SchadenNehmen(spielerAngriff);
                Sprechen($"{schokoB.Name} bekommt -{spielerAngriff} Schaden!");
                Console.ReadKey();


                if (schokoB.HP > 0)
                {
                    Console.Clear();
                    int bossAngriff = schokoB.Angriff();
                    Sprechen($"{schokoB.Name} greift {spieler.Name} an!");                    
                    spieler.SchadenNehmen(bossAngriff);
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
                                schokoB.SchadenNehmen(5);
                                Sprechen($"\n\n\nBrokkobomb!!!\n{schokoB.Name} bekommt -10 Schaden!");
                                spieler.Inventar.Remove("Brokkobomb");
                                Console.ReadKey();
                                if (schokoB.HP > 0)
                                {
                                    Console.Clear();
                                    int monsterAngriff = schokoB.Angriff();
                                    Sprechen($"\n\n\n{schokoB.Name} greift {spieler.Name} an!");
                                    spieler.SchadenNehmen(monsterAngriff);
                                    Sprechen($"{spieler.Name} bekommt -{monsterAngriff} Schaden");
                                    Console.ReadKey();
                                }
                                inventarCheck2 = false;
                            }
                            else
                            {
                                Console.Clear();
                                Sprechen("Keine Brokkobomb mehr!");// kann nicht zeigen
                                Console.ReadKey();
                                inventarCheck2 = false;
                            }
                            break;

                        case "2":

                            if (spieler.Inventar.Contains("Heiltrank"))// Copilot: dann kann es "Heiltrank" als "2" abrufen und bringen
                            {
                                spieler.Heilen();
                                Console.Clear();
                                Sprechen("Schluck*Schluck");
                                Sprechen($"{spieler.Name} hat einen Heiltrank getrunken.\n + 50 HP\n<{spieler.Name} : {spieler.HP}>");
                                Console.ReadKey();
                                inventarCheck2 = false;

                            }
                            else
                            {
                                Sprechen("Keine Heiltrank mehr!");
                                Console.ReadKey();
                                inventarCheck2 = false;
                            }
                            break;


                        case "3":
                            
                            Sprechen("Zurück zum Kampf!");
                            Console.ReadKey();
                            inventarCheck2 = false;
                            break;
                        default:
                            Sprechen("Wähle richitig!");
                            Console.ReadKey();
                            break;
                    }
                }
                break;

            case "3":

                Sprechen("Lauf weg!");
                Console.ReadKey();
                StartMenu();
                break;

            default:
                Sprechen("Wähle ricitig!");
                Console.ReadKey();
                break;
        }
        Console.Clear();

        if (schokoB.HP <= 0)
        {            
            Sprechen("Du hast SchokoB besiegt und das süße Schloss gerettet!");
            Console.ReadKey();
            Console.Clear();
            charakter.Charakter(10);
            Sprechen("Süße Ending!!");
            Console.ReadKey();
            StartMenu();
        }
    }
}
void Zelten() {}
void NeueRunde(int aktNummer)
{
    Console.Clear();
    Sprechen("Ein Monster erscheint!");
    Console.ReadKey();

    Random rnd = new Random();
    Monster neuesMonster;

    if (aktNummer == 1)
    {
        neuesMonster = rnd.Next(2) == 0
            ? new Monster("Knoppers (Lv.1)", 35, 10, 15, 20)
            : new Monster("Hanuta (Lv.1)", 40, 15, 20, 20);
    }
    else if (aktNummer == 2)
    {
        neuesMonster = rnd.Next(2) == 0
            ? new Monster("Trolli (Lv.2)", 40, 15, 20, 30)
            : new Monster("Haribo (Lv.2)", 50, 20, 25, 30);
    }
    else // Akt3
    {
        neuesMonster = new Monster("Toffee (Lv.3)", 55, 30, 35, 50);
    }

    Kampf(neuesMonster);
}
void AddSkript() { }
void Header()
{
    Console.WriteLine(@".oOo.oOo.oOo.oOo.oOo.oOo.oOo.oOo.
|>  |>                     |>  |>
/\  /\                     /\  /\
||__||  Das Süße Schloss   ||__||
| oo |                     | oo |
| || |_.""._.""._.""._.""._.""._| || |");
    Console.WriteLine("");
    Console.WriteLine("");
}

void Sprechen(string text)// nur hier außer andere Klasse

{
    foreach (char c in text)
    {
        Console.Write(string.Join("", c));
        Thread.Sleep(25);

    }
    Console.WriteLine();
}