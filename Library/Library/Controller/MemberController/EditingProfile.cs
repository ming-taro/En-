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
        private void ReflectChangedInformation(int menu, string changedItem, MemberVO member)   //수정된 정보 반영
        {
            bookDatabaseManager.UpdateToMember(menu, member.Id, changedItem);   //DB에 수정된 회원정보 업데이트

            switch (menu)                                 //MemberVO에 수정된 회원정보 업데이트
            {
                case (int)Constants.ProfileMenu.FIRST:    //아이디
                    member.Id = changedItem;
                    break;
                case (int)Constants.ProfileMenu.SECOND:   //비밀번호
                    member.Password = changedItem;
                    break;
                case (int)Constants.ProfileMenu.FOURTH:   //이름
                    member.Name = changedItem;
                    break;
                case (int)Constants.ProfileMenu.FIFTH:   //나이
                    member.Age = changedItem;
                    break;
                case (int)Constants.ProfileMenu.SIXTH:   //휴대전화
                    member.PhoneNumber = changedItem;
                    break;
                case (int)Constants.ProfileMenu.SEVENTH: //주소
                    member.Address = changedItem;
                    break;
            }
        }
        private string InputProfile(int menu, SignUp signUp)  //수정할 회원정보 입력
        {
            string changedItem = Constants.ESC;

            switch (menu)
            {
                case (int)Constants.ProfileMenu.FIRST:    //아이디
                    changedItem = signUp.InputId(10, (int)Constants.ProfileMenu.FIRST);
                    break;
                case (int)Constants.ProfileMenu.SECOND:   //비밀번호
                    changedItem = signUp.InputPassword(12, (int)Constants.ProfileMenu.SECOND, Constants.MEMBER_ID_REGEX, Constants.MESSAGE_ABOUT_MEMBER_ID);
                    changedItem = signUp.ReconfirmPassword(19, (int)Constants.ProfileMenu.THIRD, changedItem);  //비밀번호 재확인
                    break;
                case (int)Constants.ProfileMenu.FOURTH:   //이름
                    changedItem = signUp.InputPassword(8, (int)Constants.ProfileMenu.FOURTH, Constants.NAME_REGEX, Constants.MESSAGE_ABOUT_MEMBER_NAME);
                    break;
                case (int)Constants.ProfileMenu.FIFTH:    //나이
                    changedItem = signUp.InputPassword(8, (int)Constants.ProfileMenu.FIFTH, Constants.AGE_REGEX, Constants.MESSAGE_ABOUT_AGE);
                    break;
                case (int)Constants.ProfileMenu.SIXTH:    //휴대전화
                    changedItem = signUp.InputPassword(12, (int)Constants.ProfileMenu.SIXTH, Constants.PHONE_NUMBER_REGEX, Constants.MESSAGE_ABOUT_PHONE_NUMBER);
                    break;
                case (int)Constants.ProfileMenu.SEVENTH:  //주소
                    changedItem = signUp.InputPassword(8, (int)Constants.ProfileMenu.SEVENTH, Constants.ADDRESS_REGEX, Constants.MESSAGE_ABOUT_ADDRESS);
                    break;
            }

            return changedItem;
        }
        public void EditProfile(MemberVO member, SignUp signUp, Keyboard keyboard)
        {
            string changedItem;
            int menu;

            keyboard.SetPosition(0, (int)Constants.ProfileMenu.FIRST);  //커서 위치 조정

            while (Constants.INPUT_VALUE)
            {
                memberView.PrintProfile(member);                     //회원정보수정 화면 출력

                menu = keyboard.SelectMenu((int)Constants.ProfileMenu.FIRST, (int)Constants.ProfileMenu.SEVENTH, (int)Constants.ProfileMenu.STEP);  //수정할 항목 선택
                if (menu == (int)Constants.Keyboard.ESCAPE) return;  //메뉴선택 중 뒤로가기를 누르면 종료
                menu = keyboard.Top;                                 //커서의 top값 == 수정할 항목

                changedItem = InputProfile(menu, signUp);            //선택한 정보에 해당하는 회원정보 수정
                if (changedItem.Equals(Constants.ESC)) continue;     //입력 중 Esc -> 수정할 항목 다시 선택

                ReflectChangedInformation(menu, changedItem, member);//DB, MemberVO에 수정사항 업데이트

                //if (changedItem != Constants.ESC) library.UpdateMember(memberId, query);  //수정된 정보 DB에 반영(정보입력 중 esc->뒤로가기(수정할 메뉴 선택으로))
                //if (changedItem != Constants.ESC && menu == (int)Constants.ProfileMenu.FIRST) memberId = changedItem;  //방금 수정한 정보가 아이디라면 myId에 변경된 아이디 저장
            }
        }
    }
}
