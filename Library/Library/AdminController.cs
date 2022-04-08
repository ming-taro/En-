using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminController
    {
        Value value = new Value();
        private List<BookVO> bookList;   //책목록
        public AdminController()
        {
            InintBookList();
        }
        public void InintBookList()
        {
            bookList = new List<BookVO>(); 

            string path = "./text/BookList.txt";
            StreamReader reader = new StreamReader(path);
            while (reader.Peek() >= 0)
            {
                String[] text = reader.ReadLine().ToString().Split(",");     //책 정보를 읽어옴
                bookList.Add(new BookVO(text[0], text[1], text[2], text[3], text[4], text[5])); //초기 책 목록을 리스트에 저장
            }
            reader.Close();
        }
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
            testingLibrary.InitCursorPosition();
            bool isSelectMenu = testingLibrary.SelectMenu(13, 17);   //메뉴선택 완료
            int menu = testingLibrary.GetTop();  //메뉴의 해당 커서값
            Console.WriteLine();

            switch (menu)
            {
                case 13:  //도서검색
                    SearchingBook searchingBook= new SearchingBook(bookList);
                    ControlSearchingBook();
                    break;
            }
        }
        public void ControlSearchingBook()
        {
            Screen screen = new Screen();
            screen.PrintSearchingBook(bookList);

            TestingLibrary testingLibrary = new TestingLibrary();
            testingLibrary.SetPosition(0, 1);     //커서위치 시작점(left = 0, top = 1)
            bool isSelectMenu = testingLibrary.SelectMenu(1, 3);  //메뉴선택
            int menu = testingLibrary.GetTop();   //메뉴선택 완료(1.도서명  2.출판사  3.저자)

            Console.SetCursorPosition(10, menu);
            string bookName = Console.ReadLine();

            switch (menu)
            {
                case 1:
                    screen.PrintSearchingName(bookName, bookList);
                    Console.ReadLine();
                    break;
            }

        }
    }
}
