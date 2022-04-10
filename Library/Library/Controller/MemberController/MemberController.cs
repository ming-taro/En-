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
                case 13:  //로그인
                    ControlSignIn();
                    ControlMemberMode(17);  //회원 모드로(1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료)
                    break;
                case 14:  //회원가입(미완)
                    SignUp signUp = new SignUp();
                    break;
                case 15:   //종료(미완)
                    break;
            }
        }
        public void ControlMemberMode(int maxTop)    //1.회원가입  2.로그인  3.종료(미완)
        {
            Screen screen = new Screen();
            StartingLibrary testingLibrary = new StartingLibrary();
            testingLibrary.InitCursorPosition();
            int menu;

            while (Constants.MEMBER_MODE)
            {
                if (maxTop == 15) screen.PrintMemberMenu();//1.로그인  2.회원가입  3.종료
                else screen.PrintMemberMode();             //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
                menu = testingLibrary.SelectMenu(13, maxTop);//메뉴선택
                if (menu == Constants.ESCAPE) break;       //메뉴선택 중 뒤로가기 -> 메인화면으로
                menu = testingLibrary.GetTop();            //메뉴의 해당 커서값
                if (maxTop == 15) SelectMenu1(menu);       //1.로그인  2.회원가입  3.종료
                else SelectMenu2(menu);                    //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
            }
            //회원 모드 종료 -> 메인화면으로 돌아감
        }
        public void SelectMenu2(int menu)
        {
            int function = Constants.COMPLETE_FUNCTION;

            switch (menu)
            {
                case 13:  //도서검색
                    AdminController adminController = new AdminController();
                    adminController.SelectMenu(menu);
                    break;
                case 14:  //도서대여(미완)
                    Borrowing borrowing = new Borrowing();
                    function = borrowing.ControlBorrowing(myId);
                    break;
                case 15:   //도서반납
                    Returning returning = new Returning(myId);
                    function = returning.ControlReturning(myId);
                    
                    break;
                case 16:   //개인정보수정(미완)
                    break;
                case 17:   //종료(미완)
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
