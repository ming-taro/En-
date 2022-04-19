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
                connection = new MySqlConnection("Server=localhost;Port=3306;Database=booklist;Uid=root;Pwd=0000");
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
            StartQuery("INSERT INTO booklist VALUES ('" + id + "','" + name + "','" + publisher + "','" + author + "','" + price + "'," + quantity + ");");
        }
        public void InsertMember(string id, string password, string name, string age, string phoneNumber, string address)
        {
            StartQuery("INSERT INTO member VALUES ('" + id + "','" + password + "','" + name + "','" + age + "','" + phoneNumber + "','" + address + "');");
        }
        public void InsertBorrowBook(string memberId, string bookId)
        {
            StartQuery("INSERT INTO borrowBook(memberId,bookId) VALUES ('" + memberId + "', '" + bookId + "');");
        }
        public void DeleteBorrowBook(string memberId, string bookId)
        {
            StartQuery("DELETE FROM borrowBook WHERE memberId='" + memberId + "' and bookId='" + bookId + "';");
        }
        public void UpdateQuantity(string bookId)
        {
            StartQuery("UPDATE book SET quantity = quantity + 1 WHERE id='" + bookId + "';");
        }
        public void StartQuery(string sql)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("실패");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
