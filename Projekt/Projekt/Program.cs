using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Program
    {
        static readonly int CZAS_TWORZENIA_DRZEWA_MS = 300;

        static void Main(string[] args)
        {
            //Odbieranie komunikatu o rozmiarze planszy
            try
            {
                var rozm = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                ZakończProgramPoBłędzie("Błąd w trakcie wczytywania rozmiaru planszy", e.ToString());
            }
            Console.WriteLine("ok");

            //Odbieranie komunikatu o wstępnie zajetych polach
            try
            {
                var wstępnieZaj = Console.ReadLine();
            }
            catch (Exception e)
            {
                ZakończProgramPoBłędzie("Błąd wstępnego zapłęniania planszy", e.ToString());
            }

            Console.WriteLine("ok");

            //Pętla główna w której toczy sie gra
            while(true)
            {
                // Odczytywanie ruchu przychodzącego
                var ruchPrzychodzący = Console.ReadLine();

                //Sprawdzanie czy jest to pierwszy ruch, czy nie
                if(ruchPrzychodzący == "start")
                {
                    var start = DateTime.UtcNow;
                    while (DateTime.UtcNow - start < new TimeSpan(0, 0, 0, 0, CZAS_TWORZENIA_DRZEWA_MS))
                    {
                        //TODO: Napisać logikę
                    }
                }
                else
                {
                    var start = DateTime.UtcNow;
                    while (DateTime.UtcNow - start < new TimeSpan(0, 0, 0, 0, CZAS_TWORZENIA_DRZEWA_MS))
                    {

                    }
                }

                //Wypisywanie ruchu wychodzącego
                Console.WriteLine();
            }
        }

        static void ZakończProgramPoBłędzie(string komunikat ,string e)
        {
            Console.WriteLine(komunikat + "\n" + e);
            Console.WriteLine("Wcisnij przycisk aby zakończyć");
            Console.ReadKey();
            Environment.Exit(-1);
        }
    }
}
