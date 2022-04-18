using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LibraryVO
    {
        private static LibraryVO instance = null;
        private MySqlConnection connection;
        /// <summary>
        /// ///////////////////////////////////////////////////
        /// </summary>
        public List<BookVO> bookList = new List<BookVO>();
        public List<BorrowVO> borrowList = new List<BorrowVO>();
        public List<MemberVO> memberList = new List<MemberVO>();

        public static LibraryVO GetLibraryVO()
        {
            if (instance == null)
            {
                instance = new LibraryVO();
            }
            return instance;
        }
        private LibraryVO() 
        {
            try
            {
                connection = new MySqlConnection("Server=localhost;Port=3306;Database=booklist;Uid=root;Pwd=root");
                connection.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public MySqlConnection Connection
        {
            get { return connection; }
        }
        public void Close()
        {
            if(connection != null)
            {
                connection.Close();
            }
        }
   
        
        public void InsertBookList(string id, string name, string publisher, string author, string price, string quantity)  //책목록
        {
            try
            {
                string sql = "INSERT INTO booklist VALUES (" + id + "," + name + "," + publisher + "," + author + "," + price + "," + quantity + ");";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
                
                //MySqlDataReader table = command.ExecuteReader();
                //MySqlDataReader table = command.ExecuteReader();

                /*while (table.Read())
                {

                    //bookList.Add(new BookVO(table["id"].ToString(), table["name"].ToString(), table["publisher"].ToString(), table["author"].ToString(), table["price"].ToString(), table["quantity"].ToString()));
                    //Console.WriteLine("{0} {1} {2} {3} {4} {5}", table["id"], table["name"], table["publisher"], table["author"], table["price"], table["quantity"]);
                }
                table.Close();*/

            }
            catch (Exception ex)
            {
                Console.WriteLine("실패");
                Console.WriteLine(ex.ToString());
            }

        }
        /*public void SetMemberList()  //회원목록
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=booklist;Uid=root;Pwd=root"))
            {
                try//예외 처리
                {
                    connection.Open();
                    string sql = "select*from member";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    MySqlDataReader table = command.ExecuteReader();

                    while (table.Read())
                    {
                        memberList.Add(new MemberVO(table["id"].ToString(), table["password"].ToString(), table["name"].ToString(), table["age"].ToString(), table["phoneNumber"].ToString(), table["address"].ToString()));
                    }
                    table.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }

            }
        }*/
    }
}
