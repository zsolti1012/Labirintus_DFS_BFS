using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.IO;


namespace LabirintusTeszt
{
    //Created by Zsolt Oravecz







    

    public static class Program
{

        static public int SIZEY;
        static public int SIZEX;
        static public Point start;
        static public Point destination;
        static public int steps;
        static public int dequeues;
        static public int recursions = -1;
        static public int MERETX;
        static public int MERETY;


        public static void bfsinit(string labirinth)
        {
            string filename = labirinth ;//"labirintus50.txt";
            List<string> Read = new List<string>();

            string line;
           try
            {

                StreamReader sr = new StreamReader(filename);
                int counter = 0;
                while ((line = sr.ReadLine()) != null)
                {
                   
                    if (counter == 0)
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            Read.Add(Convert.ToString(line[i]));

                        }
                        counter++;
                    }
                    else
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            Read[i] = Read[i] + Convert.ToString(line[i]);
                        }
                    }
                }
                //string a = Convert.ToString(line[1]);
                sr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("FileReading is inadequate!");
            }

            ////EZ működik csak fordítva
            Program.SIZEX = Read[0].Length;//Beolvasott[0].Length - 3;
            Rajzolo.MERETX = Program.SIZEX;
            Program.SIZEY = Read.Count - 3;//Beolvasott.Count;
            Rajzolo.MERETY = Program.SIZEY;
            string[,] Palya2 = new string[Program.SIZEY, Program.SIZEX];

            for (int y = 1; y < Read.Count - 2; y++)
            {
                int a = Read[y].Length;
                for (int x = 0; x < Read[y].Length; x++)
                {
                    string c = Convert.ToString(Read[y][x]);
                    Palya2[y - 1, x] = Convert.ToString(Read[y][x]);
                    if (Palya2[y - 1, x] == "S")
                    {
                        Program.start = new Point(y - 1, x);
                        Palya2[y - 1, x] = " ";
                    }
                    if (Palya2[y - 1, x] == "C")
                    {
                        Program.destination = new Point(y - 1, x);
                        Palya2[y - 1, x] = " ";
                    }
                    //string b = Palya2[y, x - 1];
                }
            }


            /* ures palyara general egy labirintust */

            //Rajzolo.ures(Rajzolo.Palya,MERETX,MERETY);

            ///
            /// Labirintus Generáló fgv.
            ///
            //int kezdopontx = 1;
            //int kezdoponty = 1;
            //Rajzolo.labirintus(Rajzolo.Palya,kezdopontx,kezdoponty);
            //Rajzolo.Palya[4, 0] = "S";
            //Rajzolo.Palya[MERETY - 2, MERETX - 1] = "C";
            //Rajzolo.kirajzol(Rajzolo.Palya);

            bool depthsuccess = false;




            //Kirajzoló algoritmus
            Rajzolo.kirajzol(Palya2);

            Console.ResetColor();
            Console.WriteLine("<Press Enter!>");
            Console.ReadLine();
            //Melysegi.megfejt(Palya2, start.Y, start.X, destination.Y, destination.X);
            depthsuccess = Melysegi.megfejt(Palya2, Program.start.Y, Program.start.X, Program.destination.Y, Program.destination.X);
            Melysegi.kirajzol(Palya2);

            //-----------------------------------------

            int[,] Palya3 = new int[Program.SIZEX, Program.SIZEY];
            for (int i = 0; i < Program.SIZEX; i++)
            {
                for (int j = 0; j < Program.SIZEY; j++)
                {
                    if (Palya2[j, i] == "X")
                    {
                        Palya3[i, j] = 0;
                    }
                    else
                    {
                        Palya3[i, j] = 1;
                    }
                }
            }

            bfs2.COL = Program.SIZEY;
            bfs2.ROW = Program.SIZEX;
            //OSZLOP - SOR
            bfs2.source = new bfs2.Point(Program.start.Y, Program.start.X);
            bfs2.destination = new bfs2.Point(Program.destination.Y, Program.destination.X);

            int dist = bfs2.BFS(Palya3, bfs2.source, bfs2.destination);
            //Console.Clear();
            Console.ResetColor();
            Console.WriteLine("<Press Enter!>");
            Console.ReadLine();
            bfs2.printPath(bfs2.curr);


            if (!depthsuccess)
            {
                Console.WriteLine("Path doesn't exist");
            }
            else
            {
                Console.WriteLine("Path is {0} steps.", Program.steps);
            }

            if (dist == int.MaxValue || dist == -1)
                Console.WriteLine("Optimal Path doesn't exist");
            else Console.WriteLine("Optimal Path is {0} steps.", dist);

            Console.WriteLine("Resursions number: {0}", Program.recursions);
            Console.WriteLine("Queue using: {0}", Program.dequeues);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" ");
            Console.ResetColor();
            Console.WriteLine("- Dead End");


            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(" ");
            Console.ResetColor();
            Console.WriteLine("- Path");


            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(" ");
            Console.ResetColor();
            Console.WriteLine("- Optimal Path");
            //-------------------------------------------


        }


        public static void dfsinit(Point s,Point d)
        {


            ////EZ működik csak fordítva
            Program.SIZEX = 15;//Beolvasott[0].Length;//Beolvasott[0].Length - 3;
            Rajzolo.MERETX = Program.SIZEX;
            Program.SIZEY = 35;//Beolvasott.Count - 3;//Beolvasott.Count;
            Rajzolo.MERETY = Program.SIZEY;




            /* ures palyara general egy labirintust */

            Rajzolo.ures(Rajzolo.Palya, SIZEX, SIZEY);

            ///
            /// Labirintus Generáló fgv.
            ///
            int kezdopontx = 1;
            int kezdoponty = 1;
            Rajzolo.labirintus(Rajzolo.Palya, kezdopontx, kezdoponty);

            Rajzolo.Palya[s.X, s.Y] = " ";
            Rajzolo.Palya[s.X, s.Y+1] = " ";
            Rajzolo.Palya[d.X, d.Y] = " ";
            Rajzolo.kirajzol(Rajzolo.Palya);




            /*

            //Kirajzoló algoritmus
            Rajzolo.kirajzol(Palya2);

            Console.ResetColor();
            Console.WriteLine("<Press Enter!>");
            Console.ReadLine();
            */
            bool depthsuccess = Melysegi.megfejt(Rajzolo.Palya, 0, 4, d.Y, d.X);
            Melysegi.kirajzol(Rajzolo.Palya);

            //-----------------------------------------
            //Console.ReadLine();
            int[,] Palya3 = new int[Program.SIZEX, Program.SIZEY];
            for (int i = 0; i < Program.SIZEX; i++)
            {
                for (int j = 0; j < Program.SIZEY; j++)
                {
                    if (Rajzolo.Palya[j, i] == "X")
                    {
                        Palya3[i, j] = 0;
                    }
                    else
                    {
                        Palya3[i, j] = 1;
                    }
                }
            }

            bfs2.COL = Program.SIZEY;
            bfs2.ROW = Program.SIZEX;
            //OSZLOP - SOR
            bfs2.source = new bfs2.Point(s.Y,s.X);
            bfs2.destination = new bfs2.Point(d.Y, d.X);

            int dist = bfs2.BFS(Palya3, bfs2.source, bfs2.destination);
            //Console.Clear();
            Console.ResetColor();
            Console.WriteLine("<Press Enter!>");
            Console.ReadLine();
            bfs2.printPath(bfs2.curr);


            if (!depthsuccess)
            {
                Console.WriteLine("Path doesn't exist");
            }
            else
            {
                Console.WriteLine("DFS-Path is {0} steps.", steps);
            }

            if (dist == int.MaxValue || dist == -1)
                Console.WriteLine("Optimal Path doesn't exist");
            else Console.WriteLine("BFS-Optimal Path is {0} steps.", dist);
            Console.WriteLine("Recursions number: {0}", recursions);
            Console.WriteLine("Queue using: {0}", dequeues);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" ");
            Console.ResetColor();
            Console.WriteLine("- Dead End");


            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(" ");
            Console.ResetColor();
            Console.WriteLine("- Path");


            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(" ");
            Console.ResetColor();
            Console.WriteLine("- Optimal Path");
        }


        //Megadhatjuk a beolvasandó labirintus nevét



        public static void Main(string[] args)
    {
            string answer;
            do
            {
                Console.Clear();
                Console.WriteLine("Do you want to generate a maze(Press G) or do you enter one?(Press E)");
                answer = Console.ReadLine();
            } while (answer!="E"&&answer!="G");
            if(answer=="E")
            {
                Console.WriteLine("Please tyep a labirint: (for example: labirintus.txt) ");
                string labirinth = Console.ReadLine();
                bfsinit(labirinth);
            }
            else
            {
                Console.WriteLine("Enter Start x: (4)");
                int x = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Start y:  (0)");
                int y = int.Parse(Console.ReadLine());
                Point s = new Point(x,y);

                Console.WriteLine("Enter destination x:  (33)");
                x = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter destination y:  (14)");
                y = int.Parse(Console.ReadLine());
                Point d = new Point(x,y);
               
                    dfsinit(s,d);
                
               
             
                
            }
            
        Console.ReadLine();



    }
}

}

