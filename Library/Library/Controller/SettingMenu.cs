using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class SettingMenu
    {

    }
    class MemberMenu   //유저모드 선택 -> 1.회원가입  2.로그인  3.종료
    {
        public MemberMenu()
        {
            Screen screen = new Screen();
            string[] menu = { "회원가입", "로그인", "종료"};  
            screen.PrintMain(menu);                           //회원모드화면 출력
        }
    }
    class SignUp     //1.회원가입
    {
        public SignUp()
        {
            Screen screen = new Screen();
            screen.PrintSingUp();
            Console.SetCursorPosition(8, 5);    //아이디
            Console.ReadLine();
            Console.SetCursorPosition(10, 8);   //비밀번호
            Console.ReadLine();
            Console.SetCursorPosition(17, 11);  //비밀번호 재확인
            Console.ReadLine();
            Console.SetCursorPosition(6, 13);   //이름
            Console.ReadLine();
            Console.SetCursorPosition(6, 16);   //나이
            Console.ReadLine();
            Console.SetCursorPosition(10, 19);  //휴대전화
            Console.ReadLine();
            Console.SetCursorPosition(13, 22);  //도로명 주소
            Console.ReadLine();
        }
    }
    class SignIn
    {
        public SignIn()
        {
            PrintScreen();
        }
        public void PrintScreen()
        {
            Screen screen = new Screen();
            screen.PrintSingIn("로그인");
        }
        public bool SignInAdmin(ref string id, ref string password)
        {
            Console.SetCursorPosition(8, 5);
            id = Console.ReadLine();     //아이디 입력
            Console.SetCursorPosition(10, 6);
            password = Console.ReadLine();     //비밀번호 입력

            return Constants.GOING_NEXT;
        }
    }
    
    
    class SearchingMember
    {
        public SearchingMember(List<MemberVO> memberList)  //삭제할 회원 아이디 검색 화면 출력
        {
            Screen screen = new Screen();
            screen.PrintSearchingMember(memberList);
        }
        public int ControlSearchingMember(List<MemberVO> memberList)
        {
            Screen screen = new Screen();
            StartingLibrary testingLibrary = new StartingLibrary();
            Console.SetCursorPosition(22, 1);
            string memberId = Console.ReadLine();  //회원아이디 입력받기

            return Constants.COMPLETE_FUNCTION;       //검색결과 출력까지 모두 완료
        }
    }
    class BorrowingBook
    {
        List<BookVO> bookList = new List<BookVO>();
        public BorrowingBook(string myId)
        {
            InitBorrowList(myId);
        }
        public void InitBorrowList(string myId)
        {
            string path = "./text/BorrowList.txt";
            StreamReader reader = new StreamReader(path);

            while (reader.Peek() >= 0)
            {
                String[] text = reader.ReadLine().ToString().Split(",");   //도서대여 정보를 읽어옴
                if (myId.Equals(text[0]))
                {
                    bookList.Add(new BookVO(text[1], text[2], text[3], text[4], text[5], text[6]));
                }
            }
            reader.Close();
        }
        public void ControlBorrowingBook()
        {
            Screen screen = new Screen();
            screen.PrintBorrowingBook(bookList, "\n☞대여할 도서 번호: ");  //대여한 도서 목록 출력
            Console.SetCursorPosition(20, 1);
            string bookId = Console.ReadLine();     //반납할 도서 번호 입력받기

        }
    }
    class ReturningBook
    {
        public void ControlReturningBook()
        {
            AdminController adminController = new AdminController();
            Screen screen = new Screen();
            //screen.PrintBorrowingBook(adminController.bookList, "\n☞반납할 도서 번호: ");
            Console.Read();
        }
    }
}
