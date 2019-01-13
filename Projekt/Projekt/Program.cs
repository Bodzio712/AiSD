﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Program
    {
        static int rozm;
        static Plansza plansza;
        static Drzewo drzewo;

        static readonly int CZAS_TWORZENIA_DRZEWA_MS = 300;

        static void Main(string[] args)
        {
            #region KOMUNIKACJA_WSTEPNA
            //Odbieranie komunikatu o rozmiarze planszy
            try
            {
                rozm = int.Parse(Console.ReadLine());
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
                plansza = Komunikator.WstepneUzupelnianie(rozm, wstępnieZaj);
            }
            catch (Exception e)
            {
                ZakończProgramPoBłędzie("Błąd wstępnego zapłęniania planszy", e.ToString());
            }
            Console.WriteLine("ok");
            #endregion

            //Pętla główna w której toczy sie gra
            while (true)
            {
                // Odczytywanie ruchu przychodzącego
                var ruchPrzychodzący = Console.ReadLine();

                //Sprawdzanie czy jest to pierwszy ruch, czy nie
                if(ruchPrzychodzący == "start")
                {
                    Ruch ruchPrzeciwnika = null;
                }
                else
                {
                    Ruch ruchPrzeciwnika = Komunikator.OdczytajRuch(ruchPrzychodzący);
                }
                
                var mojRuch = obliczRuch();

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

        public static Ruch obliczRuch()
        {
            //TODO: Dokończyć obliczanie ruchu
            return new Ruch(null, null);
        }
    }
}
