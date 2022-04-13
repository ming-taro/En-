using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    class Program
    {
        public void test1()
        {

        }
        static void Main(string[] args)
        {
            //Starting starting = new Starting();
            //starting.StartLibrary();
            
            List<BookVO> bookList1 = BookVO.GetBookList();
            Console.WriteLine(bookList1[0].Id);

            bookList1.Add(new BookVO("a", "a", "a", "a", "a", "a"));
            Console.WriteLine(bookList1[10].Id);

            List<BookVO> bookList2 = BookVO.GetBookList();
            Console.WriteLine(bookList2[10].Id);
        }
    }
}
//관리자
//아이디 :   12345
//비밀번호 : 00000
//회원
//아이디 :   aaabbb
//비밀번호 : 000111
