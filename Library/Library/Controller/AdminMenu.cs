using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminMenu
    {
    }
    class AdminMode
    {
        public AdminMode()
        {
            Screen screen = new Screen();
            string[] menu = { "도서 검색", "도서 등록", "도서 수량 관리", "도서 삭제", "회원정보 관리" };
            screen.PrintMain(menu);
        }
    }
    class SearchingBook  //1.도서 검색
    {
        private BookListVO bookListVO = BookListVO.getBookListVO();  //도서목록
        SearchingScreen screen;
        public SearchingBook()   //도서검색화면 출력
        {
            screen = new SearchingScreen();
            screen.PrintSearchingBook(bookListVO.bookList);
        }
        public int ControlSearchingBook()  //도서검색하기(검색어 입력 -> 목록 출력)
        {
            StartingLibrary testingLibrary = new StartingLibrary();
            testingLibrary.SetPosition(0, 1);     //커서위치
            int menu = testingLibrary.SelectMenu(1, 3);               //메뉴선택
            if (menu == Constants.ESCAPE) return Constants.ESCAPE;    //메뉴선택도중 뒤로가기 -> 관리자 메뉴로 돌아감

            menu = testingLibrary.GetTop();       //메뉴선택 완료(1.도서명  2.출판사  3.저자)
            Console.SetCursorPosition(10, menu);  //커서위치
            string name = Console.ReadLine();     //검색어 입력받기(------------------>검색어 입력도중 뒤로가기, 입력예외처리 추가 필요)
            screen.PrintSearchingBook(menu, name, bookListVO.bookList);   //검색결과로 나온 책목록 출력

            return Constants.COMPLETE_FUNCTION;       //검색결과 출력까지 모두 완료
        }
    }

    class RegisteringBook
    {
        public RegisteringBook()
        {
            RegisteringScreen screen = new RegisteringScreen();
            screen.PrintRegistering(); //도서등록 입력화면
        }
        public int ControlRegistering()
        {
            string[] book = new string[6];
            Console.SetCursorPosition(10, 7);
            book[0] = Console.ReadLine();  //도서번호
            Console.SetCursorPosition(8, 9);
            book[1] = Console.ReadLine();  //도서명
            Console.SetCursorPosition(8, 11);
            book[2] = Console.ReadLine();  //출판사
            Console.SetCursorPosition(6, 13);
            book[3] = Console.ReadLine();  //저자
            Console.SetCursorPosition(6, 15);
            book[4] = Console.ReadLine();  //가격
            Console.SetCursorPosition(6, 17);
            book[5] = Console.ReadLine();  //수량

            BookListVO bookListVO = BookListVO.getBookListVO();  //도서목록
            bookListVO.bookList.Add(new BookVO(book[0], book[1], book[2], book[3], book[4], book[5])); //도서목록에 등록된 도서정보 추가

            return Constants.COMPLETE_FUNCTION;       //도서등록까지 모두 완료
        }
    }
}
