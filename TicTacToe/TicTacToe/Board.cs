using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{

    class Board
    {
        
        private List<char> spaceDrawType = new List<char>();    //보드판에 그려진 (X or O)표시 저장
        private List<int> spaceNumber = new List<int>();        //X or O표시가 있는 칸 번호를 저장하는 리스트
        public const bool CHECK_WIN = true;

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

        public bool IsSameType(int spaceNumber1, int spaceNumber2, int spaceNumber3) //나란한 세 칸이 같은 타입인지 확인하는 함수
        {
            if (spaceDrawType[spaceNumber1] == spaceDrawType[spaceNumber2] && spaceDrawType[spaceNumber2] == spaceDrawType[spaceNumber3]) return CHECK_WIN;
            else return !CHECK_WIN;
        }
        public bool CheckDiagonal(int spaceNumber)   //대각선상 연달아 3개가 그려져있는지 체크하는 함수
        {
            switch (spaceNumber)   //보드판에서 0, 2, 4, 6, 8번에 그려져있을때만 대각선을 검사한다
            {
                case 0: case 8:
                    if (IsSameType(0, 4, 8)) return CHECK_WIN;
                    else return !CHECK_WIN;
                case 2: case 6:
                    if (IsSameType(2, 4, 6)) return CHECK_WIN;
                    else return !CHECK_WIN;
                case 4:
                    if (IsSameType(0, 4, 8) || IsSameType(2, 4, 6)) return CHECK_WIN;
                    else return !CHECK_WIN;
                default:            //대각선상에 그림을 그린적이 없는 경우 false 반환
                    return !CHECK_WIN;
            }
        }

        public bool CheckRow(int spaceNumber)
        {
            switch (spaceNumber)
            {
                case 0: case 1: case 2:
                    if (IsSameType(0, 1, 2)) return CHECK_WIN;
                    else return !CHECK_WIN;
                case 3: case 4: case 5:
                    if (IsSameType(3, 4, 5)) return CHECK_WIN;
                    else return !CHECK_WIN;
                default:   //case 6, 7, 8
                    if (IsSameType(6, 7, 8)) return CHECK_WIN;
                    else return !CHECK_WIN;
            }
        }
        public bool CheckWin(Player player)
        {
            foreach (int spaceNumber in player.mySpaceNumber)
            {
                if (CheckDiagonal(spaceNumber)) return CHECK_WIN;    //대각선 승리 확인
                else if (CheckRow(spaceNumber)) return CHECK_WIN;    //가로 승리 확인
            }
        }
    }
}
