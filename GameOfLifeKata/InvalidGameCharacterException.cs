using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeKata
{
    public class InvalidGameCharacterException : Exception
    {
        public override string ToString()
        {
            return "Invalid character found in game definition";
        }
    }
}
