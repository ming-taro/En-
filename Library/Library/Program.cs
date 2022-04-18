using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            //Library library = new Library();
            //library.StartLibrary();

            using(MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=booklist;Uid=root;Pwd=root"))
            {
                try//예외 처리
                {
                    connection.Open();
                    string sql = "select*from book";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        Console.WriteLine("{0} {1} {2} {3} {4} {5}", table["id"], table["name"], table["publisher"], table["author"], table["price"], table["quantity"]);
                    }
                    table.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }

            }

        }
    }
}
//관리자
//아이디 :   12345
//비밀번호 : 00000
//회원
//아이디 :   aaabbb
//비밀번호 : 000111
