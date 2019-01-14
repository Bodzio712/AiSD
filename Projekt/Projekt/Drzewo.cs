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

        public Węzeł Przeszukaj ()
        {
            //TODO: Dokończyć przeszukiwanie
            return null;
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

        public void DodajDzieci()
        {
            var rozpatrywanyWezel = this;

            var mozliwosci = this.plansza.ZnajdzWszystkieMozliwosciRuchu();

            foreach (var item in mozliwosci)
            {
                var pl = new Plansza();
                pl.plansza = plansza.plansza;
                pl.Rozm = plansza.Rozm;
                dzieci.Add(new Węzeł(item, rozpatrywanyWezel, pl.KopiujIWykonajRuch(item), rozpatrywanyWezel.strona == Węzeł.Strona.ja ? Węzeł.Strona.przeciwnik : Węzeł.Strona.ja));
            }

            foreach (var item in dzieci)
            {
                item.DodajDzieci();
            }
        }

        public int[] OszacujSzanse()
        {
            int[] odpowiedz = new int[] { };
            foreach (var item in this.dzieci)
            {
                odpowiedz = item.OszacujSzanse();

                if (this.plansza.ZnajdzWszystkieMozliwosciRuchu().Count == 0 && this.strona == Strona.ja)
                {
                    IloscWygranych++;
                    IloscRozegranych++;
                }
                else if (this.plansza.ZnajdzWszystkieMozliwosciRuchu().Count == 0 && this.strona == Strona.przeciwnik)
                {
                    IloscRozegranych++;
                }
                else
                {
                    IloscWygranych += odpowiedz[0];
                    IloscRozegranych += odpowiedz[1];
                }
                SzansaNaWygraną = IloscWygranych / IloscRozegranych;

            }
            return new int[] { IloscWygranych, IloscRozegranych };
        }
    }
}
