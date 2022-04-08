using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            TestingLibrary testingLibrary = new TestingLibrary();
            testingLibrary.TestLibrary();
            /*List<BookVO> bookList = new List<BookVO>();

            string path = "./text/BookList.txt";
            StreamReader reader = new StreamReader(path);
            while (reader.Peek() >= 0)
            {
                String[] text = reader.ReadLine().ToString().Split(",");     //책 정보를 읽어옴
                bookList.Add(new BookVO(text[0], text[1], text[2], text[3], text[4], text[5])); //초기 책 목록을 리스트에 저장
            }
            reader.Close();

            for(int i=0; i<bookList.Count; i++)
            {
                Console.WriteLine(bookList[i]);
            }*/
        }
    }
}
//관리자
//아이디 : 123456789
//비밀번호 : 00000
