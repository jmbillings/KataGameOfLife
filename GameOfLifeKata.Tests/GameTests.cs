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
            Assert.AreEqual(game.m_Rows, expectedRows);
            Assert.AreEqual(game.m_Columns, expectedColumns);    
        }


    }
}
