using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberView
    {
        Logo logo;
        public MemberView(Logo logo)
        {
            this.logo = logo;
        }
        public void PrintBookList(List<BookVO> bookList)   //도서목록 출력
        {
            logo.PrintLine();
            for (int i = 0; i < bookList.Count; i++)
            {
                Console.WriteLine(bookList[i]);
                logo.PrintSingleLine();
            }
            Console.SetCursorPosition(0, Console.CursorTop - 3);
            logo.PrintLine();
        }
        public void PrintMyBookList(List<BorrowBookVO> myBookList) //회원의 도서 대여 목록 출력
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            logo.PrintLine();
            for (int i = 0; i < myBookList.Count; i++)
            {
                Console.WriteLine(myBookList[i]);
                logo.PrintSingleLine();
            }
            Console.SetCursorPosition(0, Console.CursorTop - 3);
            logo.PrintLine();
        }
        public void PrintBookRental(List<BookVO> bookList)
        {
            logo.PrintMenu("도서 대출");
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.ZERO, "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.FIRST, "☞대출할 도서 번호:", ConsoleColor.Gray);
            logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.SECOND, "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━", ConsoleColor.Gray);
            Console.SetCursorPosition(0, (int)Constants.SearchMenu.THIRD);
            PrintBookList(bookList);    //도서 검색 결과 출력
        }
        public void PrintBookRentalSuccess(List<BorrowBookVO> myBookList)  //도서 대여 완료 메세지 + 회원의 대여 목록 출력
        {
            logo.PrintMenu("도서 대출 완료");
            logo.PrintMessage(0, (int)Constants.SearchMenu.ZERO, "대출 정보 조회", ConsoleColor.Gray);
            PrintMyBookList(myBookList);
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_AND_ENTER, ConsoleColor.Gray);
        }
        public void PrintBookReturnSuccess(BorrowBookVO book)              //도서 반납 완료 메세지 + 반납한 도서 정보 출력
        {
            logo.PrintMenu("도서 반납 완료");
            logo.PrintMessage(21, Console.CursorTop + 1, ">반납한 도서 정보<", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            logo.PrintLine();
            Console.WriteLine(book);
            logo.PrintLine();
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_MESSAGE, ConsoleColor.Yellow);
        }
        public void PrintProfile(MemberVO member) //회원정보수정 화면 출력
        {
            logo.PrintMenu("회원 정보 수정");
            Console.WriteLine(Constants.ESC_MESSAGE);
            Console.WriteLine("\n\n        (수정하려는 정보를 선택해 [Enter]키를 누르세요)");

            Console.SetCursorPosition(0, (int)Constants.EditMenu.FIRST);
            Console.WriteLine("☞아이디: " + member.Id);
            Console.SetCursorPosition(0, (int)Constants.EditMenu.SECOND);
            Console.WriteLine("☞비밀번호: " + member.Password);
            Console.SetCursorPosition(0, (int)Constants.EditMenu.THIRD);
            Console.WriteLine("  비밀번호 재확인: ");
            Console.SetCursorPosition(0, (int)Constants.EditMenu.FOURTH);
            Console.WriteLine("☞이름: " + member.Name);
            Console.SetCursorPosition(0, (int)Constants.EditMenu.FIFTH);
            Console.WriteLine("☞나이: " + member.Age);
            Console.SetCursorPosition(0, (int)Constants.EditMenu.SIXTH);
            Console.WriteLine("☞휴대전화: " + member.PhoneNumber);
            Console.SetCursorPosition(0, (int)Constants.EditMenu.SEVENTH);
            Console.WriteLine("☞주소: " + member.Address);
        }
        public void PrintSingUp()
        {
            logo.PrintMenu("회원가입");
            Console.WriteLine(Constants.ESC_MESSAGE);
            logo.PrintMessage(0, (int)Constants.Registration.FIRST, "아이디:\n(5~10자의 영어, 숫자만 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.SECOND, "비밀번호:\n(5~10자의 영어, 숫자만 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.THIRD, "비밀번호 재확인:", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.FOURTH, "이름:\n(영어, 한글만 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.FIFTH, "나이:\n(숫자만 입력해주세요.)", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.SIXTH, "휴대전화:\n(숫자만 입력해주세요.(입력형식: 010-0000-0000))", ConsoleColor.Gray);
            logo.PrintMessage(0, (int)Constants.Registration.SEVENTH, "주소:\n(ex: 서울특별시 광진구 군자동)", ConsoleColor.Gray);
        }
        public void PrintSuccessMessage()
        {
            logo.PrintMenu("회원가입 완료");
            Console.WriteLine("\n\n\n                [회원가입이 완료되었습니다]\n\n");
            Console.WriteLine("\n          등록하신 회원 정보로 로그인이 가능합니다.");
            Console.WriteLine("\n            ESC키를 누르면 회원모드로 돌아갑니다.");
        }
    }
}
