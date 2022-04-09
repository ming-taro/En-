using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Screen
    {
        public void PrintTitle()
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("┃                                                         ┃");
            Console.WriteLine("┃    **          **                                       ┃");
            Console.WriteLine("┃    **          **                                       ┃");
            Console.WriteLine("┃    **      ★  ****   *  **   **     *  **    *    **   ┃");
            Console.WriteLine("┃    **      **  **☆*  **     *☆*    **       *  **     ┃");
            Console.WriteLine("┃    ******  **  ****   **      **  *  **       **        ┃");
            Console.WriteLine("┃                                             **          ┃");
            Console.WriteLine("┃                                           **            ┃ ");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            Console.WriteLine("     >>메뉴를 방향키로 이동하고 [Enter]키를 눌러주세요<<");
        }
        public void PrintMenu(string menu)
        {
            Console.Clear();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("[" + menu + "]\n");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        //string
        //메인화면 -> (회원,관리자,종료)
        //회원메뉴 -> (회원가입,로그인,종료)
        //관리자모드 -> (도서검색,도서등록,도서수량관리,도서삭제,회원정보관리,종료)
        //회원모드 -> (도서검색,도서대여,도서반납,개인정보수정,종료)
        public void PrintMain(string[] menu)
        {
            Console.Clear();
            PrintTitle();
            for(int i=0; i < menu.Length; i++)
            {
                Console.SetCursorPosition(25, 13 + i);
                Console.WriteLine("☞" + menu[i]);
            }
            Console.SetCursorPosition(25,13);
        }
        public void PrintSingIn(string menu)
        {
            PrintMenu(menu);
            Console.WriteLine("아이디: ");
            Console.Write("비밀번호: ");
        }
        
        public void PrintSingUp()
        {
            Console.Clear();
            PrintMenu("회원가입");
            Console.WriteLine("아이디: \n(5~10자의 영어, 숫자만 입력해주세요.)\n");
            Console.WriteLine("비밀번호: \n(5~10자의 영어, 숫자만 입력해주세요.)\n");
            Console.WriteLine("비밀번호 재확인: \n");
            Console.WriteLine("이름: \n(영어, 한글만 입력해주세요.)\n");
            Console.WriteLine("나이: \n(숫자만 입력해주세요.)\n");
            Console.WriteLine("휴대전화: \n(숫자만 입력해주세요.)\n");
            Console.WriteLine("도로명 주소: \n(ex: 서울특별시 광진구 능동로 209)\n");
        }
        public void PrintMemberMenu()
        {
            string[] menu = { "로그인", "회원가입", "종료"};
            PrintMain(menu);
        }
        public void PrintMemberMode()
        {
            string[] menu = { "도서 검색", "도서 대여", "도서 반납", "개인 정보 수정", "종료"};
            PrintMain(menu);
        }
        public void PrintAdminMode()
        {
            string[] menu = { "도서 검색", "도서 등록", "도서 수량 관리", "도서 삭제", "회원정보 관리"};
            PrintMain(menu);
        }
        public void PrintBookList(List<BookVO> bookList)
        {
            Console.SetCursorPosition(0,5); 
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");
            
            for (int i=0; i<bookList.Count; i++)
            {
                Console.WriteLine(bookList[i]);
                Console.WriteLine("\n=============================================================\n");
            }
        }
        public void PrintSearchBox()
        {
            Console.Clear();
            Console.WriteLine("\n☞도서명: ");
            Console.WriteLine("☞출판사: ");
            Console.WriteLine("☞저자: ");
        }
        public void PrintSearchingBook(List<BookVO> bookList)
        {
            PrintSearchBox();
            PrintBookList(bookList);
        }
        public void PrintSearchingBook(int menu, string name, List<BookVO> bookList)
        {
            Console.Clear();
            Console.WriteLine("\n=============================================================\n");
            for (int i = 0; i < bookList.Count; i++)
            {
                //1.도서명   2.출판사   3.저자
                if (menu == 1 && bookList[i].Name.Contains(name) || menu == 2 && bookList[i].Publisher.Contains(name) || menu == 3 && bookList[i].Author == name)
                {
                    Console.WriteLine(bookList[i]);
                    Console.WriteLine("\n=============================================================\n");
                }
            }
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        public void PrintSearchingMember(List<MemberVO> memberList)
        {
            Console.Clear();
            Console.WriteLine("\n☞삭제할 회원 아이디: ");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");

            for (int i = 0; i < memberList.Count; i++)
            {
                Console.WriteLine(memberList[i]);
                Console.WriteLine("\n=============================================================\n");
            }
        }
        public void PrintSearchingMember(string id, List<MemberVO> memberList)
        {
            Console.Clear();
            Console.WriteLine("\n=============================================================\n");
            for(int i=0; i<memberList.Count; i++)
            {
                if (memberList[i].Id.Equals(id))
                {
                    Console.WriteLine(memberList[i]);
                    Console.WriteLine("\n=============================================================\n");
                }
            }
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        public void PrintBorrowingBook(List<BookVO> bookList, string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("\n=============================================================\n");

            for (int j = 0; j < bookList.Count; j++)
            {
                Console.WriteLine(bookList[j]);
                Console.WriteLine("\n=============================================================\n");
            }
        }
    }
}
