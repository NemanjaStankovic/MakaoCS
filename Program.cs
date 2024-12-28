using System;
using System.Linq.Expressions;

namespace Makao
{
    public class Program
    {
        static void Shuffle<T>(T[] array)
        {
            Random rng = new Random();
            int n = array.Length;

            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
        static void RemoveLastElement<T>(ref T[] array, out T k)
        {
            if (array.Length == 0)
                throw new InvalidOperationException("Array is empty.");

            T[] newArray = new T[array.Length - 1];

            k = array[array.Length - 1];

            Array.Copy(array, newArray, newArray.Length);

            array = newArray;
        }

        static void AddElementToArray<T>(ref T[] array, T element)
        {
            T[] newArray;
            if (array == null)
            {
                newArray = new T[1];
                newArray[0] = element;
                array = newArray;
            }
            else
            {
                newArray = new T[array.Length + 1];
                Array.Copy(array, newArray, array.Length);
                newArray[array.Length] = element;
                array = newArray;
            }
        }
        public static Karta[] Mesaj()
        {
            Karta[] karte = new Karta[52];
            char[] znak = new char[4];
            znak[0] = 's';
            znak[1] = 'k';
            znak[2] = 'l';
            znak[3] = 'd';
            int brojac = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 15; j++)
                {
                    if (j == 11)
                        continue;
                    karte[brojac] = new Karta(j, znak[i]);
                    brojac++;
                }
            }
            Shuffle(karte); //vrhSpila = 51; ?????
            return karte;
        }
        public static void Deli5(ref Karta[] karte, ref Karta[] primac)
        {
            for (int i = 0; i < 5; i++)
            {
                Karta izvucenaKarta = new Karta();
                RemoveLastElement(ref karte, out izvucenaKarta);
                AddElementToArray(ref primac, izvucenaKarta);
            }
        }

        public static bool BaciKartuNaSto(ref Karta igrac, ref Karta[] sto)
        {
            AddElementToArray(ref sto, igrac);
            return true;
        }
        public static void IzbaciKartuIzRuku(ref Karta[] ruka, ref Karta karta)
        {
            if (ruka.Length == 0)
                throw new InvalidOperationException("Array is empty.");

            Karta[] newArray = new Karta[ruka.Length - 1];

            int brojac = 0;

            for (int i = 0; i < ruka.Length; i++)
            {
                if (ruka[i].znak == karta.znak && ruka[i].broj == karta.broj)
                {

                }
                else
                {
                    newArray[brojac] = ruka[i];
                    brojac++;
                }
            }
            ruka = newArray;
        }
        public static void ProveraZadnjeKarteUSpilu(ref Karta[] spil, ref Karta[] sto)
        {
            if (spil.Length == 0)
            {
                Karta k = null;
                RemoveLastElement(ref sto, out k);
                spil = sto;
                Shuffle(spil);
                AddElementToArray(ref sto, k);
            }
        }
        public static void UzmiDveKarte(ref Karta[] spil, ref Karta[] igrac)
        {
            Karta k = new Karta();
            for (int i = 0; i < 2; i++)
            {
                RemoveLastElement(ref spil, out k);
                AddElementToArray(ref igrac, k);
            }
        }

        public static void UzmiCetiriKarata(ref Karta[] spil, ref Karta[] igrac)
        {
            Karta k = new Karta();
            for (int i = 0; i < 4; i++)
            {
                RemoveLastElement(ref spil, out k);
                AddElementToArray(ref igrac, k);
            }
        }

        static void Main(string[] args)
        {
            int koIgra = 0;
            int kupi2Stack = 0;
            Karta[] rukaIgrac=null;
            Karta[] rukaKomp=null;
            Karta[] sto = null;
            //int vrhSpila = 51;

            Karta[] karte = Mesaj();

            Deli5(ref karte,ref rukaIgrac);
            Deli5(ref karte, ref rukaKomp);
            Karta k = null;
            RemoveLastElement(ref karte, out k);
            BaciKartuNaSto(ref k, ref sto);
            for (int i = 0; i < rukaIgrac.Length; i++)
            {
                Console.WriteLine("Igrac: " + rukaIgrac[i].broj + " " + rukaIgrac[i].znak);
            }
            for (int i = 0; i < rukaKomp.Length; i++)
            {
                Console.WriteLine("Komp: " + rukaKomp[i].broj + " " + rukaKomp[i].znak);
            }
            Console.WriteLine("Sto: " + sto[sto.Length - 1].broj + " " + sto[sto.Length - 1].znak);
            while (true)
            {
                if (koIgra == 0)
                {
                    //igraj igrac 1
                    Console.WriteLine("Odaberite kartu za igranje igrac[broj znak]");
                    string pom = Console.ReadLine();
                    string[] rezultatParsiranja = pom.Split(' ');
                    Int32.TryParse(rezultatParsiranja[0], out int brojZaPretragu);
                    char znakZaPretragu = char.Parse(rezultatParsiranja[1]);
                    Karta k1 = rukaIgrac.FirstOrDefault(p => p.broj == brojZaPretragu && p.znak == znakZaPretragu);
                    //Console.WriteLine("Ovu kartu oces "+k1.broj+k1.znak);
                    if (sto[sto.Length - 1].znak == k1.znak || sto[sto.Length - 1].broj == k1.broj || k1.broj == 12)
                    {
                        BaciKartuNaSto(ref k1, ref sto);
                        IzbaciKartuIzRuku(ref rukaIgrac, ref k1);
                        koIgra = 1;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (kupi2Stack != 0)  // N A S T A V I !
                    {

                    }
                                    //igraj komp
                    Console.WriteLine("Odaberite kartu za igranje igrac[broj znak]");
                    string pom = Console.ReadLine();
                    string[] rezultatParsiranja = pom.Split(' ');
                    Int32.TryParse(rezultatParsiranja[0], out int brojZaPretragu);
                    char znakZaPretragu = char.Parse(rezultatParsiranja[1]);
                    Karta k1 = rukaKomp.FirstOrDefault(p => p.broj == brojZaPretragu && p.znak == znakZaPretragu);
                    //Console.WriteLine("Ovu kartu oces "+k1.broj+k1.znak);
                    if (sto[sto.Length - 1].znak == k1.znak || sto[sto.Length - 1].broj == k1.broj || k1.broj == 12)
                    {
                        BaciKartuNaSto(ref k1, ref sto);
                        IzbaciKartuIzRuku(ref rukaKomp, ref k1);
                        koIgra = 0;
                        if (k1.broj == 8 || k1.broj==1)
                        {
                            koIgra = 1;
                        }
                        if (k1.broj == 2 && k1.znak == 'd')
                        {
                            UzmiCetiriKarata(ref karte, ref rukaKomp);
                        }
                        if (k1.broj == 7)
                        {
                            kupi2Stack++;         // N A S T A V I !
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
                Console.Clear();
                //for (int i = 0; i < karte.Length; i++)
                //{
                //    Console.WriteLine(karte[i].broj + " " + karte[i].znak);
                //}
                for (int i = 0; i < rukaIgrac.Length; i++)
                {
                    Console.WriteLine("Igrac: " + rukaIgrac[i].broj + " " + rukaIgrac[i].znak);
                }
                for (int i = 0; i < rukaKomp.Length; i++)
                {
                    Console.WriteLine("Komp: " + rukaKomp[i].broj + " " + rukaKomp[i].znak);
                }
                Console.WriteLine("Sto: " + sto[sto.Length - 1].broj + " " + sto[sto.Length - 1].znak);
                //Console.WriteLine("Preostale karte");
                //for (int i = 0; i < karte.Length; i++)
                //{
                //    Console.WriteLine(karte[i].broj + " " + karte[i].znak);
                //}
                //igraj igrac 2
            }
        }
    }
}