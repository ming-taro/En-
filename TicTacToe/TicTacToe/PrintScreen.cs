using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class PrintScreen
    {
        public void PrintMainScreen()     //게임 시작화면을 출력하는 함수
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>TicTacToe 게임<<<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("======================모드 선택=======================\n\n\n\n");
            Console.WriteLine("              1. User    vs    Computer               \n");
            Console.WriteLine("              2. User1   vs    User2                  \n\n\n\n");
            Console.WriteLine("======================================================");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
        }

        public void PrintModeScreen(int mode)   //모드 화면을 출력하는 함수
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
         
        public void PrintScoreBoard(Player player1, Player player2)    //스코어보드를 출력하는 함수
        {
            switch (player1.Name)
            {
                case "User" :
                    Console.WriteLine("\n           "+ player1.Name + "(X): " + player1.Score + "   vs   Computer(O): " + player2.Score + "\n");
                    break;
                default:
                    Console.WriteLine("\n            " + player2.Name + "(X): " + player1.Score + "   vs   User2(O): " + player2.Score + "\n");
                    break;
            }
        }

        public void PrintBoardScreen(Board board)   //보드판을 출력하는 함수
        {
            Console.WriteLine("\n                             ");
            Console.WriteLine("                  ┏━━━┳━━━┳━━━┓");
            Console.WriteLine("                  ┃ " + board.GetSpaceDrawType(0) + " ┃ " + board.GetSpaceDrawType(1) + " ┃ " + board.GetSpaceDrawType(2) + " ┃");
            Console.WriteLine("                  ┣━━━╋━━━╋━━━┫ ");
            Console.WriteLine("                  ┃ " + board.GetSpaceDrawType(3) + " ┃ " + board.GetSpaceDrawType(4) + " ┃ " + board.GetSpaceDrawType(5) + " ┃");
            Console.WriteLine("                  ┣━━━╋━━━╋━━━┫ ");
            Console.WriteLine("                  ┃ " + board.GetSpaceDrawType(6) + " ┃ " + board.GetSpaceDrawType(7) + " ┃ " + board.GetSpaceDrawType(8) + " ┃");
            Console.WriteLine("                  ┗━━━┻━━━┻━━━┛");
        }

        public void PrintWinner(string name)       //엔딩화면을 출력하는 함수 - 1
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>게임을 종료합니다<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("======================================================\n\n\n\n\n");
            Console.WriteLine("                 [" + name + " is the winner!]\n\n\n\n\n");
            Console.WriteLine("======================================================");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
        }

        public void PrintDraw()                    //엔딩화면을 출력하는 함수 - 2
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>게임을 종료합니다<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("======================================================\n\n\n\n\n\n");
            Console.WriteLine("                        [Draw]\n\n\n\n\n\n");
            Console.WriteLine("======================================================");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
        }

        public void PrnitRetry(string name)        //재시도 화면을 출력하는 함수
        {
            if (name.Equals("Computer")) Console.WriteLine("\n-------Computer의 승리입니다. 다시하시겠습니까?-------");
            else if (name.Equals("User")) Console.WriteLine("\n---------User의 승리입니다. 다시하시겠습니까?---------");
            else Console.WriteLine("\n---------" + name + "의 승리입니다. 다시하시겠습니까?--------");
            Console.WriteLine("\n              1. 다시 시작        2. 종료             ");
            Console.WriteLine("------------------------------------------------------");
        }

        public void PrintPlayScreen(Player player1, Player player2, Board board)       //게임진행화면을 출력하는 함수
        {
            Console.Clear();
            if (player1.Name.Equals("User")) PrintModeScreen(1);          //모드 출력
            else PrintModeScreen(2);
            PrintScoreBoard(player1, player2);                            //스코어보드 출력
            PrintBoardScreen(board);                                      //보드판 출력
            Console.WriteLine("\n>>>>>>>>>>>>>>>>>>번호를 입력해주세요<<<<<<<<<<<<<<<<<");
        }
    }
}
