using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BookDAO
    {
        private static BookDAO bookDAO;
        private string connectionString;
        private BookDAO()
        {
            connectionString = Constants.SERVER + Constants.PORT + Constants.DATABASE + Constants.ID + Constants.PASSWORD;
        }
        public static BookDAO GetInstance()
        {
            if (bookDAO == null)
            {
                bookDAO = new BookDAO();
            }
            return bookDAO;
        }
        public List<BookVO> MakeBookList(int searchType, string searchWord)  //검색 결과 도서목록
        {
            List<BookVO> bookList = new List<BookVO>();
            string query = "";

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

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader table = command.ExecuteReader();

            while (table.Read())
            {
                bookList.Add(new BookVO(table["id"].ToString(), table["name"].ToString(), table["author"].ToString(), table["publisher"].ToString(), table["publicationDate"].ToString(),
                    table["isbn"].ToString(), table["price"].ToString(), table["bookIntroduction"].ToString(), table["quantity"].ToString()));
            }
            table.Close();
            connection.Close();

            return bookList;
        }
        public List<BorrowBookVO> MakeMyBookList(string query, string memberId)    //회원의 도서대여목록
        {
            List<BorrowBookVO> borrowList = new List<BorrowBookVO>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            MySqlDataReader table = command.ExecuteReader();
            while (table.Read())
            {
                borrowList.Add(new BorrowBookVO(table["memberId"].ToString(), table["id"].ToString(), table["name"].ToString(), table["publisher"].ToString(), table["author"].ToString(), table["price"].ToString(), table["rentalPeriod"].ToString()));
            }
            table.Close();
            connection.Close();

            return borrowList;
        }
        public bool IsMemberBorrowingBook(string memberId)  //회원이 도서를 대여중인지 확인
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(Constants.MEMBER_BORROWING_BOOK, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            MySqlDataReader table = command.ExecuteReader();
            table.Read();
            if (table.HasRows)
            {
                table.Close();
                connection.Close();
                return Constants.IS_MEMBER_BORROWING_BOOK;
            }
            else
            {
                table.Close();
                connection.Close();
                return Constants.IS_MEMBER_NOT_BORROWING_BOOK;
            }
        }
        public bool IsDuplicateBookId(string bookId)   //입력한 도서번호가 중복된 아이디인지 검사
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(Constants.DUPLICATE_BOOK_ID, connection);
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));

            MySqlDataReader table = command.ExecuteReader();
            if (table.HasRows)
            {
                table.Close();
                connection.Close();
                return Constants.IS_DUPLICATE_ID;  //이미 존재하는 아이디
            }

            table.Close();
            connection.Close();
            return Constants.IS_NON_DUPLICATE_ID;  //중복없는 아이디 -> 입력가능
        }
        public bool IsBookOnLoan(string bookId)   //해당 도서를 대여중인 회원이 있는지 확인
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(Constants.BOOK_ON_LOAN, connection);
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));

            MySqlDataReader table = command.ExecuteReader();
            if (table.HasRows)
            {
                table.Close();
                connection.Close();
                return Constants.IS_BOOK_ON_LOAN;     //대여중인 회원이 있는 도서 -> 삭제 불가
            }

            table.Close();
            connection.Close();
            return Constants.IS_BOOK_NOT_ON_LOAN;   //대여한 회원이 없는 도서 -> 삭제 가능
        }

        public void AddToRentalList(string memberId, string bookId)  //대여목록 추가
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(Constants.ADDITION_TO_RENTAL_LIST, connection);  //대여목록에 추가
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.Parameters.Add(new MySqlParameter("@rentalPeriod", DateTime.Now.ToString("yyyy.MM.dd") + " ~ " + DateTime.Now.AddDays(14).ToString("yyyy.MM.dd")));
            command.ExecuteNonQuery();

            command = new MySqlCommand(Constants.DECREASE_IN_BOOK_QUANTITY, connection);   //도서수량 -1
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));                
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteFromRentalList(string memberId, string bookId) //대여도서 반납 후 대여목록에서 삭제
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(Constants.DELETION_FROM_RENTAL_LIST, connection); //대여목록에서 삭제
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.ExecuteNonQuery();

            command = new MySqlCommand(Constants.INCREASE_IN_BOOK_QUANTITY, connection);   //도서수량 +1
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void AddToBookList(string query, BookVO book)  //책목록에 도서정보 추가
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Add(new MySqlParameter("@id", book.Id));
            command.Parameters.Add(new MySqlParameter("@name", book.Name));
            command.Parameters.Add(new MySqlParameter("@publisher", book.Publisher));
            command.Parameters.Add(new MySqlParameter("@author", book.Author));
            command.Parameters.Add(new MySqlParameter("@price", book.Price));
            command.Parameters.Add(new MySqlParameter("@quantity", Int32.Parse(book.Quantity)));
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteFromBookList(string bookId)  //도서목록에서 해당 도서 삭제
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(Constants.DELETION_FROM_BOOK_LIST, connection);
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateOnBookId(string changedId, string bookId)  //도서번호 수정
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(Constants.UPDATE_ON_BOOK_ID, connection);
            command.Parameters.Add(new MySqlParameter("@id", changedId));
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
