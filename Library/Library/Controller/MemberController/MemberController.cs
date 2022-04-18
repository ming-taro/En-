using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberController
    {
        private string myId;
        public MemberController()
        {
            myId = "";
        }
        
        public void ControlSignIn()
        {
            SignIn signIn = new SignIn();  //회원 로그인 화면으로 이동
            myId = signIn.SignInMember();  //회원 아이디 
        }
        public void SelectMenu1(int menu)
        {
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:  //로그인
                    ControlSignIn();
                    ControlMemberMode(16);  //회원 모드로(1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료)
                    break;
                case (int)Constants.Menu.SECOND:  //회원가입
                    SignUp signUp = new SignUp();
                    signUp.ControlSignUp();
                    break;
            }

            if (menu == (int)Constants.Menu.SECOND) GoBack();  //회원가입 완료 후 뒤로가기 -> 회원메뉴(1.로그인  2.회원가입)로 이동
        }
        public void ControlMemberMode(int maxTop)    //1.회원가입  2.로그인 
        {
            MenuScreen menuScreen = new MenuScreen();
            Keyboard keyboard = new Keyboard();
            keyboard.InitCursorPosition();
            int menu;

            while (Constants.MEMBER_MODE)
            {
                if (maxTop == 14) menuScreen.PrintMemberMenu();//1.로그인  2.회원가입
                else menuScreen.PrintMemberMode();             //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
                
                menu = keyboard.SelectMenu(13, maxTop, 1);     //메뉴선택
                if (menu == Constants.ESCAPE) break;           //메뉴선택 중 뒤로가기 -> 메인화면으로
                menu = keyboard.Top;                      //메뉴의 해당 커서값

                if (maxTop == 14) SelectMenu1(menu);           //1.로그인  2.회원가입
                else SelectMenu2(menu);                        //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
            }
            //회원 모드 종료 -> 메인화면으로 돌아감
        }
        public void SelectMenu2(int menu)
        {
            int function = 0;

            switch (menu)
            {
                case (int)Constants.Menu.FIRST:  //도서검색
                    AdminController adminController = new AdminController();
                    adminController.SelectMenu(menu);
                    break;
                case (int)Constants.Menu.SECOND:  //도서대여
                    BorrowingBook borrowing = new BorrowingBook();
                    function = borrowing.ControlBorrowing(myId);
                    break;
                case (int)Constants.Menu.THIRD:   //도서반납
                    ReturningBook returning = new ReturningBook(myId);
                    function = returning.ControlReturning(myId);
                    break;
                case (int)Constants.Menu.FOURTH:   //개인정보수정
                    EditingProfile editingProfle = new EditingProfile(myId);
                    myId = editingProfle.ControlEditingProfile();  //아이디를 수정했을 수 있음 -> 수정된 아이디를 myId에 저장
                    break;
            }

            //기능을 모두 수행함 : 뒤로가기 입력받음 -> 회원메뉴로 돌아감
            if (menu != 13 && function == Constants.COMPLETE_FUNCTION) GoBack();
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
