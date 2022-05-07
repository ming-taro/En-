using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberManagement
    {
        private MemberDAO memberDAO = MemberDAO.GetInstance();
        private MemberVO member;
        private Keyboard keyboard;
        private EnteringText text;
        private Logo logo;
        private MemberView memberView;
        private AdminView adminView;
        private Exception exception;

        private SignIn signIn;                 //로그인
        private SignUp signUp;                 //회원가입
        private BookSearch bookSearch;         //1. 도서검색
        private BookRental bookRental;         //2. 도서대여
        private BookReturn bookReturn;         //3. 도서반납
        private ProfileEdition profileEdition; //4.회원정보 수정
        public MemberManagement(Keyboard keyboard)
        {
            this.keyboard = keyboard;
            text = new EnteringText();
            logo = new Logo();
            memberView = new MemberView(logo);
            adminView = new AdminView();
            exception = new Exception();

            signIn = new SignIn(text, logo);
            signUp = new SignUp(text, logo, memberView);
            bookSearch = new BookSearch(text, adminView, logo);
            bookRental = new BookRental(text, memberView, logo);
            bookReturn = new BookReturn(text, memberView, logo);
            profileEdition = new ProfileEdition(memberView);
        }
        private void SelectMenu(int menu)
        {
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:  //도서검색
                    bookSearch.ManageBookSearch(member.Id, keyboard);
                    break;
                case (int)Constants.Menu.SECOND: //도서대출
                    bookRental.SearchBookToBorrow(member.Id, bookSearch, keyboard);
                    break;
                case (int)Constants.Menu.THIRD:  //도서반납
                    bookReturn.ReturnBook(member.Id, keyboard);
                    break;
                case (int)Constants.Menu.FOURTH: //개인정보수정
                    profileEdition.EditProfile(member, signUp, keyboard);  //아이디를 수정했을 수 있음 -> 수정된 아이디를 myId에 저장
                    break;
            }
        }
        private void SelectMemberMode(int menu)
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
                member = memberDAO.GetMemberAccount(memberId);  //로그인 성공 -> 로그인한 회원VO 생성
                StartMemberMode((int)Constants.Menu.FOURTH);     //회원 모드로(1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료)
            }
        }
        public void StartMemberMode(int maxTop)   
        {
            int menu;

            while (Constants.MEMBER_MODE)
            {
                keyboard.InitCursorPosition();

                if (maxTop == (int)Constants.Menu.SECOND) logo.PrintMemberMenu();//1.로그인  2.회원가입
                else logo.PrintMemberMode();           //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
                
                menu = keyboard.SelectMenu((int)Constants.Menu.FIRST, maxTop, (int)Constants.Menu.STEP);     //메뉴선택
                if (menu == (int)Constants.Keyboard.ESCAPE) break;           //메뉴선택 중 뒤로가기 -> 메인화면으로
                menu = keyboard.Top;                           //메뉴의 해당 커서값

                if (maxTop == (int)Constants.Menu.SECOND) SelectMemberMode(menu);      //1.로그인  2.회원가입
                else SelectMenu(menu);                         //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
            }
            //회원 모드 종료 -> 메인화면으로 돌아감
        }
        
    }
}
