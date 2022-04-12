using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminController
    {
        public void ControlAdminMode()     //관리자 모드 관리
        {
            SignIn signIn = new SignIn();
            signIn.SignInAdmin();           //관리자 로그인

            MenuScreen menuScreen = new MenuScreen();
            Keyboard keyboard = new Keyboard();
            int menu;

            while (Constants.ADMIN_MODE)
            {
                menuScreen.PrintAdminMode();
                keyboard.InitCursorPosition();
                menu = keyboard.SelectMenu(13, 17, 1); //메뉴선택 완료
                if (menu == Constants.ESCAPE) break;      //관리자 메뉴 선택중 esc -> 관리자 모드 종료(메인으로 돌아감)
                menu = keyboard.GetTop();           //메뉴의 해당 커서값
                SelectMenu(menu);                         //해당 메뉴 기능 실행
            }
        }
        
        public void SelectMenu(int menu) //관리자 메뉴에서 선택
        {
            int function = Constants.COMPLETE_FUNCTION;
            
            switch (menu)
            {
                case 13:  //도서 이름 검색
                    SearchingBook searchingBook = new SearchingBook();
                    function = searchingBook.ControlSearchingBook();
                    break;
                case 14:  //도서 등록
                    Registering registering = new Registering();
                    function = registering.ControlRegistering();
                    break;
                case 15:  //도서 수량 관리(미완)
                    break;
                case 16:  //도서 삭제(미완)
                    break;
                case 17:  //회원관리
                    DeletingMember deletingMember = new DeletingMember();
                    function = deletingMember.ControlDeletingMember();
                    break;

            }

            //기능을 모두 수행함 : 뒤로가기 입력받음 -> 관리자메뉴로 돌아감
            if (function == Constants.COMPLETE_FUNCTION) GoBack();
        }
        public void GoBack()  //뒤로가기
        {
            while (Constants.INPUT_VALUE)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape) break;
            }   //escape키 입력 -> 뒤로가기
        }
    }
}
