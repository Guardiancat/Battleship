using System;
using System.Collections.Generic;




namespace Schiffe
{
    internal class Schlachtfeld
    {
        // Die Feldgröße ist auf 10x10 festgelegt
        private const int groesse = 10;

        // Zellenanordnung des Schlachtfelds
        private Zelle[,] zellen;

        // Liste der Schiffe auf dem Schlachtfeld
        private List<Schiff> schiffe;

        // Konstruktor zur Initialisierung des Schlachtfelds
        public Schlachtfeld()
        {
            zellen = new Zelle[groesse, groesse];
            schiffe = new List<Schiff>();

            // Initialisierung der Zellen mit Koordinaten
            for (int reihe = 0; reihe < groesse; reihe++)
            {
                for (int spalte = 0; spalte < groesse; spalte++)
                {
                    zellen[reihe, spalte] = new Zelle((char)('A' + spalte), reihe + 1);
                }
            }

            // Zufälliges Platzieren der Schiffe
            Random rand = new Random();
            PlatziereSchiffe(rand);
        }

        // Methode zur zufälligen Platzierung der Schiffe
        private void PlatziereSchiffe(Random rand)
        {
            for (byte groesse = 2; groesse <= 5; groesse++)
            {
                bool platziert = false;
                while (!platziert)
                {
                    // Zufällige Startkoordinaten und Richtung (0 - horizontal, 1 - vertikal)
                    int startSpalte = rand.Next(0, Schlachtfeld.groesse);
                    int startReihe = rand.Next(0, Schlachtfeld.groesse);
                    bool istHorizontal = rand.Next(2) == 0;

                    // Überprüfung, ob das Schiff ins Feld passt und keine Überschneidungen vorliegen
                    if (PasstInsFeld(startReihe, startSpalte, groesse, istHorizontal) &&
                        KeineUeberschneidung(startReihe, startSpalte, groesse, istHorizontal))
                    {
                        Schiff schiff = new Schiff(groesse);
                        for (int i = 0; i < groesse; i++)
                        {
                            int reihe = istHorizontal ? startReihe : startReihe + i;
                            int spalte = istHorizontal ? startSpalte + i : startSpalte;
                            schiff.FügeKoordinatenHinzu(zellen[reihe, spalte]);
                        }
                        schiffe.Add(schiff);
                        platziert = true;

                        // Markierung der belegten Zellen
                        foreach (var zelle in schiff.Positionen)
                        {
                            zelle.IstBelegt = true;
                        }
                    }
                }
            }
        }

        // Überprüfung, ob das Schiff ins Spielfeld passt
        private bool PasstInsFeld(int startReihe, int startSpalte, int groesse, bool istHorizontal)
        {
            if (istHorizontal)
                return startSpalte + groesse <= Schlachtfeld.groesse;
            else
                return startReihe + groesse <= Schlachtfeld.groesse;
        }

        // Überprüfung, ob keine Überschneidungen mit anderen Schiffen vorliegen
        private bool KeineUeberschneidung(int startReihe, int startSpalte, int groesse, bool istHorizontal)
        {
            for (int i = 0; i < groesse; i++)
            {
                int reihe = istHorizontal ? startReihe : startReihe + i;
                int spalte = istHorizontal ? startSpalte + i : startSpalte;
                if (zellen[reihe, spalte].IstBelegt)  // Überprüfung, ob die Zelle belegt ist
                    return false;
            }
            return true;
        }

        // Methode zur Überprüfung eines Treffers
        public bool PruefeTreffer(char spalte, int reihe)
        {
            foreach (var schiff in schiffe)
            {
                foreach (var zelle in schiff.Positionen)
                {
                    if (zelle.Spalte == spalte && zelle.Reihe == reihe)
                    {
                        zelle.IstGetroffen = true;  // Treffer markieren
                        return true;  // Treffer
                    }
                }
            }
            zellen[reihe - 1, spalte - 'A'].IstGetroffen = true;  // Fehlschuss markieren
            return false;  // Fehlschuss
        }

        // Methode zur Darstellung des Schlachtfelds
        public void Zeichnen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("     A   B   C   D   E   F   G   H   I   J");  // Spaltenüberschriften
            Console.ResetColor();

            for (int reihe = 0; reihe < groesse; reihe++)
            {
                Console.Write("   +");
                for (int spalte = 0; spalte < groesse; spalte++)
                {
                    Console.Write("---+");
                }
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{reihe + 1,2},|");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;

                for (int spalte = 0; spalte < groesse; spalte++)
                {
                    Zelle zelle = zellen[reihe, spalte];
                    if (zelle.IstGetroffen)
                    {
                        Console.ForegroundColor = zelle.IstBelegt ? ConsoleColor.Red : ConsoleColor.Gray;
                        Console.Write(zelle.IstBelegt ? " X " : " O ");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.Write(" ~ ");
                    }
                    Console.Write("|");
                }
                Console.WriteLine();
            }

            Console.Write("   +");
            for (int spalte = 0; spalte < groesse; spalte++)
            {
                Console.Write("---+");
            }
            Console.WriteLine();
        }
    }
}