using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{

    class Board
    {
        private List<char> spaceDrawType = new List<char>();    //보드판에 그려진 (X or O)'표시' 저장
        public const bool CHECK_WIN = true;
        public const int WRONG_VALUE = -1;

        public Board()    //생성자
        {
            InitBoard();
        }
        public void InitBoard()            //보드판 초기화 함수
        {
            spaceDrawType.Clear();

            for (int i = 0; i < 9; i++)
            {
                spaceDrawType.Add(' ');    //초기 보드판에는 각 칸마다 공백을 저장한다
            }
        }

        public void SetOneSpace(int spaceNumber, char drawType)   //해당 칸에 x or o를 그리는 함수
        {
            spaceDrawType[spaceNumber] = drawType;                //입력받은 칸 번호에 X or O를 저장
        }
        

        public char GetSpaceDrawType(int spaceNumber)  //해당 칸에 그려진 값 얻기
        {
            return spaceDrawType[spaceNumber];   //입력받은 행, 열에 대한 칸 번호를 찾아 그려진 값 반환
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
                case (2, 0):
                    return 6;
                case (2, 1):
                    return 7;
                default:         //case (2,2)
                    return 8;
            }
        }
        public bool IsValidSpace(string Input)    //해당 칸번호가 비어있는(유효한) 칸번호인지 확인하는 함수
        {
            int spaceNumber = Convert.ToInt32(Input) - 1;   //입력받은 칸 번호

            if (spaceDrawType[spaceNumber] == ' ') return true;     //비어있다면 true
            else return false;                                      //X or O 표시가 있다면 false

        }

        /*public int FindEmptySpace(int space1, int space2, int space3)
        {
            if (spaceDrawType[space1] == ' ' && spaceDrawType[space2] == spaceDrawType[space3]) return space1;
            else if (spaceDrawType[space2] == ' ' && spaceDrawType[space1] == spaceDrawType[space3]) return space2;
            else if (spaceDrawType[space3] == ' ' && spaceDrawType[space1] == spaceDrawType[space2]) return space3;
            else return WRONG_VALUE;
        }*/
        public int FindSpaceToWin()                 //승리할 수 있는 칸의 위치를 찾는 함수
        {
            if (spaceDrawType[2] == ' ') return 2;  //컴퓨터가 입력 세번만에 이길 수 있음
            else if (spaceDrawType[3] == ' ' && spaceDrawType[0] == ' ') return 0;    //컴퓨터의 세번째 입력값 : 이길 수 있는 경우
            else if (spaceDrawType[7] == ' ' && spaceDrawType[8] == ' ') return 8;    //컴퓨터의 세번째 입력값 : 이길 수 있는 경우
            else if (spaceDrawType[3] == ' ') return 3;   //컴퓨터의 네번째 입력값 : 0,3,6 승리
            else if (spaceDrawType[8] == ' ') return 8;   //컴퓨터의 네번째 입력값 : 
            else if (spaceDrawType[0] == ' ') return 0;
            else if (spaceDrawType[7] == ' ') return 7;
            else return WRONG_VALUE;

        }
        public void InputComputerPoint(Player player)
        {
            if (spaceDrawType[6] == ' ')
            {
                spaceDrawType[6] = 'O';        //컴퓨터의 첫 번째 입력
                player.AddMySpaceNumber(6);
                return;
            }
            else if (spaceDrawType[4] == ' ')
            {
                spaceDrawType[4] = '0';        //컴퓨터의 두 번째 입력
                player.AddMySpaceNumber(4);
                return;
            }
            else                               //컴퓨터의 세 번째 입력~
            {
                int spaceNumber = FindSpaceToWin();
                spaceDrawType[spaceNumber] = 'O';
                player.AddMySpaceNumber(spaceNumber);
                return;
            }
        }

        public bool IsSameType(int spaceNumber1, int spaceNumber2, int spaceNumber3) //나란한 세 칸이 같은 타입인지 확인하는 함수
        {
            if (spaceDrawType[spaceNumber1] == spaceDrawType[spaceNumber2] && spaceDrawType[spaceNumber2] == spaceDrawType[spaceNumber3]) return CHECK_WIN;
            else return !CHECK_WIN;
        }

        public bool CheckDiagonal(int spaceNumber)   //대각선방향을 검사해 승리를 확인하는 함수
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

        public bool CheckRow(int spaceNumber)    //가로방향을 검사해 승리를 확인하는 함수
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

        public bool CheckColumn(int spaceNumber)  //세로방향을 검사해 승리를 확인하는 함수
        {
            switch(spaceNumber){
                case 0: case 3: case 6:
                    if (IsSameType(0, 3, 6)) return CHECK_WIN;
                    else return !CHECK_WIN;
                case 1: case 4: case 7:
                    if (IsSameType(1, 4, 7)) return CHECK_WIN;
                    else return !CHECK_WIN;
                default:   //case 2, 5, 8
                    if (IsSameType(2, 5, 8)) return CHECK_WIN;
                    else return !CHECK_WIN;
            }
        }
        public bool CheckWin(Player player)    //player의 칸 리스트를 순회하며 대각선, 가로, 세로의 승리여부를 확인하는 함수
        {
            foreach (int spaceNumber in player.mySpaceNumber)        
            {
                if (CheckDiagonal(spaceNumber)) return CHECK_WIN;    //대각선 승리 확인
                else if (CheckRow(spaceNumber)) return CHECK_WIN;    //가로 승리 확인
                else if (CheckColumn(spaceNumber)) return CHECK_WIN; //세로 승리 확인
            }

            return !CHECK_WIN;          //연달아 3개가 그려지지 않았으므로 false 반환
        }
    }
}
