using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminController
    {
        public void ControlAdminSignIn()  //관리자 로그인 관리
        {
            string id = "";
            string password = "";
            SignIn admin = new SignIn();  //2. 관리자모드(->관리자 로그인 화면으로 이동)
            bool input = Constants.WRONG_VALUE;

            while (input == Constants.WRONG_VALUE)
            {
                admin.SignInAdmin(ref id, ref password);   //로그인
                if (id == "12345" && password == "00000")
                {
                    ControlAdminMode();  //관리자 모드로
                    input = Constants.RIGHT_VALUE;
                }
                else
                {
                    admin.PrintScreen();//로그인 실패시 다시 입력(+오류 메세지)
                    Console.WriteLine("\n\n"+ Constants.SIGN_IN_ERROR);
                }
            }
        }
        public void ControlAdminMode()     //관리자 모드 관리
        {
            Screen screen = new Screen();
            StartingLibrary testingLibrary = new StartingLibrary();
            int menu;

            while (Constants.ADMIN_MODE)
            {
                screen.PrintAdminMode();       //관리자 모드 출력
                testingLibrary.InitCursorPosition();
                menu = testingLibrary.SelectMenu(13, 17); //메뉴선택 완료
                if (menu == Constants.ESCAPE) break;          //관리자 메뉴 선택중 esc -> 관리자 모드 종료
                menu = testingLibrary.GetTop();//메뉴의 해당 커서값
                SelectMenu(menu);              //해당 메뉴 기능 실행
            }
            //관리자 모드 종료 -> 메인화면으로 돌아감
        }
        
        public void SelectMenu(int menu) //관리자 메뉴에서 선택
        {
            int function = Constants.COMPLETE_FUNCTION;
            MemberController memberController = new MemberController();

            switch (menu)
            {
                case 13:  //도서 이름 검색
                    SearchingBook searchingBook = new SearchingBook();
                    function = searchingBook.ControlSearchingBook();
                    break;
                case 14:  //도서 등록(미완)
                    RegisteringBook registeringBook = new RegisteringBook();
                    function = registeringBook.ControlRegistering();
                    break;
                case 15:  //도서 수량 관리(미완)
                    break;
                case 16:  //도서 삭제(미완)
                    break;
                case 17:  //회원관리(미완 -> 회원출력만 가능)
                    SearchingMember searchingMember = new SearchingMember(memberController.memberList);
                    function = searchingMember.ControlSearchingMember(memberController.memberList);
                    break;

            }

            //기능을 모두 수행함 : 뒤로가기 입력받음 -> 관리자메뉴로 돌아감
            if (function == Constants.COMPLETE_FUNCTION) GoBack();
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
