using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Board
    {
        private char[,] board = new char[3,3];
        List<string> boardValue;

        public Board()
        {
            InitBoard();
        }
        public void InitBoard()
        {
            for(int i=0; i<3; i++)
            {
                board[i, 0] = ' ';
                board[i, 1] = ' ';
                board[i, 2] = ' ';
            }
        }

        public void SetOneSpace(int row, int column, char drawType)
        {
            board[row, column] = drawType;
        }

        public char GetOneSpace(int row, int column)
        {
            return board[row, column];
        }
    }
}
