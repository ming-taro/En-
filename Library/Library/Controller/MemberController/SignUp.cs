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
            LibraryVO libraryVO = LibraryVO.GetLibraryVO(); 

            for(int i=0; i<libraryVO.memberList.Count; i++)
            {
                if (libraryVO.memberList[i].Id.Equals(id)) return Constants.DUPLICATE_ID;  //중복된 아이디 -> 입력불가
            }
            return !Constants.DUPLICATE_ID;    //기존에 없는 아이디 -> 입력가능
        }
        public string InputId(int left, int top)  //아이디 입력
        {
            EnteringText text = new EnteringText();
            string id;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);
                id = text.EnterText(left, top, "");  //아이디 입력
                if (id.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, @"^[a-zA-Z0-9]{5,10}$"))  //입력형식에 맞지 않음 -> 아이디 다시입력
                {
                    PrintInputBox(0, top + 1, "(5~10자의 영어, 숫자만 다시 입력해주세요.)         ");
                }
                else if (IsDuplicatedId(id))  //입력형식은 맞지만, 기존회원과 중복된 아이디인 경우 -> 아이디 다시입력
                {
                    PrintInputBox(0, top + 1, "(이미 사용중인 아이디입니다. 다시 입력해주세요.)    ");
                }
                else
                {
                    PrintInputBox(0, top + 1, "(사용 가능한 아이디입니다.)                          ");
                    break;  //입력형식에 맞고, 기존에 없는 새로운 아이디 -> 아이디 등록가능
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return id;   //사용 가능한 아이디
        }
        public string InputPassword(int left, int top, string regexText, string errorMessage)
        {
            EnteringText text = new EnteringText();
            string password;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);  
                password = text.EnterText(left, top, "");
                if (password.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(password) || !Regex.IsMatch(password, regexText))   //입력형식에 맞지 않은 경우 -> 비밀번호 다시 입력
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
        public void ReconfirmPassword(int left, int top, string password1)   //비밀번호 재확인
        {
            string password2;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);
                password2 = Console.ReadLine();   //비밀번호 입력
                if (string.IsNullOrEmpty(password2) || !password1.Equals(password2))   //입력형식에 맞지 않은 경우 -> 비밀번호 다시 입력
                {
                    PrintInputBox(0, top + 1, "(비밀번호가 맞지 않습니다. 다시 입력해주세요.)         ");
                }
                else
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);  //비밀번호를 알맞게 입력 -> 이름 입력으로 넘어감
                    break;
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
        }
        public void AddMemberList(string id, string password, string name, string age, string phoneNumber, string address)   //회원가입완료 후 -> 회원리스트에 가입한 정보 저장
        {
            LibraryVO libraryVO = LibraryVO.GetLibraryVO();
            libraryVO.memberList.Add(new MemberVO(id, password, name, age, phoneNumber, address));
        }
        public void ControlSignUp()
        {
            SignUpScreen signUpScreen = new SignUpScreen();
            signUpScreen.PrintSingUp();  //회원가입화면 출력

            string id = InputId(8, 5);
            if (id.Equals(Constants.ESC)) return; //esc -> 뒤로가기

            string password = InputPassword(10, 8, @"^[a-zA-Z0-9]{5,10}$", "(5~10자의 영어, 숫자만 다시 입력해주세요.)         ");
            if (password.Equals(Constants.ESC)) return;
            ReconfirmPassword(17, 11, password); 
            
            string name = InputPassword(6, 14, @"^[a-zA-Z가-힣]{1,30}$", "(30자 이내의 영어, 한글만 다시 입력해주세요.)             ");
            if (name.Equals(Constants.ESC)) return;

            string age = InputPassword(6, 17, @"^[1-9]{1}[0-9]{0,1}$", "(1~99세까지 입력 가능합니다.)                ");
            if (age.Equals(Constants.ESC)) return;

            string phoneNumber = InputPassword(10, 20, @"010-[0-9]{4}-[0-9]{4}$", "(양식에 맞춰 다시 입력해주세요.(ex: 010-0000-0000))              ");
            if (phoneNumber.Equals(Constants.ESC)) return;

            string address = InputPassword(6, 23, @"[가-힣]+(시|도)\s[가-힣]+(시|군|구)\s[가-힣]+(읍|면|동)", "(양식에 맞춰 입력해주세요.(ex: 서울특별시 광진구 군자동))       ");
            if (address.Equals(Constants.ESC)) return;

            AddMemberList(id, password, name, age, phoneNumber, address);              //회원리스트에 가입정보 저장
            signUpScreen.PrintSuccessMessage();  //회원가입 축하 화면 출력
        }
    }
}
