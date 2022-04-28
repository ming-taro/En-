using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class EditingProfile
    {
        private BookDAO bookDatabaseManager;
        private MemberView memberView;

        public EditingProfile()
        {
            bookDatabaseManager = new BookDAO();
            memberView = new MemberView();
        }
        private string InputProfile(int menu, ref string query)
        {
            SignUp signUp = new SignUp();
            string changedItem = "";

            switch (menu)
            {
                case (int)Constants.ProfileMenu.FIRST:    //아이디 입력
                    changedItem = signUp.InputId(10, (int)Constants.ProfileMenu.FIRST);
                    query = "id='" + changedItem + "'";   //회원정보를 수정할 쿼리문
                    break;
                case (int)Constants.ProfileMenu.SECOND:   //비밀번호 입력
                    changedItem = signUp.InputPassword(12, (int)Constants.ProfileMenu.SECOND, @"^[a-zA-Z0-9]{5,10}$", "(5~10자의 영어, 숫자만 다시 입력해주세요.)         ");
                    signUp.ReconfirmPassword(19, (int)Constants.ProfileMenu.THIRD, changedItem);  //비밀번호 재확인
                    query = "password='" + changedItem + "'";
                    break;
                case (int)Constants.ProfileMenu.FOURTH:   //이름
                    changedItem = signUp.InputPassword(8, (int)Constants.ProfileMenu.FOURTH, @"^[a-zA-Z가-힣]{1,30}$", "(30자 이내의 영어, 한글만 다시 입력해주세요.)             ");
                    query = "name='" + changedItem + "'";
                    break;
                case (int)Constants.ProfileMenu.FIFTH:   //나이
                    changedItem = signUp.InputPassword(8, (int)Constants.ProfileMenu.FIFTH, @"^[1-9]{1}[0-9]{0,1}$", "(1~99세까지 입력 가능합니다.)                ");
                    query = "age='" + changedItem + "'";
                    break;
                case (int)Constants.ProfileMenu.SIXTH:   //휴대전화
                    changedItem = signUp.InputPassword(12, (int)Constants.ProfileMenu.SIXTH, @"010-[0-9]{4}-[0-9]{4}$", "(양식에 맞춰 다시 입력해주세요.(ex: 010-0000-0000))              ");
                    query = "phoneNumber='" + changedItem + "'";
                    break;
                case (int)Constants.ProfileMenu.SEVENTH:   //주소
                    changedItem = signUp.InputPassword(8, (int)Constants.ProfileMenu.SEVENTH, @"[가-힣]+(시|도)\s[가-힣]+(시|군|구)\s[가-힣]+(읍|면|동)", "(양식에 맞춰 입력해주세요.(ex: 서울특별시 광진구 군자동))         ");
                    query = "address='" + changedItem + "'";
                    break;
            }

            return changedItem;
        }
        public string EditProfile(string memberId, Keyboard keyboard)
        {
            string changedItem = "";
            string query = "";
            int menu;

            keyboard.SetPosition(0, (int)Constants.ProfileMenu.FIRST);  //커서 위치 조정

            while (Constants.INPUT_VALUE)
            {
                //memberView.PrintProfile();            //회원정보수정 화면

                menu = keyboard.SelectMenu((int)Constants.ProfileMenu.FIRST, (int)Constants.ProfileMenu.SEVENTH, (int)Constants.ProfileMenu.STEP);
                if (menu == Constants.ESCAPE) return memberId;           //메뉴선택 중 뒤로가기를 누르면 종료(아이디를 수정했다면 수정된 id를 리턴해줘야 한다)
                menu = keyboard.Top;

                changedItem = InputProfile(menu, ref query);                          //선택한 정보에 해당하는 회원정보 수정
                //if (changedItem != Constants.ESC) library.UpdateMember(memberId, query);  //수정된 정보 DB에 반영(정보입력 중 esc->뒤로가기(수정할 메뉴 선택으로))
                if (changedItem != Constants.ESC && menu == (int)Constants.ProfileMenu.FIRST) memberId = changedItem;  //방금 수정한 정보가 아이디라면 myId에 변경된 아이디 저장
            }
        }
    }
}
