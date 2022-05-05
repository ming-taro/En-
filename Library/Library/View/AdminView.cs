using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminView
    {
        Logo logo;
        public AdminView()
        {
           logo = new Logo();
        }
        public void PrintBookList(List<BookVO> bookList)   //도서목록 출력
        {
            logo.PrintLine();
            for (int i = 0; i<bookList.Count; i++)
            {
                Console.WriteLine(bookList[i]);
                logo.PrintSingleLine();
            }
            Console.SetCursorPosition(0, Console.CursorTop - 3);
            logo.PrintLine();
        }
        public void PrintMemberList(List<MemberVO> memberList)  //회원목록 출력
        {
            logo.PrintLine();
            for (int i = 0; i < memberList.Count; i++)
            {
                Console.WriteLine(memberList[i]);
                logo.PrintLine();
            }
        }
        public void PrintLogList(List<LogVO> logList)
        {
            logo.PrintLine();
            for (int i = logList.Count - 1; i >= 0; i--)   //최근 추가한 값부터 출력
            {
                Console.WriteLine(logList[i]);
                logo.PrintSingleLine();
            }
            Console.SetCursorPosition(0, Console.CursorTop - 3);
            logo.PrintLine();
        }
        public void PrintBookSearch(List<BookVO> bookList)  //도서 검색
        {
            logo.PrintMenu("도서 검색");
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.ZERO, "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.FIRST, "☞도서명:", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.SECOND, "☞출판사:", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.THIRD, "☞저자  :", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.FOURTH, "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.SearchMenu.SIXTH, "검색 결과(총 " + bookList.Count + "건)", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            PrintBookList(bookList);   //전체도서 출력
        }

        public void PrintSearchResult(List<BookVO> bookList)  //도서검색 -> 검색결과로 나온 책목록 출력
        {
            PrintBookSearch(bookList);
            logo.PrintEscAndEnter(0, (int)Constants.SearchMenu.ESC_TOP); 
            Console.WindowTop = 0;
            Console.CursorVisible = Constants.IS_INVISIBLE_CURSOR;
        }
        public void PrintRegisteredBook(BookVO book)    //도서등록 -> 등록한 도서정보 출력
        {
            logo.PrintMenu("도서 등록 완료");
            logo.PrintMessage(0, (int)Constants.SearchMenu.ZERO, "등록한 도서 정보", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            logo.PrintLine();
            Console.WriteLine(book);
            logo.PrintLine();
            Console.CursorVisible = Constants.IS_INVISIBLE_CURSOR;
        }
        public void PrintBookEdition(List<BookVO> bookList)  //도서정보 수정 -> 수정할 도서번호 입력창 출력 + 도서검색결과 출력
        {                                                                           
            logo.PrintSearchBox("도서 정보 수정", "☞정보를 수정할 도서번호:", "검색 결과(총 " + bookList.Count + "건)");
            PrintBookList(bookList);
        }
        public void PrintBookDeletion(List<BookVO> bookList) //도서삭제 -> 삭제할 도서번호 입력칸 + 도서검색결과 출력
        {
            logo.PrintSearchBox("도서 삭제", "☞삭제할 도서 번호:", "검색 결과(총 " + bookList.Count + "건)");
            PrintBookList(bookList);
        }
        public void PrintDeletedBook(BookVO book)   //도서삭제 -> 삭제한 도서정보 출력
        {
            logo.PrintMenu("도서 삭제 완료");
            logo.PrintEscAndEnter(0, (int)Constants.SearchMenu.ESC_TOP);
            logo.PrintMessage(0, (int)Constants.SearchMenu.ZERO, "삭제한 도서 정보", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            logo.PrintLine();
            Console.WriteLine(book);
            logo.PrintLine();
            Console.WindowTop = 0;
            Console.CursorVisible = Constants.IS_INVISIBLE_CURSOR;
        }
        public void PrintBookRegistration()   //도서등록 -> 입력할 정보 목록 출력
        {                                               
            logo.PrintMenu("도서 등록");
            logo.PrintMessage(0, (int)Constants.Registration.FIRST, "☞도서번호:\n(0~999사이의 숫자만 입력 가능합니다.(ex: 123))", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.SECOND, "☞도서명:\n(50자 이내로 입력해주세요.(ex: 이것이 C#이다))", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.THIRD, "☞출판사:\n(50자 이내로 입력해주세요.(ex: 한빛미디어)", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.FOURTH, "☞저자:\n(50자 이내의 영어, 한글만 입력 가능합니다.(ex: 박상현)", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.FIFTH, "☞가격:\n(숫자만 입력 가능합니다.(ex: 34000)", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.SIXTH, "☞수량:\n(1~99사이의 숫자만 입력 가능합니다.(ex: 5))", ConsoleColor.Gray);
        }
        public void PrintBookRevision(BookVO book)  //도서정보수정 -> 수정할 정보 목록 출력
        {                                              
            logo.PrintMenu("도서 정보 수정");
            //Console.WriteLine("\n\n        (수정하려는 정보를 선택해 [Enter]키를 누르세요)");
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.FIRST, "☞도서번호: " + book.Id, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.FIRST + 1, "(도서번호는 변경이 불가능합니다.)", ConsoleColor.Red);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.SECOND, "☞도서명  : " + book.Name, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.SECOND + 1, "(50자 이내로 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.THIRD, "☞출판사  : " + book.Publisher, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.THIRD + 1, "(50자 이내로 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.FOURTH, "☞저자    : " + book.Author, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.FOURTH + 1, "(50자 이내의 영어, 한글만 입력 가능합니다.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.FIFTH, "☞가격    : " + book.Price, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.FIFTH + 1, "(숫자만 입력 가능합니다.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.SIXTH, "☞수량    : " + book.Quantity, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.SIXTH + 1, "(숫자만 입력 가능합니다.)", ConsoleColor.Gray);
        }
        public void PrintMemberIdInputScreen(List<MemberVO> memberList)  //회원관리 -> 회원아이디 입력칸 + 회원목록 출력
        {
            logo.PrintSearchBox("회원 삭제", "☞삭제할 회원 아이디:", "회원 목록");
            PrintMemberList(memberList);
        }
        public void PrintDeletedMember(MemberVO member)   //회원관리 -> 회원정보 삭제 완료 메세지 + 삭제한 회원정보
        {
            logo.PrintMenu("회원 삭제 완료");
            logo.PrintEscAndEnter(0, (int)Constants.SearchMenu.ESC_TOP);
            logo.PrintMessage(0, (int)Constants.SearchMenu.ZERO, "삭제한 회원 정보", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            logo.PrintLine();
            Console.WriteLine(member);
            logo.PrintLine();
            Console.WindowTop = 0;
            Console.CursorVisible = Constants.IS_INVISIBLE_CURSOR;
        }
        public void PrintRentalList(List<BorrowBookVO> rentalList)   //도서 대출 현황 리스트
        {
            logo.PrintMenu("도서 대출 현황");
            logo.PrintMessage(0, (int)Constants.SearchMenu.ZERO, "전체 회원의 도서 대출 목록(총 " + rentalList.Count + "건)", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            logo.PrintLine();
            for (int i = 0; i < rentalList.Count; i++)
            {
                Console.WriteLine("  회원ID: " + rentalList[i].MemberId);
                Console.WriteLine(rentalList[i]);
                logo.PrintSingleLine();
            }
            Console.SetCursorPosition(0, Console.CursorTop - 3);
            logo.PrintLine();
            Console.WindowTop = 0;
        }
        public void PrintNaverSearch()
        {
            logo.PrintMenu("네이버 도서 검색");
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.ZERO, "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.FIRST, "☞검색어    :", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.SECOND, "☞조회 권 수:", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.THIRD, "▶조회", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.FOURTH, "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.ZERO - 1, "검색어와 조회할 책 권 수를 입력하고 조회버튼을 눌러주세요.", ConsoleColor.Green);
        }
        public void PrintNaverSearchResult(List<BookVO> bookList)
        {
            logo.PrintSearchBox("네이버 도서 검색", "☞추가할 도서 번호:", "검색결과(총 " + bookList.Count + "건)");

            logo.PrintLine();
            for (int i = 0; i < bookList.Count; i++)
            {
                Console.Write(bookList[i]);
                logo.RemoveLine(0, Console.CursorTop);   //수량 부분 지움
                logo.PrintSingleLine();
            }
            Console.SetCursorPosition(0, Console.CursorTop - 3);
            logo.PrintLine();
            Console.WindowTop = 0;
        }

        public void PrintNoSearchResult(string searchWord)
        {
            logo.PrintMessage(0, (int)Constants.SearchMenu.FOURTH, "검색 결과(총 0건)", ConsoleColor.Gray);
            Console.WriteLine("====================================================================================================\n");
            logo.RemoveLine(5, Console.CursorTop);
            logo.PrintMessage(5, Console.CursorTop, "'" + searchWord + "'에 대한 검색결과가 없습니다.", ConsoleColor.Red);
            Console.WriteLine("\n====================================================================================================\n");
        }
        public void PrintBookRegistration(BookVO book)
        {
            logo.PrintMenu("도서 등록");
            logo.PrintMessage(0, (int)Constants.EditMenu.ZERO, "(등록할 도서의 수량을 입력해주세요.)", ConsoleColor.Yellow);
            logo.PrintMessage(0, (int)Constants.EditMenu.SECOND, "☞도서명  : " + book.Name, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.THIRD, "☞출판사  : " + book.Publisher, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.FOURTH, "☞저자    : " + book.Author, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.FIFTH, "☞가격    : " + book.Price, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.SIXTH, "☞수량    : ", ConsoleColor.Gray);
        }
        public void PrintLogManagemnet(List<LogVO> logList)
        {
            logo.PrintMenu("로그 기록");
            PrintLogList(logList);
            Console.WindowTop = 0;
            Console.CursorVisible = Constants.IS_INVISIBLE_CURSOR;
        }
        public void PrintNoLogRecord()
        {
            logo.PrintMenu("로그 기록");
            logo.PrintLine();
            logo.PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, Console.CursorTop, "          기록된 로그 내역이 없습니다", ConsoleColor.Red);
            logo.PrintLine();
            Console.CursorVisible = Constants.IS_INVISIBLE_CURSOR;
        }
    }
}
