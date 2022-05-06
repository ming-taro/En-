using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BookRental
    {
        private BookDAO bookDAO = BookDAO.GetInstance();
        private LogDAO logDAO = LogDAO.GetInstance();
        private EnteringText text;
        private MemberView memberView;
        private Logo logo;
        
        public BookRental(EnteringText text, MemberView memberView, Logo logo)
        {
            this.text = text;
            this.memberView = memberView;
            this.logo = logo;
        }
        private bool IsQuantityZero(int bookId, List<BookDTO> bookList)   //대여가능 수량이 있는 지 검사
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[bookId - 1].Quantity.Equals("0")) return Constants.IS_QUANTITY_ZERO;  //대여 가능 수량이 0권 -> 대여 불가
            }

            return Constants.IS_QUANTITY_MORE_THAN_ONE;   //대여 가능 수량이 남아있음 -> 대여가능
        }
        private string InputBookId(List<BookDTO> bookList, List<BookDTO> myBookList)    //도서명 입력 후 도서번호 입력(bookList:검색결과 도서목록)
        {
            int top = (int)Constants.SearchMenu.FIRST;
            int left = (int)Constants.InputField.LEFT;
            int exceptionLeft = (int)Constants.SearchMenu.LEFT;
            int exceptionTop = (int)Constants.Exception.TOP;
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(left, top, "");       //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))             
                {
                    return Constants.ESC;                     //도서번호 입력 중 esc -> 다시 도서검색으로
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH || Int32.Parse(bookId) < 1 || Int32.Parse(bookId) > bookList.Count)  
                {
                    logo.PrintMessage(exceptionLeft, exceptionTop, "(1~" + bookList.Count + "사이의 숫자를 입력해주세요.)                             ", ConsoleColor.Red);    //형식에 맞지 않는 입력일 경우
                }
                else if (IsQuantityZero(Int32.Parse(bookId), bookList))   
                {
                    logo.PrintMessage(exceptionLeft, exceptionTop, "(대여가능한 수량이 없습니다.)              ", ConsoleColor.Red);   //도서 수량이 0일 때
                }
                else break;  //도서대여가능 -> 해당 도서번호 리턴

                logo.RemoveLine(left, top);
            }
            return bookId;
        }
        public void SearchBookToBorrow(string memberId, BookSearch bookSearch, Keyboard keyboard)
        {
            int searchType;        //검색유형
            string searchWord;     //검색어
            string bookId;         //도서번호
            List<BookDTO> bookList;
            List<BookDTO> myBookList = bookDAO.GetMyBookList(Constants.RENTAL_LIST, memberId); //현재 로그인한 회원의 도서대여목록

            while (Constants.INPUT_VALUE)
            {
                searchType = bookSearch.SelectSearchType(keyboard);       //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break;  //검색유형 선택 중 esc -> 도서검색 종료

                searchWord = bookSearch.InputSearchWord((int)Constants.InputField.SEARCH, searchType, (int)Constants.Exception.SEARCH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;           //검색어 입력 중 esc -> 검색유형 선택으로

                bookList = bookSearch.BookList;                //도서검색결과 도서목록
                memberView.PrintBookRental(bookList);          //도서검색창 출력

                bookId = InputBookId(bookList, myBookList);    //도서번호를 입력받음
                if (bookId.Equals(Constants.ESC)) continue;    //도서번호 입력 중 esc -> 다시 도서검색으로

                bookDAO.AddToRentalList(memberId, bookList[Int32.Parse(bookId) - 1].Isbn);   //도서대출목록DB에 회원아이디,대출도서isbn,대출기간 저장
                myBookList = bookDAO.GetMyBookList(Constants.RENTAL_LIST, memberId);         //변경된 현재 로그인한 회원의 도서대여목록
                logDAO.AddToRentalList(memberId, myBookList[myBookList.Count - 1].Name);     //도서 대출기록 로그에 저장
                memberView.PrintBookRentalSuccess(myBookList); //회원의 대여목록 출력

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break; //Esc->뒤로가기, Enter->재검색
                Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
            }
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
