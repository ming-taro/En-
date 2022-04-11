using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class SearchingBook
    {
        private BookListVO bookListVO = BookListVO.GetBookListVO();  //도서목록
        SearchingScreen screen;
        public SearchingBook()   //도서검색화면 출력
        {
            screen = new SearchingScreen();
            screen.PrintSearchingBook(bookListVO.bookList);
        }
        public int ControlSearchingBook()  //도서검색하기(검색어 입력 -> 목록 출력)
        {
            Keyboard testingLibrary = new Keyboard();
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
}
