using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class SignIn
    {
        public SignIn()  //-->>안됨
        {
            SignInScreen signInScreen = new SignInScreen();
            signInScreen.PrintSignIn();
        }
        public void SignInAdmin()
        {
            SignInScreen signInScreen = new SignInScreen();   //--->수정

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(8, 5);
                string id = Console.ReadLine();           //아이디 입력
                Console.SetCursorPosition(10, 6);
                string password = Console.ReadLine();     //비밀번호 입력

                if (id == "12345" && password == "00000") break;   //관리자 로그인 완료

                signInScreen.PrintFailure();              //다시 입력해달라는 메세제 출력
            }
        }
        public bool IsExistingMember(string id, string password)  //입력된 정보가 존재하는 회원인지 확인
        {
            MemberListVO memberListVO = MemberListVO.GetMemberListVO();

            for(int i=0; i<memberListVO.memberList.Count; i++)
            {
                if (memberListVO.memberList[i].Id.Equals(id) && memberListVO.memberList[i].Password.Equals(password))
                {
                    return Constants.EXISTING_MEMBER;    //회원리스트에 있는 회원이라면 로그인 성공
                }
            }

            return !Constants.EXISTING_MEMBER;  //----->느낌표XXXX(따로만들기)
        }
        public string SignInMember()
        {
            SignInScreen signInScreen = new SignInScreen();
            string id, password;
            
            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(8, 5);
                id = Console.ReadLine();           //아이디 입력
                Console.SetCursorPosition(10, 6);
                password = Console.ReadLine();     //비밀번호 입력
                if (IsExistingMember(id, password)) break;
                signInScreen.PrintFailure();       //다시 입력해달라는 메세지 출력
            }

            return id;  
        }
    }
}
