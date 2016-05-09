namespace GameOfLifeKata
{
    interface IGameGridGenerator
    {
        bool[,] GenerateGameGrid(object[] input);
    }
}