using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            //Odbieranie komunikatu o rozmiarze planszy
            var rozm = int.Parse(Console.ReadLine());
            Console.WriteLine("ok");

            //Odbieranie komunikatu o wstępnie zajetych polach
            var wstępnieZaj = Console.ReadLine();
            Console.WriteLine("ok");

            //Pętla główna w której toczy sie gra
            while(true)
            {
                // Odczytywanie ruchu przxychodzącego
                var ruchPrzychodzący = Console.ReadLine();

                if(ruchPrzychodzący == "start")
                {

                }
                else
                {

                }

                //Wypisywanie ruchu wychodzącego
                Console.WriteLine();
            }
        }
    }
}
