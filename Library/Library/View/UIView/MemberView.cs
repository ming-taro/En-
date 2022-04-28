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
        AdminMenu adminMenu;
        public MemberView()
        {
            logo = new Logo();
            adminMenu = new AdminMenu();
        }
        public void PrintMyBookList(List<BookVO> myBookList) //회원의 도서 대여 목록 출력
        {
            logo.PrintMessage(20, Console.CursorTop + 1, ">나의 도서 대여 목록<", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            logo.PrintLine();
            for (int i = 0; i < myBookList.Count; i++)
            {
                Console.Write(myBookList[i]);
                logo.RemoveLine(0, Console.CursorTop);    //도서 수량 지움
                logo.PrintLine();
            }
        }
        public void PrintBookIdInputScreen(List<BookVO> bookList)    //전체 도서 목록 출력
        {
            logo.PrintSearchBox(Constants.BOOK_ID_TO_BORROW);        //도서번호 입력창
            adminMenu.PrintBookList(bookList, logo);                 //도서 검색 결과 출력
        }
        public void PrintBookRentalSuccess(List<BookVO> myBookList)  //도서 대여 완료 메세지 + 회원의 대여 목록 출력
        {
            logo.PrintMenu("도서 대여 완료");
            PrintMyBookList(myBookList);    
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_AND_ENTER, ConsoleColor.Yellow);
        }
        public void PrintBookReturnSuccess(BookVO book)              //도서 반납 완료 메세지 + 반납한 도서 정보 출력
        {
            logo.PrintMenu("도서 반납 완료");
            logo.PrintMessage(21, Console.CursorTop + 1, ">반납한 도서 정보<", ConsoleColor.Gray);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            logo.PrintLine();
            Console.Write(book);
            logo.RemoveLine(0, Console.CursorTop);  //수량 지움
            logo.PrintLine();
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_MESSAGE, ConsoleColor.Yellow);
        }
        public void PrintProfile(MemberVO member) //
        {
            //string query = "SELECT*FROM member WHERE id='" + memberId + "';";
            //MySqlCommand command = new MySqlCommand(query, library.Connection);
            //MySqlDataReader table = command.ExecuteReader();

            logo.PrintMenu("회원정보수정");
            Console.WriteLine(Constants.ESC_MESSAGE);
            Console.WriteLine("         >수정하려는 정보를 선택해 Enter키를 누르세요<");

            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.FIRST);
            Console.WriteLine("☞아이디: " + member.Id);
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.SECOND);
            Console.WriteLine("☞비밀번호: " + member.Password);
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.THIRD);
            Console.WriteLine("  비밀번호 재확인: ");
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.FOURTH);
            Console.WriteLine("☞이름: " + member.Name);
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.FIFTH);
            Console.WriteLine("☞나이: " + member.Age);
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.SIXTH);
            Console.WriteLine("☞휴대전화: " + member.PhoneNumber);
            Console.SetCursorPosition(0, (int)Constants.ProfileMenu.SEVENTH);
            Console.WriteLine("☞주소: " + member.Address);
        }
    }
}
