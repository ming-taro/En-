using MySql.Data.MySqlClient;
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
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private Logo logo;
        public SignIn()
        {
            bookDatabaseManager = new BookDAO();
            text = new EnteringText();
            logo = new Logo();
        }
        public string SignInAdmin()
        {
            string id;
            string password;
            AdminVO admin = bookDatabaseManager.GetAdminAccount();  //관리자 계정

            logo.PrintSignIn();   //로그인 화면 출력

            while (Constants.INPUT_VALUE)
            {
                id = text.EnterText(8, 5, "");             //아이디 입력
                if (id.Equals(Constants.ESC)) return Constants.ESC;

                password = text.EnterText(10, 6, "");      //비밀번호 입력
                if (password.Equals(Constants.ESC)) return Constants.ESC;

                if (id.Equals(admin.Id) && password.Equals(admin.Password)) break;   //관리자 로그인 완료

                logo.PrintSignInFailure();                 //다시 입력해달라는 메세지 출력
            }

            return Constants.ENTER;
        }
        public bool IsExistingMember(string id, string password)  //입력된 정보가 존재하는 회원인지 확인
        {
            LibraryVO library = LibraryVO.GetLibraryVO();

            string sql = "select*from member where id=\"" + id + "\" and password=\"" + password + "\";";
            MySqlCommand command = new MySqlCommand(sql, library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            table.Read();
            if (table.HasRows)
            {
                table.Close();
                return Constants.EXISTING_MEMBER;
            }
            else
            {
                table.Close();
                return Constants.NON_EXISTING_MEMBER;
            }
        }
        public string SignInMember()
        { 
            string id, password;

            logo.PrintSignIn();

            while (Constants.INPUT_VALUE)
            {
                id = text.EnterText(8, 5, "");             //아이디 입력
                if (id.Equals(Constants.ESC)) return Constants.ESC;
                password = text.EnterText(10, 6, "*");     //비밀번호 입력
                if (password.Equals(Constants.ESC)) return Constants.ESC;

                if (IsExistingMember(id, password)) break;
                logo.PrintSignInFailure();       //다시 입력해달라는 메세지 출력
            }

            return id;  
        }
    }
}
