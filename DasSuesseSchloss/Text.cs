using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DasSuesseSchloss;

public class Text
{ 
private Dictionary<string, string> skripte = new Dictionary<string, string>();
    public Text()
    {
        Laden();
    }
    private void Laden()
    {
        if (File.Exists("skripte.jsosn"))
        { string json = File.ReadAllText("skripte.json");
            skripte = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        else
        {
        skripte["einleitung1"] = "Das Süse Schloss.\nEines Tages wurde das glückliche und süße Sweet Land von einer Katastrophe heimgesucht.\r\nBaron Choco, ein Verräter aus Sweetland, hat den König von Sweet verraten und alle Höflinge im hohen Süßigkeitenturm von Sweet Castle eingesperrt.\r\nDie Prinzessin, die nur knapp entkommen war, kam, um den faulen Prinzen aus dem Nachbarland zu finden!!!\r\n";
            skripte[""] = "";
            
        skripte["akt1"] = "Staubig Gegend";
        skripte["akt2"] = "Boing Boing";
        skripte["akt3"] = "klebrig matschig";
        skripte["boss"] = "Böse Böse";
        skripte["ende"] = "Happy End";
        
        Speichern();
        }
        }
    public void Speichern()
    {
        string json = JsonSerializer.Serialize(skripte, new JsonSerializerOptions { WriteIndented = true});
        File.WriteAllText("skripte.json", json);
    }
public string AddSkript(string key)
{
    return skripte.ContainsKey(key) ? skripte[key] : "Fehler"; // ContainsKey: wird überprüft, ob zu dem angegebenen Schlüssel 'Key' ein Eintrag existiert. wie ein bool
}
}

