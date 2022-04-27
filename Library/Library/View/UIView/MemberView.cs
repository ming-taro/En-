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
        public MemberView()
        {
            logo = new Logo();
        }
        public void PrintBookList(List<BookVO> bookList)
        {
            logo.PrintLine();
            for (int i = 0; i < bookList.Count; i++)
            {
                Console.WriteLine("도서번호: " + bookList[i].Id);
                Console.WriteLine("도서명: " + bookList[i].Name);
                Console.WriteLine("출판사: " + bookList[i].Publisher);
                Console.WriteLine("저자: " + bookList[i].Author);
                Console.WriteLine("가격: " + bookList[i].Price);
                logo.PrintLine();
            }
        }
        public void PrintBookIdInputScreen(List<BookVO> bookList)
        {
            logo.PrintSearchBox(Constants.BOOK_ID_TO_BORROW); //도서번호 입력창
            PrintBookList(bookList);                          //도서 검색 결과 출력
        }
        public void PrintBookRentalSuccess(List<BookVO> myBookList)
        {
            logo.PrintMenu("도서 대여 완료");
            logo.PrintMessage(20, Console.CursorTop + 1, ">나의 도서 대여 목록<", ConsoleColor.Gray);
            PrintBookList(myBookList);    //나의 대여 목록 출력
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_AND_ENTER, ConsoleColor.Yellow);
        }
    }
}
