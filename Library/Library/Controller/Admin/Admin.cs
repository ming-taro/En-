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
        private Logo logo;
        private SignIn signIn;                         //로그인
        private SearchingBook searchingBook;           //1. 도서검색
        private RegisteringBook registeringBook;       //2. 도서등록
        private EditingBook editingBook;               //3. 도서정보수정
        private DeletingBook deletingBook;             //4. 도서삭제
        private DeletingMember deletingMember;         //5. 회원관리

        public Admin(Keyboard keyboard)
        {
            bookDatabaseManager = new BookDAO();
            this.keyboard = keyboard;
            logo = new Logo();

            signIn = new SignIn(bookDatabaseManager);                
            searchingBook = new SearchingBook(bookDatabaseManager);
            registeringBook = new RegisteringBook(bookDatabaseManager);
            editingBook = new EditingBook(bookDatabaseManager);
            deletingBook = new DeletingBook(bookDatabaseManager);
            deletingMember = new DeletingMember(bookDatabaseManager);
        }
        public void StartAdminMode()                      //관리자 모드 관리
        {
            string success;
            int menu;
            string[] textOfMemberMode = { "도서 검색", "도서 등록", "도서정보 수정", "도서 삭제", "회원정보 관리" };

            success = signIn.SignInAdmin();               //관리자 로그인
            if (success == Constants.ESC) return;         //로그인 도중 esc -> 뒤로가기

            while (Constants.ADMIN_MODE)                  //로그인 성공
            {
                logo.PrintMain(textOfMemberMode);         //관리자 모드 화면 출력
                keyboard.InitCursorPosition();            //커서 위치 조정

                menu = keyboard.SelectMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.FIFTH, (int)Constants.Menu.STEP); //메뉴선택 완료
                if (menu == (int)Constants.Keyboard.ESCAPE) break;  //관리자 메뉴 선택중 esc -> 관리자 모드 종료(메인으로 돌아감)
                
                menu = keyboard.Top;                      //Enter를 눌렀을 때의 커서값 == 선택한 메뉴 
                SelectMenu(menu);                         //해당 메뉴 기능 실행
            }
        }
        
        public void SelectMenu(int menu)       //관리자 메뉴에서 선택
        {
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:  //도서 이름 검색 
                    searchingBook.ShowSearchResult(keyboard);
                    break;
                case (int)Constants.Menu.SECOND:  //도서 등록
                    registeringBook.StartRegistration(keyboard);
                    break;
                case (int)Constants.Menu.THIRD:   //도서 정보 수정
                    editingBook.EditBook(searchingBook, registeringBook, keyboard);
                    break;
                case (int)Constants.Menu.FOURTH:  //도서 삭제
                    deletingBook.DeleteBook(searchingBook, keyboard);
                    break;
                case (int)Constants.Menu.FIFTH:   //회원관리
                    deletingMember.ControlDeletingMember(keyboard);
                    break;

            }
        }
    }
}
