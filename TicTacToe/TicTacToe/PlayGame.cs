﻿using System;
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
        public const int WIN = 1;
        public const int WIN_AND_RETRY = 2;
        public const int WIN_AND_CLOSE = 3;
        public const int CONTINUE = 4;

        public PlayGame()
        {
            for(int i=0; i<4; i++)
            {
                player[i] = new Player();    //player생성
            }
            player[0].SetPlayer('X', "User");
            player[1].SetPlayer('O', "Computer");
            player[2].SetPlayer('X', "User1");
            player[3].SetPlayer('O', "User2");
        }

        public void PrintInputScreen(int playerNumber, string message)    //유저가 칸번호를 입력했을 때 오류가 생기면 출력하는 화면
        {
            Console.Clear();
            if (playerNumber == 0 || playerNumber == 1)
            {
                printScreen.PrintPlayScreen(player[0], player[1], board);   //게임플레이화면 출력(User vs Computer)
            }
            else
            {
                printScreen.PrintPlayScreen(player[2], player[3], board);   //게임플레이화면 출력(User1 vs User2)
            }
            Console.WriteLine(message);                 //경고메세지 출력
        }
        public void InputUserSpaceNumber(int playerNumber)  //player(=user)의 좌표입력을 받고 리스트에 저장하는 함수
        {
            bool isValidSpaceNumber = false; //빈칸(유효한)인지 판단하는 변수
            string spaceNumber = "";         //칸번호 입력 변수

            while (!isValidSpaceNumber)
            {
                spaceNumber = Console.ReadLine();     //번호를 입력받음(1~9)
                
                if (!exception.IsValidValue(spaceNumber) || !exception.IsBetween1To9(spaceNumber))   //숫자가 아님 or 1~9가 아닌 입력
                {
                    PrintInputScreen(playerNumber, "-------------1 ~ 9 중 하나를 입력해주세요.------------");  //잘못입력시 게임플레이화면과 함께 경고 메세지 출력
                    continue;                                      //처음으로 돌아가서 칸 번호를 다시 입력받는다
                }
                else if(!board.IsValidSpace(spaceNumber))          //유효한 입력이지만 해당 칸이 빈 칸이 아닌 경우
                {
                    PrintInputScreen(playerNumber, "----------빈칸이 아닙니다. 다시 입력해주세요.---------");  //잘못입력시 게임플레이화면과 함께 경고 메세지 출력
                    continue;                                      //처음으로 돌아가서 칸 번호를 다시 입력받는다
                }
                else   //올바른 좌표를 입력받으면 while문 탈출
                {
                    isValidSpaceNumber = true;     //유효한 칸번호를 입력받았다고 판단
                }
            }
            
            board.SetOneSpace(Convert.ToInt32(spaceNumber) - 1, player[playerNumber].DrawType);   //보드객체에 입력받은 칸 정보 저장
            player[playerNumber].AddMySpaceNumber(Convert.ToInt32(spaceNumber) - 1);              //나의 칸번호 리스트에 입력한 칸번호 추가
        }
        public int LosePlayer(int winPlayer)
        {
            if (winPlayer == 0) return 1;
            else if (winPlayer == 1) return 0;
            else if (winPlayer == 2) return 3;
            else return 2;
        }
        public bool IsRetry(int winPlayer)     //게임을 다시 할지 확인하는 함수
        {
            string input = "";
            int losePlayer = LosePlayer(winPlayer);
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

            player[winPlayer].InitMySpaceNumber();           //player들의 칸번호 리스트 초기화
            player[losePlayer].InitMySpaceNumber();

            if (Convert.ToInt32(input) == 1) return true;    //다시 시작
            else return false;
        }
        
        public void UserVersusComputer()     //모드 : 1번
        {
            bool loop = true;
            int isWinAndRetry = 0;

            while (loop)
            {
                board.InputComputerPoint(player[1]);              //컴퓨터 입력
                isWinAndRetry = IsWinAndRetry(1, 0, 1);
                if (isWinAndRetry == WIN_AND_RETRY) continue;//컴퓨터 승리 + 다시시작
                else if (isWinAndRetry == WIN_AND_CLOSE) return;//컴퓨터 승리 + 종료

                InputUserSpaceNumber(0);                         //유저입력
                isWinAndRetry = IsWinAndRetry(1, 0, 1);
                if (isWinAndRetry == WIN_AND_RETRY) continue;//유저 승리 + 다시시작
                else if (isWinAndRetry == WIN_AND_CLOSE) return;//유저 승리 + 종료
            }
        }
        public int IsWinAndRetry(int winPlayer, int player1, int player2)      //player가 승리하는 경우
        {
            if (!board.CheckWin(player[winPlayer]))
            {
                printScreen.PrintPlayScreen(player[2], player[3], board);      //입력을 반영한 게임 플레이 화면 출력
                return CONTINUE;           //승부가 나지X -> 게임 재개
            }

            player[winPlayer].Score++;                         //승리 체크(스코어 +1)
            printScreen.PrintPlayScreen(player[player1], player[player2], board); //승자의 입력을 반영한 게임화면 출력

            if (IsRetry(winPlayer))                           //player가 승리 + 다시시작
            {
                board.InitBoard();                            //다시 시작한다면 보드판 초기화
                return WIN_AND_RETRY;
            }
            else                                              //player 승리 + 종료
            {
                return WIN_AND_CLOSE;
            }
        }
        public void User1VersusUser2()        //모드 : 2번
        {
            bool loop = true;
            int isWinAndRetry = 0;
            printScreen.PrintPlayScreen(player[2], player[3], board);
            while (loop)
            {
                printScreen.PrintPlayScreen(player[2], player[3], board);          //게임 플레이 화면 출력

                InputUserSpaceNumber(2);                     //유저1입력
                isWinAndRetry = IsWinAndRetry(2, 2, 3);
                if (isWinAndRetry == WIN_AND_RETRY) continue;//유저1 승리 + 다시시작
                else if (isWinAndRetry == WIN_AND_CLOSE) return;//유저1 승리 + 종료

                InputUserSpaceNumber(3);                     //유저2입력
                isWinAndRetry = IsWinAndRetry(3, 2, 3);
                if (isWinAndRetry == WIN_AND_RETRY) continue;//유저2 승리 + 다시시작
                else if (isWinAndRetry == WIN_AND_CLOSE) return;//유저2 승리 + 종료
            }

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

            if(mode == 0)    //게임 종료
            {
                return;
            }
            else if (mode == 1)
            {
                UserVersusComputer();      //유저  vs 컴퓨터 게임모드 실행
                Console.Clear();
                printScreen.PrintEndingScreen(player[0], player[1]);         //게임 종료화면 출력
            }
            else if (mode == 2)
            {
                User1VersusUser2();        //유저1 vs 유저2 게임모드 실행
                Console.Clear();
                printScreen.PrintEndingScreen(player[2], player[3]);            //게임 종료화면 출력
            }
            else    //mode == 3
            {

            }

            
        }
    }
}
