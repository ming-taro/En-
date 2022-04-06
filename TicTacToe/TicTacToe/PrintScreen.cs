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
            Console.Clear();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>TicTacToe 게임<<<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("======================모드 선택=======================\n\n");
            Console.WriteLine("              1. User    vs    Computer               \n");
            Console.WriteLine("              2. User1   vs    User2                  \n");
            Console.WriteLine("              3.     ScoreBoard\n");
            Console.WriteLine("              4.        종료\n\n\n");
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
                    Console.WriteLine("\n            "+ player1.Name + "(X): " + player1.Score + "   vs   Computer(O): " + player2.Score + "\n");
                    break;
                default:
                    Console.WriteLine("\n           " + player2.Name + "(X): " + player1.Score + "   vs   User2(O): " + player2.Score + "\n");
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

        public void PrintEndingScreen(Player player1, Player player2)       //최종스코어를 기준으로 승자를 출력하는 함수
        {
            int player1Score = player1.Score;
            int player2Score = player1.Score;

            Console.Clear();
            if (player1Score > player2Score) PrintWinner(player1.Name);           //player1이 승리하는 경우
            else if (player1Score < player2Score) PrintWinner(player2.Name);      //player2가 승리하는 경우
            else PrintDraw();                                                     //비기는 경우

        }

        public void PrintScoreBoardScreen(Player[] player)
        {
            Console.Clear();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>ScoreBoard<<<<<<<<<<<<<<<<<<<<<<\n\n\n");
            PrintScoreBoard(player[0], player[1]);
            Console.WriteLine("\n");
            PrintScoreBoard(player[2], player[3]);
            Console.WriteLine("\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("========== 1. 메인화면으로 돌아가기   2. 종료 =========\n");
        }
        public void PrintErrorScreenForInput1To9(int playerNumber, Player []player, Board board)    //유저가 칸번호를 입력했을 때 오류가 생기면 출력하는 화면
        {
            Console.Clear();
            if (playerNumber == 0 || playerNumber == 1)
            {
                PrintPlayScreen(player[0], player[1], board);   //게임플레이화면 출력(User vs Computer)
                Console.WriteLine("-------------1 ~ 9 중 하나를 입력해주세요.------------");  //잘못입력시 게임플레이화면과 함께 경고 메세지 출력
            }
            else
            {
                PrintPlayScreen(player[2], player[3], board);   //게임플레이화면 출력(User1 vs User2)
                Console.WriteLine("-------------1 ~ 9 중 하나를 입력해주세요.------------");      //잘못입력시 게임플레이화면과 함께 경고 메세지 출력
            }
        }
        public void PrintErrorScreenForInputInvalidSpace(int playerNumber, Player[] player, Board board) //유저가 칸번호를 입력했을 때 오류가 생기면 출력하는 화면
        {
            Console.Clear();
            if (playerNumber == 0 || playerNumber == 1)
            {
                PrintPlayScreen(player[0], player[1], board);   //게임플레이화면 출력(User vs Computer)
                Console.WriteLine("----------빈칸이 아닙니다. 다시 입력해주세요.---------");  //잘못입력시 게임플레이화면과 함께 경고 메세지 출력
            }
            else
            {
                PrintPlayScreen(player[2], player[3], board);   //게임플레이화면 출력(User1 vs User2)
                Console.WriteLine("----------빈칸이 아닙니다. 다시 입력해주세요.---------");  //잘못입력시 게임플레이화면과 함께 경고 메세지 출력
            }
        }
        public void PrintErrorScreenForInput1To2(int winPlayer, Player[] player, Board board)
        {
            Console.Clear();
            if (winPlayer == 0 || winPlayer == 1)
            {
                PrintPlayScreen(player[0], player[1], board);   //게임플레이화면 출력(User vs Computer)
            }
            else
            {
                PrintPlayScreen(player[2], player[3], board);   //게임플레이화면 출력(User1 vs User2)
            }
            Console.WriteLine("-------------1과 2중 하나를 입력해주세요--------------\n");  //경고메세지 출력
            PrnitRetry(player[winPlayer].Name);                                             //다시 묻는 화면 출력
        }
        public void PrintErrorScreenForInput1To4()
        {
            Console.Clear();
            PrintMainScreen();                                                              //게임 메인 화면 출력
            Console.WriteLine("-------------1 ~ 4중 하나를 입력해주세요--------------\n");  //경고메세지 출력
        }
        public void PrintErrorScreenForInputInScoreBoard(Player[] player)
        {
            Console.Clear();
            PrintScoreBoardScreen(player);
            Console.WriteLine("-------------1과 2중 하나를 입력해주세요--------------\n");  //경고메세지 출력
        }
        
        
    }
}
