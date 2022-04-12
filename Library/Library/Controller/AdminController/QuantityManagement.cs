using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class QuantityManagement
    {
        private int bookIndex;
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsBookInList(string bookId)
        {
            BookListVO bookListVO = BookListVO.GetBookListVO();   //도서목록

            for (int i = 0; i < bookListVO.bookList.Count; i++)
            {
                if (bookListVO.bookList[i].Id.Equals(bookId))
                {
                    bookIndex = i;   
                    return Constants.BOOK_IN_LIST;   //도서명,번호에 해당하는 책을 찾음
                }
            }
            return !Constants.BOOK_IN_LIST;
        }
        public void InputBookId()
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(24, 1);
                bookId = Console.ReadLine();       //도서번호를 입력받음
                if (string.IsNullOrEmpty(bookId) || !Regex.IsMatch(bookId, @"^[0-9]{1,3}$") || !IsBookInList(bookId))
                {
                    PrintInputBox(0, 2, "(목록에 없는 도서번호입니다.다시 입력해주세요.)          ");
                }
                else break;   //도서삭제 가능
                PrintInputBox(24, 1, Constants.REMOVE_LINE);
            }
        }
        public int ControlQuantity()
        {
            QuantityScreen quantityScreen = new QuantityScreen();
            quantityScreen.PrintQuantity();

            InputBookId();  //도서번호를 입력받음



            return Constants.COMPLETE_FUNCTION;
        }
    }
}
