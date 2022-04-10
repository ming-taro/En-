using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BorrowListVO
    {
        private static BorrowListVO borrowListVO = null;
        public ArrayList borrowList;
        public static BorrowListVO GetBorrowListVO()
        {
            if (borrowListVO == null)
            {
                borrowListVO = new BorrowListVO();
            }
            return borrowListVO;
        }
        public BorrowListVO()
        {
            borrowList = new ArrayList();
            BookListVO bookListVO = BookListVO.GetBookListVO();

            borrowList.Add(new BorrowVO("aaabbb", bookListVO.bookList[0]));
            borrowList.Add(new BorrowVO("aaabbb", bookListVO.bookList[1]));
            borrowList.Add(new BorrowVO("aaaccc", bookListVO.bookList[2]));
            borrowList.Add(new BorrowVO("aaaccc", bookListVO.bookList[3]));
            borrowList.Add(new BorrowVO("aaaddd", bookListVO.bookList[0]));
            borrowList.Add(new BorrowVO("aaaddd", bookListVO.bookList[2]));
        }
    }
}
