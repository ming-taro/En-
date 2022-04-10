using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BorrowVO
    {
        private string memberId;
        private BookVO bookVO;
        public BorrowVO(string memberId, BookVO bookVO)
        {
            this.memberId = memberId;
            this.bookVO = bookVO;
        }
        public string MemberId
        {
            get { return memberId; }
        }
        public BookVO BookVO
        {
            get { return bookVO; }
        }

    }
}
