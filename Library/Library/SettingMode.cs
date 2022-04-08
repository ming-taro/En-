using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    
    class SettingMode
    {

    }
    class MemberMenu   //유저모드 선택 -> 1.회원가입  2.로그인  3.종료
    {
        public MemberMenu()
        {
            Screen screen = new Screen();
            string[] menu = { "회원가입", "로그인", "종료"};  
            screen.PrintMain(menu);                           //회원모드화면 출력
        }
    }
    class SignUp     //1.회원가입
    {
        public SignUp()
        {
            Screen screen = new Screen();
            screen.PrintSingUp();
            Console.SetCursorPosition(8, 5);    //아이디
            Console.ReadLine();
            Console.SetCursorPosition(10, 8);   //비밀번호
            Console.ReadLine();
            Console.SetCursorPosition(17, 11);  //비밀번호 재확인
            Console.ReadLine();
            Console.SetCursorPosition(6, 13);   //이름
            Console.ReadLine();
            Console.SetCursorPosition(6, 16);   //나이
            Console.ReadLine();
            Console.SetCursorPosition(10, 19);  //휴대전화
            Console.ReadLine();
            Console.SetCursorPosition(13, 22);  //도로명 주소
            Console.ReadLine();
        }
    }
    class SignIn
    {
        Value value = new Value();
        public SignIn()
        {
            PrintScreen();
        }
        public void PrintScreen()
        {
            Screen screen = new Screen();
            screen.PrintSingIn("로그인");
        }
        public bool SignInAdmin(ref string id, ref string password)
        {
            Console.SetCursorPosition(8, 5);
            id = Console.ReadLine();     //아이디 입력
            Console.SetCursorPosition(10, 6);
            password = Console.ReadLine();     //비밀번호 입력

            return value.GOING_NEXT;
        }
    }
    class AdminMode
    {
        public AdminMode()
        {
            Screen screen = new Screen();
            string[] menu = { "도서 검색", "도서 등록", "도서 수량 관리", "도서 삭제", "회원정보 관리" };
            screen.PrintMain(menu);
        }
    }
    class SearchingBook
    {
        public SearchingBook(List<BookVO> bookList)   //도서검색화면 출력
        {
            Screen screen = new Screen();
            screen.PrintSearchingBook(bookList);
        }
        public void ControlSearchingBook(List<BookVO> bookList)  //도서검색하기(검색어 입력 -> 목록 출력)
        {
            Screen screen = new Screen();
            TestingLibrary testingLibrary = new TestingLibrary();
            testingLibrary.SetPosition(0, 1);     //커서위치
            bool isSelectMenu = testingLibrary.SelectMenu(1, 3);  //메뉴선택
            int menu = testingLibrary.GetTop();   //메뉴선택 완료(1.도서명  2.출판사  3.저자)

            Console.SetCursorPosition(10, menu);  //커서위치
            string name = Console.ReadLine();     //검색어 입력받기

            screen.PrintSearchingBook(menu, name, bookList);   //검색결과로 나온 책목록 출력
        }
    }
}
