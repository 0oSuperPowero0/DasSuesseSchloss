using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss;

public class Text
{ 
private Dictionary<string, string> skripte = new Dictionary<string, string>();
    public Text()
    {
        skripte["einleitung"] = "Das Süse Schloss.";
        skripte["akt1"] = "Staubig Gegend";
        skripte["akt2"] = "Boing Boing";
        skripte["akt3"] = "klebrig matschig";
        skripte["boss"] = "Böse Böse";
        skripte["ende"] = "Happy End";

    }
 
    public string AddSkript(string key)
    {
        return skripte.ContainsKey(key) ? skripte[key] : "Fehler"; // ContainsKey: wird überprüft, ob zu dem angegebenen Schlüssel 'Key' ein Eintrag existiert. wie ein bool
    }
}
