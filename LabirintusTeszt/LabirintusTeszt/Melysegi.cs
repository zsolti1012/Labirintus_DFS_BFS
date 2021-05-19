using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintusTeszt
{
    public static class Melysegi
    {
        public static int MERETY = Program.SIZEY;
        public static int MERETX = Program.SIZEX;

        //# include <stdio.h>
        //# include <stdbool.h>

        //#define MERETX 19
        //#define MERETY 13


        const string Jarat = " ";
        const string Fal = "X";
        const string Kijarat = "*";
        const string Zsakutca = ".";
        const string Kerdeses = "?";
        public static int steps = 0;


        /* a mainben levo sztring miatt ez most char tomb,
         * de a celjaink szempontjabol teljesen mindegy, hiszen
         * a fenti felsorolt ertekek szandekosan karakterkodok */
        //typedef char Palya[MERETY][MERETX + 1];

        public static void kirajzol(string[,] Palya)
        {
            for (int y = 0; y < MERETY; ++y)
            {
                for (int x = 0; x < MERETX; ++x)
                {
                    /* itt hasznalja ki, hogy az enum ertekek szandekosan
                     * egyeznek a karakterkodokkal */
                    switch (Palya[y, x])
                    {
                        case "X":
                            Console.BackgroundColor = ConsoleColor.White;
                            break;

                        case ".":
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;

                        case "?":

                            Console.BackgroundColor = ConsoleColor.Yellow;

                            break;
                        case "*":
                            
                            Console.BackgroundColor = ConsoleColor.Green;
                            steps++;
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                    }






                    Console.SetCursorPosition(y, x);
                    Console.Write(Palya[y, x]);
                }
                
                //putchar(p[y][x]);
                Console.WriteLine();
            }
            Program.steps = steps-1;
        }

        /* igazzal ter vissza, ha arrafele megtalalta,
         * hamissal, ha nem */
        public static Random rnd = new Random();
        public static int veletlen = rnd.Next(0, 4001) % 4;

        static public bool megfejt(string[,] Palya, int x, int y, int celx, int cely)
        {
            Program.recursions++;
            /* elso korben bejeloljuk ezt a helyet Kerdesesnek -
             * ez vegulis csak azert lenyeges, hogy ne jarat legyen
             * itt, mert akkor visszajohetne ide */
            Palya[y, x] = Kerdeses;

            /* egyelore nem talaljuk a Kijaratot */
            bool megtalalt = false;

            /* pont a cel8nal allunk? */
            if (x == celx && y == cely)
                megtalalt = true;


            /* ha meg nem talaltuk meg a Kijaratot... ES ha tudunk jobbra menni... */
            if (!megtalalt && x < MERETX - 1 && Palya[y, x + 1] == Jarat)
            {
            /* ha arra van a megfejtes */
                if (megfejt(Palya, x + 1, y, celx, cely)) megtalalt = true;
           }

            /* balra */
            if (!megtalalt && x > 0 && Palya[y, x - 1] == Jarat)
            {
                     if (megfejt(Palya, x - 1, y, celx, cely)) megtalalt = true;
             }

            if (!megtalalt && y > 0 && Palya[y - 1, x] == Jarat)
            {

                if (megfejt(Palya, x, y - 1, celx, cely)) megtalalt = true;
              
           }
            if (!megtalalt && y < MERETY - 1 && Palya[y + 1, x] == Jarat)
            {
                 if (megfejt(Palya, x, y + 1, celx, cely)) megtalalt = true;
               
            }










            /* viszont ha innen kiindulva meg lehet talalni a Kijaratot
             * (most mar tudjuk a fuggvenyek visszateresi ertekebol),
             * akkor a Kijarathoz vezeto utkent jeloljuk meg. */

            Palya[y, x] = megtalalt ? Kijarat : Zsakutca;
            /* jelezzuk a hivonak, hogy valahonnan errefele indulva
             * lehet-e Kijaratot talalni */
       
            return megtalalt;
        }
    }
}
