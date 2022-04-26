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
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void PrintSearchBox()  //검색모드 선택 화면
        {
            Console.Clear();
            Console.WriteLine("\n☞도서명: ");
            Console.WriteLine("☞출판사: ");
            Console.WriteLine("☞저자: ");
        }
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

            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        public void PrintSearchingBook(List<BookVO> bookList)
        {
            Console.WriteLine("\n=============================================================\n");
            for(int i = 0; i<bookList.Count; i++)
            {
                Console.WriteLine("도서번호: " + bookList[i].Id);
                Console.WriteLine("도서명: " + bookList[i].Name);
                Console.WriteLine("출판사: " + bookList[i].Publisher);
                Console.WriteLine("저자: " + bookList[i].Author);
                Console.WriteLine("가격: " + bookList[i].Price);
                Console.WriteLine("수량: " + bookList[i].Quantity);
                Console.WriteLine("\n=============================================================\n");
            }
        }
    }
}
