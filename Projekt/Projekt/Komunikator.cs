using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Komunikator
    {
        /// <summary>
        /// Metoda pozwala na stworzenie wstępnie uzupełnionej planszy gry
        /// </summary>
        /// <param name="rozm"> Rozmiar planszy z zakresu 15-51 </param>
        /// <param name="wejscie"> String z danymi wcześniej zapełnionych pól </param>
        /// <returns>Uzupełniona wstepnie plansza gry</returns>
        public static Plansza WstepneUzupelnianie(int rozm, string wejscie)
        {
            //Dzielenie stringa na poledyncze ruchy
            var zajetePoSplit = wejscie.Split(',').ToList();
            var zajete = new List<Koordynaty>();

            //Usuwanie klamer i odzielanie współrzędnych
            foreach (var ciag in zajetePoSplit)
            {
                var temp = ciag.Replace("{", string.Empty);
                temp = temp.Replace("}", string.Empty);
                var koordynaty = temp.Split(';').ToArray();
                zajete.Add(new Koordynaty(int.Parse(koordynaty[0]), int.Parse(koordynaty[1])));
            }

            //Tworzenie planszy
            var plansza = new Plansza(rozm);

            //Zapełnianie planszy predefiniowanymi elementami
            foreach (var item in zajete)
            {
                plansza.ZmienStanPola(item.kolumna, item.wiersz, false);
            }
            
            return plansza;
        }

        public static Ruch OdczytajRuch(string wejscie)
        {
            var ruchPoSplit = wejscie.Split(',').ToList();
            var ruchKoor = new List<Koordynaty>();
            foreach (var ciag in ruchPoSplit)
            {
                var temp = ciag.Replace("{", string.Empty);
                temp = temp.Replace("}", string.Empty);
                var koordynaty = temp.Split(';').ToArray();
                ruchKoor.Add(new Koordynaty(int.Parse(koordynaty[0]), int.Parse(koordynaty[1])));
            }

            var kol = new List<int>();
            var wier = new List<int>();

            foreach (var item in ruchKoor)
            {
                kol.Add(item.kolumna);
                wier.Add(item.wiersz);
            }

            return new Ruch(kol.ToArray(), wier.ToArray());
        }
    }
}
