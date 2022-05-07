using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BookRegistration
    {
        private BookDAO bookDAO = BookDAO.GetInstance();
        private LogDAO logDAO = LogDAO.GetInstance();
        private EnteringText text;
        private AdminView adminView;
        private Logo logo;

        public BookRegistration(EnteringText text, AdminView adminView, Logo logo)
        {
            this.text = text;
            this.adminView = adminView;
            this.logo = logo;
        } 
        private void PrintErrorMessage(int left, int top, string regexText)  //예외메세지
        {
            switch (regexText)
            {
                case Constants.BOOK_NAME_REGEX: //도서명
                    logo.PrintMessage(left, top, "(1~50자 이내의 문자를 입력해주세요.)", ConsoleColor.Red);
                    break;
                case Constants.AUTHOR_REGEX:   //저자
                    logo.PrintMessage(left, top, "(50자 이내의 영어, 한글만 입력해주세요.)", ConsoleColor.Red);
                    break;
                case Constants.PUBLICATION_DATE_REGEX:   //출판일
                    logo.PrintMessage(left, top, "(날짜 형식에 맞춰 입력해주세요.(ex: 2022.05.06))", ConsoleColor.Red);
                    break;
                case Constants.ISBN_REGEX:    //isbn
                    logo.PrintMessage(left, top, "(50자 이내의 숫자를 입력해주세요.)", ConsoleColor.Red);
                    break;
                case Constants.PRICE_REGEX:    //가격
                    logo.PrintMessage(left, top, "(1~1,000,000,000원 이내의 숫자를 입력해주세요.)", ConsoleColor.Red);
                    break;
                case Constants.BOOK_INTRODUCTION_REGEX:  //책소개
                    logo.PrintMessage(left, top, "(100자 이내로 입력해주세요.)", ConsoleColor.Red);
                    break;
                case Constants.QUANTITY_REGEX:   //수량
                    logo.PrintMessage(left, top, "(1~99권 이내의 숫자를 입력해주세요.)", ConsoleColor.Red);
                    break;
            }
        }
        public string InputBookName(int left, int top, string regexText)
        {
            string bookName;

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(left, top);
                bookName = text.EnterText(left, top, "");        //도서명을 입력 받음

                if (bookName.Equals(Constants.ESC) == Constants.IS_NOT_MATCH && Regex.IsMatch(bookName, regexText) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    PrintErrorMessage(left, top + 1, regexText);
                }
                else break;
            }

            if (bookName.Equals(Constants.ESC)) logo.RemoveLine(left, top); //입력 중 Esc -> 입력값 지움
            else logo.RemoveLine(left, top + 1);  //알맞은 값 입력 -> 예외메세지 지움

            return bookName;
        }
        public void ReflectChangeInDTO(int menu, string changedItem, BookDTO book)  //수정된 항목 VO에 반영
        {
            switch (menu)
            {
                case (int)Constants.EditMenu.FIRST:  //도서명
                    book.Name = changedItem;
                    break;
                case (int)Constants.EditMenu.SECOND: //저자  
                    book.Author = changedItem;
                    break;
                case (int)Constants.EditMenu.THIRD:  //출판사
                    book.Publisher = changedItem;
                    break;
                case (int)Constants.EditMenu.FOURTH: //출판일
                    book.PublicationDate = changedItem;
                    break;
                case (int)Constants.EditMenu.FIFTH:  //isbn
                    book.Isbn = changedItem;
                    break;
                case (int)Constants.EditMenu.SIXTH:  //가격
                    book.Price = changedItem;
                    break;
                case (int)Constants.EditMenu.SEVENTH://책소개
                    book.BookIntroduction = changedItem;
                    break;
                case (int)Constants.EditMenu.EIGHT:  //수량
                    book.Quantity = changedItem;
                    break;
            }
        }
        public string InputBookInformation(int menu, BookDTO book)  //수정할 도서정보입력
        {
            int left = (int)Constants.InputField.BOOK_EDITION;
            string changedItem = "";

            switch (menu)
            {
                case (int)Constants.EditMenu.FIRST:     //도서명
                    changedItem = InputBookName(left, (int)Constants.EditMenu.FIRST, Constants.BOOK_NAME_REGEX);
                    break;
                case (int)Constants.EditMenu.SECOND:    //저자
                    changedItem = InputBookName(left, (int)Constants.EditMenu.SECOND, Constants.AUTHOR_REGEX);
                    break;
                case (int)Constants.EditMenu.THIRD:     //출판사
                    changedItem = InputBookName(left, (int)Constants.EditMenu.THIRD, Constants.PUBLISHER_REGEX);
                    break;
                case (int)Constants.EditMenu.FOURTH:    //출판일 
                    changedItem = InputBookName(left, (int)Constants.EditMenu.FOURTH, Constants.PUBLICATION_DATE_REGEX);
                    break;
                case (int)Constants.EditMenu.FIFTH:     //isbn  
                    changedItem = InputBookName(left, (int)Constants.EditMenu.FIFTH, Constants.ISBN_REGEX);
                    break;
                case (int)Constants.EditMenu.SIXTH:     //가격 
                    changedItem = InputBookName(left, (int)Constants.EditMenu.SIXTH, Constants.PRICE_REGEX);
                    break;
                case (int)Constants.EditMenu.SEVENTH:   //책소개
                    changedItem = InputBookName(left, (int)Constants.EditMenu.SEVENTH, Constants.BOOK_INTRODUCTION_REGEX);
                    break;
                case (int)Constants.EditMenu.EIGHT:     //수량
                    changedItem = InputBookName(left, (int)Constants.EditMenu.EIGHT, Constants.QUANTITY_REGEX);
                    break;
            }

            return changedItem;
        }
        public bool IsRegisterable(BookDTO book)
        {
            if(book.Name == "" || book.Author == "" || book.Publisher == "" || book.PublicationDate == "" || book.Isbn == "" || book.Price == "" || book.Quantity == "")
            {
                logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.MESSAGE, "                               (책소개 외의 항목은 필수입력정보입니다.)", ConsoleColor.Red);
                return Constants.IS_NOT_REGISTERABLE; 
            }

            return Constants.IS_REGISTERABLE;
        }
        public void ManageBookRegistration(Keyboard keyboard)
        {
            BookDTO book = new BookDTO("", "", "", "", "", "", "", "", "");
            int menu;
            string changedItem;
            bool isRegisterable = Constants.IS_NOT_REGISTERABLE;

            adminView.PrintBookRevision(book, "도서 등록", "등록");   //도서등록화면 출력

            while (Constants.INPUT_VALUE)
            {
                keyboard.SetPosition((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.FIRST);
                menu = keyboard.SelectMenu((int)Constants.EditMenu.FIRST, (int)Constants.EditMenu.NINTH, (int)Constants.EditMenu.STEP);
                if (menu == (int)Constants.Keyboard.ESCAPE) return;    //입력할 항목 선택 중 Esc -> 종료
                menu = keyboard.Top;

                changedItem = InputBookInformation(menu, book);        //도서정보 입력
                if (changedItem.Equals(Constants.ESC)) continue;       //입력도중 Esc -> 입력항목 선택
                ReflectChangeInDTO(menu, changedItem, book);           //DTO에 입력한 정보 넣기

                if (menu == (int)Constants.EditMenu.NINTH) isRegisterable = IsRegisterable(book);    //등록버튼을 눌렀지만 필수입력정보를 다 입력하지 않았을 때
                if (isRegisterable == Constants.IS_REGISTERABLE) break;//도서등록 가능
            }

            bookDAO.AddToBookList(Constants.ADDITION_TO_BOOK_LIST, book);  //DB에 도서정보 저장
            logDAO.RegisterBook(book.Name);                                //로그에 도서등록 기록

            adminView.PrintRegisteredBook(book);              //등록 완료 화면 출력
            keyboard.PressESC();                              //Esc -> 종료(뒤로가기)
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
