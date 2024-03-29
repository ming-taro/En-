﻿using MySql.Data.MySqlClient;
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
        public void PrintBookList(List<BookDTO> bookList)   //도서목록 출력
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
        public void PrintLogList(List<LogDTO> logList)
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
        public void PrintBookSearch(List<BookDTO> bookList)  //도서 검색
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

        public void PrintSearchResult(List<BookDTO> bookList)  //도서검색 -> 검색결과로 나온 책목록 출력
        {
            PrintBookSearch(bookList);
            logo.PrintEscAndEnter(0, (int)Constants.SearchMenu.ESC_TOP); 
            Console.WindowTop = 0;
            Console.CursorVisible = Constants.IS_INVISIBLE_CURSOR;
        }
        public void PrintRegisteredBook(BookDTO book)    //도서등록 -> 등록한 도서정보 출력
        {
            logo.PrintMenu("도서 등록 완료");
            logo.PrintMessage(0, (int)Constants.SearchMenu.ZERO, "등록한 도서 정보", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            logo.PrintLine();
            Console.WriteLine(book);
            logo.PrintLine();
            Console.CursorVisible = Constants.IS_INVISIBLE_CURSOR;
        }
        public void PrintBookEdition(List<BookDTO> bookList)  //도서정보 수정 -> 수정할 도서번호 입력창 출력 + 도서검색결과 출력
        {                                                                           
            logo.PrintSearchBox("도서 정보 수정", "☞정보를 수정할 도서번호:", "검색 결과(총 " + bookList.Count + "건)");
            PrintBookList(bookList);
        }
        public void PrintBookDeletion(List<BookDTO> bookList) //도서삭제 -> 삭제할 도서번호 입력칸 + 도서검색결과 출력
        {
            logo.PrintSearchBox("도서 삭제", "☞삭제할 도서 번호:", "검색 결과(총 " + bookList.Count + "건)");
            PrintBookList(bookList);
        }
        public void PrintDeletedBook(BookDTO book)   //도서삭제 -> 삭제한 도서정보 출력
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
        public void PrintBookRevision(BookDTO book, string menu, string saveButton)  //도서정보수정 -> 수정할 정보 목록 출력
        {
            logo.PrintMenu(menu);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.FIRST, "☞도서명  : " + book.Name, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.FIRST + 1, "(50자 이내로 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.SECOND,  "☞저자    : " + book.Author, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.SECOND + 1, "(50자 이내의 영어, 한글을 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.THIRD, "☞출판사  : " + book.Publisher, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.THIRD + 1, "(50자 이내로 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.FOURTH,  "☞출판일  : " + book.PublicationDate, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.FOURTH + 1, "(날짜 형식에 맞춰 입력해주세요.(ex: 2022.05.06))", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.FIFTH,  "☞ISBN    : " + book.Isbn, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.FIFTH + 1, "(50자 이내의 숫자를 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.SIXTH, "☞가격    : " + book.Price, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.SIXTH + 1, "(1~1,000,000,000원 이내의 숫자를 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.SEVENTH, "☞책소개  : " + book.BookIntroduction, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.SEVENTH + 2, "(100자 이내로 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.EIGHT, "☞수량    : " + book.Quantity, ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.InputField.BOOK_EDITION, (int)Constants.EditMenu.EIGHT + 1, "(1~99권 이내의 숫자를 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.NINTH, "▶" + saveButton, ConsoleColor.Gray);
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
        public void PrintRentalList(List<BookDTO> rentalList)   //도서 대출 현황 리스트
        {
            logo.PrintMenu("도서 대출 현황");
            logo.PrintMessage(0, (int)Constants.SearchMenu.ZERO, "전체 회원의 도서 대출 목록(총 " + rentalList.Count + "건)", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            logo.PrintLine();
            for (int i = 0; i < rentalList.Count; i++)
            {
                Console.WriteLine("    순번: " + rentalList[i].Id);
                Console.WriteLine("  회원ID: " + rentalList[i].MemberId);
                Console.WriteLine("  도서명: " + rentalList[i].Name);
                Console.WriteLine("    저자: " + rentalList[i].Author);
                Console.WriteLine("  출판사: " + rentalList[i].Publisher);
                Console.WriteLine("대여기간: " + rentalList[i].RentalPeriod);
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
        public void PrintNaverSearchResult(List<BookDTO> bookList)
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
            logo.PrintMessage(0, (int)Constants.SearchMenu.FIFTH, "검색 결과(총 0건)", ConsoleColor.Gray);
            Console.WriteLine("====================================================================================================\n");
            logo.RemoveLine(5, Console.CursorTop);
            logo.PrintMessage(5, Console.CursorTop, "'" + searchWord + "'에 대한 검색결과가 없습니다.", ConsoleColor.Red);
            Console.WriteLine("\n====================================================================================================\n");
        }
        public void PrintBookRegistration(BookDTO book)
        {
            logo.PrintMenu("도서 등록");
            logo.PrintMessage((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.MESSAGE, "                                (등록할 도서의 수량을 입력해주세요.)", ConsoleColor.Green);
            logo.PrintMessage(0, (int)Constants.EditMenu.FIRST, "☞도서명  : " + book.Name, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.SECOND, "☞저자    : " + book.Author, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.THIRD, "☞출판사  : " + book.Publisher, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.FOURTH, "☞출판일  : " + book.PublicationDate, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.FIFTH, "☞ISBN    : " + book.Isbn, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.SIXTH, "☞가격    : " + book.Price, ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.SEVENTH, "☞책소개  : " + book.BookIntroduction.Substring(0,40) + "...", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.EditMenu.EIGHT, "☞수량    : ", ConsoleColor.Gray);
        }
        public void PrintLogManagemnet(List<LogDTO> logList)
        {
            logo.PrintSearchBox("로그 기록", "☞삭제할 로그 번호:", "로그 기록 조회");
            PrintLogList(logList);
            Console.WindowTop = 0;
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
