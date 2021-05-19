using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintusTeszt
{
    public static class bfs2
    {
        public static int ROW;
        public static int COL;
        public static Point source;
        public static Point destination;
        public static List<Point> points = new List<Point>();
        public static Point c;
        public static int[,] distance;// = new int[25, 51];

        // Elmentjük a mátrix celláinak koordinátáit
        public class Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;

            }
        };

        // Sor használat
        public class queueNode
        {
            //A cella koordinátái
            public Point pt;

            // a cella távolsága, az indulástól nézve
            public int dist;

            public queueNode parents;
            public queueNode(Point pt, int dist,queueNode parents)
            {
                this.pt = pt;
                this.dist = dist;
                this.parents = parents;
            }
        };

        static bool isValid(int row, int col)
        {
            //igazzal tér vissza ha a sor és az oszlop a tartományon belül van
            return (row >= 0) && (row < ROW) &&
                   (col >= 0) && (col < COL);
        }

        //A két mátrixal szemléltetjük a cellát körülvevő cellákat, szomszédokat
        static int[] rowNum = { -1, 0, 0, 1 };
        static int[] colNum = { 0, -1, 1, 0 };

        public static queueNode curr;

        //Maga a szélességi bejárás algoritmusa
        public static int BFS(int[,] mat, Point src,
                                   Point dest)
        {
            distance = new int[ROW, COL];
            //ellenőrizzük a start és a cél cellát, ha bennük nem egy, hanem nulla szerepel,
            //akkor a feladatnak biztosan nincs megoldása
            if (mat[src.x, src.y] != 1 ||
                mat[dest.x, dest.y] != 1)
                return -1;

            bool[,] visited = new bool[ROW, COL];
            

            // Jelöljük az aktuális cellát a vizsgálva kulcsszóval 
            visited[src.x, src.y] = true;

            // Létrehozzuk a sort
            Queue<queueNode> q = new Queue<queueNode>();

            // A start szimbólumnak nulla lesz a távolsága
            queueNode s = new queueNode(src, 0,null);
            q.Enqueue(s); // Enqueue source cell
            
            
            //Amíg a sor tartalmaz elemet végrehajtjuk a vizsgálatot 
            while (q.Count != 0)
            {
                 curr = q.Peek();
                Point pt = curr.pt;

                // Ha megtaláltuk a célt, akkor készen vagyunk
                 if (pt.x == dest.x && pt.y == dest.y)
                    return curr.dist;

                // Egyébként kivesszük a sorból az aktuális elemet
                q.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    int row = pt.x + rowNum[i];
                    int col = pt.y + colNum[i];

                    // Ha a következő elem, érvényes és még nem vizsgált, 
                    //akkor beletesszük a sorba
                    if (isValid(row, col) &&
                            mat[row, col] == 1 &&
                       !visited[row, col])
                    {
                        //az elemet vizsgált kulcsszóval jelöljük
                        visited[row, col] = true;
                        queueNode Adjcell = new queueNode(new Point(row, col),
                                                              curr.dist + 1,curr);
                        
                        distance[row, col] = curr.dist+1 ;
                        q.Enqueue(Adjcell);
                        Program.dequeues++;
                    }
                }
            }

             printPath(curr);
           
            //-1-el térünk vissza ha nem található útvonal
            return -1;

        
        }
        static public List<Point> paths = new List<Point>();

        public static void printPath(queueNode node)
        {
            if (node == null)
            {
                return;
            }
            printPath(node.parents);
            paths.Add(c = new Point(node.pt.x, node.pt.y));
            c = new Point(Console.CursorLeft,Console.CursorTop);
            Console.SetCursorPosition(node.pt.y, node.pt.x);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(" ");
            Console.SetCursorPosition(c.x,c.y);
            Console.ResetColor();
            //Console.WriteLine("{0},{1}",
            //node.pt.x, node.pt.y);
        }
    }
}
