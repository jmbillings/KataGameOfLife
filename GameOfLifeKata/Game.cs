using System;
using System.Text;

namespace GameOfLifeKata
{
    public class Game
    {
        public int m_ColumnCount;
        public int m_RowCount;
        public bool[,] m_Grid;
        private Random random;

        /// <summary>
        /// Initialises a new game
        /// </summary>
        /// <param name="initialGameState">string representing the initial state of the game</param>
        public Game(string initialGameState)
        {
            GetGameSize(initialGameState);
            PopulateInitialState(initialGameState);
        }

        /// <summary>
        /// Initialises a new random game
        /// </summary>
        /// <param name="rows">number of rows in the game grid</param>
        /// <param name="cols">number of columns in the game grid</param>
        public Game(int rows, int cols)
        {
            var gameStringBuilder = new StringBuilder();

            gameStringBuilder.Append(string.Format("{0} {1}\n", rows, cols));

            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
                gameStringBuilder.Append(GenerateRandomGameRow(cols));

            var initialGameState = gameStringBuilder.ToString();
            GetGameSize(initialGameState);
            PopulateInitialState(initialGameState);
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
        /// Populates the grid with the initial state of each cell
        /// </summary>
        /// <param name="initialGameState">string representing the initial state of the game</param>
        private void PopulateInitialState(string initialGameState)
        {
            var rows = initialGameState.Split('\n');
            for (var rowIndex = 1; rowIndex < rows.Length; rowIndex++)
            {
                var rowCharacters = rows[rowIndex].ToCharArray();
                for (var colIndex = 0; colIndex < rowCharacters.Length; colIndex++)
                {
                    switch (rowCharacters[colIndex])
                    {
                        case '.':
                            m_Grid[rowIndex - 1, colIndex] = false;
                            break;
                        case '*':
                            m_Grid[rowIndex - 1, colIndex] = true;
                            break;
                        default:
                            throw new InvalidGameCharacterException();
                    } 
                }
            }
        }

        /// <summary>
        /// Sets the size of the empty game grid
        /// </summary>
        /// <param name="initialGameState">string representing the initial state of the game</param>
        private void GetGameSize(string initialGameState)
        {
            try
            {
                var headerRowValues = initialGameState.Substring(0, initialGameState.IndexOf('\n')).Split(' ');

                if (headerRowValues.Length != 2)
                    throw new InvalidGameHeaderException();
                if (!int.TryParse(headerRowValues[0], out m_RowCount))
                    throw new InvalidGameHeaderException();
                if (!int.TryParse(headerRowValues[1], out m_ColumnCount))
                    throw new InvalidGameHeaderException();
                if (m_ColumnCount == 0 || m_RowCount == 0)
                    throw new InvalidGameHeaderException();

                m_Grid = new bool[m_RowCount, m_ColumnCount];
            }
            catch
            {
                throw new InvalidGameHeaderException();
            }
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

            //Rules:
            //1.Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
            //2.Any live cell with more than three live neighbours dies, as if by overcrowding.
            //3.Any live cell with two or three live neighbours lives on to the next generation.
            //4.Any dead cell with exactly three live neighbours becomes a live cell.

            if (currentCellState)
            {
                if (surroundingLiveCells < 2)
                    return false; //rule 1

                if (surroundingLiveCells > 3)
                    return false; //rule 2

                return true; //rule 3
            }

            return surroundingLiveCells == 3; //rule 4
        }

        private string GenerateRandomGameRow(int cols)
        {
            if (random == null) random = new Random();

            var rowBuilder= new StringBuilder();
            for (var colIndex = 0; colIndex < cols; colIndex++)
            {
                if (random.Next(100) < 30)
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
