﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Pole
    {

        public enum Kto { Ja, Przeciwnik, Wolne, Nikt};
        public bool puste;
        public Kto właściciel;

        public Pole()
        {
            this.puste = true;
            this.właściciel = Kto.Nikt;
        }
    }
}
