﻿using System;

namespace GameOfLifeKata
{
    public class InvalidGameHeaderException : Exception
    {
        public override string ToString()
        {
            return "Invalid game state header found. Ensure the first row contains two numbers only";
        }
    }
}
