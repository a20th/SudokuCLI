using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudokuCLI
{
    class Feladvany
    {
        public string Kezdo { get; private set; }
        public int Meret { get; private set; }

        public Feladvany(string sor)
        {
            Kezdo = sor;
            Meret = Convert.ToInt32(Math.Sqrt(sor.Length));
        }

        public void Kirajzol()
        {
            for (int i = 0; i < Kezdo.Length; i++)
            {
                if (Kezdo[i] == '0')
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(Kezdo[i]);
                }
                if (i % Meret == Meret - 1)
                {
                    Console.WriteLine();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StreamReader r = new StreamReader("feladvanyok.txt");
            Random rnd = new Random();

            List<Feladvany> filtered = new List<Feladvany>();
            List<Feladvany> l = new List<Feladvany>();

            int number;
            double percentage;
            int randomNumber;
            double emptyCount = 0;
            string fileName;

            while (!r.EndOfStream)
            {
                Feladvany f = new Feladvany(r.ReadLine());
                l.Add(f);
            }
            r.Close();

            Console.WriteLine("3. feladat: Beolvasva {0} feladvány", l.Count());

            Console.Write("4. feladat: Kérem a feladvány méretét[4..9]: ");
            do
            {
                number = int.Parse(Console.ReadLine());
                if(number > 10 && number < 3)
                {
                    Console.WriteLine("Nem felel meg a megadott szám!");
                }
            } while (number > 10 && number <3);

            foreach (Feladvany f in l)
            {
                if(f.Meret == number)
                {
                    filtered.Add(f);
                }
            }
            Console.WriteLine("A {0}x{0} méretű feladványból {1} darab vam tárolva",number,filtered.Count());

            randomNumber = rnd.Next(filtered.Count());
            Console.WriteLine("5. feladat: A kiválasztott feladvány:");
            Console.WriteLine(filtered[randomNumber].Kezdo);
            
            foreach(char c in filtered[randomNumber].Kezdo)
            {
                if(c == '0')
                {
                    emptyCount++;
                }
            }
            percentage = (emptyCount / filtered[randomNumber].Kezdo.Length) * 100;
            Console.WriteLine("6. feladat: A feladvány kitöltöttsége: {0}%", Math.Round(percentage));

            Console.WriteLine("7. feladat: A feladvány kirajzolva:");
            filtered[randomNumber].Kirajzol();

            fileName = "sudoku" + number + ".txt";
            StreamWriter w = new StreamWriter(fileName);
            foreach(Feladvany f in filtered)
            {
                w.WriteLine(f.Kezdo);
            }
            w.Close();
            Console.WriteLine("8. feladat: {0} állomány {1} darab feladvánnyal létrehozva", fileName, filtered.Count());

            Console.ReadLine();
            

        }
    }
}
