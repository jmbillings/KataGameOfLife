using System;
using System.Text;

namespace GameOfLifeKata
{
    class ConsoleGameOutput : IGameOutput
    {
        public void OutputGameGrid(bool[,] gameGrid)
        {
            Console.Clear();
            var gridStringBuilder = new StringBuilder();
            for (var rowIndex = 0; rowIndex < gameGrid.GetLength(0); rowIndex++)
            {
                var rowStringBuilder = new StringBuilder();
                for (var colIndex = 0; colIndex < gameGrid.GetLength(1); colIndex++)
                {
                    rowStringBuilder.Append(gameGrid[rowIndex, colIndex] ? '*' : '.');
                }
                gridStringBuilder.Append(rowStringBuilder.Append('\n'));
            }

            Console.WriteLine(gridStringBuilder.ToString());
        }
    }
}
