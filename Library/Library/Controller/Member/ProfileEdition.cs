using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ProfileEdition
    {
        private MemberDAO memberDAO = MemberDAO.GetInstance();
        private LogDAO logDAO = LogDAO.GetInstance();
        private MemberView memberView;

        public ProfileEdition(MemberView memberView)
        {
            this.memberView = memberView;
        }
        private void ReflectChangeInVO(int menu, string changedItem, MemberVO member)   //수정된 정보 반영
        {
            switch (menu)                                 
            {
                case (int)Constants.EditMenu.SECOND:   //비밀번호
                    member.Password = changedItem;
                    break;
                case (int)Constants.EditMenu.FOURTH:   //이름
                    member.Name = changedItem;
                    break;
                case (int)Constants.EditMenu.FIFTH:   //나이
                    member.Age = changedItem;
                    break;
                case (int)Constants.EditMenu.SIXTH:   //휴대전화
                    member.PhoneNumber = changedItem;
                    break;
                case (int)Constants.EditMenu.SEVENTH: //주소
                    member.Address = changedItem;
                    break;
            }
        }
        private string InputProfile(int menu, SignUp signUp)  //수정할 회원정보 입력
        {
            string changedItem = Constants.ESC;

            switch (menu)
            {
                case (int)Constants.EditMenu.SECOND:   //비밀번호
                    changedItem = signUp.InputPassword(12, (int)Constants.EditMenu.SECOND, Constants.MEMBER_ID_REGEX, Constants.MESSAGE_ABOUT_MEMBER_ID);
                    break;
                case (int)Constants.EditMenu.FOURTH:   //이름
                    changedItem = signUp.InputPassword(8, (int)Constants.EditMenu.FOURTH, Constants.NAME_REGEX, Constants.MESSAGE_ABOUT_MEMBER_NAME);
                    break;
                case (int)Constants.EditMenu.FIFTH:    //나이
                    changedItem = signUp.InputPassword(8, (int)Constants.EditMenu.FIFTH, Constants.AGE_REGEX, Constants.MESSAGE_ABOUT_AGE);
                    break;
                case (int)Constants.EditMenu.SIXTH:    //휴대전화
                    changedItem = signUp.InputPassword(12, (int)Constants.EditMenu.SIXTH, Constants.PHONE_NUMBER_REGEX, Constants.MESSAGE_ABOUT_PHONE_NUMBER);
                    break;
                case (int)Constants.EditMenu.SEVENTH:  //주소
                    changedItem = signUp.InputPassword(8, (int)Constants.EditMenu.SEVENTH, Constants.ADDRESS_REGEX, Constants.MESSAGE_ABOUT_ADDRESS);
                    break;
            }

            return changedItem;
        }
        public void EditProfile(MemberVO member, SignUp signUp, Keyboard keyboard)
        {
            string changedItem;
            int menu;

            keyboard.SetPosition(0, (int)Constants.EditMenu.FIRST);  //커서 위치 조정

            while (Constants.INPUT_VALUE)
            {
                memberView.PrintProfile(member);                     //회원정보수정 화면 출력

                menu = keyboard.SelectMenu((int)Constants.EditMenu.SECOND, (int)Constants.EditMenu.SEVENTH, (int)Constants.EditMenu.STEP);  //수정할 항목 선택
                if (menu == (int)Constants.Keyboard.ESCAPE) break;   //메뉴선택 중 Esc -> DB에 수정된 정보 업데이트 후 종료
                menu = keyboard.Top;                                 //커서의 top값 == 수정할 항목

                changedItem = InputProfile(menu, signUp);            //선택한 정보에 해당하는 회원정보 수정
                if (changedItem.Equals(Constants.ESC)) continue;     //입력 중 Esc -> 수정할 항목 다시 선택
                
                if(menu == (int)Constants.EditMenu.SECOND) changedItem = signUp.ReconfirmPassword(19, (int)Constants.EditMenu.THIRD, changedItem);  //비밀번호 재확인
                if (changedItem.Equals(Constants.ESC)) continue;     //비밀번호 재확인 입력 중 Esc -> 수정할 항목 다시 선택

                ReflectChangeInVO(menu, changedItem, member);//DB, MemberVO에 수정사항 업데이트
            }

            memberDAO.AddToMemberList(Constants.UPDATE_TO_MEMBER_LIST, member);   //DB에 수정된 회원정보 업데이트
            logDAO.EditMemberInformation(member.Id);
        }
    }
}
