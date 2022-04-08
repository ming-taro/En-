using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminController
    {
        public void ControlAdminMode()     //관리자 모드 관리
        {
            Screen screen = new Screen();
            screen.PrintAdminMode();       //관리자 모드 출력
        }
        public void ControlAdminSignIn()  //관리자 로그인 관리
        {
            string id = "";
            string password = "";
            SignIn admin = new SignIn();  //2. 관리자모드(->관리자 로그인 화면으로 이동)
            admin.SignInAdmin(ref id, ref password);   //로그인

            if (id == "123456789" && password == "00000")
            {
                ControlAdminMode();
            }
        }
    }
}
