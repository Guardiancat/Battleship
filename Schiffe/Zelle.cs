using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffe
{
    internal class Zelle
    {
        public char Spalte { get; set; }
        public int Reihe { get; set; }
        public bool IstBelegt { get; set; }  // Новый флаг занятости ячейки
        public bool IstGetroffen { get; set; } //флаг для попаданий
        public Zelle(char spalte, int reihe)
        {
            Spalte = spalte;
            Reihe = reihe;
            IstBelegt = false;
            IstGetroffen = false;
        }

        public string GetKoordinaten()
        {
            return $"{Spalte}{Reihe}";
        }
    }
}
