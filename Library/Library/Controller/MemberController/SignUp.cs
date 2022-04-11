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
            string id;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(8,5);
                id = Console.ReadLine();  //아이디 입력
                if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, @"^[a-zA-Z0-9]{5,10}$"))  //입력형식에 맞지 않음 -> 아이디 다시입력
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
        public string InputPassword(int left, int top, string regexText, string errorMessage)
        {
            string password;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);  
                password = Console.ReadLine();
                if (string.IsNullOrEmpty(password) || !Regex.IsMatch(password, regexText))   //입력형식에 맞지 않은 경우 -> 비밀번호 다시 입력
                {
                    PrintInputBox(0, top + 1, errorMessage);
                }
                else
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);   //올바르게 입력한 경우 -> 비밀번호 재확인으로 넘어감
                    break;
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
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
        public string InputAge()
        {
            string age;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(6, 17);
                age = Console.ReadLine();
                if (string.IsNullOrEmpty(age) || !Regex.IsMatch(age, @"^[0-9]{1,2}$"))   //형식에 맞지 않는 입력
                {
                    PrintInputBox(0, 18, "(1~99세까지 입력 가능합니다.)                ");
                }
                else
                {
                    PrintInputBox(0, 18, Constants.REMOVE_LINE);   //형식에 맞게 입력 -> 휴대전화 입력으로 넘어감
                    break;
                }
                PrintInputBox(6, 17, Constants.REMOVE_LINE);
            }
            return age;
        }
        public void ControlSignUp()
        {
            SignUpScreen signUpScreen = new SignUpScreen();
            signUpScreen.PrintSingUp();  //회원가입화면 출력
            string[] profile = new string[6];

            //아이디
            profile[0] = InputId();
            //비밀번호
            profile[1] = InputPassword(10, 8, @"^[a-zA-Z0-9]{5,10}$", "(5~10자의 영어, 숫자만 다시 입력해주세요.)         "); 
            //비밀번호 재확인
            ReconfirmPassword(profile[1]); 
            //이름
            profile[2] = InputPassword(6, 14, @"^[a-zA-Z가-힣]{1,30}$", "(30자 이내의 영어, 한글만 다시 입력해주세요.)             ");
            //나이
            profile[3] = InputPassword(6, 17, @"^[1-9]+[0-9]{0,1}$", "(1~99세까지 입력 가능합니다.)                ");       //나이
            //휴대전화
            profile[4] = InputPassword(10, 20, @"010-[0-9]{4}-[0-9]{4}$", "(양식에 맞춰 다시 입력해주세요.(ex: 010-0000-0000))              ");
            //주소
            profile[5] = InputPassword(6, 23, @"[가-힣]+(시|도)\s[가-힣]+(시|군|구)\s[가-힣]+(읍|면|동)", "(양식에 맞춰 입력해주세요.(ex: 서울특별시 광진구 군자동))");
            
        }
    }
}
