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
        private MemberVO member;
        private BookDAO bookDatabaseManager;
        private Keyboard keyboard;
        private Menu menuScreen;
        private SignIn signIn;                //로그인
        private SignUp signUp;                //회원가입
        private SearchingBook searchingBook;  //1. 도서검색
        private BorrowingBook borrowingBook;  //2. 도서대여
        private ReturningBook returningBook;  //3. 도서반납
        private EditingProfile editingProfile;//4.회원정보 수정
        public Member(Keyboard keyboard)
        {
            bookDatabaseManager = new BookDAO();
            this.keyboard = keyboard;
            menuScreen = new Menu();
            signIn = new SignIn(bookDatabaseManager);
            signUp = new SignUp(bookDatabaseManager);
            searchingBook = new SearchingBook();
            borrowingBook = new BorrowingBook(bookDatabaseManager);
            returningBook = new ReturningBook(bookDatabaseManager);
            editingProfile = new EditingProfile(bookDatabaseManager);
        }
        public void SelectMenu(int menu)
        {
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:  //도서검색
                    searchingBook.ShowSearchResult(keyboard);
                    break;
                case (int)Constants.Menu.SECOND: //도서대여
                    borrowingBook.SearchBookToBorrow(member.Id, searchingBook, keyboard);
                    break;
                case (int)Constants.Menu.THIRD:  //도서반납
                    returningBook.ShowMyBookList(member.Id, keyboard);
                    break;
                case (int)Constants.Menu.FOURTH: //개인정보수정
                    editingProfile.EditProfile(member, signUp, keyboard);  //아이디를 수정했을 수 있음 -> 수정된 아이디를 myId에 저장
                    break;
            }
        }
        public void SelectMemberMode(int menu)
        {
            string memberId = "";

            switch (menu)
            {
                case (int)Constants.Menu.FIRST:         //로그인
                    memberId = signIn.SignInToLibrary();//로그인한 회원의 아이디
                    break;
                case (int)Constants.Menu.SECOND:        //회원가입
                    signUp.SignUpForLibrary(keyboard);
                    return;
            }

            if (memberId.Equals(Constants.ESC))
            {
                return;//로그인 중 esc -> 뒤로가기
            }
            else
            {
                member = bookDatabaseManager.GetMemberAccount(memberId);  //로그인 성공 -> 로그인한 회원VO 생성
                StartMemberMode((int)Constants.Menu.FOURTH);     //회원 모드로(1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료)
            }
        }
        public void StartMemberMode(int maxTop)   
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
        
    }
}
