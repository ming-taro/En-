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
        private string id;             //도서번호
        private string name;           //도서명
        private string author;         //저자
        private string publisher;      //출판사
        private string publicationDate;//출판일
        private string isbn;           //isbn
        private string price;          //가격
        private string bookIntroduction; //책소개
        private string quantity;       //수량

        public BookVO(string id, string name, string author, string publisher, string publicationDate, string isbn, string price, string bookIntroduction, string quantity)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.publisher = publisher;
            this.publicationDate = publicationDate;
            this.isbn = isbn;
            this.price = price;
            this.bookIntroduction = bookIntroduction;
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
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }
        public string PublicationDate
        {
            get { return publicationDate; }
            set { publicationDate = value; }
        }
        public string Isbn
        {
            get { return isbn; }
            set { isbn = value; }
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
        public string BookIntroduction
        {
            get { return bookIntroduction; }
            set { bookIntroduction = value; }
        }
        public override String ToString()
        {
            return "도서번호: " + id + "\n  도서명: " + name + "\n    저자: " + author + "\n  출판사: " + publisher + "\n  출판일: " + publicationDate 
                + "\n    ISBN: " + isbn + "\n    가격: " + price + "\n  책소개: " + bookIntroduction + "\n    수량: " + quantity;
        }
    }
}
