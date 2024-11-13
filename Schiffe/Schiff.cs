using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffe
{
    internal class Schiff
    {
        byte Groess { get; set; }  // Anzahl der Decks
        public List<Zelle> Positionen { get; set; }  // Koordinate

        public Schiff(byte groesse)
        {
            Groess = groesse;
            Positionen = new List<Zelle>();
        }

        public void FügeKoordinatenHinzu(Zelle zelle)
        {
            Positionen.Add(zelle);
        }

        public void ZeigeKoordinaten()
        {
            foreach (var zelle in Positionen)
            {
                Console.WriteLine(zelle.GetKoordinaten());
            }
        }
    }
}
