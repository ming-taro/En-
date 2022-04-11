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
            Regex regex = new Regex(@"^[a-zA-Z0-9]{5,10}$"); //아이디 : 5~10자,영어,숫자
            string id;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(8,5);
                id = Console.ReadLine();  //아이디 입력
                if (string.IsNullOrEmpty(id) || !regex.IsMatch(id))  //입력형식에 맞지 않음 -> 아이디 다시입력
                {
                    PrintInputBox(0, 6, "(5~10자의 영어, 숫자만 다시 입력해주세요.)         ");
                }
                else if (IsDuplicatedId(id))  //입력형식은 맞지만, 기존회원과 중복된 아이디인 경우 -> 아이디 다시입력
                {
                    PrintInputBox(0, 6, "(이미 사용중인 아이디입니다. 다시 입력해주세요.)    ");
                }
                else
                {
                    PrintInputBox(0, 6, "(사용 가능한 아이디입니다.)                          ");
                    break;  //입력형식에 맞고, 기존에 없는 새로운 아이디 -> 아이디 등록가능
                }
                PrintInputBox(8, 5, Constants.REMOVE_LINE);
            }
            return id;   //사용 가능한 아이디
        }
        public string InputPassword()
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9]{5,10}$");
            string password;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(10, 8);  
                password = Console.ReadLine();
                if (string.IsNullOrEmpty(password) || !regex.IsMatch(password))   //입력형식에 맞지 않은 경우 -> 비밀번호 다시 입력
                {
                    PrintInputBox(0, 9, "(5~10자의 영어, 숫자만 다시 입력해주세요.)         ");
                }
                else
                {
                    PrintInputBox(0, 9, Constants.REMOVE_LINE);   //올바르게 입력한 경우 -> 비밀번호 재확인으로 넘어감
                    break;
                }
                PrintInputBox(10, 8, Constants.REMOVE_LINE);
            }
            return password;
        }
        public void ReconfirmPassword(string password1)   //비밀번호 재확인
        {
            string password2;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(17, 11);
                password2 = Console.ReadLine();   //비밀번호 입력
                if (string.IsNullOrEmpty(password2) || !password1.Equals(password2))   //입력형식에 맞지 않은 경우 -> 비밀번호 다시 입력
                {
                    PrintInputBox(0, 12, "(비밀번호가 맞지 않습니다. 다시 입력해주세요.)         ");
                }
                else
                {
                    PrintInputBox(0, 12, Constants.REMOVE_LINE);  //비밀번호를 알맞게 입력 -> 이름 입력으로 넘어감
                    break;
                }
                PrintInputBox(17, 11, Constants.REMOVE_LINE);
            }
        }
        public string InputName()  //이름을 입력받음
        {
            Regex regex = new Regex(@"^[a-zA-Z가-힣]{1,30}$");
            string name;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(6, 13);
                name = Console.ReadLine();     //이름을 입력받음
                if (string.IsNullOrEmpty(name) || !regex.IsMatch(name))   //형식에 맞지 않는 입력
                {
                    PrintInputBox(0, 14, "(30자 이내의 영어, 한글만 다시 입력해주세요.)             ");
                }
                else
                {
                    PrintInputBox(0, 14, Constants.REMOVE_LINE);   //형식에 맞게 입력 -> 휴대전화 입력으로 넘어감
                    break;
                }
                PrintInputBox(6, 13, Constants.REMOVE_LINE);
            }

            return name;
        }
        public void ControlSignUp()
        {
            SignUpScreen signUpScreen = new SignUpScreen();
            signUpScreen.PrintSingUp();  //회원가입화면 출력
            string[] profile = new string[6];

            profile[0] = InputId();        //아이디
            profile[1] = InputPassword();  //비밀번호
            ReconfirmPassword(profile[1]); //비밀번호 재확인
            profile[2] = InputName();      //이름
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
