using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class ComputerInput
    {
        public void FirstSpace(Player computer, Board board)     //컴퓨터의 첫 입력
        {
            board.SetOneSpace(6, 'O');   //첫 입력은 무조건 모퉁이
            computer.AddMySpaceNumber(6);
        }
        public void SecondSpace(Player computer, Board board)
        {
            //상대가 중앙과 맞닿은 칸에 O를 그릴 경우
            if(board.GetSpaceDrawType(1) == 'X' || board.GetSpaceDrawType(3) == 'X' || board.GetSpaceDrawType(5) == 'X' || board.GetSpaceDrawType(7) == 'X')
            {
                switch()
            }
        }
        public void SpaceToCompleteTree(Board board)
        {



        }

        public void InputComputer(Player computer, Board board)
        {
            if (computer.mySpaceNumber.Count == 0) FirstSpace(computer, board);   //첫 입력

        }

    }
}
