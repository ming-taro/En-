using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class ReturningBook
    {
        private LibraryVO library;
        public ReturningBook()
        {
            library = LibraryVO.GetLibraryVO();   
        }
        private bool IsBookIBorrowed(string memberId, string bookId)
        {
            string query = "select*from borrowBook where memberId='" + memberId + "' and bookId=" + bookId;

            MySqlCommand command = new MySqlCommand(query + ";", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                table.Close();
                return Constants.BOOK_I_BORROWED;
            }

            table.Close();
            return Constants.BOOK_I_NEVER_BORROWED;   //해당 도서를 빌린 적이 없음
        }
        private string InputBookId(string memberId)
        {
            ReturningScreen returningScreen = new ReturningScreen();
            EnteringText text = new EnteringText();
            Regex regex = new Regex(@"^[0-9]{1,3}$");
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(20, 1, "");//도서번호 입력

                if (bookId.Equals(Constants.ESC))  //esc->뒤로가기
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(bookId) || regex.IsMatch(bookId) == Constants.IS_NOT_MATCH)  //양식에 맞지 않는 입력
                {
                    returningScreen.PrintErrorMessage("(0~999사이의 숫자가 아닙니다. 다시 입력해주세요.)");
                }
                else if (IsBookIBorrowed(memberId, bookId) == Constants.BOOK_I_NEVER_BORROWED) //양식은 지켰지만, 내가 대여한 도서가 아닌 도서를 반납하려 할 때
                {
                    returningScreen.PrintErrorMessage("(대여하지 않은 도서번호입니다. 다시 입력해주세요.)");
                }
                else
                {
                    break;   //반납 성공
                }
            }

            return bookId;
        }
        private void AddInBookListVO(string bookId)
        {
            int bookIndex;

            for(bookIndex = 0; bookIndex < library.bookList.Count; bookIndex++)
            {
                if (library.bookList[bookIndex].Id.Equals(bookId)) break;//반납한 도서 아이디를 도서목록에서 찾음
            }

            int quantity = int.Parse(library.bookList[bookIndex].Quantity) + 1; //해당 도서 수량을 +1만큼
            library.bookList[bookIndex].Quantity = quantity.ToString();
        }
        public void ShowMyBookList(string memberId)
        {
            AdminMenu searchingScreen = new AdminMenu();
            LogoScreen logoScreen = new LogoScreen();
            Keyboard keyboard = new Keyboard();

            logoScreen.PrintSearchBox("☞반납할 도서 번호:");
            searchingScreen.PrintSearchingBook("select*from book,borrowBook where borrowBook.memberId='" + memberId + "' and borrowBook.bookId=book.id", library);
    
            string bookId = InputBookId(memberId);               //반납하려는 도서번호
            if (bookId.Equals(Constants.ESC)) return;            //도서번호 입력 중 esc -> 뒤로가기

            library.DeleteBorrowBook(memberId, bookId);          //대여도서목록에서 반납한 도서 데이터 삭제, 수량+1

            logoScreen.PrintMenu("도서반납 완료");
            searchingScreen.PrintSearchingBook("select*from book,borrowBook where borrowBook.memberId='" + memberId + "' and borrowBook.bookId=book.id", library);

            keyboard.PressESC(); //esc -> 뒤로가기
        }
    }
}
