using System;

namespace Schiffe
{
    internal class Menu
    {
        // Instanz des Schlachtfelds zur Verwaltung des Spielfelds
        readonly Schlachtfeld schlachtfeld = new Schlachtfeld();

        // Variable zur Steuerung der Hauptmenü-Schleife
        bool close = true;

        // Methode zur Anzeige des Hauptmenüs
        public void MethodHauptMenu()
        {
            while (close)
            {
                // Anzeige der Menüoptionen
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("   MENU   \n\n   Starten   1 \n\n   Ausloggen  2");

                // Eingabe des ausgewählten Menüpunktes
                byte ausGewähltePunkt = Convert.ToByte(Console.ReadLine());
                Console.Clear();

                switch (ausGewähltePunkt)
                {
                    case 1:
                        // Starten des Spiels
                        Console.ForegroundColor = ConsoleColor.Green;
                        SpielMenu();
                        Console.ResetColor();
                        break;
                    case 2:
                        // Beenden des Programms
                        close = false;
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        // Methode zur Steuerung des Spielmenüs
        public void SpielMenu()
        {
            schlachtfeld.Zeichnen(); // Zeichnen des Schlachtfelds

            bool imSpiel = true;

            while (imSpiel)
            {
                // Überprüfung auf ESC-Taste für den Menüwechsel
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    imSpiel = false;  // Zurück zum Hauptmenü
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;

                // Abfrage der X-Koordinate (Buchstabe A–J)
                Console.WriteLine("\n\nGeben Sie die Koordinate X (A–J) ein und drücken Sie Enter:");
                string eingabeX = Console.ReadLine().ToUpper();

                // Validierung der X-Koordinate
                if (eingabeX.Length == 1 && eingabeX[0] >= 'A' && eingabeX[0] <= 'J')
                {
                    char spalteChar = eingabeX[0];  // Umwandlung des Buchstabens

                    // Abfrage der Y-Koordinate (Zahl 1–10)
                    Console.WriteLine("Geben Sie die Koordinate Y (1–10) ein und drücken Sie Enter:");
                    string eingabeY = Console.ReadLine();

                    // Validierung der Y-Koordinate
                    if (int.TryParse(eingabeY, out int reihe) && reihe >= 1 && reihe <= 10)
                    {
                        int spalte = spalteChar - 'A';  // Umwandlung in einen Array-Index (z. B. 'A' -> 0)

                        // Überprüfung auf Treffer eines Schiffs
                        if (schlachtfeld.PruefeTreffer(spalteChar, reihe))
                        {
                            Console.WriteLine($"Treffer auf Position {spalteChar}{reihe}!");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Fehlschuss! Kein Schiff getroffen.");
                            Console.ReadKey();
                        }

                        schlachtfeld.Zeichnen();  // Aktualisierung des Schlachtfelds
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Y-Koordinate! Bitte geben Sie eine Zahl von 1 bis 10 ein.");
                    }
                }
                else
                {
                    Console.WriteLine("Ungültige X-Koordinate! Bitte geben Sie einen Buchstaben von A bis J ein.");
                }
            }
        }
    }
}

