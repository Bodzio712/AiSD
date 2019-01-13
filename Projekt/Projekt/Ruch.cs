using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Ruch
    {
        public int[] Kolumna;
        public int[] Wiersz;

        public Ruch(int[] kolumna, int[] wiersz)
        {
            Kolumna = kolumna;
            Wiersz = wiersz;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("{"+ Kolumna[0] + ";" + Wiersz[0] + "},{" + Kolumna[1] + ";" + Wiersz[1] + "}");
            return sb.ToString();
        }
    }
}
