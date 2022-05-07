using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BookDAO
    {
        private static BookDAO bookDAO;
        private string connectionString;
        private MySqlConnection connection;
        private MySqlCommand command;
        private BookDAO()
        {
            connectionString = Constants.SERVER + Constants.PORT + Constants.DATABASE + Constants.ID + Constants.PASSWORD;
            connection = new MySqlConnection(connectionString);
        }
        public static BookDAO GetInstance()
        {
            if (bookDAO == null)
            {
                bookDAO = new BookDAO();
            }
            return bookDAO;
        }
        private void OpenConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

            }
        }
        public List<BookDTO> GetBookList(int searchType, string searchWord)  //검색 결과 도서목록
        {
            List<BookDTO> bookList = new List<BookDTO>();
            string query = "";
            int bookId = 1;

            switch (searchType)
            {
                case (int)Constants.SearchMenu.ALL:        //도서목록 전체 출력
                    query = Constants.BOOK_LIST;
                    break; 
                case (int)Constants.SearchMenu.FIRST:      //도서명 검색
                    query = string.Format(Constants.BOOK_NAME_SEARCH, searchWord.ToString());
                    break;
                case (int)Constants.SearchMenu.SECOND:     //출판사 검색
                    query = string.Format(Constants.PUBLISHER_SEARCH , searchWord.ToString());
                    break;
                case (int)Constants.SearchMenu.THIRD:      //저자 검색
                    query = string.Format(Constants.AUTHOR_SEARCH, searchWord.ToString());
                    break;
            }

            OpenConnection();
            command = new MySqlCommand(query, connection);
            MySqlDataReader table = command.ExecuteReader();

            while (table.Read())
            {
                bookList.Add(new BookDTO((bookId++).ToString(), table["name"].ToString(), table["author"].ToString(), table["publisher"].ToString(), table["publicationDate"].ToString(),
                    table["isbn"].ToString(), table["price"].ToString(), table["bookIntroduction"].ToString(), table["quantity"].ToString()));
            }
            table.Close();
            connection.Close();

            return bookList;
        }
        public List<BookDTO> GetMyBookList(string query, string memberId)    //회원의 도서대여목록
        {
            List<BookDTO> myBookList = new List<BookDTO>();
            BookDTO book;
            int bookId = 1;

            OpenConnection();
            command = new MySqlCommand(query, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            MySqlDataReader table = command.ExecuteReader();
            while (table.Read())
            {
                book = new BookDTO((bookId++).ToString(), table["name"].ToString(), table["author"].ToString(), table["publisher"].ToString(), table["publicationDate"].ToString(),
                    table["isbn"].ToString(), table["price"].ToString(), table["bookIntroduction"].ToString(), table["quantity"].ToString());
                book.MemberId = table["memberId"].ToString();          //회원아이디 추가
                book.RentalPeriod = table["rentalPeriod"].ToString();  //대여기간 추가
                myBookList.Add(book);
            }
            table.Close();
            connection.Close();

            return myBookList;
        }
        public bool HasRows()
        {
            bool has_rows = Constants.NOT_HAS_ROW;
            MySqlDataReader table = command.ExecuteReader();
            table.Read();

            if (table.HasRows) has_rows = Constants.HAS_ROW;

            table.Close();
            connection.Close();

            return has_rows;
        }
        public bool IsMemberWhoBorrowedBook(string memberId)  //회원이 도서를 대여중인지 확인
        {
            OpenConnection();
            command = new MySqlCommand(Constants.MEMBER_WHO_BORROWED_BOOK, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            if(HasRows()) return Constants.IS_MEMBER_BORROWING_BOOK;
            return Constants.IS_MEMBER_NOT_BORROWING_BOOK;
        }
        
        public bool IsDuplicateBookId(string bookId)   //입력한 도서번호가 중복된 아이디인지 검사---->isbn으로 수정해야함
        {
            OpenConnection();
            command = new MySqlCommand(Constants.DUPLICATE_BOOK_ID, connection);
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));

            if (HasRows()) return Constants.IS_DUPLICATE_ID;  //이미 존재하는 아이디
            return Constants.IS_NON_DUPLICATE_ID;             //중복없는 아이디 -> 입력가능
        }
        public bool IsBookOnLoan(string isbn)   //해당 도서를 대여중인 회원이 있는지 확인
        {
            OpenConnection();
            command = new MySqlCommand(Constants.BOOK_ON_LOAN, connection);
            command.Parameters.Add(new MySqlParameter("@isbn", isbn));

            if (HasRows()) return Constants.IS_BOOK_ON_LOAN;    //대여중인 회원이 있는 도서 -> 삭제 불가
            return Constants.IS_BOOK_NOT_ON_LOAN;               //대여한 회원이 없는 도서 -> 삭제 가능
        }

        public void AddToRentalList(string memberId, string isbn)  //대여목록 추가
        {
            OpenConnection();
            command = new MySqlCommand(Constants.ADDITION_TO_RENTAL_LIST, connection);  //대여목록에 추가
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.Parameters.Add(new MySqlParameter("@isbn", isbn));
            command.Parameters.Add(new MySqlParameter("@rentalPeriod", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " ~ " + DateTime.Now.AddDays(14).ToString("yyyy/MM/dd HH:mm:ss")));
            command.ExecuteNonQuery();

            command = new MySqlCommand(Constants.DECREASE_IN_BOOK_QUANTITY, connection);   //도서수량 -1
            command.Parameters.Add(new MySqlParameter("@isbn", isbn));                
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteFromRentalList(string memberId, string rentalPeriod, string isbn) //대여도서 반납 후 대여목록에서 삭제
        {
            OpenConnection();
            command = new MySqlCommand(Constants.DELETION_FROM_RENTAL_LIST, connection); //대여목록에서 삭제
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.Parameters.Add(new MySqlParameter("@rentalPeriod", rentalPeriod));
            command.ExecuteNonQuery();

            command = new MySqlCommand(Constants.INCREASE_IN_BOOK_QUANTITY, connection);   //도서수량 +1
            command.Parameters.Add(new MySqlParameter("@isbn", isbn));
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void AddToBookList(string query, BookDTO book)  //책목록에 도서정보 추가
        {
            OpenConnection();
            command = new MySqlCommand(query, connection);
            command.Parameters.Add(new MySqlParameter("@name", book.Name));
            command.Parameters.Add(new MySqlParameter("@author", book.Author));
            command.Parameters.Add(new MySqlParameter("@publisher", book.Publisher));
            command.Parameters.Add(new MySqlParameter("@publicationDate", book.PublicationDate));
            command.Parameters.Add(new MySqlParameter("@isbn", book.Isbn));
            command.Parameters.Add(new MySqlParameter("@price", book.Price));
            command.Parameters.Add(new MySqlParameter("@bookIntroduction", book.BookIntroduction));
            command.Parameters.Add(new MySqlParameter("@quantity", Int32.Parse(book.Quantity)));
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteFromBookList(string isbn)  //도서목록에서 해당 도서 삭제
        {
            OpenConnection();
            command = new MySqlCommand(Constants.DELETION_FROM_BOOK_LIST, connection);
            command.Parameters.Add(new MySqlParameter("@isbn", isbn));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
