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
        /*public void PrintSearchingBook(string sql, LibraryVO library)//------>삭제할코드
        {
            MySqlCommand command = new MySqlCommand(sql + ";", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            Console.WriteLine("\n=============================================================\n");
            while (table.Read())
            {
                Console.WriteLine("도서번호: " + table["id"].ToString());
                Console.WriteLine("도서명: " + table["name"].ToString());
                Console.WriteLine("출판사" + table["publisher"].ToString());
                Console.WriteLine("저자: " + table["author"].ToString());
                Console.WriteLine("가격: " + table["price"].ToString());
                Console.WriteLine("수량: " + table["quantity"].ToString());
                Console.WriteLine("\n=============================================================\n");
            }
            table.Close();

            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>[ESC]:뒤로가기<<<<<<<<<<<<<<<<<<<<<<<<");
        }*/
        public void PrintBookList(List<BookVO> bookList)
        {
            logo.PrintLine();
            for (int i = 0; i<bookList.Count; i++)
            {
                Console.WriteLine(bookList[i]);
                logo.PrintLine();
            }
        }
        public void PrintMemberList(List<MemberVO> memberList)
        {
            logo.PrintLine();
            for (int i = 0; i < memberList.Count; i++)
            {
                Console.WriteLine(memberList[i]);
                logo.PrintLine();
            }
        }
        public void PrintSearchResult(List<BookVO> bookList)  //도서검색 -> 검색결과로 나온 책목록 출력
        {
            logo.PrintSearchBox(Constants.SEARCH_TYPE);
            PrintBookList(bookList);                  
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_AND_ENTER, ConsoleColor.Yellow);
        }
        public void PrintBookIdInputScreen(List<BookVO> bookList)        //도서삭제 -> 도서번호 입력칸 + 도서검색결과 출력
        {
            logo.PrintSearchBox("☞삭제할 도서 번호:");
            PrintBookList(bookList);
        }
        public void PrintMemberIdInputScreen(List<MemberVO> memberList)  //회원관리 -> 회원아이디 입력칸 + 회원목록 출력
        {
            logo.PrintSearchBox("☞삭제할 회원 아이디:");
            PrintMemberList(memberList);
        }
        public void PrintSuccess(MemberVO member)   //회원관리 -> 회원정보 삭제 완료 메세지 + 삭제한 회원정보
        {
            logo.PrintMenu("회원 삭제 완료");
            logo.PrintMessage(22, 7, ">삭제한 회원 정보<", ConsoleColor.Gray);
            Console.SetCursorPosition(0,Console.CursorTop - 1);
            logo.PrintLine();
            Console.WriteLine(member);
            logo.PrintLine();
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_AND_ENTER, ConsoleColor.Yellow);
        }
    }
}
