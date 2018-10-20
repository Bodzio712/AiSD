using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListyDom
{
    class Program
    {
        static void Main(string[] args)
        {
            var lis = new Lista<int>();
            lis.Add(5);
            lis.Add(4);
            lis.Add(100);

            var e0 = lis.WyszukajWartoscIt(5);
            var e1 = lis.WyszukajRefIt(e0);
        }
    }

    class Lista<T>
    {
        private Element<T> _glowka { get; set; }

        public Lista()
        {
            _glowka = null;
        }

        public void Add(T wartosc)
        {
            if (_glowka == null)
            {
                var e = new Element<T>(wartosc);
                _glowka = e;
                e.Poprzedni = e;
                e.Nastepny = null;
                e.Ostatni = e;
            }
            else
            {
                var e = new Element<T>(wartosc);
                e.Poprzedni = _glowka.Ostatni;
                e.Nastepny = null;
                e.Ostatni = e;
                _glowka.Nastepny = e;
                _glowka.Ostatni = e;
            }
        }

        public void Delete (Element<T> e)
        {

        }

        public Element<T> WyszukajWartoscIt(T wart)
        {
            Element<T> x = null;
            Element<T> ob = this._glowka;

            while (true)
            {
                if (ob.Wartosc.Equals(wart))
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

        public Element<T> WyszukajRefIt(Element<T> referencja)
        {
            Element<T> x = null;
            Element<T> ob = this._glowka;


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

    class Element<T>
    {
        public T Wartosc { get; set; }
        public Element<T> Nastepny { get; set; }
        public Element<T> Poprzedni { get; set; }
        public Element<T> Ostatni { get; set; }

        public Element(T wartosc)
        {
            Wartosc = wartosc;
        }

        public Element(T wartosc, Element<T> nastepny, Element<T> poprzedni, Element<T> ostatni)
        {
            Wartosc = wartosc;
            Nastepny = nastepny;
            Poprzedni = poprzedni;
            Ostatni = ostatni;
        }
    }
}
