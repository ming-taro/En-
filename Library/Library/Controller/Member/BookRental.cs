﻿using MySql.Data.MySqlClient;
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
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private MemberView memberView;
        private Logo logo;
        
        public BookRental(BookDAO bookDatabaseManager, EnteringText text, Logo logo, MemberView memberView)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            this.text = text;
            this.logo = logo;
            this.memberView = memberView;
        }

        private bool IsBookInList(string bookId, List<BookVO> bookList)   //현재 조회중인 도서목록에 있는 도서인지 검사
        {
            for (int i = 0; i < bookList.Count; i++) 
            {
                if (bookList[i].Id.Equals(bookId)) return Constants.IS_BOOK_IN_LIST;  //목록에 존재하는 도서
            }

            return Constants.IS_BOOK_NOT_IN_LIST;    //현재 조회중인 도서목록에 없는 도서를 대여하려고 함
        }
        private bool IsBookIBorrowed(string bookId, List<BorrowBookVO> myBookList)              //회원이 대여중인 도서인지 검사
        {
            for (int i = 0; i < myBookList.Count; i++)
            {
                if (myBookList[i].BookId.Equals(bookId)) return Constants.IS_BOOK_I_BORROWED;   //입력한 도서를 이미 대여중 -> 대여할 수 없음
            }

            return Constants.IS_BOOK_I_NEVER_BORROWED;  //대여하지 않은 도서 -> 대여가능
        }
        private bool IsQuantityZero(string bookId, List<BookVO> bookList)   //대여가능 수량이 있는 지 검사
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId) && bookList[i].Quantity.Equals("0")) return Constants.IS_QUANTITY_ZERO;  //대여 가능 수량이 0권 -> 대여 불가
            }

            return Constants.IS_QUANTITY_MORE_THAN_ONE;   //대여 가능 수량이 남아있음 -> 대여가능
        }
        private string InputBookId(List<BookVO> bookList, List<BorrowBookVO> myBookList)    //도서명 입력 후 도서번호 입력
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(20, (int)Constants.SearchMenu.FIRST, "");           //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))  //도서번호 입력 중 esc -> 다시 도서검색으로
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //형식에 맞지 않는 입력일 경우
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_ID_NOT_MATCH, ConsoleColor.Red);
                }
                else if (IsBookInList(bookId, bookList) == Constants.IS_BOOK_NOT_IN_LIST)  //입력형식은 맞지만, 목록에 없는 도서를 빌리려고 했을 때
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_NOT_IN_LIST, ConsoleColor.Red);
                }
                else if (IsBookIBorrowed(bookId, myBookList)) //도서목록에 있지만, 이미 대여중인 도서일 때
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_I_BORROWED, ConsoleColor.Red);
                }
                else if (IsQuantityZero(bookId, bookList))   //도서목록에 있고, 대여하지 않은 도서이지만 수량이 0일 때
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_QUANTITY_ZERO, ConsoleColor.Red);
                }
                else break;  //도서대여가능 -> 해당 도서 아이디 리턴

                logo.RemoveLine(20, (int)Constants.SearchMenu.FIRST);
            }
            return bookId;
        }
        public void SearchBookToBorrow(string memberId, BookSearch searchingBook, Keyboard keyboard)
        {
            int searchType;        //검색유형
            string searchWord;     //검색어
            string bookId;         //도서번호
            List<BorrowBookVO> myBookList = bookDatabaseManager.MakeMyBookList(Constants.RENTAL_LIST, memberId); //현재 로그인한 회원의 도서대여목록

            while (Constants.INPUT_VALUE)
            {
                searchType = searchingBook.SelectSearchType(keyboard);    //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break;  //검색유형 선택 중 esc -> 도서검색 종료

                searchWord = searchingBook.InputSearchWord((int)Constants.SearchMenu.LEFT_VALUE_OF_INPUT, searchType, (int)Constants.SearchMenu.FOURTH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;           //검색어 입력 중 esc -> 검색유형 선택으로

                memberView.PrintBookIdInputScreen(searchingBook.BookList);//도서검색창 출력

                bookId = InputBookId(searchingBook.BookList, myBookList); //도서번호를 입력받음
                if (bookId.Equals(Constants.ESC)) continue;               //도서번호 입력 중 esc -> 다시 도서검색으로

                bookDatabaseManager.AddToRentalList(memberId, bookId);    //DB에 변경된 정보 저장
                myBookList = bookDatabaseManager.MakeMyBookList(Constants.RENTAL_LIST, memberId);//변경된 현재 로그인한 회원의 도서대여목록
                memberView.PrintBookRentalSuccess(myBookList);            //회원의 대여목록 출력

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break; //Esc->뒤로가기, Enter->재검색
            }
        }
    }
}