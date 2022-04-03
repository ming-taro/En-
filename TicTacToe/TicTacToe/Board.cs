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

        public void AddSpaceNumberList(int spaceNumber)
        {
            this.spaceNumber.Add(spaceNumber);
        }

        public int FindSpaceNumber(int row, int column)
        {
            switch (row, column)
            {
                case (0, 0):
                    return 1;
                case (0, 1):
                    return 2;
                case (0, 2):
                    return 3;
                case (1, 0):
                    return 4;
                case (1, 1):
                    return 5;
                case (1, 2):
                    return 6;
                case (1, 3):
                    return 7;
                case (2, 0):
                    return 8;
                default:
                    return 9;
            }
        }

        public void FindRandomValidSpace()
        {
            Random randomNumber = new Random();
            int spaceNumber = randomNumber.Next(1, 10);

            while (!this.spaceNumber.Contains(spaceNumber))
            {
                spaceNumber = randomNumber.Next(1, 10);
            }

            this.spaceNumber.Add(spaceNumber);
        }
    }
}
