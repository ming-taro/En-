using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Admin
    {
        public void StartAdminMode()     //관리자 모드 관리
        {
            SignIn signIn = new SignIn();
            string success = signIn.SignInAdmin();        //관리자 로그인
            if (success == Constants.ESC) return;         //로그인 도중 esc -> 뒤로가기

            MenuScreen menuScreen = new MenuScreen();
            Keyboard keyboard = new Keyboard();
            int menu;

            while (Constants.ADMIN_MODE)
            {
                menuScreen.PrintAdminMode();
                keyboard.InitCursorPosition();
                menu = keyboard.SelectMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.FIFTH, (int)Constants.Menu.STEP); //메뉴선택 완료
                if (menu == Constants.ESCAPE) break;      //관리자 메뉴 선택중 esc -> 관리자 모드 종료(메인으로 돌아감)
                menu = keyboard.Top;           //메뉴의 해당 커서값
                SelectMenu(menu);                         //해당 메뉴 기능 실행
            }
        }
        
        public void SelectMenu(int menu) //관리자 메뉴에서 선택
        {
            int function = 0;
            
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:  //도서 이름 검색 
                    SearchingBook searchingBook = new SearchingBook();
                    searchingBook.ShowSearchResult();
                    break;
                case (int)Constants.Menu.SECOND:  //도서 등록
                    RegisteringBook registeringBook = new RegisteringBook();
                    registeringBook.StartRegistration();
                    break;
                case (int)Constants.Menu.THIRD:  //도서 수량 관리
                    EditingBook editingBook = new EditingBook();
                    function = editingBook.ControlEditingBook();
                    break;
                case (int)Constants.Menu.FOURTH:  //도서 삭제
                    DeletingBook deletingBook = new DeletingBook();
                    deletingBook.DeleteBook();
                    break;
                case (int)Constants.Menu.FIFTH:  //회원관리
                    DeletingMember deletingMember = new DeletingMember();
                    function = deletingMember.ControlDeletingMember();
                    break;

            }

            //기능을 모두 수행함 : 뒤로가기 입력받음 -> 관리자메뉴로 돌아감
            if (function == Constants.COMPLETE_FUNCTION) GoBack(); ///---->
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
