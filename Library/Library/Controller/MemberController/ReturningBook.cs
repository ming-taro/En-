﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class ReturningBook
    {
        private List<BookVO> myBorrowList;
        public ReturningBook(string memberId)
        {
            myBorrowList = new List<BookVO>();                           //나의 도서 대여 목록
            BorrowListVO borrowListVO = BorrowListVO.GetBorrowListVO();  //도서목록

            for (int i = 0; i < borrowListVO.borrowList.Count; i++)
            {
                if (borrowListVO.borrowList[i].MemberId.Equals(memberId))
                {
                    myBorrowList.Add(borrowListVO.borrowList[i].BookVO); //나의 도서 대여 목록에 책 정보 저장
                }
            }

            ReturningScreen returningScreen = new ReturningScreen();
            returningScreen.PrintReturning(myBorrowList);                 //회원의 도서대여목록 출력
        }
        public bool IsBookIBorrowed(string bookId)
        {
            for (int i = 0; i < myBorrowList.Count; i++)
            {
                if (myBorrowList[i].Id.Equals(bookId))  //나의 대여목록과 반납하려는 대여도서의 번호가 일치할 경우
                {
                    myBorrowList.RemoveAt(i);   //나의 대여목록에서 반납한 책 정보 삭제
                    return Constants.BOOK_I_BORROWED;   
                }
            }
            return !Constants.BOOK_I_BORROWED;
        }
        public string InputBookId()
        {
            ReturningScreen returningScreen = new ReturningScreen();
            Regex regex = new Regex(@"^[0-9]{1,3}$");
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(20, 1);
                bookId = Console.ReadLine();
                if (string.IsNullOrEmpty(bookId) || !regex.IsMatch(bookId))  //양식에 맞지 않는 입력
                {
                    returningScreen.PrintErrorMessage("(0~999사이의 숫자가 아닙니다. 다시 입력해주세요.)");
                }
                else if (!IsBookIBorrowed(bookId)) //양식은 지켰지만, 내가 대여한 도서가 아닌 도서를 반납하려 할 때
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
        public void RemoveInBorrowListVO(string memberId, string bookId)
        {
            BorrowListVO borrowListVO = BorrowListVO.GetBorrowListVO();   //대여 도서 목록

            for(int i=0; i<borrowListVO.borrowList.Count; i++)
            {
                if(borrowListVO.borrowList[i].MemberId.Equals(memberId) && borrowListVO.borrowList[i].BookVO.Id.Equals(bookId))
                {
                    borrowListVO.borrowList.RemoveAt(i);   //대여도서목록에서 반납한 도서 데이터 삭제
                    break;
                }
            }
        }
        public void AddInBookListVO(string bookId)
        {
            BookListVO bookListVO = BookListVO.GetBookListVO();
            int i;

            for(i = 0; i<bookListVO.bookList.Count; i++)
            {
                if (bookListVO.bookList[i].Id.Equals(bookId)) break;//반납한 도서 아이디를 도서목록에서 찾음
            }

            int quantity = int.Parse(bookListVO.bookList[i].Quantity) + 1; //해당 도서 수량을 +1만큼
            bookListVO.bookList[i].Quantity = quantity.ToString();
        }
        public int ControlReturning(string memberId)
        {
            Keyboard keyboard = new Keyboard(0, 1);
            int menu = keyboard.SelectMenu(1, 1, 0);
            if (menu == Constants.ESCAPE) return Constants.ESCAPE;   //뒤로가기 -> 회원모드로 돌아감

            string bookId = InputBookId();                       //반납하려는 도서번호
            RemoveInBorrowListVO(memberId, bookId);              //대여도서목록에서 반납한 도서 데이터 삭제
            AddInBookListVO(bookId);
            ReturningScreen returningScreen = new ReturningScreen();
            returningScreen.PrintSuccessMessage(myBorrowList);   //반납 후 나의 대여 도서 목록 출력

            return Constants.COMPLETE_FUNCTION;       //반납 완료 후 나의 대여도서목록출력까지 모두 완료
        }
    }
}
