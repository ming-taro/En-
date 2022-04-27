using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class RentalVO//--------->삭제할 클래스(파일삭제->안 씀)
    {
        private string memberId;
        private BookVO bookVO;
        public RentalVO(string memberId, BookVO bookVO)
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
