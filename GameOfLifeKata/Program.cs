using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeKata
{
    class Program
    {
        private static Game m_Game;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                WriteUsage();
                return;
            }

            switch (args[0])
            {
                case "-file":
                    //load the file
                    break;
                case "-gen":
                    if (args.Length != 3)
                    {
                        Console.WriteLine("Please pass a row and column count when using -gen");
                        return;
                    }

                    int rowCount;
                    int columnCount;
                    if (int.TryParse(args[1], out rowCount) && int.TryParse(args[2], out columnCount))
                    {
                        m_Game = new Game(rowCount, columnCount);
                    }
                    else
                    {
                        Console.WriteLine("Couldn't parse the row / column values\n");
                        return;
                    }
                    break;
            }
        }

        private static void WriteUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("GameOfLifeKata -file <filename>");
            Console.WriteLine("Starts a game with the file specified as the initial state\n");
            Console.WriteLine("GameOfLifeKata -gen <rows> <cols>");
            Console.WriteLine("Starts a random game with a size of rows / cols (maximum of 50x50");
        }
    }
}
