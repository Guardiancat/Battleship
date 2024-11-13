using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffe
{
    internal class Schlachtfeld
    {
        private const int groesse = 10;   // Die Feldgröße ist auf 10x10 festgelegt.
        private Zelle[,] zellen;          // Zellenanordnung
        private List<Schiff> schiffe;     // Список кораблей

        public Schlachtfeld()
        {
            zellen = new Zelle[groesse, groesse];
            schiffe = new List<Schiff>();

            // Initialisierung von Zellen mit Koordinaten
            for (int reihe = 0; reihe < groesse; reihe++)
            {
                for (int spalte = 0; spalte < groesse; spalte++)
                {
                    zellen[reihe, spalte] = new Zelle((char)('A' + spalte), reihe + 1);
                }
            }

            // Рандомное размещение кораблей
            Random rand = new Random();
            PlatziereSchiffe(rand);

        }

        // Метод для размещения кораблей
        private void PlatziereSchiffe(Random rand)
        {
            for (byte groesse = 2; groesse <= 5; groesse++)
            {
                bool platziert = false;
                while (!platziert)
                {
                    // Рандомные начальные координаты и направление (0 - горизонтально, 1 - вертикально)
                    int startSpalte = rand.Next(0, groesse);
                    int startReihe = rand.Next(0, groesse);
                    bool istHorizontal = rand.Next(2) == 0;

                    // Проверка, что корабль помещается и не пересекает другие
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
                        foreach (var zelle in schiff.Positionen)
                        {
                            zelle.IstBelegt = true;
                        }
                    }
                }
            }

        }
        private bool PasstInsFeld(int startReihe, int startSpalte, int groesse, bool istHorizontal)
        {
            if (istHorizontal)
                return startSpalte + groesse <= Schlachtfeld.groesse;
            else
                return startReihe + groesse <= Schlachtfeld.groesse;
        }
        // Проверка на отсутствие пересечений с другими кораблями
        private bool KeineUeberschneidung(int startReihe, int startSpalte, int groesse, bool istHorizontal)
        {
            for (int i = 0; i < groesse; i++)
            {
                int reihe = istHorizontal ? startReihe : startReihe + i;
                int spalte = istHorizontal ? startSpalte + i : startSpalte;
                if (zellen[reihe, spalte].IstBelegt)  // Проверяем, занята ли ячейка
                    return false;
            }
            return true;
        }
        // Проверка попадания
        public bool PruefeTreffer(char spalte, int reihe)
        {
            foreach (var schiff in schiffe)
            {
                foreach (var zelle in schiff.Positionen)
                {
                    if (zelle.Spalte == spalte && zelle.Reihe == reihe)
                    {
                        zelle.IstGetroffen = true;  // Отмечаем попадание
                        return true;  // Попадание
                    }
                }
            }
            zellen[reihe - 1, spalte - 'A'].IstGetroffen = true;  // Отмечаем промах
            return false;  // Промах
        }

        // Метод отрисовки поля
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
