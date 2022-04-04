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

        public Board()    //생성자
        {
            InitBoard();
        }
        public void InitBoard()             //보드판 초기화 함수
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
        

        public char GetSpaceDrawType(int spaceNumber)  //해당 칸에 그려진 값 얻기
        {
            return spaceDrawType[spaceNumber];   //입력받은 행, 열에 대한 칸 번호를 찾아 그려진 값 반환
        }

        public void AddSpaceNumberList(int spaceNumber)  //그림이 그려진 칸 번호를 리스트에 추가하는 함수
        {
            this.spaceNumber.Add(spaceNumber);   //해당 칸 번호를 리스트에 추가
        }

        public int FindSpaceNumber(int row, int column)  //행, 열값에 따른 칸 번호를 찾아 반환해주는 함수
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

        public void FindRandomValidSpace()   //빈 칸(유효한) 중 랜덤한 칸 번호를 찾아 그리는 함수(컴퓨터가 사용)
        {
            Random randomNumber = new Random();
            int spaceNumber = randomNumber.Next(0, 9);      //0~8(칸번호)사이의 랜덤한 수 

            while (!this.spaceNumber.Contains(spaceNumber)) //칸번호 리스트에 없는 번호를 찾음
            {
                spaceNumber = randomNumber.Next(1, 10);    
            }

            this.spaceNumber.Add(spaceNumber);    //찾은 칸번호를 칸번호 리스트에 저장
            spaceDrawType[spaceNumber] = 'O';     //해당 칸번호에 그림 저장(user : X, computer : O)
        }
    }
}
