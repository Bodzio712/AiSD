using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var e5 = new Element(112, null);
            var e4 = new Element(997, e5);
            var e3 = new Element(999, e4);
            var e2 = new Element(998, e3);
            var e1 = new Element(112, e2);
            var e0 = new Element(991, e1);
            var Li = new Lista(e0);

            var WyszukanyPoWartosci = Li.WyszukajWartosc(112);
            Console.WriteLine("Wyszukany element po wartości to: " + WyszukanyPoWartosci.ToString());

            var WyszukanyPoReferencji = Li.WyszukajRef(e4);
            Console.WriteLine("Wyszukany element po referencji to: " + WyszukanyPoReferencji.ToString());

        }
    }

    class Lista
    {
        Element PierwszyElement;

        public Lista()
        {
        }

        public Lista(Element pierwszyElement)
        {
            PierwszyElement = pierwszyElement;
        }

        public Element WyszukajWartosc(int wart)
        {
            Element x = null;
            Element ob = this.PierwszyElement;

            while (true)
            {
                if (ob.Wartosc == wart)
                {
                    x = ob;
                    break;
                }

                if (ob.Nastepny == null)
                    break;
                else
                    ob = ob.Nastepny;
            }

            return x;
        }

        public Element WyszukajRef(Element referencja)
        {
            Element x = null;
            Element ob = this.PierwszyElement;


            while (true)
            {
                if (ob == referencja)
                {
                    x = ob;
                    break;
                }

                if (ob.Nastepny == null)
                    break;
                else
                    ob = ob.Nastepny;
            }

            return x;
        }
    }

    class Element
    {

        public int Wartosc;
        public Element Nastepny;

        public Element(int wartosc, Element nastepny)
        {
            Wartosc = wartosc;
            Nastepny = nastepny;
        }

        public override string ToString()
        {
            return this.Wartosc.ToString();
        }
    }
}
