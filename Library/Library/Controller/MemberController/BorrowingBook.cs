using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BorrowingBook
    {
        private List<BookVO> bookList;
        private LibraryVO library;
        public BorrowingBook()
        {
            bookList = new List<BookVO>();
            library = LibraryVO.GetLibraryVO();
        }
        public void PrintBorrowing()
        {
            BorrowingScreen borrowingScreen = new BorrowingScreen();
            borrowingScreen.PrintBorrowing();
        }
        public void PrintInputBox(string message)
        {
            Console.SetCursorPosition(0, 2);
            Console.Write(message);
        }
        public bool IsBookIBorrowed(string memberId, string bookId)
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
            return Constants.BOOK_I_NEVER_BORROWED;   //해당 검색어를 포함하는 도서를 찾지 못함
        }
        public bool IsBookOnList(string searchWord)   //입력값이 중복된 아이디인지 검사
        {
            string query = searchWord;

            MySqlCommand command = new MySqlCommand(query + ";", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                table.Close();
                return Constants.BOOK_IN_LIST;
            }

            table.Close();
            return Constants.BOOK_NOT_IN_LIST;   //해당 검색어를 포함하는 도서를 찾지 못함
        }
        /*public bool IsQuantityZero(string bookId)
        {
            for(int i = 0; i<bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId) && bookList[i].Quantity.Equals("0"))
                {
                    return Constants.QUANTITY_ZERO;
                }
            }
            
            return !Constants.QUANTITY_ZERO;
        }*/
        public string InputBookId(string memberId, string searchWord)          //도서명 입력 후 도서번호 입력
        {
            EnteringText text = new EnteringText();
            Regex regex = new Regex(@"^[0-9]{1,3}$");   //도서번호:숫자,0~999까지
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(20, 1, "");            //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(bookId) || regex.IsMatch(bookId) == Constants.IS_NOT_MATCH) //형식에 맞지 않는 입력일 경우
                {
                    PrintInputBox("(0~999사이의 숫자가 아닙니다.다시 입력해주세요.)               ");
                }
                else if (IsBookOnList(searchWord + " and id=" + bookId) == Constants.BOOK_NOT_IN_LIST)  //입력형식은 맞지만, 목록에 없는 도서를 빌리려고 했을 때
                {
                    PrintInputBox("(현재 조회 목록에 없는 도서번호입니다. 다시 입력해주세요.)           ");
                }
                else if (IsBookIBorrowed(memberId, bookId))  //도서목록에 있지만, 이미 대여중인 도서일 때
                {
                    PrintInputBox("(이미 대여중인 도서입니다. 다른 도서를 선택해주세요.)                  ");
                }
                else if (IsBookOnList(searchWord + " and quantity!=0"))   //도서목록에 있고, 대여하지 않은 도서이지만 수량이 0일 때
                {
                    PrintInputBox("(대여가능한 도서가 0권입니다. 다른 도서를 선택해주세요.)              ");
                }
                else break;  //도서대여가능 -> 해당 도서 아이디 리턴

                Console.SetCursorPosition(20, 1);
                Console.Write(Constants.REMOVE_LINE);
            }
            return bookId;
        }
        public BookVO FindBookInList(string bookId)    //입력받은 도서번호에 해당하는 도서정보 리턴
        {
            int bookIndex;

            for(bookIndex = 0; bookIndex < library.bookList.Count; bookIndex++)
            {
                if (library.bookList[bookIndex].Id.Equals(bookId)) break;   //목록에서 해당 도서를 찾음
            }

            int quantity = int.Parse(library.bookList[bookIndex].Quantity) - 1; //해당 도서의 수량 -1
            library.bookList[bookIndex].Quantity = quantity.ToString();         //도서정보에 변경된 수량 반영
            
            return library.bookList[bookIndex];  
        }
        public void AddBorrowList(string memberId, string bookId)   //도서번호를 알맞게 입력받아 도서대여 완료 -> 현재 도서대여목록에 데이터 추가
        {




            BorrowVO borrowVO = new BorrowVO(memberId, FindBookInList(bookId));
            library.borrowList.Add(borrowVO);  //도서대여목록에 대여한 도서정보와 회원번호 추가
        }
        public void ControlBorrowing(string memberId)
        {
            SearchingScreen searchingScreen = new SearchingScreen();
            BorrowingScreen borrowingScreen = new BorrowingScreen();
            LogoScreen logoScreen = new LogoScreen();

            SearchingBook searchingBook = new SearchingBook();
            string searchWord = searchingBook.SearchBook(); //도서명,출판사, 저자명 검색(리턴값 -> 쿼리문)
            if (searchWord.Equals(Constants.ESC)) return;   //입력 중 esc -> 뒤로가기

            logoScreen.PrintSearchBox("☞대여할 도서 번호:");
            searchingScreen.PrintSearchingBook(searchWord, library); //검색결과로 나온 책목록 출력

            string bookId = InputBookId(memberId, searchWord);     //도서번호를 입력받음
            if (bookId.Equals(Constants.ESC)) return;  //도서번호 입력 중 esc -> 대여종료

            borrowingScreen.PrintSuccessMessage();     //도서대여 완료 메세지 출력
            library.InsertBorrowBook();
        }
    }
}
