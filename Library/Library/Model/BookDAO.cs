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
        private MySqlConnection connection;
        public BookDAO()
        {
            connection = Connector.GetConnection();
        }
        public AdminVO GetAdminAccount()   //관리자 계정
        { 
            AdminVO admin;

            MySqlCommand command = new MySqlCommand(Constants.ADMIN_ACCOUNT, connection);
            MySqlDataReader table = command.ExecuteReader();

            table.Read();
            admin = new AdminVO(table["id"].ToString(), table["password"].ToString());
            table.Close();

            return admin;
        }
        public MemberVO GetMemberAccount(string memberId)   //입력받은 id와 일치하는 회원계정
        {
            MemberVO member;

            MySqlCommand command = new MySqlCommand(Constants.MEMBER_ACCOUNT, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            MySqlDataReader table = command.ExecuteReader();
            table.Read();
            member = new MemberVO(table["id"].ToString(), table["password"].ToString(), table["name"].ToString(), table["age"].ToString(), table["phoneNumber"].ToString(), table["address"].ToString());
            table.Close();

            return member;
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

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader table = command.ExecuteReader();

            while (table.Read())
            {
                bookList.Add(new BookVO(table["id"].ToString(), table["name"].ToString(), table["publisher"].ToString(), table["author"].ToString(), table["price"].ToString(), table["quantity"].ToString()));
            }
            table.Close();

            return bookList;
        }
        public List<BorrowBookVO> MakeMyBookList(string query, string memberId)    //회원의 도서대여목록
        {
            List<BorrowBookVO> borrowList = new List<BorrowBookVO>();

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            MySqlDataReader table = command.ExecuteReader();
            while (table.Read())
            {
                borrowList.Add(new BorrowBookVO(table["memberId"].ToString(), table["id"].ToString(), table["name"].ToString(), table["publisher"].ToString(), table["author"].ToString(), table["price"].ToString(), table["rentalPeriod"].ToString()));
            }
            table.Close();

            return borrowList;
        }
        public List<MemberVO> MakeMemberList()  //회원목록
        {
            List<MemberVO> memberList = new List<MemberVO>();

            MySqlCommand command = new MySqlCommand(Constants.MEMBER_LIST, connection);
            MySqlDataReader table = command.ExecuteReader();

            while (table.Read())
            {
                memberList.Add(new MemberVO(table["id"].ToString(), table["password"].ToString(), table["name"].ToString(), table["age"].ToString(), table["phoneNumber"].ToString(), table["address"].ToString()));
            }
            table.Close();

            return memberList;
        }
        public bool IsExistingMember(string memberId, string password)  //입력된 정보가 존재하는 회원인지 확인
        {
            MySqlCommand command = new MySqlCommand(Constants.MEMBER_CONFIRMATION, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.Parameters.Add(new MySqlParameter("@password", password));

            MySqlDataReader table = command.ExecuteReader();
            table.Read();
            if (table.HasRows)
            {
                table.Close();
                return Constants.IS_EXISTING_MEMBER;
            }
            else
            {
                table.Close();
                return Constants.IS_NON_EXISTING_MEMBER;
            }
        }
        public bool IsDuplicateMemberId(string memberId)  //기존 회원의 아이디와 중복되는지 확인
        {
            MySqlCommand command = new MySqlCommand(Constants.DUPLICATE_MEMBER_ID, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            MySqlDataReader table = command.ExecuteReader();
            table.Read();
            if (table.HasRows)
            {
                table.Close();
                return Constants.IS_DUPLICATE_ID;
            }
            else
            {
                table.Close();
                return Constants.IS_NON_DUPLICATE_ID;
            }
        }
        public bool IsMemberBorrowingBook(string memberId)  //회원이 도서를 대여중인지 확인
        {
            MySqlCommand command = new MySqlCommand(Constants.MEMBER_BORROWING_BOOK, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            MySqlDataReader table = command.ExecuteReader();
            table.Read();
            if (table.HasRows)
            {
                table.Close();
                return Constants.IS_MEMBER_BORROWING_BOOK;
            }
            else
            {
                table.Close();
                return Constants.IS_MEMBER_NOT_BORROWING_BOOK;
            }
        }
        public bool IsDuplicateBookId(string bookId)   //입력한 도서번호가 중복된 아이디인지 검사
        {
            MySqlCommand command = new MySqlCommand(Constants.DUPLICATE_BOOK_ID, connection);
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));

            MySqlDataReader table = command.ExecuteReader();
            if (table.HasRows)
            {
                table.Close();
                return Constants.IS_DUPLICATE_ID;  //이미 존재하는 아이디
            }

            table.Close();
            return Constants.IS_NON_DUPLICATE_ID;  //중복없는 아이디 -> 입력가능
        }
        public bool IsBookOnLoan(string bookId)   //해당 도서를 대여중인 회원이 있는지 확인
        {
            MySqlCommand command = new MySqlCommand(Constants.BOOK_ON_LOAN, connection);
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));

            MySqlDataReader table = command.ExecuteReader();
            if (table.HasRows)
            {
                table.Close();
                return Constants.IS_BOOK_ON_LOAN;     //대여중인 회원이 있는 도서 -> 삭제 불가
            }

            table.Close();
            return Constants.IS_BOOK_NOT_ON_LOAN;   //대여한 회원이 없는 도서 -> 삭제 가능
        }

        public void AddToRentalList(string memberId, string bookId)  //대여목록 추가
        {
            MySqlCommand command = new MySqlCommand(Constants.ADDITION_TO_RENTAL_LIST, connection);  //대여목록에 추가
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.Parameters.Add(new MySqlParameter("@rentalPeriod", DateTime.Now.ToString("yyyy.MM.dd") + " ~ " + DateTime.Now.AddDays(14).ToString("yyyy.MM.dd")));
            command.ExecuteNonQuery();

            command = new MySqlCommand(Constants.DECREASE_IN_BOOK_QUANTITY, connection);   //도서수량 -1
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));                
            command.ExecuteNonQuery();
        }
        public void DeleteFromRentalList(string memberId, string bookId) //대여도서 반납 후 대여목록에서 삭제
        {
            MySqlCommand command = new MySqlCommand(Constants.DELETION_FROM_RENTAL_LIST, connection); //대여목록에서 삭제
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.ExecuteNonQuery();

            command = new MySqlCommand(Constants.INCREASE_IN_BOOK_QUANTITY, connection);   //도서수량 +1
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.ExecuteNonQuery();
        }
        public void AddToBookList(string query, BookVO book)  //책목록에 도서정보 추가
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Add(new MySqlParameter("@id", book.Id));
            command.Parameters.Add(new MySqlParameter("@name", book.Name));
            command.Parameters.Add(new MySqlParameter("@publisher", book.Publisher));
            command.Parameters.Add(new MySqlParameter("@author", book.Author));
            command.Parameters.Add(new MySqlParameter("@price", book.Price));
            command.Parameters.Add(new MySqlParameter("@quantity", Int32.Parse(book.Quantity)));
            command.ExecuteNonQuery();
        }
        public void AddToMemberList(string query, MemberVO member)   //회원목록에 회원정보 추가
        {
            MySqlCommand command = new MySqlCommand(query, connection); 
            command.Parameters.Add(new MySqlParameter("@id", member.Id));
            command.Parameters.Add(new MySqlParameter("@password", member.Password));
            command.Parameters.Add(new MySqlParameter("@name", member.Name));
            command.Parameters.Add(new MySqlParameter("@age", member.Age));
            command.Parameters.Add(new MySqlParameter("@phoneNumber", member.PhoneNumber));
            command.Parameters.Add(new MySqlParameter("@address", member.Address));
            command.ExecuteNonQuery();
        }
        public void UpdateOnMemberId(string changedId, string memberId)  //회원 아이디 수정
        {
            MySqlCommand command = new MySqlCommand(Constants.UPDATE_ON_MEMBER_ID, connection);
            command.Parameters.Add(new MySqlParameter("@id", changedId));
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.ExecuteNonQuery();
        }

        public void DeleteFromMemberList(string memberId)      //회원목록에서 회원정보 삭제
        { 
            MySqlCommand command = new MySqlCommand(Constants.DELETION_FROM_MEMBER_LIST, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.ExecuteNonQuery();
        }
        
        public void DeleteFromBookList(string bookId)  //도서목록에서 해당 도서 삭제
        {
            MySqlCommand command = new MySqlCommand(Constants.DELETION_FROM_BOOK_LIST, connection);
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.ExecuteNonQuery();
        }
        public void UpdateOnBookId(string changedId, string bookId)  //도서번호 수정
        {
            MySqlCommand command = new MySqlCommand(Constants.UPDATE_ON_BOOK_ID, connection);
            command.Parameters.Add(new MySqlParameter("@id", changedId));
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.ExecuteNonQuery();
        }
    }
}
