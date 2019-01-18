using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Projekt
{
    public class Drzewo
    {
        public Węzeł korzeń;

        public Drzewo(Węzeł korzeń)
        {
            this.korzeń = korzeń;
        }

        

        public Ruch ZnajdzOptymalnyRuch(Plansza plansza)
        {
            var stanObecny = this.korzeń.Przeszukaj(plansza, Węzeł.Strona.przeciwnik);

            if (stanObecny == null)
                return null;

            double szansa = -1;
            Ruch docelowy = null;

            foreach (var item in stanObecny.dzieci)
            {
                double szansaNaWygr = (double)item.IloscWygranych / (double)item.IloscRozegranych;

                if (szansa < szansaNaWygr)
                {
                    szansa = szansaNaWygr;
                    docelowy = item.ruch;
                }
            }

            return docelowy;
        }
    }

    public class Węzeł
    {
        //Enum czy ruch mój, czy przecwnika
        public enum Strona { ja, przeciwnik}

        //Dane w węzłach
        public Ruch ruch;
        public Węzeł ojciec;
        public int któryKrok;
        public Plansza plansza;
        public Strona strona;
        public List<Węzeł> dzieci;
        public double SzansaNaWygraną;
        public int IloscWygranych;
        public int IloscRozegranych;

        public Węzeł(Ruch ruch, Węzeł ojciec, Plansza plansza, Strona strona)
        {
            this.ruch = ruch;
            this.ojciec = ojciec;
            this.plansza = (Plansza)plansza.Clone();
            this.strona = strona;
            dzieci = new List<Węzeł>();
        }

        public void DodajDzieci(DateTime start)
        {
            var czas = DateTime.Now - start;
            if (czas.Milliseconds > Program.CZAS_NA_RUCH)
                return;

            var rozpatrywanyWezel = this;

            var mozliwosci = this.plansza.ZnajdzWszystkieMozliwosciRuchu();

            foreach (var item in mozliwosci)
            {
                var pl = plansza.SkopiujPole(plansza);
                dzieci.Add(new Węzeł(item, rozpatrywanyWezel, pl.KopiujIWykonajRuch(item), rozpatrywanyWezel.strona == Węzeł.Strona.ja ? Węzeł.Strona.przeciwnik : Węzeł.Strona.ja));
            }

            foreach (var item in dzieci)
            {
                czas = DateTime.Now - start;
                if (czas.Milliseconds > Program.CZAS_NA_RUCH)
                    return;

                item.DodajDzieci(start);
            }
        }

        public int[] OszacujSzanse(DateTime start)
        {
            var czas = DateTime.Now - start;
            if (czas.Milliseconds > Program.CZAS_NA_RUCH)
                return null;

            int[] odpowiedz = new int[] { };
            foreach (var item in this.dzieci)
            {
                czas = DateTime.Now - start;
                if (czas.Milliseconds > Program.CZAS_NA_RUCH)
                    return null;

                odpowiedz = item.OszacujSzanse(start);

                IloscWygranych += odpowiedz[0];
                IloscRozegranych += odpowiedz[1];

                SzansaNaWygraną = (double)IloscWygranych / (double)IloscRozegranych;
            }

            if (this.dzieci.Count == 0 && this.strona == Strona.ja)
            {
                IloscWygranych++;
                IloscRozegranych++;
            }
            else if (this.dzieci.Count == 0 && this.strona == Strona.przeciwnik)
            {
                IloscRozegranych++;
            }
            SzansaNaWygraną = (double)IloscWygranych / (double)IloscRozegranych;


            return new int[] { IloscWygranych, IloscRozegranych };
        }

        public Węzeł Przeszukaj(Plansza plansza, Węzeł.Strona strona)
        {
            if (this.plansza.CzyTakieSamePlansze(plansza) == true && this.strona == strona)
            {
                return this;
            }

            foreach (var item in this.dzieci)
            {
                item.Przeszukaj(plansza, strona);
            }

            return null;
        }
    }
}
