using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class PlayGame  
    {
        Player player1 = new Player();
        Player player2 = new Player();
        Board board = new Board();

        public void InputUserPoint(Player player)       //player(=user)의 좌표입력을 받고 리스트에 저장하는 함수
        {
            Console.WriteLine(">>>>>>>>좌표를 입력해주세요<<<<<<<<");
            Console.Write(">행 : ");
            string input = Console.ReadLine();      //행 입력
            int row = Convert.ToInt32(input);
            Console.Write(">열 : ");
            input = Console.ReadLine();             //열 입력
            int column = Convert.ToInt32(input);

            board.SetOneSpace(row, column, player.DrawType);                       //보드객체에 입력받은 칸 정보 저장
            player.AddSpaceNumber(board.FindSpaceNumber(row, column));      //player객체의 칸 번호 리스트에 입력한 칸 번호 저장
        }

        public void UserVersusComputer()
        {
            bool loop = true;
            
            player1.DrawType = 'X';    //user
            player2.DrawType = 'O';    //computer

            while (loop)
            {
                InputUserPoint(player1);            //유저 입력
                if (board.CheckWin(player1))
                {
                    board.SetScore(0);     //유저 승리 체크(유저 스코어 +1)
                }
                board.FindRandomValidSpace();       //컴퓨터 입력
                
            }


        }

        public void StartGame()
        {
            PrintScreen printScreen = new PrintScreen();
            printScreen.PrintMainScreen();                       //게임 메인 화면 출력

            int mode = Convert.ToInt32(Console.ReadLine());      //모드를 입력받으면 메인화면을 지움 -> 모드화면 출력
            Console.Clear();
            printScreen.PrintModeScreen(mode);                   //모드화면 출력
            printScreen.PrintScoreBoard(mode, 0, 0);             //스코어보드 출력
            printScreen.PrintBoardScreen(board);                 //보드판 출력

            bool loop = true;

            UserVersusComputer();      //유저 vs 컴퓨터 게임모드 실행
        }


    }
}
