using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    class BookListVO
    {
        private static BookListVO bookListVO = null;
        public List<BookVO> bookList;
        public static BookListVO getBookList()
        {
            if(bookListVO == null)
            {
                bookListVO = new BookListVO();
            }
            return bookListVO;
        }
        public BookListVO()
        {
            bookList = new List<BookVO>();

            bookList.Add(new BookVO("1", "혼자 공부하는 자바", "한빛미디어", "신용권", "24000", "5"));
            bookList.Add(new BookVO("2", "혼자 공부하는 파이썬", "한빛미디어", "윤인성", "18000", "5"));
            bookList.Add(new BookVO("3", "혼자 공부하는 첫 프로그래밍 with 파이썬", "한빛미디어", "문현일", "17000", "5"));
            bookList.Add(new BookVO("4", "이것이 자바다", "한빛미디어", "신용권", "30000", "4"));
            bookList.Add(new BookVO("5", "이것이 취업을 위한 코딩 테스트다 with 파이썬", "한빛미디어", "나동빈", "34000", "4"));
            bookList.Add(new BookVO("6", "Do it! 깡샘의 안드로이드 앱 프로그래밍 with 코틀린", "이지스퍼블리싱", "김성윤", "36000", "7"));
            bookList.Add(new BookVO("7", "그림으로 쉽게 설명하는 안드로이드 프로그래밍", "생능", "천인국", "35000", "6"));
            bookList.Add(new BookVO("8", "Do it! 안드로이드 앱 프로그래밍", "이지스퍼블리싱", "정재곤", "40000", "6"));
            bookList.Add(new BookVO("9", "이것이 안드로이드다 with 코틀린", "한빛미디어", "고돈호", "34000", "4"));
            bookList.Add(new BookVO("10", "윤성우의 열형 C++ 프로그래밍", "오렌지미디어", "윤성우", "27000", "3"));
        }
    }
}
