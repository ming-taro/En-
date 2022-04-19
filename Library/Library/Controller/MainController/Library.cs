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
                    Admin admin = new Admin();    //관리자 로그인 화면으로 이동
                    admin.StartAdminMode();            //관리자 모드로     
                    break;
            }
        }
        public void StartLibrary()  //메인화면
        {
            MenuScreen menuScreen = new MenuScreen();  //메인화면 출력
            menuScreen.PrintMainMenu();

            Keyboard keyboard = new Keyboard(25, (int)Constants.Menu.FIRST);
            int key;

            while (Constants.KEYBOARD_OPERATION)
            {
                key = keyboard.SelectMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.THIRD, (int)Constants.Menu.STEP);      //입력받은 키값
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
