using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeKata.Tests
{
    public class GameTests
    {
        private static readonly object[] m_SourceGames = {
            new object[] {"1 1\n.", new bool[,] { { false } } },
            new object[] {"1 1\n*", new bool[,] { { true } } },
            new object[] {"2 2\n.*\n*.", new bool[,] { {false,true }, { true, false} } },
            new object[] {"4 4\n...*\n..**\n....\n*...", new bool[,] { { false, false, false, true }, { false, false, true, true }, { false, false, false, false}, { true, false, false, false } } }
                                    };

        private static readonly object[] m_SourceGameUpdateOnce = {
            new object[] {"1 1\n*", new bool[,] { { false } } }, //one live cell dies
            new object[] {"2 2\n**\n*.", new bool[,] { {true,true }, { true, true} } }, //one dead cell should become alive with exactly three neighbours
            new object[] {"4 3\n..**\n..**\n..**", new bool[,] { { false, false, true, true }, { false, false, false, false }, { false, false, true, true} } } //live cells with > 3 live neighbours die
        };

        [Test]
        [TestCase("1 2\n", 1, 2)]
        [TestCase("10 20\n", 10, 20)]
        [TestCase("100 200\n", 100, 200)]
        public void ValidGameHeaderSetsColumnsAndRowsCorrectly(string gameHeader, int expectedRows, int expectedColumns)
        {
            Game game = new Game(gameHeader);
            Assert.AreEqual(expectedRows, game.m_RowCount);
            Assert.AreEqual(expectedColumns, game.m_ColumnCount);
        }

        [Test]
        [TestCase("0 1\n")]
        [TestCase("1 0\n")]
        [TestCase("100 200 300\n")]
        [TestCase("1\n")]
        [TestCase("\n")]
        [TestCase("a b")]
        [TestCase("1 2")]
        public void InvalidGameHeaderThrowsCorrectException(string gameHeader)
        {
            Game game;
            Assert.Throws<InvalidGameHeaderException>(() => game = new Game(gameHeader));
        }

        [Test]
        [TestCaseSource("m_SourceGames")]
        public void InitialGameStateIsPopulatedCorrectly(string initialGameState, bool[,] expectedGameState)
        {
            Game game = new Game(initialGameState);
            Assert.AreEqual(expectedGameState, game.m_Grid);
        }

        [Test]
        [TestCase("1 1\na")]
        [TestCase("2 2\n..\n*x")]
        public void InvalidCharacterInGameDefinitionThrowsExpectedException(string initialGameState)
        {
            Game game;
            Assert.Throws<InvalidGameCharacterException>(() => game = new Game(initialGameState));
        }

        [Test]
        [TestCaseSource("m_SourceGameUpdateOnce")]
        public void GameStateIsCorrectAfterOneUpdate(string initialGameState, bool[,] expectedGameState)
        {
            Game game = new Game(initialGameState);
            game.UpdateGame();
            Assert.AreEqual(expectedGameState, game.m_Grid);
        }
        
    }
}
