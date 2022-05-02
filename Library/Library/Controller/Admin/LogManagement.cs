using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LogManagement
    {
        private Logo logo;

        public LogManagement(Logo logo)
        {
            this.logo = logo;
        }
        public void SelectMenu(int menu)
        {
            switch (menu)
            {

            }
        }
        public void ManageLog(Keyboard keyboard)
        {
            int menu;

            while (Constants.ADMIN_MODE)                  //로그인 성공
            {
                logo.PrintLogManagement();                //로그 관리 화면 출력
                keyboard.InitCursorPosition();            //커서 위치 조정

                menu = keyboard.SelectMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.FOURTH, (int)Constants.Menu.STEP); //메뉴선택 완료
                if (menu == (int)Constants.Keyboard.ESCAPE) break;  //관리자 메뉴 선택중 esc -> 관리자 모드 종료(메인으로 돌아감)

                menu = keyboard.Top;                      //Enter를 눌렀을 때의 커서값 == 선택한 메뉴 
                SelectMenu(menu);                         //해당 메뉴 기능 실행
            }
        }
    }
}
