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
   
        public void InsertBookList(string id, string name, string publisher, string author, string price, string quantity)  //책목록에 도서정보 추가
        {
            StartNonQuery("INSERT INTO booklist VALUES ('" + id + "','" + name + "','" + publisher + "','" + author + "','" + price + "'," + quantity + ");");
        }
        public void InsertMember(string id, string password, string name, string age, string phoneNumber, string address)   //회원목록에 회원정보 추가
        {
            StartNonQuery("INSERT INTO member VALUES ('" + id + "','" + password + "','" + name + "','" + age + "','" + phoneNumber + "','" + address + "');");
        }
        public void UpdateMember(string id, string query)
        {
            StartNonQuery("UPDATE member SET " + query + " WHERE id='" + id + "';");
        }
        public void InsertBorrowBook(string memberId, string bookId)  //대여목록 추가
        {
            StartNonQuery("INSERT INTO borrowBook(memberId,bookId) VALUES ('" + memberId + "', '" + bookId + "');"); //대여목록에 추가
            StartNonQuery("UPDATE book SET quantity = quantity - 1 WHERE id='" + bookId + "';");                     //도서수량 -1
        }
        public void DeleteBorrowBook(string memberId, string bookId)  //대여도서 반납 후 대여목록에서 삭제
        {
            StartNonQuery("DELETE FROM borrowBook WHERE memberId='" + memberId + "' and bookId='" + bookId + "';");  //대여목록에서 삭제
            StartNonQuery("UPDATE book SET quantity = quantity + 1 WHERE id='" + bookId + "';");                     //도서수량 +1
        }
        public void StartNonQuery(string query)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("실패");
                Console.WriteLine(ex.ToString());
            }
        }
        public void StartQuery(string query)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(query + ";", connection);
                MySqlDataReader table = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("실패");
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
