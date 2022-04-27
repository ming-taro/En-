﻿using MySql.Data.MySqlClient;
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
                case (int)Constants.SearchMenu.FIRST:      //도서명 검색
                    query = Constants.BOOK_NAME_SEARCH + searchWord + Constants.END_OF_SEARCH_QUERY;
                    break;
                case (int)Constants.SearchMenu.SECOND:     //출판사 검색
                    query = Constants.PUBLISHER_SEARCH + searchWord + Constants.END_OF_SEARCH_QUERY;
                    break;
                case (int)Constants.SearchMenu.THIRD:      //저자 검색
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
        public List<BookVO> MakeBorrowList(string memberId)
        {
            List<BookVO> borrowList = new List<BookVO>();
            string query = Constants.RENTAL_LIST + memberId + Constants.END_OF_STRING_QUERY;

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader table = command.ExecuteReader();

            while (table.Read())
            {
                borrowList.Add(new BookVO(table["id"].ToString(), table["name"].ToString(), table["publisher"].ToString(), table["author"].ToString(), table["price"].ToString(), table["quantity"].ToString()));
            }
            table.Close();

            return borrowList;
        }

        public void AddToRentalList(string memberId, string bookId)  //대여목록 추가
        {
            StartNonQuery(Constants.ADDITION_TO_RENTAL_LIST + memberId + "', '" + bookId + Constants.END_OF_VALUE_QUERY); //대여목록에 추가
            StartNonQuery(Constants.BOOK_QUANTITY_CORRECTION + bookId + Constants.END_OF_STRING_QUERY);                   //도서수량 -1
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
