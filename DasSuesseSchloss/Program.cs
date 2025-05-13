using System.Security.Cryptography.X509Certificates;

using DasSuesseSchloss;

//  Console.SetWindowSize(100, 50);
//  Console.SetBufferSize(100, 50);
// Konsole Fenster Größe


Spieler spieler = new Spieler();


Console.WriteLine("1.Starten");
Console.WriteLine("2.Speichern");
Console.WriteLine("3.Verlasen");
string antwort = (Console.ReadLine() ?? "").Trim();

if (antwort == "1")
{
    Console.Clear();
    Sprechen($"HILFE!!\nHilf mir!");
    Console.WriteLine();
    Console.Clear();
    // Akt1(); // !! endlose Schleife

}
else if (antwort == "2")
{
    spieler.Speichern();
}
else if (antwort == "3")
{
    Sprechen("Möchten Sie ins Bett gehen?.");
    Environment.Exit(0);// Beenden
}
else
{ Console.WriteLine("Wählen Sie 1, 2 oder 3"); }

void Sprechen(string text)// nur hier außer andere Klasse
{
    foreach (char c in text)
    {
        Console.WriteLine(string.Join("", c));
        Thread.Sleep(50);
    }
    Console.WriteLine();
}

void Akt1()
{
    Console.WriteLine(" Willkommen in der Plätzchen Wüste!");
    Kampf(new List<MonsterGruppe>
            {
                new MonsterGruppe("Knoppers", 35, 10, 15, 1, 3, 5, "Plätzchen Wüste"),
                new MonsterGruppe("Hanuta", 40, 15, 20, 1, 2, 6, "Plätzchen Wüste")
            });

    Console.Clear();
    Akt1();
}

void Akt2()
{
    Console.WriteLine(" Willkommen in Pummelig Gummifeld!");
    Kampf(new List<MonsterGruppe>
            {
                new MonsterGruppe("Trolli", 40, 20, 25, 1, 3, 8, "Pummelig Gummifeld"),
                new MonsterGruppe("Haribo", 50, 25, 40, 1, 2, 10, "Pummelig Gummifeld")
            });

    Console.Clear();
    Akt2();
}

void Akt3()
{
    Console.WriteLine(" Willkommen in Karamell Sumpf!");
    Kampf(new List<MonsterGruppe>
            {
                new MonsterGruppe("Toffee", 60, 35, 45, 1, 3, 20, "Karamell Sumpf")
            });

}

void Bossbegegnen()
{
    Boss schokoB = new Boss();
    schokoB.Dialog1();
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
                            spieler.Heilen();
                        }
                        else if (itemWahl == "3")
                        {
                            spieler.Sprechen("Zurück zum Kampf!");
                            inventarCheck = false;
                        }
                        else
                        {
                            spieler.Sprechen("Wähle richitig ein!");
                        }
                    }                    
                }
                else if (waehlen == "3")
                {
                    spieler.Sprechen("Lauf weg!");
                    break;
                }
                else { Sprechen("Wähle ricitig ein!"); }

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