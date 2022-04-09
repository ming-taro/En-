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
        
    }
}
