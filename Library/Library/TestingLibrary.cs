using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class TestingLibrary
    {
        private List<BookVO> bookList;
        private List<MemberVO> memberList;

        public void InintBookList()
        {
            bookList = new List<BookVO>(); //책목록

            string path = "./text/BookList.txt";
            StreamReader reader = new StreamReader(path);
            while (reader.Peek() >= 0)
            {
                String[] text = reader.ReadLine().ToString().Split(",");     //책 정보를 읽어옴
                bookList.Add(new BookVO(text[0], text[1], text[2], text[3], text[4], text[5])); //초기 책 목록을 리스트에 저장
            }
            reader.Close();
        }
        public void InitMemberList()
        {
            memberList = new List<MemberVO>(); //회원목록

            string path = "./text/MemberList.txt";
            StreamReader reader = new StreamReader(path);
            while (reader.Peek() >= 0)
            {
                String[] text = reader.ReadLine().ToString().Split(",");     //회원 정보를 읽어옴
                memberList.Add(new MemberVO(text[0], text[1], text[2], text[3], text[4], text[5])); //초기 회원 목록을 리스트에 저장
            }
            reader.Close();
        }
        public TestingLibrary()
        {
            InintBookList();   //초기 책목록
            InitMemberList();  //초기 회원목록
            AdminVO admin = new AdminVO();  //관리자
        }
        public void SelectMenu(int left, int right)
        {
            Screen screen = new Screen();

            switch (left, right)
            {
                case (25, 13):
                    MemberMenu memberMenu = new MemberMenu();
                    break;
                case (25, 14):
                    AdminSignUp adminSignUp = new AdminSignUp();
                    break;
            }


        }
        public void TestLibrary()
        {
            string[] menu = { "회원 모드", "관리자 모드", "종료" };  //테스트케이스
            Screen screen = new Screen();
            screen.PrintMain(menu);
            int left = 25, top = 13;
            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(left, ++top);
                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(left, --top);
                        break;
                    case ConsoleKey.Enter:
                        SelectMenu(left, top);
                        break;
                }
            }
        }
    }
}
