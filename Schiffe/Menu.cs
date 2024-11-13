using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffe
{
    internal class Menu
    {
        
       readonly Schlachtfeld schlachtfeld = new Schlachtfeld();
        bool close = true;

        public void MethodHauptMenu()
        {

            while (close)
            {
                
                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.WriteLine("   MENU   \n\n   Starten   1 \n\n   Auslogen  2");
                byte ausGewähltePunkt = Convert.ToByte(Console.ReadLine());
                Console.Clear();
                switch (ausGewähltePunkt)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        SpielMenu();
                        Console.ResetColor();
                        
                        break;
                    case 2:
                        close = false;
                        Console.ReadKey(true);
                        break;
                }

            }   
        }
        public void SpielMenu()
        {
            schlachtfeld.Zeichnen();
           

            bool imSpiel = true;

            while (imSpiel)
            {
                // Проверка нажатия Esc для выхода
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    imSpiel = false;  // Возврат в главное меню
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;

                // Ввод координаты по оси X (буква A–J)
                Console.WriteLine("\n\nGeben Sie die Koordinate X (A–J) ein und drücken Sie Enter:");
                string eingabeX = Console.ReadLine().ToUpper();

                // Проверка корректности ввода для X
                if (eingabeX.Length == 1 && eingabeX[0] >= 'A' && eingabeX[0] <= 'J')
                {
                    char spalteChar = eingabeX[0];  // Преобразуем введённую букву

                    // Ввод координаты по оси Y (число 1–10)
                    Console.WriteLine("Geben Sie die Koordinate Y (1–10) ein und drücken Sie Enter:");
                    string eingabeY = Console.ReadLine();

                    // Проверка корректности ввода для Y
                    if (int.TryParse(eingabeY, out int reihe) && reihe >= 1 && reihe <= 10)
                    {
                        int spalte = spalteChar - 'A';  // Преобразуем букву в индекс массива (например, 'A' -> 0)

                        // Проверка на попадание по кораблю
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

                        schlachtfeld.Zeichnen();  // Обновляем поле
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

