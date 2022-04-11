using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MenuScreen
    {
        LogoScreen logoScreen;
        public MenuScreen()
        {
            logoScreen = new LogoScreen();
        }
        public void PrintMainMenu()
        {
            Console.SetWindowSize(61, 40);
            string[] menu = { "회원 모드", "관리자 모드", "종료" };   //메인화면 메뉴 
            logoScreen.PrintMain(menu);
        }
        public void PrintMemberMenu()
        {
            
            string[] menu = { "로그인", "회원가입" };
            logoScreen.PrintMain(menu);
        }
        public void PrintMemberMode()
        {
            string[] menu = { "도서 검색", "도서 대여", "도서 반납", "개인 정보 수정" };
            logoScreen.PrintMain(menu);
        }
        public void PrintAdminMode()
        {
            string[] menu = { "도서 검색", "도서 등록", "도서 수량 관리", "도서 삭제", "회원정보 관리" };
            logoScreen.PrintMain(menu);
        }
    }
}
