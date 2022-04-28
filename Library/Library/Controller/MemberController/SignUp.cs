﻿using MySql.Data.MySqlClient;
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
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private MemberView memberView;
        private Logo logo;
        public SignUp()
        {
            bookDatabaseManager = new BookDAO();
            text = new EnteringText();
            memberView = new MemberView();
            logo = new Logo();
        }
        public bool IsDuplicatedId(string id)  //기존 회원의 아이디와 중복되는지 확인
        {
            LibraryVO library = LibraryVO.GetLibraryVO();

            string sql = "select*from member where id=\"" + id + "\";";
            MySqlCommand command = new MySqlCommand(sql, library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            table.Read();
            if (table.HasRows)
            {
                table.Close();
                return Constants.DUPLICATE_ID;
            }
            else
            {
                table.Close();
                return Constants.NON_DUPLICATE_ID;
            }
        }
        public string InputId(int left, int top)  //아이디 입력
        {
            string id;

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(left, top);
                id = text.EnterText(left, top, "");  //아이디 입력

                if (id.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(id, Constants.MEMBER_ID_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않음
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_MEMBER_ID, ConsoleColor.Red);
                }
                else if (IsDuplicatedId(id))  //입력형식은 맞지만, 기존회원과 중복된 아이디인 경우
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_DUPLICATED_ID, ConsoleColor.Red);
                }
                else                          //입력형식에 맞고, 기존에 없는 새로운 아이디 -> 아이디 등록가능
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_AVAILABLE_ID, ConsoleColor.Green);
                    break;  
                }
            }
            return id;   //사용 가능한 아이디
        }
        public string InputPassword(int left, int top, string regexText, string errorMessage)
        {
            string password;

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(left, top);
                password = text.EnterText(left, top, "");  //비밀번호 입력

                if (password.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(password, regexText) == Constants.IS_NOT_MATCH)   //입력형식에 맞지 않은 경우 -> 비밀번호 다시 입력
                {
                    logo.PrintMessage(0, top + 1, errorMessage, ConsoleColor.Red);
                }
                else
                {
                    logo.RemoveLine(0, top + 1);   //올바르게 입력한 경우 -> 비밀번호 재확인으로 넘어감
                    break;
                }
            }
            return password;
        }
        public string ReconfirmPassword(int left, int top, string password)   //비밀번호 재확인
        {
            EnteringText text = new EnteringText();
            string reconfirm;

            while (Constants.INPUT_VALUE)
            {
                reconfirm = text.EnterText(left, top, "");  //비밀번호 입력

                if (reconfirm.Equals(Constants.ESC))        //비밀번호 확인 중 뒤로가기
                {
                    return Constants.ESC;
                }
                else if (password.Equals(reconfirm) == Constants.IS_NOT_MATCH)   //입력형식에 맞지 않은 경우 -> 비밀번호 다시 입력
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_PASSWORD, ConsoleColor.Red);
                }
                else
                {
                    logo.RemoveLine(0, top + 1);  //비밀번호를 알맞게 입력 -> 이름 입력으로 넘어감
                    break;
                }
                logo.RemoveLine(left, top);
            }

            return reconfirm;
        }
        public void ControlSignUp()
        {
            MemberVO memberAccount;
            SignUpScreen signUpScreen = new SignUpScreen();
            signUpScreen.PrintSingUp();  //회원가입화면 출력

            string id = InputId(8, 5);
            if (id.Equals(Constants.ESC)) return; //esc -> 뒤로가기

            string password = InputPassword(10, 8, Constants.MEMBER_ID_REGEX, Constants.MESSAGE_ABOUT_MEMBER_ID);
            if (password.Equals(Constants.ESC)) return;

            string reconfirm = ReconfirmPassword(17, 11, password);
            if (reconfirm.Equals(Constants.ESC)) return;
            
            string name = InputPassword(6, 14, Constants.NAME_REGEX, Constants.MESSAGE_ABOUT_MEMBER_NAME);
            if (name.Equals(Constants.ESC)) return;

            string age = InputPassword(6, 17, Constants.AGE_REGEX, Constants.MESSAGE_ABOUT_AGE);
            if (age.Equals(Constants.ESC)) return;

            string phoneNumber = InputPassword(10, 20, Constants.PHONE_NUMBER_REGEX, Constants.MESSAGE_ABOUT_PHONE_NUMBER);
            if (phoneNumber.Equals(Constants.ESC)) return;

            string address = InputPassword(6, 23, Constants.ADDRESS_REGEX, Constants.MESSAGE_ABOUT_ADDRESS);
            if (address.Equals(Constants.ESC)) return;

            memberAccount = new MemberVO(id, password, name, age, phoneNumber, address);   //입력이 완료된 회원 정보

            //library.InsertMember(id, password, name, age, phoneNumber, address);  //회원리스트에 가입정보 저장

            signUpScreen.PrintSuccessMessage();  //회원가입 축하 화면 출력

            Keyboard keyboard = new Keyboard();
            keyboard.PressESC();
        }
    }
}
