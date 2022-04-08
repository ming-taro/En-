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
        private int left = 25, top = 13;
        public const int CLOSE_PROGRAM = -3;
        public const int INVALID_INPUT = -2;
        public const int ESCAPE = -1;
        public const int MOVING_CURSOR = 0;
        public const int ENTERING_MENU = 1;

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
        public void ControlMain()
        {
            SelectMenu(13, 14); //메뉴를 선택함(커서 위치가 해당 메뉴옆에 위치)

            switch (left, top)  //커서의 위치값으로 메뉴를 구분
            {
                case (25, 13):
                    MemberMenu memberMenu = new MemberMenu();     //회원모드
                    break;
                case (25, 14):
                    AdminSignUp adminSignUp = new AdminSignUp();  //관리자모드
                    break;
            }
        }
        public void SelectMenu(int minTop, int maxTop)              //메뉴 고르기
        {
            int entering = MOVING_CURSOR;
            int key;

            while (entering != CLOSE_PROGRAM)
            { 
                key = ControlKeyboard();      //키보드를 입력받음
                //if (top < 13 || top > 14) continue;
                if (top < minTop || top > maxTop) continue;
                switch (key)
                {
                    case ENTERING_MENU:            //메뉴입력 -> 해당 메뉴로 이동(1.회원모드   2.관리자모드)
                        entering = ENTERING_MENU;
                        break;
                    case ESCAPE:
                        entering = CLOSE_PROGRAM;  //뒤로가기(->메인화면으로)
                        break;
                }
            }
        }
        public int ControlKeyboard() //현재 키보드 입력값 반환
        {
            int input = INVALID_INPUT;
            ConsoleKeyInfo keyInfo;

            while (input == INVALID_INPUT)   //다른 키를 입력하면 올바른 키를 입력할때까지 무한루프
            {
                keyInfo = Console.ReadKey(); //키를 입력받음 
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(left, ++top);   //↓방향키 입력
                        return MOVING_CURSOR;
                    case ConsoleKey.UpArrow:                      //↑방향키 입력
                        Console.SetCursorPosition(left, --top);
                        return MOVING_CURSOR;
                    case ConsoleKey.Enter:                        //enter 입력
                        return ENTERING_MENU;
                    case ConsoleKey.Escape:                       //escape 입력
                        return ESCAPE;
                }
            }
            return INVALID_INPUT;
        }
        public void TestLibrary()  //메인화면
        {
            string[] menu = { "회원 모드", "관리자 모드", "종료" }; //메인화면 메뉴 
            Screen screen = new Screen();
            screen.PrintMain(menu);

            int entering = MOVING_CURSOR;
            int key;

            while (entering != CLOSE_PROGRAM)
            {
                Console.SetCursorPosition(left, top);
                key = ControlKeyboard();            //입력받은 키값
                if (top == 12)
                {
                    top = 13;
                    continue;
                }
                else if(top == 16)
                {
                    top = 15;
                    continue;
                }

                switch (key)
                {
                    case ENTERING_MENU:            //메뉴입력 -> 해당 메뉴로 이동
                        ControlMain();
                        break;
                    case ESCAPE:
                        entering = CLOSE_PROGRAM;  //프로그램 종료
                        break;
                }
            }

            Console.SetCursorPosition(25, 20);
        }
    }
}
