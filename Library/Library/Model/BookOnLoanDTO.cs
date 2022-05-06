using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BookOnLoanDTO
    {
        private string memberId;      //회원번호
        private string bookId;            //도서번호
        private string name;          //도서명
        private string publisher;     //출판사
        private string author;        //저자
        private string price;         //가격
        private string rentalPeriod;  //대여기간

        public BookOnLoanDTO(string memberId, string bookId, string name, string publisher, string author, string price, string rentalPeriod)
        {
            this.memberId = memberId;
            this.bookId = bookId;
            this.name = name;
            this.publisher = publisher;
            this.author = author;
            this.price = price;
            this.rentalPeriod = rentalPeriod;
        }
        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }
        public string BookId
        {
            get { return bookId; }
            set { bookId = value; }
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
            get { return rentalPeriod; }
            set { rentalPeriod = value; }
        }
        public override String ToString()
        {
            return "도서번호: " + bookId + "\n  도서명: " + name + "\n  출판사: " + publisher +
                "\n    저자: " + author + "\n    가격: " + price + "\n대여기간: " + rentalPeriod;
        }
    }
}
