using System;

namespace ListyDom
{
    class Program
    {
        static void Main(string[] args)
        {
            var lis = new Lista<int>();
            for (int i = 0; i < 10000; i++)
                lis.Dodaj(i);

            DateTime t0;
            DateTime t1;

            Console.WriteLine("--- Wyszukiwanie po wartości ---");

            t0 = DateTime.Now;
            for (int i = 0; i < 1000; i++)
                lis.WyszukajWartoscIt(9999);
            t1 = DateTime.Now;
            Console.WriteLine("Czas wyszukiwania iteracyjnego po wartości: " + (t1 - t0).TotalMilliseconds);

            t0 = DateTime.Now;
            for (int i = 0; i < 1000; i++)
                lis.WyszukajWartoscRek(9999);
            t1 = DateTime.Now;
            Console.WriteLine("Czas wyszukiwania rekurencyjnego po wartości: " + (t1 - t0).TotalMilliseconds);


            Console.WriteLine("--- Wyszukiwanie po referencji ---");

            t0 = DateTime.Now;
            for (int i = 0; i < 1000; i++)
                lis.WyszukajRefIt(lis.GetOgon());
            t1 = DateTime.Now;
            Console.WriteLine("Czas wyszukiwania iteracyjnego po referencji: " + (t1 - t0).TotalMilliseconds);

            t0 = DateTime.Now;
            for (int i = 0; i < 1000; i++)
                lis.WyszukajRefRek(lis.GetOgon());
            t1 = DateTime.Now;
            Console.WriteLine("Czas wyszukiwania rekurencyjnego po referencji: " + (t1 - t0).TotalMilliseconds);

        }
    }

    class Lista<T>
    {
        private Element<T> _glowka { get; set; }
        private Element<T> _ogon { get; set; }


        public Lista()
        {
            _glowka = null;
            _ogon = null;
        }

        public void Dodaj(T wartosc)
        {
            if (_glowka == null)
            {
                var e = new Element<T>(wartosc);
                _glowka = e;
                e.Poprzedni = e;
                e.Nastepny = null;
                _ogon = e;
            }
            else
            {
                var e = new Element<T>(wartosc);
                e.Poprzedni = _ogon;
                e.Nastepny = null;
                _ogon = e;
                e.Poprzedni.Nastepny = e;
            }
        }

        public void Usun(Element<T> e)
        {
            e.Poprzedni.Nastepny = e.Nastepny;
            if (e.Nastepny == null)
                _glowka = e.Poprzedni;
            else
            {
                _ogon = e.Nastepny;
                e.Nastepny.Poprzedni = e.Poprzedni;
            }
        }

        public void WstawPoElemencie(T wartosc, Element<T> element)
        {
            var elementPoKtorym = WyszukajRefIt(element);
            var wstawiany = new Element<T>(wartosc);
            if (elementPoKtorym.Nastepny == null)
            {
                elementPoKtorym.Nastepny = wstawiany;
                wstawiany.Nastepny = null;
                _ogon = wstawiany;
            }
            else
            {
                wstawiany.Nastepny = elementPoKtorym.Nastepny;
                elementPoKtorym.Nastepny = wstawiany;
            }
        }

        public Element<T> WyszukajWartoscIt(T wart)
        {
            Element<T> x = null;
            Element<T> ob = _glowka;

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


        public Element<T> WyszukajRefRek(Element<T> referencja)
        {
            Element<T> element = _glowka;
            if (element.Equals(referencja))
                return referencja;
            else if (element.Nastepny == null)
                return null;
            else
                return WyszukajRefRek(referencja, element.Nastepny);
        }

        private Element<T> WyszukajRefRek(Element<T> referencja, Element<T> element)
        {
            if (element.Equals(referencja))
                return referencja;
            else if (element.Nastepny == null)
                return null;
            else
                return WyszukajRefRek(referencja, element.Nastepny);
        }

        public Element<T> WyszukajWartoscRek(T wart)
        {
            Element<T> element = _glowka;
            if (element.Wartosc.Equals(wart))
                return element;
            else if (element.Nastepny == null)
                return null;
            else
                return WyszukajWartoscRek(wart, element.Nastepny);
        }

        public Element<T> WyszukajWartoscRek(T wart, Element<T> element)
        {
            if (element.Wartosc.Equals(wart))
                return element;
            else if (element.Nastepny == null)
                return null;
            else
                return WyszukajWartoscRek(wart, element.Nastepny);
        }

        public Element<T> GetHead()
        {
            return _glowka;
        }

        public Element<T> GetOgon()
        {
            return _ogon;
        }

    }

    class Element<T>
    {
        public T Wartosc { get; set; }
        public Element<T> Nastepny { get; set; }
        public Element<T> Poprzedni { get; set; }

        public Element(T wartosc)
        {
            Wartosc = wartosc;
        }

        public Element(T wartosc, Element<T> nastepny, Element<T> poprzedni)
        {
            Wartosc = wartosc;
            Nastepny = nastepny;
            Poprzedni = poprzedni;
        }
    }
}
