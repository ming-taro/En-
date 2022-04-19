using MySql.Data.MySqlClient;
using System;

namespace Library
{
    class EditingScreen
    {
        public void PrintProfile(string memberId, LibraryVO library)
        {
            string query = "SELECT*FROM member WHERE id='" + memberId + "';";
            MySqlCommand command = new MySqlCommand(query, library.Connection);
            MySqlDataReader table = command.ExecuteReader();
            LogoScreen logoScreen = new LogoScreen();

            logoScreen.PrintMenu("회원정보수정");
            Console.WriteLine("=======================뒤로가기:[ESC]========================\n");
            Console.WriteLine("         >수정하려는 정보를 선택해 Enter키를 누르세요<");

            table.Read();
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.FIRST);
            Console.WriteLine("☞아이디: " + table["id"].ToString());
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.SECOND);
            Console.WriteLine("☞비밀번호: " + table["password"].ToString());
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.THIRD);
            Console.WriteLine("  비밀번호 재확인: ");
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.FOURTH);
            Console.WriteLine("☞이름: " + table["name"].ToString());
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.FIFTH);
            Console.WriteLine("☞나이: " + table["age"].ToString());
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.SIXTH);
            Console.WriteLine("☞휴대전화: " + table["phoneNumber"].ToString());
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.SEVENTH);
            Console.WriteLine("☞주소: " + table["address"].ToString());
            table.Close();
        }
    }
}
