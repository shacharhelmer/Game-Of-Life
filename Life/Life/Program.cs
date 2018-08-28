using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Life
{
    class Program
    {
        static int lifeSize = 20;
        static Random rnd = new Random();
        static int generations = 0;
        static void Main(string[] args)
        {
            bool[,] life = new bool[lifeSize, lifeSize];
            while (true) // Generates games
            {
                Setup(life); // sets the life map and seeds new creatures
                playLife(life); // plays the current life
            }

        }

        public static void playLife(bool[,] life)
        {
            while (true)
            {
                PrintLife(life);
                System.Threading.Thread.Sleep(70);
                if (IsLifeEqual(life, Evolve(life)))
                {
                    Console.SetCursorPosition(0, 30);
                    Console.WriteLine("Life died out in: " + generations + "generations!");
                    break;
                }
                else
                {
                    life = Evolve(life);
                    generations++;
                }
            }
        }

        public static void Setup(bool[,] life)
        {
            SetLife(life);
            SetSeed(life);
        }

        /// <summary>
        /// Checks two life maps for equal living and dead cells
        /// </summary>
        /// <param name="curr"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public static bool IsLifeEqual(bool[,] curr, bool[,] next)
        {
            for (int i = 0; i < lifeSize; i++)
            {
                for (int j = 0; j < lifeSize; j++)
                {
                    if (curr[i, j] != next[i, j])
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Changes the life map according to the laws of life
        /// </summary>
        /// <param name="l">The life map to change</param>
        /// <returns></returns>
        public static bool[,] Evolve(bool[,] l)
        {
            bool[,] newLife = new bool[lifeSize, lifeSize];
            bool cell = false;
            int count = 0;
            for(int i = 0; i < lifeSize; i++)
                for(int j = 0; j < lifeSize; j++)
                {
                    cell = l[i, j];
                    count = NeighbourCount(l, i, j);
                    if (cell == false && count == 3)
                        newLife[i, j] = true;
                    else if (cell && (count < 2 || count > 3))
                        newLife[i, j] = false;
                    else
                        newLife[i, j] = cell;
                }
            return newLife;
        }

        /// <summary>
        /// Counts the living neighbours of a cell
        /// </summary>
        /// <param name="l">The life map to check on</param>
        /// <param name="x">The wanted cell's x coordinate</param>
        /// <param name="y">The wanted cell's y coordinate</param>
        /// <returns></returns>
        public static int NeighbourCount(bool[,] l, int x, int y)
        {
            int count = 0;
            for (int rowOffset = -1; rowOffset < 2; rowOffset++) 
                for(int columnOffset = -1; columnOffset < 2; columnOffset++)
                {
                    if (rowOffset == 0 && columnOffset == 0)
                        continue;
                    else
                    {
                        if (x + rowOffset >= 0 && x + rowOffset < lifeSize && y + columnOffset >= 0 && y + columnOffset < lifeSize)
                            if (l[x + rowOffset, y + columnOffset])
                                count++;
                    }                   
                }
            return count;
        }

        /// <summary>
        /// Resets the life map to be all dead cells
        /// </summary>
        /// <param name="l">The life map to reset</param>
        public static void SetLife(bool[,] l)
        {
            for(int i = 0; i < lifeSize; i++)
                for(int j = 0; j < lifeSize; j++)
                {
                    l[i, j] = false;
                }
        }

        /// <summary>
        /// Prints the life map to the console
        /// </summary>
        /// <param name="l">The life map to print</param>
        public static void PrintLife(bool[,] l)
        {
            for(int i = 0; i < lifeSize; i++)
                for(int j = 0; j < lifeSize; j++)
                {
                    Console.SetCursorPosition(i, j);
                    if (l[i, j])
                        Console.Write('&');
                    else
                        Console.Write('-');
                }
        }


        /// <summary>
        /// Sets choosen cells to be the staring living cells
        /// </summary>
        /// <param name="l">The life map to set on</param>
        public static void SetSeed(bool[,] l)
        {
            int x = 0;
            int y = 0;
            
            for(int i = 0; i < (lifeSize*lifeSize) / 2; i++)
            {
                x = rnd.Next(0, lifeSize);
                y = rnd.Next(0, lifeSize);
                if (!l[x, y])
                    l[x, y] = true;
            }
        }      
    }
}
      