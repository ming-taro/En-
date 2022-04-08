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
            Console.WriteLine("\n  ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("  ┃                                                         ┃");
            Console.WriteLine("  ┃    **          **                                       ┃");
            Console.WriteLine("  ┃    **          **                                       ┃");
            Console.WriteLine("  ┃    **      ★  ****   *  **   **     *  **    *    **   ┃");
            Console.WriteLine("  ┃    **      **  **☆*  **     *☆*    **       *  **     ┃");
            Console.WriteLine("  ┃    ******  **  ****   **      **  *  **       **        ┃");
            Console.WriteLine("  ┃                                             **          ┃");
            Console.WriteLine("  ┃                                           **            ┃ ");
            Console.WriteLine("  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
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
        public void PrintAdminMode()
        {
            string[] menu = { "도서 검색", "도서 등록", "도서 수량 관리", "도서 삭제", "회원정보 관리"};
            PrintMain(menu);
        }
        public void PrintBookList(List<BookVO> bookList)
        {
            Console.SetCursorPosition(0,5);
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            
            for (int i=0; i<bookList.Count; i++)
            {
                Console.WriteLine(bookList[i]);
                Console.WriteLine("\n=============================================================\n");
            }
        }
        public void PrintSearchBox()
        {
            Console.Clear();
            
            Console.WriteLine("\n1.도서명: ");
            Console.WriteLine("2.출판사: ");
            Console.WriteLine("3.저자: ");
        }
        public void PrintSearchingBook(List<BookVO> bookList)
        {
            PrintSearchBox();
            PrintBookList(bookList);
        }
        public void PrintSearchingName(string bookname, List<BookVO> bookList)
        {
            Console.Clear();
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Name.Contains(bookname))
                {
                    Console.WriteLine(bookList[i]);
                    Console.WriteLine("\n=============================================================\n");
                }
            }
        }
    }
}
