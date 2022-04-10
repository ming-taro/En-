﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BorrowingScreen
    {
        public void PrintBorrowing()
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintSearchBox("\n☞도서명으로 검색: ");  //검색창 출력

            BookListVO bookListVO = BookListVO.GetBookListVO();
            ListScreen listScreen = new ListScreen();
            listScreen.PrintBookList(bookListVO.bookList);        //도서목록 출력
        }
    }
}
