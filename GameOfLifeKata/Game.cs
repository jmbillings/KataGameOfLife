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
            GetGameSize(initialGameState);  
        }

        /// <summary>
        /// Updates the game, calculating life and death
        /// </summary>
        public void UpdateGame()
        {

        }

        private void GetGameSize(string initialGameState)
        {
            try
            {
                string[] headerRowValues = initialGameState.Substring(0, initialGameState.IndexOf('\n')).Split(' ');

                if (headerRowValues.Length != 2)
                    throw new InvalidGameHeaderException();
                if (!int.TryParse(headerRowValues[0], out m_Rows))
                    throw new InvalidGameHeaderException();
                if (!int.TryParse(headerRowValues[1], out m_Columns))
                    throw new InvalidGameHeaderException();
                if (m_Columns == 0 || m_Rows == 0)
                    throw new InvalidGameHeaderException();

            }
            catch
            {
                throw new InvalidGameHeaderException();
            }
        }
    }
}
