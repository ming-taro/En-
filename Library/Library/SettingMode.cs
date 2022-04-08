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
        public SignIn()
        {
            Screen screen = new Screen();
            screen.PrintSingIn("로그인");
            Console.SetCursorPosition(8, 5);
            Console.ReadLine();     //아이디 입력
            Console.SetCursorPosition(10, 6);
            Console.ReadLine();     //비밀번호 입력
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
}
