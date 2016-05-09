using System;
using System.Text;

namespace GameOfLifeKata
{
    public class Game
    {
        public int m_ColumnCount;
        public int m_RowCount;
        public bool[,] m_Grid;

        public void GenerateRandomGame(int rows, int columns)
        {
            var randomGameGridGenerator = new RandomGameGridGenerator();
            m_Grid = randomGameGridGenerator.GenerateGameGrid(new object[] {rows, columns});
            m_RowCount = m_Grid.GetLength(0);
            m_ColumnCount = m_Grid.GetLength(1);
        }

        public void GenerateGameFromString(string initialGameState)
        {
            var stringGameGridGenerator = new StringGameGridGenerator();
            m_Grid = stringGameGridGenerator.GenerateGameGrid(new object[] {initialGameState});
            m_RowCount = m_Grid.GetLength(0);
            m_ColumnCount = m_Grid.GetLength(1);
        }

        /// <summary>
        /// Updates the game, calculating life and death
        /// </summary>
        public void UpdateGame()
        {
            //make a copy of the grid to work on to avoid each new cell calculation affecting the next
            var newValuesGrid = (bool[,])m_Grid.Clone();
            for (var rowIndex = 0; rowIndex < m_RowCount; rowIndex++)
            {
                for (var colIndex = 0; colIndex < m_ColumnCount; colIndex++)
                {
                    var newState = GetNewLifeOrDeathState(rowIndex, colIndex, m_Grid[rowIndex, colIndex]);
                    newValuesGrid[rowIndex, colIndex] = newState;
                }
            }

            //replace the game grid with the working copy
            m_Grid = newValuesGrid;
        }
        
        /// <summary>
        /// Gets the new state of a cell based on the rules of the game
        /// </summary>
        /// <param name="rowIndex">row in the grid of the cell</param>
        /// <param name="colIndex">column in the grid of the cell</param>
        /// <param name="currentCellState">the current cell state (alive or dead)</param>
        /// <returns></returns>
        private bool GetNewLifeOrDeathState(int rowIndex, int colIndex, bool currentCellState)
        {
            var surroundingLiveCells = 0;

            for (var rowCounter = rowIndex - 1; rowCounter <= rowIndex + 1; rowCounter++)
            {
                for (var colCounter = colIndex - 1; colCounter <= colIndex + 1; colCounter++)
                {
                    try
                    {
                        //don't include the cell we're updating in the calculation
                        if (rowCounter == rowIndex && colCounter == colIndex)
                            continue;

                        //find the value of the surrounding cell at the given point
                        var workingCell = m_Grid[rowCounter, colCounter];
                        if (workingCell)
                            surroundingLiveCells++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //we expect this to happen around the edge of the grid, so continue.
                        //TODO: don't rely on exceptions for control flow
                    }
                }
            }

            if (currentCellState)
            {
                if (surroundingLiveCells < 2)
                    return false;

                if (surroundingLiveCells > 3)
                    return false;

                return true;
            }

            return surroundingLiveCells == 3;
        }
    }
}
