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
            string query = string.Format(Constants.MEMBER_ACCOUNT, memberId.ToString());

            MySqlCommand command = new MySqlCommand(query, connection);
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
        public List<BookVO> MakeMyBookList(string memberId)    //회원의 도서대여목록
        {
            List<BookVO> borrowList = new List<BookVO>();
            string query = string.Format(Constants.RENTAL_LIST, memberId.ToString());

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader table = command.ExecuteReader();

            while (table.Read())
            {
                borrowList.Add(new BookVO(table["id"].ToString(), table["name"].ToString(), table["publisher"].ToString(), table["author"].ToString(), table["price"].ToString(), table["quantity"].ToString()));
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
        public bool IsExistingMember(string id, string password)  //입력된 정보가 존재하는 회원인지 확인
        {
            string query = string.Format(Constants.MEMBER_CONFIRMATION, id.ToString(), password.ToString());

            MySqlCommand command = new MySqlCommand(query, connection);
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
        public bool IsDuplicatedId(string id)  //기존 회원의 아이디와 중복되는지 확인
        {
            string query = string.Format(Constants.DUPLICATED_ID, id.ToString());

            MySqlCommand command = new MySqlCommand(query, connection);
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
        public bool IsMemberBorrowingBook(string id)  //회원이 도서를 대여중인지 확인
        {
            string query = string.Format(Constants.MEMBER_BORROWING_BOOK, id.ToString());

            MySqlCommand command = new MySqlCommand(query, connection);
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

        public void AddToRentalList(string memberId, string bookId)  //대여목록 추가
        {
            StartNonQuery(string.Format(Constants.ADDITION_TO_RENTAL_LIST, memberId.ToString(), bookId.ToString())); //대여목록에 추가
            StartNonQuery(string.Format(Constants.DECREASE_IN_BOOK_QUANTITY, bookId.ToString()));                    //도서수량 -1
        }
        public void DeleteFromRentalList(string memberId, string bookId) //대여도서 반납 후 대여목록에서 삭제
        {
            StartNonQuery(string.Format(Constants.DELETION_FROM_RENTAL_LIST, memberId.ToString(), bookId.ToString()));  //대여목록에서 삭제
            StartNonQuery(string.Format(Constants.INCREASE_IN_BOOK_QUANTITY, bookId.ToString()));                                //도서수량 +1
        }
        public void AddToMember(MemberVO memberAccount)   //회원목록에 회원정보 추가
        {
            StartNonQuery(Constants.ADDITION_TO_MEMBER + memberAccount.PrintMember() + ";");
        }
        public void UpdateToMember(int menu, string id, string changedItem) //회원정보 수정(menu:수정할 항목, id:회원아이디, changedItem:수정된 정보)
        {
            switch (menu)
            {
                case (int)Constants.ProfileMenu.FIRST:    //아이디
                    StartNonQuery(string.Format(Constants.UPDATE_ON_MEMBER_ID, changedItem.ToString(), id.ToString()));    //회원목록에 회원 아이디 수정
                    StartNonQuery(string.Format(Constants.UPDATE_ON_RENTAL_LIST, changedItem.ToString(), id.ToString()));  //도서대여목록에서 회원 아이디 수정
                    break;
                case (int)Constants.ProfileMenu.SECOND:   //비밀번호
                    StartNonQuery(string.Format(Constants.UPDATE_ON_PASSWORD, changedItem.ToString(), id.ToString()));
                    break;
                case (int)Constants.ProfileMenu.FOURTH:   //이름
                    StartNonQuery(string.Format(Constants.UPDATE_ON_MEMBER_NAME, changedItem.ToString(), id.ToString()));
                    break;
                case (int)Constants.ProfileMenu.FIFTH:   //나이
                    StartNonQuery(string.Format(Constants.UPDATE_ON_AGE, changedItem.ToString(), id.ToString()));
                    break;
                case (int)Constants.ProfileMenu.SIXTH:   //휴대전화
                    StartNonQuery(string.Format(Constants.UPDATE_ON_PHONE_NUMBER, changedItem.ToString(), id.ToString()));
                    break;
                case (int)Constants.ProfileMenu.SEVENTH: //주소
                    StartNonQuery(string.Format(Constants.UPDATE_ON_ADDRESS, changedItem.ToString(), id.ToString()));
                    break;
            }
        }
        public void DeleteFromMemberList(string id)      //회원목록에서 회원정보 삭제
        {
            StartNonQuery(string.Format(Constants.DELETION_FROM_MEMBER_LIST, id.ToString()));
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
    }
}
