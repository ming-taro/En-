using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class SignUp  //회원가입
    {
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsDuplicatedId(string id)  //기존 회원의 아이디와 중복되는지 확인
        {
            MemberListVO memberListVO = MemberListVO.GetMemberListVO(); //회원목록

            for(int i=0; i<memberListVO.memberList.Count; i++)
            {
                if (memberListVO.memberList[i].Id.Equals(id)) return Constants.DUPLICATE_ID;  //중복된 아이디 -> 입력불가
            }
            return !Constants.DUPLICATE_ID;    //기존에 없는 아이디 -> 입력가능
        }
        public string InputId()  //아이디 입력
        {
            Regex regex = new Regex(@"^[a-zA-z0-9]{5,10}$");
            string id;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(8,5);
                id = Console.ReadLine();  //아이디 입력
                if (string.IsNullOrEmpty(id) || !regex.IsMatch(id))  //입력형식에 맞지 않음 -> 아이디 다시입력
                {
                    PrintInputBox(0, 6, "(5~10자의 영어, 숫자만 다시 입력해주세요.)                    ");
                }
                else if (IsDuplicatedId(id))  //입력형식은 맞지만, 기존회원과 중복된 아이디인 경우 -> 아이디 다시입력
                {
                    Console.SetCursorPosition(0, 6);
                    PrintInputBox(0, 6, "(이미 사용중인 아이디입니다. 다시 입력해주세요.)              ");
                }
                else
                {
                    PrintInputBox(0,6, "(사용 가능한 아이디입니다.)                                     ");
                    break;  //입력형식에 맞고, 기존에 없는 새로운 아이디 -> 아이디 등록가능
                }
                PrintInputBox(8, 5, Constants.REMOVE_LINE);
            }
            return id;   //사용 가능한 아이디
        }
        public void ControlSignUp()
        {
            SignUpScreen signUpScreen = new SignUpScreen();
            signUpScreen.PrintSingUp();  //회원가입화면 출력
            string[] profile = new string[6];

            profile[0] = InputId();   //아이디
            
            Console.SetCursorPosition(10, 8);   //비밀번호
            Console.ReadLine();
            Console.SetCursorPosition(17, 11);  //비밀번호 재확인
            Console.ReadLine();
            Console.SetCursorPosition(6, 13);   //이름
            Console.ReadLine();
            Console.SetCursorPosition(6, 16);   //나이
            Console.ReadLine();
            Console.SetCursorPosition(10, 19);  //휴대전화
            Console.ReadLine();
            Console.SetCursorPosition(13, 22);  //도로명 주소
            Console.ReadLine();
        }
    }
}
