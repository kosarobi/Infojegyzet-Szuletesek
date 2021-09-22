using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Szuletesek
{
    class Program
    {
        static List<Szuletesek> szuletesek = new List<Szuletesek>();

        static void Main(string[] args)
        {
            f01Beolvas();
            f04();
            f05();
            f06();
            f07();
            f08();
            f09();
            Console.ReadKey();
        }

        private static void f09()
        {
            var evek = szuletesek.GroupBy(x => x.Szuldatum.Year);
            foreach (var item in evek)
            {
                Console.WriteLine($"\t{item.Key} év - {szuletesek.Count(x=>x.Szuldatum.Year.Equals(item.Key))}");
            }
        }

        private static void f08()
        {
            string vane = szuletesek.Any(x=>x.Szuldatum.Month.Equals(02) && x.Szuldatum.Day.Equals(24)) ? "született" : "nem született";
            Console.WriteLine($"8. feladat: Szökőnapon {vane} baba!");
        }

        private static void f07()
        {
            var sorrend = szuletesek.OrderBy(x => x.Szuldatum.Year);
            Console.WriteLine($"7. feladat: A vizsgált időszak: {sorrend.First().Szuldatum.Year} - {sorrend.Last().Szuldatum.Year}");
        }

        private static void f06()
        {
            Console.WriteLine($"6. feladat: Fiúk száma: {szuletesek.Count(x => x.Nem.Equals("1") || x.Nem.Equals("3"))}");
        }

        private static void f05()
        {
            Console.WriteLine($"5. feladat: Vas megyében a vizsgált évek alatt {szuletesek.Count()} csecsemő született");
        }

        private static void f04()
        {
            Console.WriteLine("4. feladat: Ellenőrzés");
            foreach (var item in szuletesek)
            {
                if (!CdvE11(item.Teljesazonosito))
                {
                    Console.WriteLine($"\tHibás a {item.Nem}-{item.Szuletes}-{item.Azonosito}-{item.Egyedikod} személyi azonosító");
                }
            }
        }

        static bool CdvE11(string azonosito)
        {
            int utolso = Convert.ToInt32(azonosito.Substring(azonosito.Length - 1, 1));
            int osszeg = 0;
            int leptet = 10;
            for (int i = 1; i <= azonosito.Length - 1; i++)
            {
                osszeg = osszeg + Convert.ToInt32(azonosito[i - 1]) * leptet;
                leptet--;
            }

            int egyedikod = osszeg % 11;

            if (egyedikod != utolso)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void f01Beolvas()
        {
            StreamReader sr = new StreamReader("vas.txt", Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                szuletesek.Add(new Szuletesek(sr.ReadLine()));
            }
            sr.Close();
        }
    }

    class Szuletesek
    {
        private string nem;
        private string szuletes;
        private DateTime szuldatum;
        private string azonosito;
        private string egyedikod;
        private string teljesazonosito;

        public Szuletesek(string Adatsor)
        {
            string[] sor = Adatsor.Split('-');
            string ev="";
            nem = sor[0];
            szuletes = sor[1];
            if (szuletes.StartsWith("0")){
                ev = "20" + szuletes.Substring(0, 2) + "-" + szuletes.Substring(2,2) + "-" + szuletes.Substring(4,2);
            }
            if (szuletes.StartsWith("9"))
            {
                ev = "19" + szuletes.Substring(0, 2) + "-" + szuletes.Substring(2, 2) + "-" + szuletes.Substring(4, 2); ;
            }
            Szuldatum = Convert.ToDateTime(ev);
            azonosito = sor[2].Remove(sor[2].Length - 1);
            egyedikod = sor[2].Substring(sor[2].Length - 1, 1);
            teljesazonosito = nem + szuletes + azonosito + egyedikod;
        }

        public string Nem { get => nem; set => nem = value; }
        public string Szuletes { get => szuletes; set => szuletes = value; }
        public string Azonosito { get => azonosito; set => azonosito = value; }
        public string Egyedikod { get => egyedikod; set => egyedikod = value; }
        public string Teljesazonosito { get => teljesazonosito; set => teljesazonosito = value; }
        public DateTime Szuldatum { get => szuldatum; set => szuldatum = value; }
    }
}
