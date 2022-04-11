using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class SettingMenu
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
    
    class SearchingMember
    {
        public SearchingMember(List<MemberVO> memberList)  //삭제할 회원 아이디 검색 화면 출력
        {
            Screen screen = new Screen();
            screen.PrintSearchingMember(memberList);
        }
        public int ControlSearchingMember(List<MemberVO> memberList)
        {
            Screen screen = new Screen();
            StartingLibrary testingLibrary = new StartingLibrary();
            Console.SetCursorPosition(22, 1);
            string memberId = Console.ReadLine();  //회원아이디 입력받기

            return Constants.COMPLETE_FUNCTION;       //검색결과 출력까지 모두 완료
        }
    }
}
