using System;
using System.Collections.Generic;

namespace Jumper.Game
{
    public class Game
    {
        public bool IsWon(List<char> blanks)
        {
            bool won = true;
            for (int i = 0; i < blanks.Count; i++)
            {
                if (blanks[i] == '_')
                    won = false;
            }
            return won;
        }
        public bool IsLost(bool haveParachute)
        {
            return !haveParachute;
        }
    }
}