using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminVO
    {
        protected string id;
        protected string password;
        public AdminVO()
        {
            id = "123456789";
            password = "0000";
        }

        public override string ToString()
        {
            return "아이디: " + id + "\n비밀번호: " + password;
        }

    }
    
    class BorrowBookVO
    {
        private string memberId;
        private string bookId;
        public BorrowBookVO(string memberId, string bookId)
        {
            this.memberId = memberId;
            this.bookId = bookId;
        }
        public string MemberId
        {
            get { return memberId; }
        }
        public string BookId
        {
            get { return bookId; }
        }
        public void InitBorrowList(List<BorrowBookVO> borrowList)
        {
            borrowList = new List<BorrowBookVO>();

            string path = "./text/BookList.txt";
            StreamReader reader = new StreamReader(path);
            while (reader.Peek() >= 0)
            {
                String[] text = reader.ReadLine().ToString().Split(",");   //도서대여 정보를 읽어옴
                borrowList.Add(new BorrowBookVO(text[0], text[1])); //초기 책 목록을 리스트에 저장
            }
            reader.Close();
        }
    }
}
