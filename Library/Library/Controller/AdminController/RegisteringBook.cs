﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class RegisteringBook
    {
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsDuplicateId(string bookId)   //입력한 도서번호가 중복된 아이디인지 검사
        {
            LibraryVO library = LibraryVO.GetLibraryVO();  //도서목록

            MySqlCommand command = new MySqlCommand("select id from book where id='" + bookId + "';", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                table.Close();
                return Constants.IS_DUPLICATE_ID;  //이미 존재하는 아이디
            }

            table.Close();
            return Constants.IS_NON_DUPLICATE_ID;  //중복없는 아이디 -> 입력가능
        }
        public string InputBookId(int left, int top)
        {
            EnteringText text = new EnteringText();
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(left, top, "");        //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))//뒤로가기
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(bookId) || Regex.IsMatch(bookId, @"^[0-9]{1,3}$") == Constants.IS_NOT_MATCH) //양식에 맞지 않은 입력
                {
                    PrintInputBox(0, top + 1, "(0~999사이의 숫자가 아닙니다. 다시 입력해주세요.)          ");
                }
                else if (IsDuplicateId(bookId))      //입력 양식은 맞지만 도서아이디가 이미 존재하는 경우
                {
                    PrintInputBox(0, top + 1, "(이미 존재하는 아이디입니다. 다시 입력해주세요.)          ");
                }
                else
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return bookId;
        }
        public string InputBookName(int left, int top)
        {
            EnteringText text = new EnteringText();
            string bookName;

            while (Constants.INPUT_VALUE)
            {
                bookName = text.EnterText(left, top, "");        //(도서명/출판사)를 입력 받음
                if (bookName.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(bookName) || Regex.IsMatch(bookName, @"^[\w]{1,1}[^\e]{0,49}$") == Constants.IS_NOT_MATCH)
                {
                    PrintInputBox(0, top + 1, "(공백으로 시작하지 않는 50자 이내의 글자를 입력해주세요.)                    ");
                }
                else
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return bookName;
        }
        public string InputAuthor(int left, int top)
        {
            EnteringText text = new EnteringText();
            string author;

            while (Constants.INPUT_VALUE)
            {
                author = text.EnterText(left, top, "");            //저자를 입력받음
                if (author.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(author) || Regex.IsMatch(author, @"^[a-zA-Z가-힣]{1,50}$") == Constants.IS_NOT_MATCH)
                {
                    PrintInputBox(0, top + 1, "(50자 이내의 영어, 한글만 입력해주세요.)                            ");
                }
                else
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return author;
        }
        public string InputPrice(int left, int top)
        {
            EnteringText text = new EnteringText();
            string price;

            while (Constants.INPUT_VALUE)
            {
                price = text.EnterText(left, top, "");             //가격을 입력받음
                if (price.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(price) || Regex.IsMatch(price, @"^[1-9]{1}[0-9]{0,9}") == Constants.IS_NOT_MATCH)
                {
                    PrintInputBox(0, top + 1, "(0이상의 숫자를 10자 이내로 다시 입력해주세요.)                                 ");
                }
                else
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return price;
        }
        public string InputQuantity(int left, int top)
        {
            EnteringText text = new EnteringText();
            string quantity;

            while (Constants.INPUT_VALUE)
            {
                quantity = text.EnterText(left, top, "");                    //수량을 입력받음
                if (quantity.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(quantity) || Regex.IsMatch(quantity, @"^[1-9]{1}[0-9]{0,1}$") == Constants.IS_NOT_MATCH)
                {
                    PrintInputBox(0, top + 1, "(1~99사이의 숫자만 가능합니다. 다시 입력해주세요.)                            ");
                }
                else
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return quantity;
        }
        public void StartRegistration()
        {
            RegisteringScreen screen = new RegisteringScreen();
            screen.PrintRegistering(); //도서등록 입력화면

            string id = InputBookId(10, (int)Constants.Registration.FIRST);              //도서번호
            if (id.Equals(Constants.ESC)) return; 
            string name = InputBookName(8, (int)Constants.Registration.SECOND);          //도서명
            if (name.Equals(Constants.ESC)) return;
            string publisher = InputBookName(8, (int)Constants.Registration.THIRD);      //출판사
            if (publisher.Equals(Constants.ESC)) return;
            string author = InputAuthor(6, (int)Constants.Registration.FOURTH);          //저자
            if (author.Equals(Constants.ESC)) return;
            string price = InputPrice(6, (int)Constants.Registration.FIFTH);             //가격
            if (price.Equals(Constants.ESC)) return;
            string quantity = InputQuantity(6, (int)Constants.Registration.SIXTH);       //수량
            if (quantity.Equals(Constants.ESC)) return;

            LibraryVO library = LibraryVO.GetLibraryVO();  //도서목록
            library.InsertBookList(id, name, publisher, author, price, quantity); //도서목록에 등록된 도서정보 추가

            screen = new RegisteringScreen();
            screen.PrintComplete();                   //등록 완료 화면 출력

            Keyboard keyboard = new Keyboard();
            keyboard.PressESC();                      //esc->종료(뒤로가기)
        }
    }
}
