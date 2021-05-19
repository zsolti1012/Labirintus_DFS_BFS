using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintusTeszt
{

    public   class Rajzolo

    {

        public static int MERETX=35;
        public static int MERETY=15;

        public static string[,] Palya=new string[35,15];
        /* szandekosan karakterkodok, igy konnyu printfelni */
        

        //public Dictionary<string, char> dict = new Dictionary<string, char>() {
        //    { "Jarat",' '},{ "Fal",'X'} };

        enum Irany
        {
            FEL = 0, LE, BALRA, JOBBRA
        }





       

        /* az egesz palyat fallal tolti ki */



        public static void ures(string[,] Palya,int Meretx,int Merety)
        {
            
            //Palya = new string [MERETY, MERETX];
            for (int y = 0; y < MERETY; y++)
            {
                for (int x = 0; x < MERETX; x++)
                {

                    Palya[y, x] = "X";
                }
            }
        }

        /* kirajzolja */
        public static void kirajzol(string[,] Palya)
        {
            for (int y = 0; y < MERETY; ++y)
            {
                for (int x = 0; x < MERETX; ++x)
                {
                    /* itt hasznalja ki, hogy az enum ertekek szandekosan
                     * egyeznek a karakterkodokkal */
                    if (Palya[y, x] == "X")
                    { Console.BackgroundColor = ConsoleColor.White; }
                    else { Console.BackgroundColor = ConsoleColor.Black; }
                    
                    Console.SetCursorPosition(y, x);
                    Console.Write(Palya[y, x]);
                }
                //putchar(p[y][x]);
                Console.WriteLine();
            }
        }
        public static Random rnd = new Random();
        /* ez maga a generalo fuggveny */
        public static string[] iranyok = new string[] { "fel", "le", "jobbra", "balra" };
        public static void labirintus(string[,] Palya, int x, int y)
        {



            /* erre a pontra hivtak minket, ide lerakunk egy darabka jaratot. */
            Palya[y, x] = " ";

            /* a tomb keverese */
            for (int i = 3; i > 0; --i)
            {   /* mindegyiket... */
                int r = rnd.Next(0, 4001) % 4;
                int random;   /* egy veletlenszeruen valasztottal... */


                string temp = iranyok[i];    /* megcsereljuk. */
                iranyok[i] = iranyok[r];
                iranyok[r] = temp;
            }

            /* a kevert iranyok szerint mindenfele probalunk menni, ha lehet. */
            for (int i = 0; i < 4; ++i)
                switch (iranyok[i])
                {
                    case "fel":
                        if (y >= 2 && Palya[y - 2, x] != " ")
                        {
                            Palya[y - 1, x] = " ";      /* elinditjuk arrafele a jaratot */
                            labirintus(Palya, x, y - 2); /* es rekurzive megyunk tovabb */
                        }
                        break;
                    case "balra":
                        if (x >= 2 && Palya[y, x - 2] != " ")
                        {
                            Palya[y, x - 1] = " ";
                            labirintus(Palya, x - 2, y);
                        }
                        break;
                    case "le":
                        if (y < MERETY - 2 && Palya[y + 2, x] != " ")
                        {
                            Palya[y + 1, x] = " ";
                            labirintus(Palya, x, y + 2);
                        }
                        break;
                    case "jobbra":
                        if (x < MERETX - 2 && Palya[y, x + 2] != " ")
                        {
                            Palya[y, x + 1] = " ";
                            labirintus(Palya, x + 2, y);
                        }
                        break;
                }
        }

    }
}