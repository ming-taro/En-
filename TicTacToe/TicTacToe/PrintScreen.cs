using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class PrintScreen
    {
        public void PrintMainScreen()
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>TicTacToe 게임<<<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("======================모드 선택=======================\n");
            Console.WriteLine("              1. User    vs    Computer               \n");
            Console.WriteLine("              2. User1   vs    User2                  \n");
            Console.WriteLine("======================================================\n");
            Console.Write(">모드를 입력해주세요 : "); 
        }

        public void PrintModeScreen(int mode)
        {
            switch (mode)
            {
                case 1:
                    Console.WriteLine("==================User  vs  Computer==================");
                    break;
                case 2:
                    Console.WriteLine("===================User1  vs  User2===================");
                    break;
            }
        }

        public void PrintScoreBoard(int mode, int player1Score, int player2Score)
        {
            switch (mode)
            {
                case 1:
                    Console.WriteLine("\n             User : " + player1Score + "   vs   Computer : " + player2Score + "\n");
                    break;
                case 2:
                    Console.WriteLine("\n              User1 : " + player1Score + "   vs   User2 : " + player2Score + "\n");
                    break;
            }
        }

        public void PrintBoardScreen(Board board)   //보드판을 출력하는 함수
        {
            Console.WriteLine("                  ┏━━━┳━━━┳━━━┓");
            Console.WriteLine("                  ┃ " + board.GetSpaceDrawType(0) + " ┃ " + board.GetSpaceDrawType(1) + " ┃ " + board.GetSpaceDrawType(2) + " ┃");
            Console.WriteLine("                  ┣━━━╋━━━╋━━━┫ ");
            Console.WriteLine("                  ┃ " + board.GetSpaceDrawType(3) + " ┃ " + board.GetSpaceDrawType(4) + " ┃ " + board.GetSpaceDrawType(5) + " ┃");
            Console.WriteLine("                  ┣━━━╋━━━╋━━━┫ ");
            Console.WriteLine("                  ┃ " + board.GetSpaceDrawType(6) + " ┃ " + board.GetSpaceDrawType(7) + " ┃ " + board.GetSpaceDrawType(8) + " ┃");
            Console.WriteLine("                  ┗━━━┻━━━┻━━━┛");
        }

        public void PrintPlayScreen(int mode, Board board)     //게임진행화면을 출력하는 함수
        {
            Console.Clear();
            PrintModeScreen(mode);                               //모드 출력
            PrintScoreBoard(mode, board.GetScore(0), board.GetScore(1));             //스코어보드 출력
            PrintBoardScreen(board);                 //보드판 출력

        }
    }
}
