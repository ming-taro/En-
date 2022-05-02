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
        private Keyboard keyboard;
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private Logo logo;
        private AdminView adminView;
        private Exception exception;

        private SignIn signIn;                         //로그인
        private BookSearch bookSearch;           //1. 도서검색
        private BookRegistration bookRegistration;       //2. 도서등록
        private BookEdition bookEdition;               //3. 도서정보수정
        private BookDeletion bookDeletion;             //4. 도서삭제
        private MemberDeletion memberDeletion;         //5. 회원관리

        public Admin(Keyboard keyboard)
        {
            this.keyboard = keyboard;
            bookDatabaseManager = new BookDAO();
            text = new EnteringText();
            logo = new Logo();
            adminView = new AdminView();
            exception = new Exception();

            signIn = new SignIn(bookDatabaseManager, text, logo);
            bookSearch = new BookSearch(bookDatabaseManager, text, adminView, exception);
            bookRegistration = new BookRegistration(bookDatabaseManager, text, adminView, exception);
            bookEdition = new BookEdition(bookDatabaseManager, text, adminView, exception);
            bookDeletion = new BookDeletion(bookDatabaseManager, text, adminView, exception);
            memberDeletion = new MemberDeletion(bookDatabaseManager, text, adminView, exception);
        }
        private void SelectMenu(int menu)       //관리자 메뉴에서 선택
        {
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:  //도서 이름 검색 
                    bookSearch.ShowSearchResult(keyboard);
                    break;
                case (int)Constants.Menu.SECOND:  //도서 등록
                    bookRegistration.StartRegistration(keyboard);
                    break;
                case (int)Constants.Menu.THIRD:   //도서 정보 수정
                    bookEdition.EditBook(bookSearch, bookRegistration, keyboard);
                    break;
                case (int)Constants.Menu.FOURTH:  //도서 삭제
                    bookDeletion.DeleteBook(bookSearch, keyboard);
                    break;
                case (int)Constants.Menu.FIFTH:   //도서 대출 현황
                    adminView.PrintRentalList(bookDatabaseManager.MakeMyBookList(Constants.RENTAL_LIST_INQUIRY, ""));
                    keyboard.PressESC();
                    break;
                case (int)Constants.Menu.SIXTH:   //회원관리
                    memberDeletion.DeleteMember(keyboard);
                    break;

            }
        }
        public void StartAdminMode()                      //관리자 모드 관리
        {
            string success;
            int menu;
            string[] textOfMemberMode = { "도서 검색", "도서 등록", "도서 정보 수정", "도서 삭제", "도서 대출 현황", "회원 정보 관리" };

            success = signIn.SignInAdmin();               //관리자 로그인
            if (success == Constants.ESC) return;         //로그인 도중 esc -> 뒤로가기

            while (Constants.ADMIN_MODE)                  //로그인 성공
            {
                logo.PrintMain(textOfMemberMode);         //관리자 모드 화면 출력
                keyboard.InitCursorPosition();            //커서 위치 조정

                menu = keyboard.SelectMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.SIXTH, (int)Constants.Menu.STEP); //메뉴선택 완료
                if (menu == (int)Constants.Keyboard.ESCAPE) break;  //관리자 메뉴 선택중 esc -> 관리자 모드 종료(메인으로 돌아감)
                
                menu = keyboard.Top;                      //Enter를 눌렀을 때의 커서값 == 선택한 메뉴 
                SelectMenu(menu);                         //해당 메뉴 기능 실행
            }
        }
    }
}
