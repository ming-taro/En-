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
    class MemberMenu
    {
        public MemberMenu()
        {
            Screen screen = new Screen();
            string[] menu = { "회원가입", "로그인", "종료"};
            screen.PrintMain(menu);
        }
    }
    class AdminSignUp
    {
        public AdminSignUp()
        {
            Screen screen = new Screen();
            screen.PrintSingUp("로그인");
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
