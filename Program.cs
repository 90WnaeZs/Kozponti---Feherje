using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feherjek
{
    public class Élelmiszerek
    {
        public string Nev;
        public string Kategoria;
        public int Energia_kj;
        public int Energia_kcal;
        public double Feherje_g;
        public double Zsir_g;
        public double Szenhidrat_g;

        //Nev;Kategoria;Energia_kj;Energia_kcal;Feherje_g;Zsir_g;Szenhidrat_g
    }

    class Program
    {
        string filepath = @"C:\Users\Zs\Desktop\2020.03.03\Első vizsgázók\források\2. feladat\feherjek.txt";
        string elsősor = "";

        List<Élelmiszerek> élelmiszer_lista = new List<Élelmiszerek>();
        List<string> kategóriák = new List<string>();
        
        static void Main(string[] args)
        {
            Program p = new Program();
            p.harmadik_feladat();
            p.negyedik_feladat();
            p.ötödik_feladat();
            p.hatodik_feladat();
            p.hetedik_feladat();
            p.nyolcadik_feladat();
            Console.ReadLine();
        }

        public void harmadik_feladat()
        {
            // Meg kell számolni hány élelmiszer van
            
            int számláló = 0;
            
            using (var filestream = File.OpenRead(filepath))
            {
                using (StreamReader SR = new StreamReader(filestream, Encoding.UTF8))
                {
                    elsősor = SR.ReadLine();
                    while (SR.ReadLine() != null)
                    {
                        számláló++;
                    }
                    SR.Close();
                }
                Console.WriteLine("3. feladat: Élelmiszerek száma: " + számláló + " db");
                
                filestream.Close();
            }
        }

        public void negyedik_feladat()
        {
            // Legnagyobb fehérje tartalmú élelmiszer neve és kategóriája

            double legnagyobb = 0;
            

            using (var fileStream2=File.OpenRead(filepath))
            {
                using (StreamReader SR2=new StreamReader(fileStream2,Encoding.UTF8))
                {
                    elsősor = SR2.ReadLine();
                    string s;
                    while ((s=SR2.ReadLine())!=null)
                    {
                        string[] tomb = s.Split(';');

                        Élelmiszerek é = new Élelmiszerek();
                        é.Nev = tomb[0];
                        é.Kategoria = tomb[1];
                        é.Energia_kj = Convert.ToInt32(tomb[2]);
                        é.Energia_kcal = Convert.ToInt32(tomb[3]);
                        é.Feherje_g = Convert.ToDouble(tomb[4]);
                        é.Zsir_g = Convert.ToDouble(tomb[5]);
                        é.Szenhidrat_g = Convert.ToDouble(tomb[6]);
                        if (é.Feherje_g > legnagyobb)
                        {
                            legnagyobb = é.Feherje_g;
                        }
                        élelmiszer_lista.Add(é);
                    }
                }
            }

            foreach (var item in élelmiszer_lista)
            {
                if (item.Feherje_g==legnagyobb)
                {
                    Console.WriteLine("4. feladat: A legnagyobb fehérjetartalom:");
                    Console.WriteLine("\tÉtel neve: "+item.Nev);
                    Console.WriteLine("\tÉtel kategóriája: " + item.Kategoria);
                }
            }
            
        }

        public void ötödik_feladat()
        {
            // Gabonafélék átlagos fehérje tartalma

            double összeg = 0;
            double count = 0;
            double átlag = 0;
            
            foreach (var item in élelmiszer_lista)
            {
                if(item.Kategoria=="Gabonafélék")
                {
                    count++;
                    összeg += item.Feherje_g;
                }
            }
            átlag = összeg / count;
            Console.WriteLine("5. feladat: Gabonafélék átlagos fehérjetartalma: "+ átlag+" gramm");
        }

        public void hatodik_feladat()
        {
            // Felhasználó által bevitt karakterláncot tartalmazza-e az élelmiszer neve

            Console.WriteLine("6. feladat: Kérek egy karakterláncot: ");
            string bevitt = Console.ReadLine();
            foreach (var item in élelmiszer_lista)
            {
                bool contains = item.Nev.IndexOf(bevitt, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    Console.WriteLine("\tNév: "+item.Nev+" Kategória: "+item.Kategoria+" Fehérje: "+item.Feherje_g);
                }
            }
        }


        public void hetedik_feladat()
        {
            // Kategóriák csoportosítása és kiíratása, ha 10-nél több van belőle

            int szamlalo = 0;
            foreach (var item in élelmiszer_lista)
            {
                if(!kategóriák.Contains(item.Kategoria))
                {
                    kategóriák.Add(item.Kategoria);
                }
            }
            foreach (var kat in kategóriák)
            {
                szamlalo = 0;
                foreach (var élelmiszer in élelmiszer_lista)
                {
                    if(élelmiszer.Kategoria==kat)
                    {
                        szamlalo++;
                    }
                }
                if (szamlalo<10)
                {
                    Console.WriteLine(kat + " " + szamlalo);
                }
            }
        }

        public void nyolcadik_feladat()
        {
            // Gabonafélék nevének és fehérje tartalmának fájlba íratása

            Console.WriteLine("8. feladat: gabonafelek.txt");
            string hova = @"C:\Users\Zs\Desktop\gabonafelek.txt";

            var filestream = File.Open(hova, FileMode.Open);
            using (StreamWriter SW = new StreamWriter(filestream, Encoding.UTF8))
            {
                foreach (var item in élelmiszer_lista)
                {
                    if (item.Kategoria == "Gabonafélék")
                    {
                        SW.WriteLine(item.Nev + ";" + item.Feherje_g);
                    }
                }
            }
            filestream.Close();
        }
    }
}
