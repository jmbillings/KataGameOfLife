using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeKata
{
    public class Game
    {
        public int m_Columns;
        public int m_Rows;

        /// <summary>
        /// Initialises a new game
        /// </summary>
        /// <param name="initialGameState">string representing the initial state of the game</param>
        public Game(string initialGameState)
        {
            try
            {
                string[] headerRowValues = initialGameState.Substring(0, initialGameState.IndexOf('\n')).Split(' ');
                if (!int.TryParse(headerRowValues[0], out m_Rows))
                    throw new InvalidGameHeaderException();
                if (!int.TryParse(headerRowValues[1], out m_Columns))
                    throw new InvalidGameHeaderException();
            }
            catch
            {
                throw new InvalidGameHeaderException();
            }
        }

        /// <summary>
        /// Updates the game, calculating life and death
        /// </summary>
        public void UpdateGame()
        {

        }
    }
}
