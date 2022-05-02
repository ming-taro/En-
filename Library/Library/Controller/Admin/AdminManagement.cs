using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminManagement
    {
        private BookDAO bookDAO = BookDAO.GetInstance();
        private Keyboard keyboard;
        private EnteringText text;
        private Logo logo;
        private AdminView adminView;
        private Exception exception;

        private SignIn signIn;                         //로그인
        private BookSearch bookSearch;                 //1. 도서검색
        private BookRegistration bookRegistration;     //2. 도서등록
        private BookEdition bookEdition;               //3. 도서정보수정
        private BookDeletion bookDeletion;             //4. 도서삭제
        private MemberDeletion memberDeletion;         //5. 회원관리
        private NaverSearch naverSearch;               //7. 네이버 도서 검색
        private LogManagement logManagement;           //8. 로그관리
        public AdminManagement(Keyboard keyboard)
        {
            this.keyboard = keyboard;
            text = new EnteringText();
            logo = new Logo();
            adminView = new AdminView();
            exception = new Exception();

            signIn = new SignIn(text, logo);
            bookSearch = new BookSearch(text, adminView, exception);
            bookRegistration = new BookRegistration(text, adminView, exception);
            bookEdition = new BookEdition(text, adminView, exception);
            bookDeletion = new BookDeletion(text, adminView, exception);
            memberDeletion = new MemberDeletion(text, adminView, exception);
            naverSearch = new NaverSearch(text, adminView, exception);
            logManagement = new LogManagement(logo);
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
                    adminView.PrintRentalList(bookDAO.MakeMyBookList(Constants.RENTAL_LIST_INQUIRY, ""));
                    keyboard.PressESC();
                    break;
                case (int)Constants.Menu.SIXTH:   //회원관리
                    memberDeletion.DeleteMember(keyboard);
                    break;
                case (int)Constants.Menu.SEVENTH: //네이버 검색
                    naverSearch.SearchBook(keyboard, bookRegistration);
                    break;
                case (int)Constants.Menu.EIGHT:   //로그관리
                    logManagement.ManageLog(keyboard);
                    break;

            }
        }
        public void StartAdminMode()                      //관리자 모드 관리
        {
            string success;
            int menu;
            
            success = signIn.SignInAdmin();               //관리자 로그인
            if (success == Constants.ESC) return;         //로그인 도중 esc -> 뒤로가기

            while (Constants.ADMIN_MODE)                  //로그인 성공
            {
                logo.PrintAdminMode();                    //관리자 모드 화면 출력
                keyboard.InitCursorPosition();            //커서 위치 조정

                menu = keyboard.SelectMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.EIGHT, (int)Constants.Menu.STEP); //메뉴선택 완료
                if (menu == (int)Constants.Keyboard.ESCAPE) break;  //관리자 메뉴 선택중 esc -> 관리자 모드 종료(메인으로 돌아감)
                
                menu = keyboard.Top;                      //Enter를 눌렀을 때의 커서값 == 선택한 메뉴 
                SelectMenu(menu);                         //해당 메뉴 기능 실행
            }
        }
    }
}
