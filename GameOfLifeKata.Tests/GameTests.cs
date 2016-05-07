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
        [Test]
        [TestCase("1 2\n", 1, 2)]
        [TestCase("10 20\n", 10, 20)]
        [TestCase("100 200\n", 100, 200)]
        public void ValidGameHeaderSetsColumnsAndRowsCorrectly(string gameHeader, int expectedRows, int expectedColumns)
        {
            Game game = new Game(gameHeader);
            Assert.AreEqual(game.m_RowCount, expectedRows);
            Assert.AreEqual(game.m_ColumnCount, expectedColumns);    
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
    }
}
