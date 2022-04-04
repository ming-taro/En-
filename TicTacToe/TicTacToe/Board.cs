using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Board
    {
        //private char[,] board = new char[3,3];
        private List<char> spaceDrawType = new List<char>();    //보드판에 그려진 (X or O)표시 저장
        private List<int> spaceNumber = new List<int>();        //X or O표시가 있는 칸 번호를 저장하는 리스트

        public Board()
        {
            InitBoard();
        }
        public void InitBoard()  
        {
            spaceDrawType.Clear();     
            spaceNumber.Clear();

            for(int i=0; i<9; i++)
            {
                spaceDrawType.Add(' ');    //초기 보드판에는 각 칸마다 공백을 저장한다
            }
        }

        public void SetOneSpace(int row, int column, char drawType)   //해당 칸에 x or o를 그리는 함수
        {
            spaceDrawType[FindSpaceNumber(row, column)] = drawType;   //입력받은 행, 열에 대한 칸 번호를 찾아 X or O를 저장
        }
        

        public char GetSpaceDrawType(int row, int column)  //해당 칸에 그려진 값 얻기
        {
            return spaceDrawType[FindSpaceNumber(row, column)];
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
                    return 0;
                case (0, 1):
                    return 1;
                case (0, 2):
                    return 2;
                case (1, 0):
                    return 3;
                case (1, 1):
                    return 4;
                case (1, 2):
                    return 5;
                case (1, 3):
                    return 6;
                case (2, 0):
                    return 7;
                default:
                    return 8;
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
