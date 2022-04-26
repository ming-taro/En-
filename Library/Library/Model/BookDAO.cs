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
        public List<BookVO> MakeBookList(int searchType, string searchWord)
        {
            List<BookVO> bookList = new List<BookVO>();
            string query = "";

            switch (searchType)
            {
                case (int)Constants.SearchMenu.ALL:        //도서목록 전체 출력
                    query = Constants.BOOK_LIST;
                    break;
                case (int)Constants.SearchMenu.BOOK_NAME:  //도서명 검색
                    query = Constants.BOOK_NAME_SEARCH + searchWord + Constants.END_OF_SEARCH_QUERY;
                    break;
                case (int)Constants.SearchMenu.PUBLISHER:  //출판사 검색
                    query = Constants.PUBLISHER_SEARCH + searchWord + Constants.END_OF_SEARCH_QUERY;
                    break;
                case (int)Constants.SearchMenu.AUTHOR:     //저자 검색
                    query = Constants.AUTHOR_SEARCH + searchWord + Constants.END_OF_SEARCH_QUERY;
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
    }
}
