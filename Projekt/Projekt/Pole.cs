using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Pole
    {
        public enum Kto { Ja, Przeciwnik, Wolne};

        public bool puste;
        public Kto właściciel;
    }
}
