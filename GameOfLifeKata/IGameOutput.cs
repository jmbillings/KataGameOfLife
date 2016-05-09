using System;
namespace GameOfLifeKata
{
    public interface  IGameOutput
    {
        string OutputGameGrid(bool[,] gameGrid);
    }
}
