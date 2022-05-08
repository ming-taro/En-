using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library
{
    class LibraryMenu
    {
        Keyboard keyboard;
        Logo logo;
        MemberManagement member;
        AdminManagement admin;

        public LibraryMenu()
        {
            keyboard = new Keyboard();
            logo = new Logo();
            member = new MemberManagement(keyboard);
            admin = new AdminManagement(keyboard);
        }
        private void StartMain(int top)
        {
            switch (top)  //커서의 위치값으로 메뉴를 구분
            {
                case (int)Constants.Menu.FIRST:
                    member.StartMemberMode((int)Constants.Menu.SECOND);       //회원메뉴 선택화면으로 이동
                    break;
                case (int)Constants.Menu.SECOND:                              
                    admin.StartAdminMode();           //관리자 모드로 이동    
                    break;
            }
        }
        public void StartLibrary()  //메인화면
        {
            int key;

            logo.PrintStartScreen();
            keyboard.InitCursorPosition();  //커서 위치 조정

            while (Constants.KEYBOARD_OPERATION)
            {
                key = keyboard.SelectMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.THIRD, (int)Constants.Menu.STEP);      //입력받은 키값
                
                if (key == (int)Constants.Keyboard.ENTER && keyboard.Top == (int)Constants.Menu.THIRD)     //프로그램 종료
                {
                    break;
                }
                else if (key == (int)Constants.Keyboard.ENTER)   //메뉴입력 -> 해당 메뉴로 이동
                {
                    StartMain(keyboard.Top);
                    logo.PrintStartScreen();
                    keyboard.InitCursorPosition();  //커서 위치 조정
                }

            }

            Console.SetCursorPosition(0, 25);
        }
    }
}
