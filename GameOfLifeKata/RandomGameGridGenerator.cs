using System;
using System.Text;

namespace GameOfLifeKata
{
    class RandomGameGridGenerator : IGameGridGenerator
    {
        private Random m_Random;

        public bool[,] GenerateGameGrid(object[] input)
        {
            var rows = Convert.ToInt32(input[0]);
            var cols = Convert.ToInt32(input[1]);

            var gameStringBuilder = new StringBuilder();

            gameStringBuilder.Append(string.Format("{0} {1}\n", rows, cols));

            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
                gameStringBuilder.Append(GenerateRandomGameRow(cols));

            var initialGameState = gameStringBuilder.ToString();

            var gameGrid = new bool[rows, cols];
            
            GameUtils.PopulateInitialState(initialGameState, ref gameGrid);

            return gameGrid;
        }

        private string GenerateRandomGameRow(int cols)
        {
            if (m_Random == null) m_Random = new Random();

            var rowBuilder = new StringBuilder();
            for (var colIndex = 0; colIndex < cols; colIndex++)
            {
                if (m_Random.Next(100) < 30)
                {
                    // will be true 30% of the time
                    rowBuilder.Append('*');
                }
                else
                {
                    rowBuilder.Append('.');
                }
            }
            return rowBuilder.Append('\n').ToString();
        }
    }
}
