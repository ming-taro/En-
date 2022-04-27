using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View.UIView
{
    class MemberView
    {
        Logo logo;
        public MemberView()
        {
            logo = new Logo();
        }
        public void PrintBookList(List<BookVO> bookList)
        {
            logo.PrintLine();
            for (int i = 0; i < bookList.Count; i++)
            {
                Console.WriteLine("도서번호: " + bookList[i].Id);
                Console.WriteLine("도서명: " + bookList[i].Name);
                Console.WriteLine("출판사: " + bookList[i].Publisher);
                Console.WriteLine("저자: " + bookList[i].Author);
                Console.WriteLine("가격: " + bookList[i].Price);
                logo.PrintLine();
            }
        }
    }
}
