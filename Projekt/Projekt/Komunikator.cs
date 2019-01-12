using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Komunikator
    {
        public static Plansza WstepneUzupelnianie(int rozm, string wejscie)
        {
            var zajetePoSplit = wejscie.Split(',').ToList();
            var zajete = new List<Koordynaty>();
            foreach (var ciag in zajetePoSplit)
            {
                var temp = ciag.Replace("{", string.Empty);
                temp = temp.Replace("}", string.Empty);
                var koordynaty = temp.Split(';').ToArray();
                zajete.Add(new Koordynaty(int.Parse(koordynaty[0]), int.Parse(koordynaty[1])));
            }

            var plansza = new Plansza(rozm);

            foreach (var item in zajete)
            {
                plansza.ZmienStanPola(item.kolumna, item.wiersz, false);
            }
            
            return plansza;
        }
    }
}
