﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class StartingLibrary
    {
        private int left = 25, top = 13;
        public StartingLibrary()
        {
            AdminVO admin = new AdminVO();  //관리자
        }
        public int GetTop()
        {
            return top;
        }
        public void InitCursorPosition()
        {
            left = 25;
            top = 13;
        }
        public void SetPosition(int left, int top)
        {
            this.left = left;
            this.top = top;
        }
        
        public void ControlMain()
        {
            switch (left, top)  //커서의 위치값으로 메뉴를 구분
            {
                case (25, 13):
                    MemberController memberController = new MemberController(); //1. 회원모드
                    memberController.ControlMemberMode(15);       //회원메뉴 컨트롤로 이동
                    break;
                case (25, 14):                                    //2. 관리자 모드
                    AdminController adminController = new AdminController();
                    adminController.ControlAdminSignIn();         //관리자 로그인 화면으로 이동
                    break;
            }
        }
        public int SelectMenu(int minTop, int maxTop)//메뉴 고르기 -> 고른 메뉴의 커서값을 left,top에 반영
        {
            int entering = Constants.MOVING_CURSOR;     
            int key = Constants.MOVING_CURSOR;
            
            while (entering != Constants.CLOSE_PROGRAM) //키보드 방향키로 움직이는 동안
            {
                Console.SetCursorPosition(left, top);
                key = ControlKeyboard();            //키보드를 입력받음
                if (IsOutOfMenu(minTop, maxTop)) continue;  //메뉴를 벗어나는 이동은 X

                if (key == Constants.ENTERING_MENU || key == Constants.ESCAPE) break;//메뉴입력 -> 해당 메뉴로 이동(1.회원모드   2.관리자모드)
            }

            return key;
        }
        public int ControlKeyboard() //현재 키보드 입력값 반환
        {
            int input = Constants.INVALID_INPUT;
            ConsoleKeyInfo keyInfo;

            while (input == Constants.INVALID_INPUT)   //다른 키를 입력하면 올바른 키를 입력할때까지 무한루프
            {
                keyInfo = Console.ReadKey(true); //키를 입력받음 
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(left, ++top);   //↓방향키 입력
                        return Constants.MOVING_CURSOR;
                    case ConsoleKey.UpArrow:                      //↑방향키 입력
                        Console.SetCursorPosition(left, --top);
                        return Constants.MOVING_CURSOR;
                    case ConsoleKey.Enter:                        //enter 입력
                        return Constants.ENTERING_MENU;
                    case ConsoleKey.Escape:                       //escape 입력
                        return Constants.ESCAPE;
                }
            }
            return Constants.INVALID_INPUT;
        }
        public bool IsOutOfMenu(int minTop, int maxTop)
        {
            if (top == (minTop - 1)) top = minTop;
            else if (top == (maxTop + 1)) top = maxTop;
            else return !Constants.OUT_OF_MENU;

            return Constants.OUT_OF_MENU;
        }
        public void TestLibrary()  //메인화면
        {
            Console.SetWindowSize(61,40);
            string[] menu = { "회원 모드", "관리자 모드", "종료" }; //메인화면 메뉴 
            Screen screen = new Screen();
            screen.PrintMain(menu);

            int entering = Constants.MOVING_CURSOR;
            int key;

            while (entering != Constants.CLOSE_PROGRAM)
            {
                Console.SetCursorPosition(left, top);
                key = ControlKeyboard();            //입력받은 키값
                if (IsOutOfMenu(13, 15)) continue;  //메뉴를 벗어나는 이동은 X

                if (key == Constants.ESCAPE || key == Constants.ENTERING_MENU && top == 15)     //프로그램 종료
                {
                    entering = Constants.CLOSE_PROGRAM;
                }
                else if (key == Constants.ENTERING_MENU)   //메뉴입력 -> 해당 메뉴로 이동
                {  
                    ControlMain();
                    screen.PrintMain(menu);
                } 
                    
            }

            Console.SetCursorPosition(25, 20);
        }
    }
}