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

        }
    }
    class AdminSignIn
    {
        public AdminSignIn()
        {
            Screen screen = new Screen();
            screen.PrintSingIn("로그인");
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
