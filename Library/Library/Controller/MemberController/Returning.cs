using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class Returning
    {
        private List<BookVO> myBorrowList;
        public Returning(string memberId)
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
        public bool IsBookIBorrowed(string memberId)
        {
            BorrowListVO borrowListVO = BorrowListVO.GetBorrowListVO();

            for (int i = 0; i < borrowListVO.borrowList.Count; i++)
            {
                if (borrowListVO.borrowList[i].MemberId.Equals(memberId))
                {
                    return Constants.BOOK_I_BORROWED;   //나의 대여목록에 있는 책일 경우
                }
            }
            return !Constants.BOOK_I_BORROWED;
        }
        public void ControlReturning()
        {
            ReturningScreen returningScreen = new ReturningScreen();
            Regex regex = new Regex(@"^[0-9]{1,3}$");
            string bookName;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(20, 1);
                bookName = Console.ReadLine();
                if(string.IsNullOrEmpty(bookName) || !regex.IsMatch(bookName))  //양식에 맞지 않는 입력
                {
                    returningScreen.PrintErrorMessage("(0~999사이의 숫자가 아닙니다. 다시 입력해주세요.)");
                }
            }
            
        }
    }
}
