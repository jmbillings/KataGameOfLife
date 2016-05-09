using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeKata
{
    static class GameUtils
    {
        /// <summary>
        /// Populates the grid with the initial state of each cell
        /// </summary>
        /// <param name="initialGameState">string representing the initial state of the game</param>
        /// <param name="grid">game grid to be populated</param>
        internal static void PopulateInitialState(string initialGameState, ref bool[,] grid)
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
                            grid[rowIndex - 1, colIndex] = false;
                            break;
                        case '*':
                            grid[rowIndex - 1, colIndex] = true;
                            break;
                        default:
                            throw new InvalidGameCharacterException();
                    }
                }
            }
        }
    }
}
