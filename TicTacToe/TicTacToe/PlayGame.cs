﻿using System;
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
        PrintScreen printScreen = new PrintScreen();

        public void InputUserPoint(Player player)       //player(=user)의 좌표입력을 받고 리스트에 저장하는 함수
        {
            Console.WriteLine("\n>>>>>>>>>>>>>>>>>>좌표를 입력해주세요<<<<<<<<<<<<<<<<<");
            Console.Write(player.Name + ">행 : ");
            string input = Console.ReadLine();      //행 입력
            int row = Convert.ToInt32(input);
            Console.Write(player.Name + ">열 : ");
            input = Console.ReadLine();             //열 입력
            int column = Convert.ToInt32(input);

            board.SetOneSpace(row, column, player.DrawType);                //보드객체에 입력받은 칸 정보 저장
            player.AddSpaceNumber(board.FindSpaceNumber(row, column));      //player객체의 칸 번호 리스트에 입력한 칸 번호 저장
        }
        
        public bool IsRetry(string name)     //게임을 다시 할지 확인하는 함수
        {
            printScreen.PrnitRetry(name);                             //다시 묻는 화면 출력
            Console.Write(">입력 : ");
            int retry = Convert.ToInt32(Console.ReadLine());          //게임을 다시 할지 입력받음

            player1.InitMySpaceNumber();     //player들의 칸번호 리스트 초기화
            player2.InitMySpaceNumber();

            if (retry == 1) return true;
            else return false;
        }
        public void UserVersusComputer()
        {
            bool loop = true;

            player1.SetPlayer('X', "User");
            player2.SetPlayer('O', "Computer");

            while (loop)
            {
                printScreen.PrintPlayScreen(1, board);          //게임 플레이 화면 출력

                InputUserPoint(player1);                        //유저입력
                if (board.CheckWin(player1))                    //유저가 승리하는 경우
                {                  
                    board.SetScore(0);                          //유저 승리 체크(유저 스코어 +1)
                    printScreen.PrintPlayScreen(1, board);      //유저입력을 반영한 게임화면 출력
                    if (IsRetry("User")) board.InitBoard();     //다시 시작한다면 보드판 초기화
                    else return;                                //게임 종료
                }

                board.FindRandomValidSpace(player2);            //컴퓨터 입력
                if (board.CheckWin(player2))                    //컴퓨터가 승리하는 경우
                {
                    board.SetScore(1);                          //컴퓨터 승리 체크(컴퓨터 스코어 +1)
                    printScreen.PrintPlayScreen(1, board);      //컴퓨터입력을 반영한 게임화면 출력
                    if (IsRetry("Computer")) board.InitBoard(); //다시 시작한다면 보드판 초기화
                    else return;                                //게임종료
                }
            }
        }
        public void User1VersusUser2()
        {
            bool loop = true;

            player1.SetPlayer('X', "User1");
            player2.SetPlayer('O', "User2");

            while (loop)
            {
                printScreen.PrintPlayScreen(2, board);          //게임 플레이 화면 출력

                InputUserPoint(player1);                        //유저1입력
                if (board.CheckWin(player1))                    //유저1이 승리하는 경우
                {
                    board.SetScore(0);                          //유저1의 승리 체크(유저 스코어 +1)
                    printScreen.PrintPlayScreen(2, board);      //유저1의 입력을 반영한 게임화면 출력
                    if (IsRetry("User1"))
                    {
                        board.InitBoard();                      //다시 시작한다면 보드판 초기화
                        printScreen.PrintPlayScreen(2, board);  //게임 플레이 화면 출력
                    }
                    else return;                                //게임 종료
                }
                else
                {
                    printScreen.PrintPlayScreen(2, board);      //게임 플레이 화면 출력
                }

                InputUserPoint(player2);                        //유저2입력
                if (board.CheckWin(player2))                    //유저2가 승리하는 경우
                {
                    board.SetScore(1);                          //유저2 승리 체크(유저 스코어 +1)
                    printScreen.PrintPlayScreen(2, board);      //유저2 입력을 반영한 게임화면 출력
                    if (IsRetry("User2")) board.InitBoard();    //다시 시작한다면 보드판 초기화
                    else return;                                //게임 종료
                }
            }

        }

        public void PrintWinner()
        {
            int player1Score = board.GetScore(0);
            int player2Score = board.GetScore(1);

            if (player1Score > player2Score) printScreen.PrintWinner(player1.Name);
            else if (player1Score < player2Score) printScreen.PrintWinner(player2.Name);
            else printScreen.PrintDraw();

        }
        public void StartGame()
        {
            PrintScreen printScreen = new PrintScreen();
            printScreen.PrintMainScreen();                       //게임 메인 화면 출력

            int mode = Convert.ToInt32(Console.ReadLine());      //모드를 입력받으면 메인화면을 지움 -> 모드화면 출력

            if (mode == 1) UserVersusComputer();      //유저  vs 컴퓨터 게임모드 실행
            else if (mode == 2) User1VersusUser2();   //유저1 vs 유저2 게임모드 실행

            PrintWinner();
        }


    }
}
