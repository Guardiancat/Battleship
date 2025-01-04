

namespace Schiffe
{
    internal class Zelle
    {
        // Die Spalte der Zelle (Buchstabe A–J)
        public char Spalte { get; set; }

        // Die Reihe der Zelle (Zahl 1–10)
        public int Reihe { get; set; }

        // Flag zur Markierung, ob die Zelle belegt ist (z. B. durch ein Schiff)
        public bool IstBelegt { get; set; }

        // Flag zur Markierung, ob die Zelle getroffen wurde
        public bool IstGetroffen { get; set; }

        // Konstruktor zur Initialisierung einer Zelle mit Spalte und Reihe
        public Zelle(char spalte, int reihe)
        {
            Spalte = spalte;  // Setzt die Spalte
            Reihe = reihe;    // Setzt die Reihe
            IstBelegt = false; // Standardmäßig nicht belegt
            IstGetroffen = false; // Standardmäßig nicht getroffen
        }

        // Methode zur Ausgabe der Koordinaten der Zelle als String
        public string GetKoordinaten()
        {
            return $"{Spalte}{Reihe}";
        }
    }
}