using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    class Library
    {
        static void Main(string[] args)
        {
            LibraryMenu libraryMenu = new LibraryMenu();
            libraryMenu.StartLibrary();
        }
    }
}
//관리자
//아이디 :   12345
//비밀번호 : 00000
//회원
//아이디 :   aaaccc
//비밀번호 : 000222
