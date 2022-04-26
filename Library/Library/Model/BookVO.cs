using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BookVO
    {
        private string id;            //도서번호
        private string name;          //도서명
        private string publisher;     //출판사
        private string author;        //저자
        private string price;         //가격
        private string quantity;      //수량
        private static List<BookVO> bookList; //--------->삭제할 코드
        public static List<BookVO> GetBookList()//--------->삭제할 코드
        {
            if(bookList == null)
            {
                bookList = new List<BookVO>();
                InitBookList();
            }
            return bookList;
        }
        public static void InitBookList()//--------->삭제할 코드
        {
            bookList.Add(new BookVO("1", "혼자 공부하는 자바", "한빛미디어", "신용권", "24000", "5"));
            bookList.Add(new BookVO("2", "혼자 공부하는 파이썬", "한빛미디어", "윤인성", "18000", "5"));
            bookList.Add(new BookVO("3", "혼자 공부하는 첫 프로그래밍 with 파이썬", "한빛미디어", "문현일", "17000", "0"));
            bookList.Add(new BookVO("4", "이것이 자바다", "한빛미디어", "신용권", "30000", "4"));
            bookList.Add(new BookVO("5", "이것이 취업을 위한 코딩 테스트다 with 파이썬", "한빛미디어", "나동빈", "34000", "4"));
            bookList.Add(new BookVO("6", "Do it! 깡샘의 안드로이드 앱 프로그래밍 with 코틀린", "이지스퍼블리싱", "김성윤", "36000", "7"));
            bookList.Add(new BookVO("7", "그림으로 쉽게 설명하는 안드로이드 프로그래밍", "생능", "천인국", "35000", "6"));
            bookList.Add(new BookVO("8", "Do it! 안드로이드 앱 프로그래밍", "이지스퍼블리싱", "정재곤", "40000", "6"));
            bookList.Add(new BookVO("9", "이것이 안드로이드다 with 코틀린", "한빛미디어", "고돈호", "34000", "4"));
            bookList.Add(new BookVO("10", "윤성우의 열혈 C++ 프로그래밍", "오렌지미디어", "윤성우", "27000", "3"));
        }
        
        public BookVO(string id, string name, string publisher, string author, string price, string quantity)
        {
            this.id = id;
            this.name = name;
            this.publisher = publisher;
            this.author = author;
            this.price = price;
            this.quantity = quantity;
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Price
        {
            get { return price; }
            set { price = value; }
        }
        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public override String ToString()
        {
            return "도서번호: " + id + "\n도서명: " + name + "\n출판사: " + publisher +
                "\n저자: " + author + "\n가격: " + price + "\n수량: " + quantity;
        }
    }
}
