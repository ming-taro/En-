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
        public SignIn(BookDAO bookDatabaseManager, EnteringText text, Logo logo)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            this.text = text;
            this.logo = logo;
        }
        public string SignInAdmin()
        {
            string id;
            string password;
            AdminVO adminAccount = bookDatabaseManager.GetAdminAccount();  //관리자 계정

            logo.PrintSignIn();   //로그인 화면 출력

            while (Constants.INPUT_VALUE)
            {
                id = text.EnterText((int)Constants.SignIn.INPUT, (int)Constants.SignIn.ID, "");                    //아이디 입력
                if (id.Equals(Constants.ESC)) return Constants.ESC;

                password = text.EnterText((int)Constants.SignIn.INPUT, (int)Constants.SignIn.PASSWORD, "*"); //비밀번호 입력
                if (password.Equals(Constants.ESC)) return Constants.ESC;

                if (id.Equals(adminAccount.Id) && password.Equals(adminAccount.Password)) break;   //관리자 로그인 완료

                logo.PrintSignInFailure();  
            }

            return Constants.ENTER;
        }
        public string SignInToLibrary()
        {
            string id;
            string password;

            logo.PrintSignIn();

            while (Constants.INPUT_VALUE)
            {
                id = text.EnterText((int)Constants.SignIn.INPUT, (int)Constants.SignIn.ID, "");                     //아이디 입력
                if (id.Equals(Constants.ESC)) return Constants.ESC;

                password = text.EnterText((int)Constants.SignIn.INPUT, (int)Constants.SignIn.PASSWORD, "*");  //비밀번호 입력
                if (password.Equals(Constants.ESC)) return Constants.ESC;

                if (bookDatabaseManager.IsExistingMember(id, password)) break;  //존재하는 회원 -> 로그인 성공

                logo.PrintSignInFailure();      
            }

            return id;  
        }
    }
}
