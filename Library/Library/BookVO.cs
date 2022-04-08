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
        public BookVO(string id, string name, string publisher, string author, string price, string quantity)
        {
            this.id = id;
            this.name = name;
            this.publisher = publisher;
            this.author = author;
            this.price = price;
            this.quantity = quantity;
        }
        public string Name
        {
            get { return name; }
        }
        
        public override String ToString()
        {
            return "도서번호: " + id + "\n도서명: " + name + "\n출판사: " + publisher +
                "\n저자: " + author + "\n가격: " + price + "\n수량: " + quantity;
        }
    }
}
