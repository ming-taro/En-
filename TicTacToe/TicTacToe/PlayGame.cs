using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class PlayGame  
    {
        Player[] player = new Player[4];
        Board board = new Board();
        PrintScreen printScreen = new PrintScreen();
        ExceptionHandling exception = new ExceptionHandling();

        public PlayGame()
        {
            for(int i=0; i<4; i++)
            {
                player[i] = new Player();    //player생성
            }
        }

        public void PrintInputScreen(int mode, string message)    //유저가 칸번호를 입력했을 때 오류가 생기면 출력하는 화면
        {
            Console.Clear();
            if (mode == 1)
            {
                printScreen.PrintPlayScreen(player[0], player[1], board);   //게임플레이화면 출력(User vs Computer)
            }
            else
            {
                printScreen.PrintPlayScreen(player[2], player[3], board);   //게임플레이화면 출력(User1 vs User2)
            }
            Console.WriteLine(message);                 //경고메세지 출력
        }
        public void InputUserSpaceNumber(int mode, char drawType)  //player(=user)의 좌표입력을 받고 리스트에 저장하는 함수
        {
            bool isValidSpaceNumber = false; //빈칸(유효한)인지 판단하는 변수
            string spaceNumber = "";        //칸번호 입력 변수

            while (!isValidSpaceNumber)
            {
                spaceNumber = Console.ReadLine();     //번호를 입력받음(1~9)
                
                if (!exception.IsValidValue(spaceNumber))                 //숫자가 아닌 입력
                {
                    PrintInputScreen(mode, "-------------1 ~ 9 중 하나를 입력해주세요.------------");  //잘못입력시 게임플레이화면과 함께 경고 메세지 출력
                    continue;                                      //처음으로 돌아가서 칸 번호를 다시 입력받는다
                }
                else if(!board.IsValidSpace(spaceNumber))                //유효한 입력이지만 해당 칸이 빈 칸이 아닌 경우
                {
                    PrintInputScreen(mode, "----------빈칸이 아닙니다. 다시 입력해주세요.---------");  //잘못입력시 게임플레이화면과 함께 경고 메세지 출력
                    continue;                                      //처음으로 돌아가서 칸 번호를 다시 입력받는다
                }
                else   //올바른 좌표를 입력받으면 while문 탈출
                {
                    isValidSpaceNumber = true;     //유효한 칸번호를 입력받았다고 판단
                }
            }
            
            board.SetOneSpace(Convert.ToInt32(spaceNumber), drawType);     //보드객체에 입력받은 칸 정보 저장
        }
        
        public bool IsRetry(int winPlayer, int losePlayer)     //게임을 다시 할지 확인하는 함수
        {
            string input = "";
            int retry = 0;
            bool loop = true;

            printScreen.PrnitRetry(player[winPlayer].Name);                             //다시 묻는 화면 출력

            while (loop)
            {
                Console.Write(">입력 : ");
                input = Console.ReadLine();
                if (!exception.IsValidValue(input) || !exception.IsNumber1Or2(input))    //유효하지 않은 입력 or 1,2가 아닌 경우
                {
                    Console.Clear();
                    printScreen.PrintPlayScreen(player[winPlayer], player[losePlayer], board);            //게임 플레이화면 출력
                    printScreen.PrnitRetry(player[winPlayer].Name);                        //다시 묻는 화면 출력
                    Console.WriteLine("-------------1과 2중 하나를 입력해주세요--------------\n");  //경고메세지 출력
                    continue;                                 //처음으로 돌아가서 다시 입력받음
                }
                else
                {
                    loop = false;
                }
            }

            retry = Convert.ToInt32(input);  //게임을 다시 할지를 저장한 변수

            player[winPlayer].InitMySpaceNumber();     //player들의 칸번호 리스트 초기화
            player[losePlayer].InitMySpaceNumber();

            if (retry == 1) return true;
            else return false;
        }
        
        public void UserVersusComputer()     //모드 : 1번
        {
            bool loop = true;

            player[0].SetPlayer('X', "User");
            player[1].SetPlayer('O', "Computer");

            while (loop)
            {
                board.InputComputerPoint(player[1]);              //컴퓨터 입력
                if (board.CheckWin(player[1]))                    //컴퓨터가 승리하는 경우
                {
                    player[1].Score++;                            //컴퓨터 승리 체크(컴퓨터 스코어 +1)
                    printScreen.PrintPlayScreen(player[0], player[1], board);      //컴퓨터입력을 반영한 게임화면 출력
                    if (IsRetry(1, 0)) board.InitBoard(); //다시 시작한다면 보드판 초기화
                    else return;                                //게임종료
                }
                printScreen.PrintPlayScreen(player[0], player[1], board);          //게임 플레이 화면 출력

                InputUserSpaceNumber(1, player[0].DrawType);      //유저입력
                if (board.CheckWin(player[0]))                    //유저가 승리하는 경우
                {
                    player[0].Score++;                            //유저 승리 체크(유저 스코어 +1)
                    printScreen.PrintPlayScreen(player[0], player[1], board);      //유저입력을 반영한 게임화면 출력
                    if (IsRetry(0, 1)) board.InitBoard();       //다시 시작한다면 보드판 초기화
                    else return;                                //게임 종료
                }
            }
        }
        /*public bool IsWinAndRetry(Player player, int mode, int scoreNumber)    //player가 승리하는 경우
        {
            board.SetScore(scoreNumber);               //player 승리 체크(player 스코어 +1)
            printScreen.PrintPlayScreen(mode, board);  //player입력을 반영한 게임화면 출력
            if (IsRetry(player.Name, mode))            //
            {
                board.InitBoard();                     //다시 시작한다면 보드판 초기화
                return true;
            }
            else return false;                         //게임은 이겼지만 다시 시작X

        }*/
        public void User1VersusUser2()        //모드 : 2번
        {
            bool loop = true;

            player[2].SetPlayer('X', "User1");
            player[3].SetPlayer('O', "User2");

            while (loop)
            {
                printScreen.PrintPlayScreen(player[2], player[3], board);          //게임 플레이 화면 출력

                InputUserSpaceNumber(2 , 'X');                     //유저1입력
                if (board.CheckWin(player[2]))                    //유저1이 승리하는 경우
                {
                    player[2].Score++;                          //유저1의 승리 체크(유저 스코어 +1)
                    printScreen.PrintPlayScreen(player[2], player[3], board);      //유저1의 입력을 반영한 게임화면 출력
                    if (IsRetry(2, 3))
                    {
                        board.InitBoard();                      //다시 시작한다면 보드판 초기화
                    }
                    else return;                                //게임 종료
                }
                printScreen.PrintPlayScreen(player[2], player[3], board);          //게임 플레이 화면 출력

                InputUserSpaceNumber(3, 'O');                     //유저2입력
                if (board.CheckWin(player[3]))                    //유저2가 승리하는 경우
                {
                    player[3].Score++;                          //유저2 승리 체크(유저 스코어 +1)
                    printScreen.PrintPlayScreen(player[2], player[3], board);      //유저2 입력을 반영한 게임화면 출력
                    if (IsRetry(3, 2)) board.InitBoard(); //다시 시작한다면 보드판 초기화
                    else return;                                //게임 종료
                }
            }

        }
        public void PrintWinner(int player1, int player2)       //최종스코어를 기준으로 승자를 출력하는 함수
        {
            int player1Score = player[player1].Score;
            int player2Score = player[player2].Score;

            if (player1Score > player2Score) printScreen.PrintWinner(player[player1].Name);           //player1이 승리하는 경우
            else if (player1Score < player2Score) printScreen.PrintWinner(player[player2].Name);      //player2가 승리하는 경우
            else printScreen.PrintDraw();                                                     //비기는 경우

        }
        public int InputMode() //모드를 입력받는 함수
        {
            bool loop = true;
            string input = "";

            while (loop)
            {
                Console.Write(">모드를 입력해주세요 : ");
                input = Console.ReadLine();                   //모드를 입력받음
                if (!exception.IsValidValue(input) || !exception.IsNumber1Or2(input))    //유효하지 않은 입력 or 1,2가 아닌 경우
                {
                    Console.Clear();
                    printScreen.PrintMainScreen();            //게임 메인 화면 출력
                    Console.WriteLine("-------------1과 2중 하나를 입력해주세요--------------\n");  //경고메세지 출력
                    continue;                                 //처음으로 돌아가서 다시 입력받음
                }
                else  //1 or 2중 하나가 입력되는 경우(올바른 입력)
                {
                    loop = false;                                                                                                                                                                                       
                }
            }

            return Convert.ToInt32(input);   //올바르게 입력받은 모드값 반환
        }
        public void StartGame()
        {
            printScreen.PrintMainScreen();            //게임 메인 화면 출력
            int mode = InputMode();                   //모드를 입력받음

            if (mode == 1)
            {
                UserVersusComputer();      //유저  vs 컴퓨터 게임모드 실행
                Console.Clear();
                PrintWinner(0, 1);         //게임 종료화면 출력
            }
            else if (mode == 2)
            {
                User1VersusUser2();        //유저1 vs 유저2 게임모드 실행
                Console.Clear();
                PrintWinner(2, 3);             //게임 종료화면 출력
            }

            
        }
    }
}
