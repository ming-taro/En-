using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberView
    {
        Logo logo;
        AdminMenu adminMenu;
        public MemberView()
        {
            logo = new Logo();
            adminMenu = new AdminMenu();
        }
        public void PrintMyBookList(List<BookVO> myBookList)
        {
            logo.PrintMessage(20, Console.CursorTop + 1, ">나의 도서 대여 목록<", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            logo.PrintLine();
            for (int i = 0; i < myBookList.Count; i++)
            {
                Console.Write(myBookList[i]);
                logo.RemoveLine(0, Console.CursorTop);    //도서 수량 지움
                logo.PrintLine();
            }
        }
        public void PrintBookIdInputScreen(List<BookVO> bookList)
        {
            logo.PrintSearchBox(Constants.BOOK_ID_TO_BORROW); //도서번호 입력창
            adminMenu.PrintBookList(bookList, logo);                        //도서 검색 결과 출력
        }
        public void PrintBookRentalSuccess(List<BookVO> myBookList)
        {
            logo.PrintMenu("도서 대여 완료");
            PrintMyBookList(myBookList);    //나의 대여 목록 출력
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_AND_ENTER, ConsoleColor.Yellow);
        }
        public void PrintBookReturnSuccess(BookVO book)
        {
            logo.PrintMenu("도서 반납 완료");
            logo.PrintMessage(21, Console.CursorTop + 1, ">반납한 도서 정보<", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            logo.PrintLine();
            Console.Write(book);
            logo.RemoveLine(0, Console.CursorTop);  //수량 지움
            logo.PrintLine();
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_MESSAGE, ConsoleColor.Yellow);
        }
    }
}
