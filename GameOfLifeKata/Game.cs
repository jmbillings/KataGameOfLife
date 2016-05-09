﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeKata
{
    public class Game
    {
        public int m_ColumnCount;
        public int m_RowCount;
        public bool[,] m_Grid;

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
        /// Updates the game, calculating life and death
        /// </summary>
        public void UpdateGame()
        {
            //make a copy of the grid to work on to avoid each new cell calculation affecting the next
            bool[,] newValuesGrid = (bool[,])m_Grid.Clone();
            for (int rowIndex = 0; rowIndex < m_RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < m_ColumnCount; colIndex++)
                {
                    bool newState = GetNewLifeOrDeathState(rowIndex, colIndex, m_Grid[rowIndex, colIndex]);
                    newValuesGrid[rowIndex, colIndex] = newState;
                }
            }

            //replace the game grid with the working copy
            m_Grid = newValuesGrid;
        }

        private void PopulateInitialState(string initialGameState)
        {
            string[] rows = initialGameState.Split('\n');
            for (int rowIndex = 1; rowIndex < rows.Length; rowIndex++)
            {
                char[] rowCharacters = rows[rowIndex].ToCharArray();
                for (int colIndex = 0; colIndex < rowCharacters.Length; colIndex++)
                {
                    if (rowCharacters[colIndex] == '.')
                        m_Grid[rowIndex - 1, colIndex] = false;
                    else if (rowCharacters[colIndex] == '*')
                        m_Grid[rowIndex - 1, colIndex] = true;
                    else
                        throw new InvalidGameCharacterException(); 
                }
            }
        }

        private void GetGameSize(string initialGameState)
        {
            try
            {
                string[] headerRowValues = initialGameState.Substring(0, initialGameState.IndexOf('\n')).Split(' ');

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

        private bool GetNewLifeOrDeathState(int rowIndex, int colIndex, bool currentCellState)
        {
            int surroundingLiveCells = 0;
            int surroundingDeadCells = 0;

            for (int rowCounter = rowIndex - 1; rowCounter <= rowIndex + 1; rowCounter++)
            {
                for (int colCounter = colIndex - 1; colCounter <= colIndex + 1; colCounter++)
                {
                    try
                    {
                        //don't include the cell we're updating in the calculation
                        if (rowCounter == rowIndex && colCounter == colIndex)
                            continue;

                        //find the value of the surrounding cell at the given point
                        bool workingCell = m_Grid[rowCounter, colCounter];
                        if (workingCell)
                            surroundingLiveCells++;
                        else
                            surroundingDeadCells++;
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
    }
}
