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
                    var resultC = Compress(line); //Kompresja
                    Console.WriteLine("Wynik kompresji: " + resultC); //Wypisiwanie ciągu indeksów do konsoli
                    var resultD = Decompress(resultC); //Dekompresja
                    Console.WriteLine("Wynik dekompresji: " + resultD); // Wypisanie wyniki dekompresji
                    int intsInCompress = CountWordsInCompressed(resultC); //Obliczanie długosci komunikatu po kompresji
                    int charInInput = line.Length; //Obliczanie długosci komunkatu przed kompresją
                    Console.WriteLine("Skompresowane: " + intsInCompress); //Wypisanie długości komunikatu po kompresji
                    Console.WriteLine("Nieskompresowane: " + charInInput); //Wypisanie długości komunikatu przed kompresją
                    double compressionPerce = 100 - (Convert.ToDouble(intsInCompress) / Convert.ToDouble(charInInput)) * 100; //Obliczanie stopnie konwersji.
                    //Założenie: indeksy komunikatu skompr. i znaki komunikatu n.skompr. sa zapisane na takiej samej ilości pamięci
                    Console.WriteLine("Kompresja = " + compressionPerce + "%"); //Wypisywanie procentu kompresji
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

        public static string Decompress(string input)
        {
            var dictionary = new Dictionary<int, string>(); //Inicjowanie słownika, który będzie zawierał "Słowa" i ich indeksy

            var result = new StringBuilder(); //Tworzenie StringBuildera, do którego będą dopisywane znaki wyjściowe

            for (int i = 0; i < 256; i++) //Inicjowanie słownika w pęt;o
            {
                dictionary.Add(i ,((char)i).ToString()); //Dodawanie do słownika kolejnego znaku
            }

            int n = 256; //Ustawianie znacznika dla naxtępnej pozycji w słowniku

            var inputList = new List<int>(); //Tworzenie listy danych wejściowych

            var x = new StringBuilder();

            foreach (var item in input)
            {
                if(item != ' ')
                {
                    x.Append(item);
                }
                else
                {
                    inputList.Add(int.Parse(x.ToString()));
                    x.Clear();
                }
            }

            int word = inputList[0];
            string tmp; // Tworzenie zmiennej wyjściowej dla ideksu ciągu w slowniku
            dictionary.TryGetValue(word, out tmp); //Wyciagnie indeksu dla danego słowa w słowniku
            result.Append(tmp); //Dodawanie pierwszego słowa do wyniku (ono musi pochodzi z słownika)

            for (int i = 1;  i < inputList.Count; i++) //Iterowanie po kolejnych "kodach"
            {
                string value; //Tworzenie zmiennej, do niej będzie wciągna wartość z słownika
                dictionary.TryGetValue(word, out value); //Pobieranie wartości z słownika
                if (dictionary.ContainsKey(inputList[i])) //Jeśli klucz istnieje
                {
                    dictionary.Add(n++, value + dictionary[inputList[i]][0]); //Dodawanie do słownika nowego znaku
                    result.Append(dictionary[inputList[i]]); //Dopisywanie odkodowanego słowa do rezultatu
                }
                else // W przeciwnym wypadku
                {
                    dictionary.Add(n++, value + value[0]); //Dodawanie do słownika nowego słowa
                    result.Append(value + value[0]); //Dopisywanie do wyniku
                }
                word = inputList[i]; //Pobieranie "ziarna" dla kolejnego słowa
            }
            return result.ToString(); //Zwracanie rezultatu
        }

        static int CountWordsInCompressed(string input)
        {
            int result = 0;

            foreach (var item in input)
            {
                if (item == ' ')
                {
                    result++;
                }
            }

            return result;
        }
    }
}
