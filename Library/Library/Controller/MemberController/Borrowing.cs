using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class Borrowing
    {
        private List<BookVO> bookList;   
        public Borrowing()
        {
            bookList = new List<BookVO>();
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
        public void AddBookOnList(string bookName)   //입력받은 도서명을 포함하는 도서목록리스트 생성
        {
            BookListVO bookListVO = BookListVO.GetBookListVO();

            for(int i=0; i<bookListVO.bookList.Count; i++)
            {
                if (bookListVO.bookList[i].Name.Contains(bookName))
                {
                    bookList.Add(bookListVO.bookList[i]);   //입력된 도서명을 포함하는 도서를 찾아 리스트에 저장
                }
            }
        }
        public bool IsBookIBorrowed(string memberId, string bookId)
        {
            BorrowListVO borrowListVO = BorrowListVO.GetBorrowListVO();  //대여목록

            for(int i=0; i<borrowListVO.borrowList.Count; i++)
            {
                //대여리스트에서 회원아이디와 책아이디를 비교 -> 현재 대여중인 도서인지 확인
                if (borrowListVO.borrowList[i].MemberId.Equals(memberId) && borrowListVO.borrowList[i].BookVO.Id.Equals(bookId))
                {
                    return Constants.BOOK_I_BORROWED; //이미 대여중인 도서 -> 도서 대여 불가
                }
            }

            return !Constants.BOOK_I_BORROWED;        //빌린 적 없는 도서 -> 도서 대여 가능함
        }
        public bool IsBookInList(string bookName)   //입력받은 도서명 -> 도서목록에 존재하는 책인지 확인
        {
            BookListVO bookListVO = BookListVO.GetBookListVO();

            for (int i = 0; i < bookListVO.bookList.Count; i++)
            {
                if (bookListVO.bookList[i].Name.Contains(bookName))
                {
                    return Constants.BOOK_IN_LIST;   //입력된 도서명이 목록에 있음
                }
            }
            return !Constants.BOOK_IN_LIST;
        }
        public bool IsDuplicateId(string bookId)   //입력값이 중복된 아이디인지 검사
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId))
                {
                    return Constants.DUPLICATE_ID;  //도서목록에 존재함
                }
            }
            return !Constants.DUPLICATE_ID;  //도서목록에 없는 책을 대여하려 함
        }
        public bool IsQuantityZero(string bookId)
        {
            for(int i = 0; i<bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId) && bookList[i].Quantity.Equals("0"))
                {
                    return Constants.QUANTITY_ZERO;
                }
            }
            
            return !Constants.QUANTITY_ZERO;
        }
        public string InputBookName()
        {
            Regex regex = new Regex(@"^[\w]{1,1}[^\e]{0,49}$");   //도서명: 1~50자 이내
            string bookName;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(15, 1);
                bookName = Console.ReadLine();                   //도서명을 입력 받음
                if (string.IsNullOrEmpty(bookName) || !regex.IsMatch(bookName))  //입력형식에 맞지 않은 경우
                {
                    PrintInputBox("\n(공백으로 시작하지 않는 50자 이내의 글자를 입력해주세요.)                    ");
                }
                else if(!IsBookInList(bookName))  //입력형식은 맞지만, 도서목록에 없는 도서명일 경우
                {
                    PrintInputBox("\n(검색어를 포함하는 도서가 없습니다. 다시 입력해주세요.)                    ");
                }
                else break; ;  //도서목록에 있는 도서명을 검색한 경우

                Console.SetCursorPosition(0, 1);
                Console.WriteLine("☞도서명 검색:                                                      ");
            }
            return bookName;
        }
        public string InputBookId(string memberId)          //도서명 입력 후 도서번호 입력
        {
            Regex regex = new Regex(@"^[0-9]{1,3}$");   //도서번호:숫자,0~999까지
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("☞대여할 도서 번호:                                                   ");
                Console.SetCursorPosition(20, 1);
                bookId = Console.ReadLine();            //도서번호를 입력 받음
                if (string.IsNullOrEmpty(bookId) || !regex.IsMatch(bookId)) //형식에 맞지 않는 입력일 경우
                {
                    PrintInputBox("(0~999사이의 숫자가 아닙니다.다시 입력해주세요.)               ");
                }
                else if (!IsDuplicateId(bookId))  //입력형식은 맞지만, 목록에 없는 도서를 빌리려고 했을 때
                {
                    PrintInputBox("(현재 조회 목록에 없는 도서번호입니다. 다시 입력해주세요.)           ");
                    //return Constants.RE_ENTER;    //도서명부터 다시 검색
                }
                else if (IsBookIBorrowed(memberId, bookId))  //도서목록에 있지만, 이미 대여중인 도서일 때
                {
                    PrintInputBox("(이미 대여중인 도서입니다. 다른 도서를 선택해주세요.)                  ");
                }
                else if (IsQuantityZero(bookId))   //도서목록에 있고, 대여하지 않은 도서이지만 수량이 0일 때
                {
                    PrintInputBox("(대여가능한 도서가 0권입니다. 다른 도서를 선택해주세요.)              ");
                    //return Constants.RE_ENTER;    //도서명부터 다시 검색
                }
                else break;  //도서대여가능 -> 해당 도서 아이디 리턴
            }
            return bookId;
        }
        public BookVO FindBookInList(string bookId)    //입력받은 도서번호에 해당하는 도서정보 리턴
        {
            BookListVO bookListVO = BookListVO.GetBookListVO();  //도서목록
            int i;

            for(i = 0; i<bookListVO.bookList.Count; i++)
            {
                if (bookListVO.bookList[i].Id.Equals(bookId)) break;   //목록에서 해당 도서를 찾음
            }

            int quantity = int.Parse(bookListVO.bookList[i].Quantity) - 1; //해당 도서의 수량 -1
            bookListVO.bookList[i].Quantity = quantity.ToString();         //도서정보에 변경된 수량 반영
            return bookListVO.bookList[i];  
        }
        public void AddBorrowList(string memberId, string bookId)   //도서번호를 알맞게 입력받아 도서대여 완료 -> 현재 도서대여목록에 데이터 추가
        {
            BorrowListVO borrowListVO = BorrowListVO.GetBorrowListVO();  //도서대여목록
            BorrowVO borrowVO = new BorrowVO(memberId, FindBookInList(bookId));
            borrowListVO.borrowList.Add(borrowVO);  //도서대여목록에 대여한 도서정보와 회원번호 추가
        }
        public int ControlBorrowing(string memberId)
        {
            ListScreen listScreen = new ListScreen();
            BorrowingScreen borrowingScreen = new BorrowingScreen();
            string bookName, bookId;

            while (Constants.INPUT_VALUE)
            {
                PrintBorrowing();
                bookName = InputBookName();    //먼저 도서명 입력받기
                AddBookOnList(bookName);              //검색어를 포함하는 책 리스트 저장
                Console.Clear();                    
                listScreen.PrintBookList(bookList);   //도서명 검색결과 목록 출력
                bookId = InputBookId(memberId);//도서번호를 입력받음
                break;
            }

            borrowingScreen.PrintSuccessMessage();//도서대여 완료 메세지 출력
            AddBorrowList(memberId, bookId);

            return Constants.COMPLETE_FUNCTION;
        }
    }
}
