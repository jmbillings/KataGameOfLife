using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
                    var fileContents = File.ReadAllText(args[1]);
                    m_Game = new Game(fileContents);
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
                        if (rowCount <= 50 && columnCount <= 50)
                        {
                            m_Game = new Game(rowCount, columnCount);
                        }
                        else
                            Console.WriteLine("A game grid can be 50x50 at most");
                    }
                    else
                        Console.WriteLine("Couldn't parse the row / column values\n");

                    break;
            }

            OutputGameGrid();
            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            StartGame();
        }

        private static void WriteUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("GameOfLifeKata -file <filename>");
            Console.WriteLine("Starts a game with the file specified as the initial state\n");
            Console.WriteLine("GameOfLifeKata -gen <rows> <cols>");
            Console.WriteLine("Starts a random game with a size of rows / cols (maximum of 50x50");
        }

        private static void StartGame()
        {
            var iterations = 0;
            while (iterations < 1000)
            {
                Thread.Sleep(50);
                m_Game.UpdateGame();
                OutputGameGrid();
                Console.WriteLine(iterations + "/1000 iterations (ctrl+C to abort)");
                iterations++;
            }
        }

        private static void OutputGameGrid()
        {
            Console.Clear();
            var gridStringBuilder = new StringBuilder();
            for (var rowIndex = 0; rowIndex < m_Game.m_RowCount; rowIndex++)
            {
                var rowStringBuilder = new StringBuilder();
                for(var colIndex = 0; colIndex < m_Game.m_ColumnCount; colIndex++)
                {
                    rowStringBuilder.Append(m_Game.m_Grid[rowIndex, colIndex] ? '*' : '.');
                }
                gridStringBuilder.Append(rowStringBuilder.Append('\n'));
            }   

            Console.WriteLine(gridStringBuilder.ToString());
        }
    }
}
