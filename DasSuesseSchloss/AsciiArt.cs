using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasSuesseSchloss
{
    public class AsciiArt
    {
        public List<string> charakterListe = new List<string>
{@"
  
       /\/\/\
      |   P  |
      /(((())\  
      ( O _O )  
     </__||__\>  
      _||  ||_
",
@"
  
       /\/\/\
      |   S  |
     ((((( ))))  
   (((( O _O )))) 
   (((/_o||o_\)))  
     |/ /--\ \|  
",
@"
  
       /\/\/\                 /\/\/\
      |   P  |               |  S   |
      /(((())\              (((( ))))) 
      ( O _O )            (((( O_ O )))) 
     </__||__\>           (((/_o||o_\))) 
      _||  ||_              |/ /--\ \| 
",
@"
  
       ================
       |//\\//\\//\\//|\
  ((() |\\//\\//\\//\\| ()))
    \\ |//\\0 M 0 \\//| //
     \\|\\//\\//\\//\\|//
       |//\\//\\//\\//| |
       |\\//\\//\\//\\| |
       |//\\//\\//\\//| |
       =================
",
@"
 
       XXXXXXXXXXXXXX         
   ((()X \/ \/ \/  XXY()))        
     \\XX 0 M 0 X XXXX//        
      \X X X X X X XX//         
       XX X X X X XXXX         
       X X X X X X XXX         
       XXXXXXXXXXXXXXX 
",
@"
     
           HANUTA      
       ================            KNOPPERS
       |//\\//\\//\\//|\           
  ((() |\\//\\//\\//\\| ()))    XXXXXXXXXXXXXX  
    \\ |//\\0 M 0 \\//| //   ((()X \/ \/ \/  XXY()))  
     \\|\\//\\//\\//\\|//     \\XX 0 M 0 X XXXX//        
       |//\\//\\//\\//| |      \X X X X X X XX//  
       |\\//\\//\\//\\| |       XX X X X X XXXX 
       |//\\//\\//\\//| |       X X X X X X XXX   
       =================        XXXXXXXXXXXXXXX 
",
@"
       HARIBO
      (H)---(H)
      ( 0 M 0 )       
      ( () () )
      (()___())
",
@"
     
      TROLLI   
      (0v0 )  _
       (  )__( )
        (__)_)_)
",
@"
       HARIBO
      (H)---(H)      TROLLI   
      ( 0 M 0 )      (0v0 )  _       
      ( () () )       (  )__( )
      (()___())        (__)_)_)
",
            @"
              BITTER SCHOKOLADE
           _________________________
           |xxx|xxx|xxx|xxx|xxx|xxx|
           |xxx|xxx|xxx|xxx|xxx|xxx|
           -------------------------
           |xxx|xxx|xxx|xxx|xxx|xxx|
           |xxx__xx|xxx|xxx|xx__xxx|
           ---/_m_\---------/_m_\---
           |xxx|xxx|xxx|xxx|xxx|xxx|
           |xxx|xxx|xxx|xxx|xxx|xxx|
        ///--------VVVVVVVVV--------\\\
      ///  |xxx|xxx|x     x|xxx|xxx|  \\\
     ///   |xxx|xxx|AAAAAAA|xxx|xxx|   \\\
    ///    -------------------------    \\\
  ((()()   |xxx|xxx|xxx|xxx|xxx|xxx|   ()()))
           |   |   |\xx|xx/|   |   |
           -------------------------
           |   |   |   |   |   |   |
           |   |   |   |   |   |   |
           -------------------------
"};
        public void Charakter(int charakterNummer)
        {
            if (charakterNummer >= 0 && charakterNummer < charakterListe.Count)
            {
                Console.WriteLine(charakterListe[charakterNummer]);
                // 0- PrinzenRolle, 1- SüßPrinzessin, 2-PS, 3- Hanuta, 4-Knoppers, 5-HK, 6-Haribo, 7-Trolli,8-HT, 9-SchokoB
            }
            else
            {
                Console.WriteLine("Fehler");
            }
        }

    }
}
