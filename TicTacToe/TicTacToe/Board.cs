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
        private List<int> validSpaceNumber = new List<int>();        //비어있는 '칸 번호'를 저장하는 리스트
        private List<int> score = new List<int>();              //점수 리스트
        public const bool CHECK_WIN = true;

        public Board()    //생성자
        {
            InitBoard();
            score.Add(0); //점수는 모두 0으로 초기화
            score.Add(0);
        }
        public void InitBoard()            //보드판 초기화 함수
        {
            spaceDrawType.Clear();
            validSpaceNumber.Clear();

            for (int i = 0; i < 9; i++)
            {
                spaceDrawType.Add(' ');    //초기 보드판에는 각 칸마다 공백을 저장한다
                validSpaceNumber.Add(i);        //비어있는 칸번호 리스트(0~8번 : 총 9칸)
            }
        }

        public void SetScore(int scoreIndex)
        {
            score[scoreIndex]++;
        }
        public int GetScore(int scoreIndex)
        {
            return score[scoreIndex];
        }

        public void SetOneSpace(int row, int column, char drawType)   //해당 칸에 x or o를 그리는 함수
        {
            spaceDrawType[FindSpaceNumber(row, column)] = drawType;   //입력받은 행, 열에 대한 칸 번호를 찾아 X or O를 저장
            RemoveSpaceNumberList(FindSpaceNumber(row, column));      //칸리스트에서 해당 칸번호 삭제
        }
        

        public char GetSpaceDrawType(int spaceNumber)  //해당 칸에 그려진 값 얻기
        {
            return spaceDrawType[spaceNumber];   //입력받은 행, 열에 대한 칸 번호를 찾아 그려진 값 반환
        }

        public void RemoveSpaceNumberList(int spaceNumber)  //그림이 그려진 칸 번호를 리스트에서 삭제하는 함수
        {
            validSpaceNumber.Remove(spaceNumber);   //해당 칸 번호를 리스트에서 삭제
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

        public void FindRandomValidSpace(Player player)   //빈 칸(유효한) 중 랜덤한 칸 번호를 찾아 그리는 함수(컴퓨터가 사용)
        {
            Random randomNumber = new Random();
            int spaceNumberIndex = randomNumber.Next(0, validSpaceNumber.Count);  //빈칸번호를 저장한 리스트의 랜덤한 인덱스 번호 추출 
            validSpaceNumber.RemoveAt(spaceNumberIndex); //컴퓨터가 입력할 칸번호를 빈칸번호 리스트에서 삭제
            
            int spaceNumber = validSpaceNumber[spaceNumberIndex];    //빈칸번호 리스트에서 삭제한 인덱스번호에 해당하는 빈칸번호
            spaceDrawType[spaceNumber] = 'O';     //해당 칸번호에 그림 저장(user : X, computer : O)
            player.AddSpaceNumber(spaceNumber);   //컴퓨터객체에서 나의 칸번호 리스트에 칸번호 추가
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
