using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Plansza : ICloneable
    {
        public Pole[,] plansza { get; set; }
        public int Rozm { get; set; }

        public Plansza(int rozm)
        {
            plansza = new Pole[rozm, rozm];
            Rozm = rozm;
        }

        public Plansza(Pole[,] poles, int rozm)
        {
            this.plansza = poles;
            this.Rozm = rozm;
        }

        public Plansza() { }

        public object Clone()
        {
            return this.SkopiujPole(this);
        }

        public Plansza KopiujIWykonajRuch(Ruch ruch)
        {
            DodajRuchDoPlanszy(ruch);
            return this;
        }

        public Pole[,] DajTabliceDwuWym()
        {
            return this.plansza;
        }

        public void DodajRuchDoPlanszy(Ruch ruch)
        {
            ZmienStanPola(ruch.Kolumna[0], ruch.Wiersz[0], false);
            ZmienStanPola(ruch.Kolumna[1], ruch.Wiersz[1], false);
        }

        public void ZmienStanPola(int kol, int wier, bool puste)
        {
            if (plansza[kol, wier] == null)
            {
                plansza[kol, wier] = new Pole();
            }
            plansza[kol, wier].puste = puste;
        }

        public Ruch CzyMożnaTuWpasowaćKlocek(int kolumna, int wiersz)
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

            if (plansza[kolumna, wiersz] == null)
            {
                ZmienStanPola(kolumna, wiersz, true);
            }
            if (plansza[kolumna, w1] == null)
            {
                ZmienStanPola(kolumna, w1, true);
            }
            if (plansza[kolumna, w2] == null)
            {
                ZmienStanPola(kolumna, w2, true);
            }
            if (plansza[k1, wiersz] == null)
            {
                ZmienStanPola(k1, wiersz, true);
            }
            if (plansza[k2, wiersz] == null)
            {
                ZmienStanPola(k2, wiersz, true);
            }

            if (plansza[kolumna, wiersz].puste)
            {
                if (plansza[kolumna, w1].puste == true)
                {
                    return new Ruch(new int[] { kolumna, kolumna }, new int[] { wiersz, w1 });
                }
                else if (plansza[kolumna, w2].puste == true)
                {
                    return new Ruch(new int[] { kolumna, kolumna }, new int[] { wiersz, w2 });
                }
                else if (plansza[k1, wiersz].puste == true)
                {
                    return new Ruch(new int[] { kolumna, k1 }, new int[] { wiersz, wiersz });
                }
                else if (plansza[k2, wiersz].puste == true)
                {
                    return new Ruch(new int[] { kolumna, k2 }, new int[] { wiersz, wiersz });
                }
            }
            return null;
        }
        public Ruch CzyJestWolneMiejsceNaPlanszy(int rozm)
        {
            for (int i = 0; i < rozm; i++)
            {
                for (int j = 0; j < rozm; j++)
                {
                    var ruch = CzyMożnaTuWpasowaćKlocek(i, j);
                    if (ruch != null)
                        return ruch;
                }
            }
            return null;
        }

        public List<Ruch> ZnajdzWszystkieMozliwosciRuchu()
        {
            var mozliwosciRuchu = new List<Ruch>();

            for (int i = 0; i < Rozm; i++)
            {
                for (int j = 0; j < Rozm; j++)
                {
                    var ruch = CzyMożnaTuWpasowaćKlocek(i, j);
                    if (ruch != null)
                        mozliwosciRuchu.Add(ruch);
                }
            }

            var unikalneMozliwosciRuchu = new List<Ruch>();

            foreach (var mozliwosc in mozliwosciRuchu)
            {
                var unikat = true;
                foreach (var unikalna in unikalneMozliwosciRuchu)
                {
                    if (mozliwosc.SaTakieSame(unikalna) == false)
                    { }
                    else
                    {
                        unikat = false;
                        break;
                    }
                }
                if (unikat == true)
                    unikalneMozliwosciRuchu.Add(mozliwosc);
            }

            return unikalneMozliwosciRuchu;
        }

        public int PoliczNulle()
        {
            int LiczbaNulli = 0;

            for (int i = 0; i < Rozm; i++)
            {
                for (int j = 0; j < Rozm; j++)
                {
                    var ruch = CzyMożnaTuWpasowaćKlocek(i, j);
                    if (ruch == null)
                        LiczbaNulli++;
                }
            }

            return LiczbaNulli;
        }

        public Drzewo ZbudujDrzewo()
        {
            var drzewo = new Drzewo(new Węzeł(null, null, SkopiujPole(this), Węzeł.Strona.przeciwnik));

            drzewo.korzeń.DodajDzieci();

            return drzewo;
        }

        public Plansza SkopiujPole(Plansza plansza)
        {
            var pl = new Plansza();
            pl.Rozm = plansza.Rozm;
            pl.plansza = new Pole[pl.Rozm, pl.Rozm];
            for (int i = 0; i < plansza.Rozm; i++)
            {
                for (int j = 0; j < plansza.Rozm; j++)
                {
                    pl.plansza[i, j] = new Pole();
                    pl.plansza[i, j].puste = plansza.plansza[i, j].puste;
                }

            }
            return pl;
        }
    }
}
