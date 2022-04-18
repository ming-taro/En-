using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class EditingProfile
    {
        string myId;
        public EditingProfile(string myId)
        {
            this.myId = myId;
        }
        public MemberVO FindMember(string memberId)
        {
            LibraryVO library = LibraryVO.GetLibraryVO();
            MemberVO memberVO = null;

            for(int i = 0; i<library.memberList.Count; i++)
            {
                if (library.memberList[i].Id.Equals(memberId))
                {
                    memberVO = library.memberList[i];
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
                case (int)Constants.ProfileMenu.FIRST:   //아이디 입력
                    profile = signUp.InputId(10, (int)Constants.ProfileMenu.FIRST);
                    memberVO.Id = profile;         //아이디 수정내역 저장
                    myId = profile;
                    break;
                case (int)Constants.ProfileMenu.SECOND:   //비밀번호 입력
                    profile = signUp.InputPassword(12, (int)Constants.ProfileMenu.SECOND, @"^[a-zA-Z0-9]{5,10}$", "(5~10자의 영어, 숫자만 다시 입력해주세요.)         ");
                    signUp.ReconfirmPassword(19, 22, profile);  //비밀번호 재확인
                    memberVO.Password = profile;   //비밀번호 수정내역 저장
                    break;
                case (int)Constants.ProfileMenu.FOURTH:   //이름
                    profile = signUp.InputPassword(8, (int)Constants.ProfileMenu.FOURTH, @"^[a-zA-Z가-힣]{1,30}$", "(30자 이내의 영어, 한글만 다시 입력해주세요.)             ");
                    memberVO.Name = profile;       //이름  수정내역 저장
                    break;
                case (int)Constants.ProfileMenu.FIFTH:   //나이
                    profile = signUp.InputPassword(8, (int)Constants.ProfileMenu.FIFTH, @"^[1-9]{1}[0-9]{0,1}$", "(1~99세까지 입력 가능합니다.)                ");
                    memberVO.Age = profile;        //나이 수정내역 저장
                    break;
                case (int)Constants.ProfileMenu.SIXTH:   //휴대전화
                    profile = signUp.InputPassword(12, (int)Constants.ProfileMenu.SIXTH, @"010-[0-9]{4}-[0-9]{4}$", "(양식에 맞춰 다시 입력해주세요.(ex: 010-0000-0000))              ");
                    memberVO.PhoneNumber = profile;//휴대전화 수정내역 저장
                    break;
                case (int)Constants.ProfileMenu.SEVENTH:   //주소
                    profile = signUp.InputPassword(8, (int)Constants.ProfileMenu.SEVENTH, @"[가-힣]+(시|도)\s[가-힣]+(시|군|구)\s[가-힣]+(읍|면|동)", "(양식에 맞춰 입력해주세요.(ex: 서울특별시 광진구 군자동))         ");
                    memberVO.Address = profile;    //주소 수정내역 저장
                    break;
            }
        }
        public string ControlEditingProfile()
        {
            Keyboard keyboard = new Keyboard(0, 16);
            EditingScreen editingScreen = new EditingScreen();
            editingScreen.PrintEditing(myId);                //회원정보수정 화면
            int menu;

            while (Constants.INPUT_VALUE)
            {
                menu = keyboard.SelectMenu(16, 34, 3);
                if (menu == Constants.ESCAPE) return myId;   //메뉴선택 중 뒤로가기를 누르면 종료(아이디를 수정했다면 memberController에 수정된 id를 리턴해줘야 한다)
                InputProfile(keyboard.Top, myId);       //선택한 정보에 해당하는 회원정보 수정 및 회원리스트에 반영
                editingScreen.PrintSuccessMessage(myId);     //수정된 정보가 반영된 화면 출력
            }
        }
    }
}
