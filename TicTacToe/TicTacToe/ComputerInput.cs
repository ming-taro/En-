using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class ComputerInput
    {
        public const int INVALID_VALUE = -1;
        public void SetComputerSpace(int space, Player computer, Board board)
        {
            board.SetOneSpace(space, 'O');
            computer.AddMySpaceNumber(space);
        }
        public int FindSecondSpace(Player computer, Board board)
        {
            //상대가 중앙과 맞닿은 칸에 O를 그릴 경우 -> 한 칸(비어있는 칸이어야 함) 떨어진 곳에 X 그리기
            if (board.GetSpaceDrawType(1) == 'X') return 0;
            else if (board.GetSpaceDrawType(3) == 'X') return 8;
            else if (board.GetSpaceDrawType(5) == 'X') return 8;
            else if (board.GetSpaceDrawType(7) == 'X') return 0;
            else return INVALID_VALUE;
        }
        public int FindThirdSpace(Player computer, Board board)
        {
            if (board.GetSpaceDrawType(4) >= '1' && board.GetSpaceDrawType(4) <= '9') return 4; //중앙이 비어 있다면 X 그리기
            else return -1;
        }
        public void InputComputer(Player computer, Board board)
        {
            int space = 6; //첫 입력은 무조건 모퉁이
            int winSpace = -1;

            if(computer.mySpaceNumber.Count >= 2) winSpace = board.FindToWinSpace(computer); //세 번째 입력부터 바로 이길 수 있는 칸이 생길 수 있음

            if (computer.mySpaceNumber.Count == 1)
            {
                space = FindSecondSpace(computer, board);  //두 번째 입력
            }
            else if(winSpace != -1)                        //세 번째 입력 -> 이길 수 있는 위치를 찾았을 때
            {
                space = winSpace;
            }
            else if (computer.mySpaceNumber.Count == 2)    //세 번째 입력
            {
                space = FindThirdSpace(computer, board);
            }
            else if(winSpace != -1)                        //네 번째 입력 -> 이길 수 있는 위치를 찾았을 때
            {
                space = winSpace;
            }

            SetComputerSpace(space, computer, board);

        }

    }
}
