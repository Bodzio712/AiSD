using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Drzewo
    {
        Węzeł korzeń;

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
        enum Strona { ja, przeciwnik}

        //Dane w węzłach
        Ruch ruch;
        int któryKrok;
        Strona strona;
        List<Węzeł> dzieci;
    }
}
