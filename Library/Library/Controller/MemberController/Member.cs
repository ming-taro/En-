using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Member
    {
        private string myId;                  //현재 로그인한 회원 아이디
        private Keyboard keyboard;
        private Menu menuScreen;
        private SignIn signIn;                //로그인
        private SearchingBook searchingBook;  //1. 도서검색
        private BorrowingBook borrowingBook;  //2. 도서대여
        private ReturningBook returningBook;  //3. 도서반납
        public Member(Keyboard keyboard)
        {
            this.keyboard = keyboard;
            menuScreen = new Menu();
            signIn = new SignIn();
            searchingBook = new SearchingBook();
            borrowingBook = new BorrowingBook();
            returningBook = new ReturningBook();
        }
        public void SelectMemberMode(int menu)
        {
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:   //로그인
                    myId = signIn.SignInMember(); //회원 아이디
                    break;
                case (int)Constants.Menu.SECOND:  //회원가입
                    SignUp signUp = new SignUp();
                    signUp.ControlSignUp();
                    break;
            }

            if (myId.Equals(Constants.ESC)) return;   //로그인 중 esc -> 뒤로가기
            else StartMemberMode(16);  //회원 모드로(1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료)
        }
        public void StartMemberMode(int maxTop)    //1.회원가입  2.로그인 
        {
            int menu;

            while (Constants.MEMBER_MODE)
            {
                keyboard.InitCursorPosition();

                if (maxTop == (int)Constants.Menu.SECOND) menuScreen.PrintMemberMenu();//1.로그인  2.회원가입
                else menuScreen.PrintMemberMode();             //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
                
                menu = keyboard.SelectMenu(13, maxTop, 1);     //메뉴선택
                if (menu == (int)Constants.Keyboard.ESCAPE) break;           //메뉴선택 중 뒤로가기 -> 메인화면으로
                menu = keyboard.Top;                           //메뉴의 해당 커서값

                if (maxTop == (int)Constants.Menu.SECOND) SelectMemberMode(menu);      //1.로그인  2.회원가입
                else SelectMenu(menu);                         //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
            }
            //회원 모드 종료 -> 메인화면으로 돌아감
        }
        public void SelectMenu(int menu)
        {
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:  //도서검색
                    searchingBook.ShowSearchResult(keyboard);
                    break;
                case (int)Constants.Menu.SECOND: //도서대여
                    borrowingBook.SearchBookToBorrow(myId, searchingBook, keyboard);
                    break;
                case (int)Constants.Menu.THIRD:  //도서반납
                    returningBook.ShowMyBookList(myId, keyboard);
                    break;
                case (int)Constants.Menu.FOURTH: //개인정보수정
                    EditingProfile editingProfile = new EditingProfile(myId);
                    myId = editingProfile.EditProfile();  //아이디를 수정했을 수 있음 -> 수정된 아이디를 myId에 저장
                    break;
            }
        }
    }
}
