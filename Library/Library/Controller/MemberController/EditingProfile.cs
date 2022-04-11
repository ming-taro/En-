using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class EditingProfile
    {
        public MemberVO FindMember(string memberId)
        {
            MemberListVO memberListVO = MemberListVO.GetMemberListVO(); //회원목록
            MemberVO memberVO = null;

            for(int i = 0; i<memberListVO.memberList.Count; i++)
            {
                if (memberListVO.memberList[i].Id.Equals(memberId))
                {
                    memberVO = memberListVO.memberList[i];
                    break;  //목록에서 해당 회원을 찾음
                }
            }

            return memberVO;
        }
        public void InputProfile(int menu, string memberId)
        {
            SignUp signUp = new SignUp();
            EditingScreen editingScreen = new EditingScreen();
            MemberVO memberVO = FindMember(memberId);            //해당 회원
            string profile;

            switch (menu)
            {
                case 16:   //아이디 입력
                    profile = signUp.InputId(10, 16);
                    memberVO.Id = profile;
                    break;
                case 19:   //비밀번호 입력
                    profile = signUp.InputPassword(12, 19, @"^[a-zA-Z0-9]{5,10}$", "(5~10자의 영어, 숫자만 다시 입력해주세요.)         ");
                    signUp.ReconfirmPassword(19, 22, profile);  //비밀번호 재확인
                    memberVO.Password = profile;
                    break;
                case 25:   //이름
                    profile = signUp.InputPassword(8, 25, @"^[a-zA-Z가-힣]{1,30}$", "(30자 이내의 영어, 한글만 다시 입력해주세요.)             ");
                    memberVO.Name = profile;
                    break;
                case 28:   //나이
                    profile = signUp.InputPassword(8, 28, @"^[1-9]+[0-9]{0,1}$", "(1~99세까지 입력 가능합니다.)                ");
                    memberVO.Age = profile;
                    break;
                case 31:   //휴대전화
                    profile = signUp.InputPassword(12, 31, @"010-[0-9]{4}-[0-9]{4}$", "(양식에 맞춰 다시 입력해주세요.(ex: 010-0000-0000))              ");
                    memberVO.PhoneNumber = profile;
                    break;
                case 34:   //주소
                    profile = signUp.InputPassword(6, 23, @"[가-힣]+(시|도)\s[가-힣]+(시|군|구)\s[가-힣]+(읍|면|동)", "(양식에 맞춰 입력해주세요.(ex: 서울특별시 광진구 군자동))         ");
                    memberVO.Address = profile;
                    break;
            }
        }
        public int ControlEditingProfile(string memberId)
        {
            Keyboard keyboard = new Keyboard(0, 16);
            EditingScreen editingScreen = new EditingScreen();
            editingScreen.PrintEditing(memberId);                //회원정보수정 화면
            int menu;

            while (Constants.INPUT_VALUE)
            {
                menu = keyboard.SelectMenu(16, 34, 3);
                if (menu == Constants.ESCAPE) return Constants.ESCAPE;      //메뉴선택 중 뒤로가기를 누르면 종료
                InputProfile(keyboard.GetTop(), memberId);       //선택한 정보에 해당하는 회원정보 수정 및 회원리스트에 반영
            }
        }
    }
}
