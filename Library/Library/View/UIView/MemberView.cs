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
        public void PrintMyBookList(List<BookVO> myBookList)
        {
            logo.PrintLine();
            for (int i = 0; i < myBookList.Count; i++)
            {
                Console.Write(myBookList[i]);
                logo.RemoveLine(0, Console.CursorTop);
                logo.PrintLine();
            }
        }
        public void PrintBookIdInputScreen(List<BookVO> bookList)
        {
            logo.PrintSearchBox(Constants.BOOK_ID_TO_BORROW); //도서번호 입력창
            PrintMyBookList(bookList);                        //도서 검색 결과 출력
        }
        public void PrintBookRentalSuccess(List<BookVO> myBookList)
        {
            logo.PrintMenu("도서 대여 완료");
            logo.PrintMessage(20, Console.CursorTop + 1, ">나의 도서 대여 목록<", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            PrintMyBookList(myBookList);    //나의 대여 목록 출력
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_AND_ENTER, ConsoleColor.Yellow);
        }
        public void PrintBookReturnSuccess()
        {
            logo.PrintMenu("도서 반납 완료");
            logo.PrintMessage(20, Console.CursorTop + 1, ">반납한 도서 정보<", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_AND_ENTER, ConsoleColor.Yellow);
        }
    }
}
