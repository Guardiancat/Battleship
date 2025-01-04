using System;
using System.Collections.Generic;

namespace Schiffe
{
    internal class Schiff
    {
        // Die Größe des Schiffs in der Anzahl der Decks
        byte Groess { get; set; }

        // Liste der Positionen des Schiffs, die die Koordinaten repräsentieren
        public List<Zelle> Positionen { get; set; }

        // Konstruktor zur Initialisierung eines Schiffs mit einer bestimmten Größe
        public Schiff(byte groesse)
        {
            Groess = groesse;  // Setzen der Größe des Schiffs
            Positionen = new List<Zelle>();  // Initialisieren der Liste für die Positionen
        }

        // Methode zum Hinzufügen einer Koordinate zur Liste der Positionen
        public void FügeKoordinatenHinzu(Zelle zelle)
        {
            Positionen.Add(zelle);
        }

        // Methode zur Ausgabe aller gespeicherten Koordinaten des Schiffs
        public void ZeigeKoordinaten()
        {
            foreach (var zelle in Positionen)
            {
                // Ausgabe der Koordinaten im Konsolenfenster
                Console.WriteLine(zelle.GetKoordinaten());
            }
        }
    }
}
