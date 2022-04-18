using System;

namespace Library
{
    class EditingScreen
    {
        public void PrintProfile(string memberId)
        {
            LibraryVO library = LibraryVO.GetLibraryVO();

            for(int i=0; i<library.memberList.Count; i++)
            {
                if (library.memberList[i].Id.Equals(memberId))
                {
                    Console.WriteLine(library.memberList[i]);  //현재 로그인한 회원의 정보 출력
                    break;
                }
            }
        }
        public void PrintEditing(string memberId)
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("회원정보수정");
            Console.WriteLine("=======================뒤로가기:[ESC]========================\n");
            PrintProfile(memberId);  //현재 로그인한 회원 정보 출력
            Console.WriteLine("\n=============================================================\n");
            Console.WriteLine("☞아이디: \n(5~10자의 영어, 숫자만 입력해주세요.)\n");
            Console.WriteLine("☞비밀번호: \n(5~10자의 영어, 숫자만 입력해주세요.)\n");
            Console.WriteLine("  비밀번호 재확인: \n\n");
            Console.WriteLine("☞이름: \n(영어, 한글만 입력해주세요.)\n");
            Console.WriteLine("☞나이: \n(숫자만 입력해주세요.)\n");
            Console.WriteLine("☞휴대전화: \n(숫자만 입력해주세요.)\n");
            Console.WriteLine("☞주소: \n(ex: 서울특별시 광진구 능동로 209)\n");
        }
        public void PrintSuccessMessage(string memberId)
        {
            PrintEditing(memberId);
            Console.SetCursorPosition(0, 14);
            Console.WriteLine("==================회원정보가 변경되었습니다===================");  //변경완료문구 출력
        }
    }
}
