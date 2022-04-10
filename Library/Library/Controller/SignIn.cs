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
        public SignIn()
        {
            SignInScreen signInScreen = new SignInScreen();
            signInScreen.PrintSignIn();
        }
        public void PrintScreen()  //지울 함수
        {
            Screen screen = new Screen();
            screen.PrintSingIn("로그인");
        }
        public bool SignInAdmin(ref string id, ref string password)
        {
            Console.SetCursorPosition(8, 5);
            id = Console.ReadLine();     //아이디 입력
            Console.SetCursorPosition(10, 6);
            password = Console.ReadLine();     //비밀번호 입력

            return Constants.GOING_NEXT;
        }
        public bool IsExistingMember(string id, string password)  //입력된 정보가 존재하는 회원인지 확인
        {
            MemberListVO memberListVO = new MemberListVO();

            for(int i=0; i<memberListVO.memberList.Count; i++)
            {
                if (memberListVO.memberList[i].Id.Equals(id) && memberListVO.memberList[i].Password.Equals(password))
                {
                    return Constants.EXISTING_MEMBER;    //회원리스트에 있는 회원이라면 로그인 성공
                }
            }

            return !Constants.EXISTING_MEMBER;
        }
        public string SignInMember()
        {
            SignInScreen signInScreen = new SignInScreen();
            Regex regex = new Regex(@"^[\e]{1,1}$");
            string id, password;
            

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(8, 5);
                id = Console.ReadLine();           //아이디 입력
                if (regex.IsMatch(id)) break;
                Console.SetCursorPosition(10, 6);
                password = Console.ReadLine();     //비밀번호 입력
                if (IsExistingMember(id, password)) break;
                signInScreen.PrintFailure();       //다시 입력해달라는 메세지 출력
            }
            Console.SetCursorPosition(0,10);
            Console.WriteLine("어우우");
            Console.ReadLine();
            return id;  
        }
    }
}
