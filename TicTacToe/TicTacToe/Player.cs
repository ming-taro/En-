using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Player
    {
        private int score;
        private char drawType;

        public Player()
        {
            score = 0;
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public char DrawType
        {
            get { return drawType; }
            set { drawType = value; }
        }
    }
}
