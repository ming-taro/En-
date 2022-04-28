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

        public EditingProfile(BookDAO bookDatabaseManager)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            memberView = new MemberView();
        }
        private string InputProfile(int menu, ref string query, SignUp signUp)  //수정할 회원정보 입력
        {
            string changedItem = "";

            switch (menu)
            {
                case (int)Constants.ProfileMenu.FIRST:    //아이디
                    changedItem = signUp.InputId(10, (int)Constants.ProfileMenu.FIRST);
                    query = "id='" + changedItem + "'";   //회원정보를 수정할 쿼리문
                    break;
                case (int)Constants.ProfileMenu.SECOND:   //비밀번호
                    changedItem = signUp.InputPassword(12, (int)Constants.ProfileMenu.SECOND, Constants.MEMBER_ID_REGEX, Constants.MESSAGE_ABOUT_MEMBER_ID);
                    changedItem = signUp.ReconfirmPassword(19, (int)Constants.ProfileMenu.THIRD, changedItem);  //비밀번호 재확인
                    query = "password='" + changedItem + "'";
                    break;
                case (int)Constants.ProfileMenu.FOURTH:   //이름
                    changedItem = signUp.InputPassword(8, (int)Constants.ProfileMenu.FOURTH, Constants.NAME_REGEX, Constants.MESSAGE_ABOUT_MEMBER_NAME);
                    query = "name='" + changedItem + "'";
                    break;
                case (int)Constants.ProfileMenu.FIFTH:   //나이
                    changedItem = signUp.InputPassword(8, (int)Constants.ProfileMenu.FIFTH, Constants.AGE_REGEX, Constants.MESSAGE_ABOUT_AGE);
                    query = "age='" + changedItem + "'";
                    break;
                case (int)Constants.ProfileMenu.SIXTH:   //휴대전화
                    changedItem = signUp.InputPassword(12, (int)Constants.ProfileMenu.SIXTH, Constants.PHONE_NUMBER_REGEX, Constants.MESSAGE_ABOUT_PHONE_NUMBER);
                    query = "phoneNumber='" + changedItem + "'";
                    break;
                case (int)Constants.ProfileMenu.SEVENTH: //주소
                    changedItem = signUp.InputPassword(8, (int)Constants.ProfileMenu.SEVENTH, Constants.ADDRESS_REGEX, Constants.MESSAGE_ABOUT_ADDRESS);
                    query = "address='" + changedItem + "'";
                    break;
            }

            return changedItem;
        }
        public void EditProfile(MemberVO member, SignUp signUp, Keyboard keyboard)
        {
            string changedItem = "";
            string query = "";
            int menu;

            keyboard.SetPosition(0, (int)Constants.ProfileMenu.FIRST);  //커서 위치 조정

            while (Constants.INPUT_VALUE)
            {
                memberView.PrintProfile(member);       //회원정보수정 화면

                menu = keyboard.SelectMenu((int)Constants.ProfileMenu.FIRST, (int)Constants.ProfileMenu.SEVENTH, (int)Constants.ProfileMenu.STEP);  //수정할 항목 선택
                if (menu == Constants.ESCAPE) return;  //메뉴선택 중 뒤로가기를 누르면 종료
                menu = keyboard.Top;

                changedItem = InputProfile(menu, ref query, signUp);                          //선택한 정보에 해당하는 회원정보 수정
                //if (changedItem != Constants.ESC) library.UpdateMember(memberId, query);  //수정된 정보 DB에 반영(정보입력 중 esc->뒤로가기(수정할 메뉴 선택으로))
                //if (changedItem != Constants.ESC && menu == (int)Constants.ProfileMenu.FIRST) memberId = changedItem;  //방금 수정한 정보가 아이디라면 myId에 변경된 아이디 저장
            }
        }
    }
}
