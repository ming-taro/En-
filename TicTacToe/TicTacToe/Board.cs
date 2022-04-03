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
        private List<int> spaceNumber = new List<int>();

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

        public void AddSpaceNumberList(int row, int column)
        {
            switch(row, column)
            {
                case (0, 0):
                    spaceNumber.Add(1);
                    break;
                case (0, 1):
                    spaceNumber.Add(2);
                    break;
                case (0, 2):
                    spaceNumber.Add(3);
                    break;
                case (1, 0):
                    spaceNumber.Add(4);
                    break;
                case (1, 1):
                    spaceNumber.Add(5);
                    break;
                case (1, 2):
                    spaceNumber.Add(6);
                    break;
                case (1, 3):
                    spaceNumber.Add(7);
                    break;
                case (2, 0):
                    spaceNumber.Add(8);
                    break;
                default:
                    spaceNumber.Add(9);
                    break;
            }
        }
    }
}
