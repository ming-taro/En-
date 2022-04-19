using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library
{
    class Library
    {

        private void StartMain(int top)
        {
            switch (top)  //커서의 위치값으로 메뉴를 구분
            {
                case (int)Constants.Menu.FIRST:
                    Member member = new Member(); //1. 회원모드
                    member.StartMemberMode(14);       //회원메뉴 컨트롤로 이동
                    break;
                case (int)Constants.Menu.SECOND:                                    //2. 관리자 모드
                    AdminController adminController = new AdminController();    //관리자 로그인 화면으로 이동
                    adminController.ControlAdminMode();            //관리자 모드로     
                    break;
            }
        }
        public void StartLibrary()  //메인화면
        {
            MenuScreen menuScreen = new MenuScreen();  //메인화면 출력
            menuScreen.PrintMainMenu();

            Keyboard keyboard = new Keyboard(25, 13);
            int key;

            while (Constants.KEYBOARD_OPERATION)
            {
                key = keyboard.ControlKeyboard(1);            //입력받은 키값
                if (keyboard.IsOutOfMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.THIRD)) continue;  //메뉴를 벗어나는 이동은 X
                     //엔터를 누른 시점의 top값 -> 선택한 메뉴를 알 수 있음
                if (key == Constants.ESCAPE || key == Constants.ENTERING_MENU && keyboard.Top == (int)Constants.Menu.THIRD)     //프로그램 종료
                {
                    break;
                }
                else if (key == Constants.ENTERING_MENU)   //메뉴입력 -> 해당 메뉴로 이동
                {
                    StartMain(keyboard.Top);
                    menuScreen.PrintMainMenu();
                }
                
            }

            Console.SetCursorPosition(0, 25);
        }
    }
}
