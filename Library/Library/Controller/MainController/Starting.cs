using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Starting
    {
        public void ControlMain(int top)
        {
            switch (top)  //커서의 위치값으로 메뉴를 구분
            {
                case 13:
                    MemberController memberController = new MemberController(); //1. 회원모드
                    memberController.ControlMemberMode(14);       //회원메뉴 컨트롤로 이동
                    break;
                case 14:                                    //2. 관리자 모드
                    AdminController adminController = new AdminController();    //관리자 로그인 화면으로 이동
                    adminController.ControlAdminMode();            //관리자 모드로     
                    break;
            }
        }
        public void StartLibrary()  //메인화면
        {
            MenuScreen menuScreen = new MenuScreen();  //메인화면 출력
            menuScreen.PrintMainMenu();

            Keyboard keyboard = new Keyboard();
            int key;
            int top = 13;

            while (Constants.KEYBOARD_OPERATION)
            {
                Console.SetCursorPosition(25, top);
                key = keyboard.ControlKeyboard(1);            //입력받은 키값
                if (keyboard.IsOutOfMenu(13, 15)) continue;  //메뉴를 벗어나는 이동은 X
                top = keyboard.GetTop();                     //엔터를 누른 시점의 top값 -> 선택한 메뉴를 알 수 있음
                if (key == Constants.ESCAPE || key == Constants.ENTERING_MENU && top == 15)     //프로그램 종료
                {
                    break;
                }
                else if (key == Constants.ENTERING_MENU)   //메뉴입력 -> 해당 메뉴로 이동
                {
                    ControlMain(keyboard.GetTop());
                    menuScreen.PrintMainMenu();
                }
                
            }

            Console.SetCursorPosition(0, 25);
        }
    }
}
