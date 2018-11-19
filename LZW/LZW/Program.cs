using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZW //Przestrzeń nazw dla pliku
{
    class Program
    {
        static void Main(string[] args) // Główna metoda programu
        {
            try //W bloku try są zawrte metody, które mogą spowodować wystąpienie wyjątku
            {
                using(StreamReader sr = new StreamReader("Input.txt")) //Tworzenie obiektu odpowiającego za wczytywanie z pliku
                {
                    var line = sr.ReadToEnd(); //Wczytywanie danych z pliku do stringa
                    var result = Compress(line); //Kompresja
                    Console.WriteLine(result); //Wypisiwanie ciągu indeksów do konsoli
                }
            }
            catch (Exception) //Obsługa wyjątku
            {
                Console.WriteLine("Wystąpił błąd w czasie wczytywania danych z pliku"); //Wypisywanie komunikatu o błędzie
            }
        }

        public static string Compress (string input) //Tworzenie nowej metody statycznej odpowiadajacej za kompresję
        {
            var dictionary = new Dictionary<string, int>(); //Inicjowanie słownika, który będzie zawierał "Słowa" i ich indeksy

            StringBuilder result = new StringBuilder(); //Tworzenie StringBuildera, do którego będą dopisywane znaki wyjściowe

            for (int i = 0; i < 256; i++) //Inicjowanie słownika w pęt;o
            {
                dictionary.Add(((char)i).ToString(), i); //Dodawanie do słownika kolejnego znaku
            }

            int n = 256; //Ustawianie znacznika dla naxtępnej pozycji w słowniku

            string prev = input[0].ToString(); //Tworzenie zmiennej zawierającej pierwszy znak z ciagu wejściowego
            for (int i = 1; i < input.Length-1; i++) //Główna pętla kompresji
            {
                if(dictionary.ContainsKey(prev+input[i])) //Sprawdzanie czy dany ciąg jest w słowniku
                {
                    prev += input[i]; //Jeśli tak, to rozszerza się ten ciag o koljny znak
                }
                else //jeśli nie
                { 
                    int x; // Tworzenie zmiennej wyjściowej dla ideksu ciągu w slowniku
                    dictionary.TryGetValue(prev, out x); //Wyciagnie indeksu dla danego słowa w słowniku
                    result.Append(x.ToString() + " "); //Dodawanie indeksu do ciagu wyjściowego
                    dictionary.Add( prev + input[i], n++); //Dodawanie ciągu do słownika
                    prev = input[i].ToString(); //Zapisywanie do zmiennej prev następnego znaku
                }
            }
            //Dodanie ostatniego symbolu po opuszczeniu pętli
            int y; //Tworzenie zmiennej wyjściowej dla ideksu ciągu w slowniku
            dictionary.TryGetValue(prev, out y);//Wyciagnie indeksu dla danego słowa w słownik
            result.Append(y.ToString() + " ");//Dodawanie indeksu do ciagu wyjściowego


            return result.ToString(); //Zwracanie siągu indeksów
        }
    }
}
