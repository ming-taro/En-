using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminController
    {
        Value value = new Value();
        
        public void ControlAdminSignIn()  //관리자 로그인 관리
        {
            string id = "";
            string password = "";
            SignIn admin = new SignIn();  //2. 관리자모드(->관리자 로그인 화면으로 이동)
            bool input = value.WRONG_VALUE;

            while (input == value.WRONG_VALUE)
            {
                admin.SignInAdmin(ref id, ref password);   //로그인
                if (id == "123456789" && password == "00000")
                {
                    ControlAdminMode();  //관리자 모드로
                    input = value.RIGHT_VALUE;
                }
                else
                {
                    admin.PrintScreen();//로그인 실패시 다시 입력(+오류 메세지)
                    Console.WriteLine("\n\n"+value.SIGN_IN_ERROR);
                }
            }
        }
        public void ControlAdminMode()     //관리자 모드 관리
        {
            Screen screen = new Screen();
            screen.PrintAdminMode();       //관리자 모드 출력

            TestingLibrary testingLibrary = new TestingLibrary();
            bool isSelectMenu = testingLibrary.SelectMenu(13, 17);   //메뉴선택
            int menu = testingLibrary.GetTop();
            Console.WriteLine();

            switch (menu)
            {

            }
        }
    }
}
