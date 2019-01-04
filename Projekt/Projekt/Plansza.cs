using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Plansza
    {
        Pole[,] plansza;
        int Rozm;
        public Plansza(int rozm)
        {
            plansza = new Pole[rozm, rozm];
            Rozm = rozm;
        }

        public bool CzyMożnaTuWpasowaćKlocek(int kolumna, int wiersz)
        {
            int k1 = kolumna - 1;
            int k2 = kolumna + 1;
            int w1 = wiersz - 1;
            int w2 = wiersz + 1;

            //Granice planszy nie stanowia problemu, więc jeśli wychodzi poza planszę to zmieniam, warość na taką z drugiego konca
            k1 = k1 < 0 ? Rozm - 1 : k1;
            k1 = k1 > Rozm - 1 ? 0 : k1;

            k2 = k2 < 0 ? Rozm - 1 : k2;
            k2 = k2 > Rozm - 1 ? 0 : k2;

            w1 = w1 < 0 ? Rozm - 1 : w1;
            w1 = w1 > Rozm - 1 ? 0 : w1;

            w2 = w2 < 0 ? Rozm - 1 : w2;
            w2 = w2 > Rozm - 1 ? 0 : w2;

            if (plansza[kolumna, wiersz].puste == true && (
                plansza[kolumna, w1].puste == true ||
                plansza[kolumna, w2].puste == true ||
                plansza[k1, wiersz].puste == true ||
                plansza[k2, wiersz].puste == true))
            {
                return true;
            }
            return false;
        }
    }
}
