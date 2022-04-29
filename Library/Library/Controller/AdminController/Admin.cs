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
        private BookDAO bookDatabaseManager;
        private Keyboard keyboard;
        private Menu menuScreen;
        private SignIn signIn;                         //로그인
        private SearchingBook searchingBook;           //1. 도서검색
        private RegisteringBook registeringBook;       //2. 도서등록
        private EditingBook editingBook;               //3. 도서정보수정
        private DeletingBook deletingBook;             //4. 도서삭제
        private DeletingMember deletingMember;         //5. 회원관리

        public Admin(Keyboard keyboard, Menu menuScreen)
        {
            bookDatabaseManager = new BookDAO();
            this.keyboard = keyboard;
            this.menuScreen = menuScreen;

            signIn = new SignIn(bookDatabaseManager);                
            searchingBook = new SearchingBook(bookDatabaseManager);
            registeringBook = new RegisteringBook();
            editingBook = new EditingBook();
            deletingBook = new DeletingBook(bookDatabaseManager);
            deletingMember = new DeletingMember(bookDatabaseManager);
        }
        public void StartAdminMode()                      //관리자 모드 관리
        {
            string success;
            int menu;

            success = signIn.SignInAdmin();               //관리자 로그인
            if (success == Constants.ESC) return;         //로그인 도중 esc -> 뒤로가기

            while (Constants.ADMIN_MODE)                  //로그인 성공
            {
                menuScreen.PrintAdminMode();              //관리자 모드 화면 출력
                keyboard.InitCursorPosition();            //커서 위치 조정

                menu = keyboard.SelectMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.FIFTH, (int)Constants.Menu.STEP); //메뉴선택 완료
                if (menu == (int)Constants.Keyboard.ESCAPE) break;  //관리자 메뉴 선택중 esc -> 관리자 모드 종료(메인으로 돌아감)
                
                menu = keyboard.Top;                      //Enter를 눌렀을 때의 커서값 == 선택한 메뉴 
                SelectMenu(menu);                         //해당 메뉴 기능 실행
            }
        }
        
        public void SelectMenu(int menu)       //관리자 메뉴에서 선택
        {
            int function = 0;  //----->삭제할 코드
            
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:  //도서 이름 검색 
                    searchingBook.ShowSearchResult(keyboard);
                    break;
                case (int)Constants.Menu.SECOND:  //도서 등록
                    registeringBook.StartRegistration();
                    break;
                case (int)Constants.Menu.THIRD:  //도서 정보 수정
                    function = editingBook.ControlEditingBook();
                    break;
                case (int)Constants.Menu.FOURTH:  //도서 삭제
                    deletingBook.DeleteBook(searchingBook, keyboard);
                    break;
                case (int)Constants.Menu.FIFTH:  //회원관리
                    deletingMember.ControlDeletingMember(keyboard);
                    break;

            }

            //기능을 모두 수행함 : 뒤로가기 입력받음 -> 관리자메뉴로 돌아감
            if (function == Constants.COMPLETE_FUNCTION) GoBack(); ///---->삭제할 코드
        }
        public void GoBack()  //뒤로가기--->삭제할 코드
        {
            while (Constants.INPUT_VALUE)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape) break;
            }   //escape키 입력 -> 뒤로가기
        }
    }
}
