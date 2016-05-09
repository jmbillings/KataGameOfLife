namespace GameOfLifeKata
{
    class StringGameGridGenerator : IGameGridGenerator
    {
        public bool[,] GenerateGameGrid(object[] input)
        {
            var initialGameState = input[0].ToString();
            var grid = GetValidEmptyGameGrid(initialGameState);
            var rows = initialGameState.Split('\n');
            for (var rowIndex = 1; rowIndex < rows.Length; rowIndex++)
            {
                var rowCharacters = rows[rowIndex].ToCharArray();
                for (var colIndex = 0; colIndex < rowCharacters.Length; colIndex++)
                {
                    if (rowCharacters[colIndex] == '.')
                        grid[rowIndex - 1, colIndex] = false;
                    else if (rowCharacters[colIndex] == '*')
                        grid[rowIndex - 1, colIndex] = true;
                    else
                    throw  new InvalidGameCharacterException();
                }
            }

            return grid;
        }

        private bool[,] GetValidEmptyGameGrid(string input)
        {
            if (!input.Contains("\n"))
                throw new InvalidGameHeaderException();
            
            var headerRowValues = input.Substring(0, input.IndexOf('\n')).Split(' ');
            if (headerRowValues.Length != 2)
                throw new InvalidGameHeaderException();

            var rowCount = 0;
            var colCount = 0;
            if (!int.TryParse(headerRowValues[0], out rowCount))
                throw new InvalidGameHeaderException();
            if (!int.TryParse(headerRowValues[1], out colCount))
                throw new InvalidGameHeaderException();

            if (rowCount == 0 || colCount == 0)
                throw new InvalidGameHeaderException();

            return new bool[rowCount, colCount];
        }
    }
}
