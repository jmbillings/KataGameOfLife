using NUnit.Framework;

namespace GameOfLifeKata.Tests
{
    public class GameTests
    {
        [Test]
        [TestCase("1 2\n", 1, 2)]
        [TestCase("10 20\n", 10, 20)]
        [TestCase("100 200\n", 100, 200)]
        public void ValidGameHeaderSetsColumnsAndRowsCorrectly(string gameHeader, int expectedRows, int expectedColumns)
        {
            var game = new Game();
            game.GenerateGameFromString(gameHeader);
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
            var game = new Game();
            Assert.Throws<InvalidGameHeaderException>(() => game.GenerateGameFromString(gameHeader));
        }

        [Test]
        [TestCaseSource("m_SourceGames")]
        public void InitialGameStateIsPopulatedCorrectly(string initialGameState, bool[,] expectedGameState)
        {
            var game = new Game();
            game.GenerateGameFromString(initialGameState);
            Assert.AreEqual(expectedGameState, game.m_Grid);
        }

        [Test]
        [TestCase("1 1\na")]
        [TestCase("2 2\n..\n*x")]
        public void InvalidCharacterInGameDefinitionThrowsExpectedException(string initialGameState)
        {
            var game = new Game();
            Assert.Throws<InvalidGameCharacterException>(() => game.GenerateGameFromString(initialGameState));
        }

        [Test]
        [TestCaseSource("m_SourceGameUpdateOnce")]
        public void GameStateIsCorrectAfterOneUpdate(string initialGameState, bool[,] expectedGameState)
        {
            var game = new Game();
            game.GenerateGameFromString(initialGameState);
            game.UpdateGame();
            Assert.AreEqual(expectedGameState, game.m_Grid);
        }

        [Test]
        [TestCaseSource("m_SourceGameUpdateTwice")]
        public void GameStateIsCorrectAfterTwoUpdates(string initialGameState, bool[,] expectedGameState)
        {
            var game = new Game();
            game.GenerateGameFromString(initialGameState);
            game.UpdateGame();
            game.UpdateGame();
            Assert.AreEqual(expectedGameState, game.m_Grid);
        }

        [Test]
        public void ARandomizedTestWillStartAndRunFor100IterationsWithoutThrowing()
        {
            var game = new Game();
            game.GenerateRandomGame(100, 100);
            var iterations = 0;
            while (iterations <= 100)
            {
                Assert.DoesNotThrow(() => game.UpdateGame());
                iterations++;
            }
        }


        private static readonly object[] m_SourceGames =
        {
            new object[] {"1 1\n.", new[,] {{false}}},
            new object[] {"1 1\n*", new[,] {{true}}},
            new object[]
            {
                "2 2\n.*\n*.",
                new[,]
                {
                    {false, true},
                    {true, false}
                }
            },
            new object[]
            {
                "4 4\n...*\n..**\n....\n*...",
                new[,]
                {
                    {false, false, false, true},
                    {false, false, true, true},
                    {false, false, false, false},
                    {true, false, false, false}
                }
            }
        };

        private static readonly object[] m_SourceGameUpdateOnce =
        {
            new object[] {"1 1\n*", new[,] {{false}}}, //one live cell dies
            new object[]
            {
                "2 2\n**\n*.",
                new[,]
                {
                    {true, true},
                    {true, true}
                }
            }, //one dead cell should become alive with exactly three neighbours
            new object[]
            {
                "3 4\n..**\n..**\n..**",
                new[,]
                {
                    {false, false, true, true},
                    {false, true, false, false},
                    {false, false, true, true}
                }
            }, //live cells with > 3 live neighbours die, dead cell with == 3 neighbours lives
            new object[]
            {
                "6 6\n......\n.**...\n**...*\n....**\n.....*\n......",
                new[,]
                {
                    {false, false, false, false, false, false},
                    {true, true, true, false, false, false},
                    {true, true, true, false, true, true},
                    {false, false, false, false, true, true},
                    {false, false, false, false, true, true},
                    {false, false, false, false, false, false}
                }
            }
        };

        private static readonly object[] m_SourceGameUpdateTwice =
       {
            new object[] {"1 1\n*", new[,] {{false}}}, //one live cell dies
            new object[]
            {
                "2 2\n**\n*.",
                new[,]
                {
                    {true, true},
                    {true, true}
                }
            }, //one dead cell should become alive with exactly three neighbours
            new object[]
            {
                "3 4\n..**\n..**\n..**",
                new[,]
                {
                    {false, false, true, false},
                    {false, true, false, false},
                    {false, false, true, false}
                }
            }, //live cells with > 3 live neighbours die, dead cell with == 3 neighbours lives
            new object[]
            {
                "6 6\n......\n.**...\n**...*\n....**\n.....*\n......",
                new[,]
                {
                    {false, true, false, false, false, false},
                    {true, false, true, true, false, false},
                    {true, false, true, false, true, true},
                    {false, true, false, false, false, false},
                    {false, false, false, false, true, true},
                    {false, false, false, false, false, false}
                }
            }
        };
    }
}
