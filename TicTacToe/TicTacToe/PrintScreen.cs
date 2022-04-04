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
            Console.WriteLine("======================모드 선택=======================\n\n");
            Console.WriteLine("              1. User    vs    Computer               \n");
            Console.WriteLine("              2. User1   vs    User2                  \n\n");
            Console.WriteLine("======================================================");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
        }

        public void PrintModeScreen(int mode)
        {
            switch (mode)
            {
                case 1:
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>User  vs  Computer<<<<<<<<<<<<<<<<<<");
                    break;
                case 2:
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>User1  vs  User2<<<<<<<<<<<<<<<<<<<");
                    break;
            }
        }

        public void PrintScoreBoard(int mode, int player1Score, int player2Score)
        {
            switch (mode)
            {
                case 1:
                    Console.WriteLine("\n           User(X): " + player1Score + "   vs   Computer(O): " + player2Score + "\n");
                    break;
                case 2:
                    Console.WriteLine("\n            User1(X): " + player1Score + "   vs   User2(O): " + player2Score + "\n");
                    break;
            }
        }

        public void PrintBoardScreen(Board board)   //보드판을 출력하는 함수
        {
            Console.WriteLine("\n                    0   1   2");
            Console.WriteLine("                  ┏━━━┳━━━┳━━━┓");
            Console.WriteLine("                 0┃ " + board.GetSpaceDrawType(0) + " ┃ " + board.GetSpaceDrawType(1) + " ┃ " + board.GetSpaceDrawType(2) + " ┃");
            Console.WriteLine("                  ┣━━━╋━━━╋━━━┫ ");
            Console.WriteLine("                 1┃ " + board.GetSpaceDrawType(3) + " ┃ " + board.GetSpaceDrawType(4) + " ┃ " + board.GetSpaceDrawType(5) + " ┃");
            Console.WriteLine("                  ┣━━━╋━━━╋━━━┫ ");
            Console.WriteLine("                 2┃ " + board.GetSpaceDrawType(6) + " ┃ " + board.GetSpaceDrawType(7) + " ┃ " + board.GetSpaceDrawType(8) + " ┃");
            Console.WriteLine("                  ┗━━━┻━━━┻━━━┛");
        }

        public void PrintWinner(string name)
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>게임을 종료합니다<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("======================================================\n");
            Console.WriteLine("                 [" + name + " is the winner!]\n");
            Console.WriteLine("======================================================");
        }

        public void PrintDraw()
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>게임을 종료합니다<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("======================================================\n");
            Console.WriteLine("                        [Draw]\n");
            Console.WriteLine("======================================================");
        }

        public void PrnitRetry(string name)
        {
            if (name.Equals("Computer")) Console.WriteLine("\n-------Computer의 승리입니다. 다시하시겠습니까?-------");
            else if (name.Equals("User")) Console.WriteLine("\n---------User의 승리입니다. 다시하시겠습니까?---------");
            else Console.WriteLine("\n---------" + name + "의 승리입니다. 다시하시겠습니까?--------");
            Console.WriteLine("\n              1. 다시 시작        2. 종료             ");
            Console.WriteLine("------------------------------------------------------");
        }

        public void PrintPlayScreen(int mode, Board board)     //게임진행화면을 출력하는 함수
        {
            Console.Clear();
            PrintModeScreen(mode);                                        //모드 출력
            PrintScoreBoard(mode, board.GetScore(0), board.GetScore(1));  //스코어보드 출력
            PrintBoardScreen(board);                                      //보드판 출력

        }

        
    }
}
