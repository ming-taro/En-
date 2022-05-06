using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Library
{
    class Library
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string s = Console.ReadLine();
                if (Regex.IsMatch(s, @"^\w{0,4}$")) Console.WriteLine("맞아");
            }


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
