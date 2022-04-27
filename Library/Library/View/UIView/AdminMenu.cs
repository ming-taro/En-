using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminMenu
    {
        
        public void PrintSearchingBook(string sql, LibraryVO library)//------>삭제할코드
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
        }
        public void PrintBookList(List<BookVO> bookList, Logo logo)
        {
            logo.PrintLine();
            for (int i = 0; i<bookList.Count; i++)
            {
                Console.WriteLine("도서번호: " + bookList[i].Id);
                Console.WriteLine("도서명: " + bookList[i].Name);
                Console.WriteLine("출판사: " + bookList[i].Publisher);
                Console.WriteLine("저자: " + bookList[i].Author);
                Console.WriteLine("가격: " + bookList[i].Price);
                Console.WriteLine("수량: " + bookList[i].Quantity);
                logo.PrintLine();
            }
        }
        public void PrintSearchResult(List<BookVO> bookList, Logo logo)
        {
            logo.PrintSearchBox(Constants.SEARCH_TYPE);
            PrintBookList(bookList, logo);                  //검색결과로 나온 책목록 출력
            logo.PrintMessage(0, Console.CursorTop - 1, Constants.ESC_AND_ENTER, ConsoleColor.Yellow);
        }
        public void PrintBookIdInputScreen(List<BookVO> bookList, Logo logo)
        {
            logo.PrintSearchBox(Constants.BOOK_ID_TO_BORROW); //도서번호 입력창
            PrintBookList(bookList, logo);                    //도서 검색 결과 출력
        }
    }
}
