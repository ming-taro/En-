﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LogDAO
    {
        private static LogDAO logDAO;
        private string connectionString;
        private MySqlConnection connection;
        private MySqlCommand command;
        private LogDAO()
        {
            connectionString = Constants.SERVER + Constants.PORT + Constants.DATABASE + Constants.ID + Constants.PASSWORD;
        }
        public static LogDAO GetInstance()
        {
            if (logDAO == null)
            {
                logDAO = new LogDAO();
            }
            return logDAO;
        }
        private void OpenConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

            }
        }
        public List<LogDTO> GetLogList()
        {
            List<LogDTO> logList = new List<LogDTO>();

            OpenConnection();
            command = new MySqlCommand(Constants.LOG_LIST, connection);
            MySqlDataReader table = command.ExecuteReader();

            int number = 1;
            while (table.Read())
            {
                logList.Add(new LogDTO(number++, table["user"].ToString(), table["menu"].ToString(), table["content"].ToString(), table["date"].ToString()));
            }
            table.Close();
            connection.Close();

            return logList;
        }
        public void AddToRentalList(string memberId, string bookName)
        {
            StartNonQuery(memberId, "도서 대출", bookName);
        }
        public void DeleteFromRentalList(string memberId, string bookName)
        {
            StartNonQuery(memberId, "도서 반납", bookName);
        }
        public void SignInSuccessfully(string memberId)
        {
            if(memberId.Equals("12345")) StartNonQuery(memberId, "로그인", "관리자 로그인");
            else StartNonQuery(memberId, "로그인", "회원 로그인");
        }
        public void SingUpSuccessfully(string memberId)
        {
            StartNonQuery(memberId, "회원가입", memberId);
        }
        public void DeleteFromBookList(string bookName)
        {
            StartNonQuery("12345", "도서 삭제", bookName);
        }
        public void EditBook(string bookName)
        {
            StartNonQuery("12345", "도서 정보 수정", bookName);
        }
        public void RegisterBook(string bookName)
        {
            StartNonQuery("12345", "도서 등록", bookName);
        }
        public void DeleteFromMemberList(string memberId)
        {
            StartNonQuery("12345", "회원 삭제", memberId);
        }
        public void SearchBook(string memberId, string searchWord)
        {
            StartNonQuery(memberId, "도서 검색", searchWord);
        }
        public void SearchBookOnNaver(string memberId, string searchWord)
        {
            StartNonQuery(memberId, "네이버 도서 검색", searchWord);
        }
        public void EditMemberInformation(string memberId)
        {
            StartNonQuery(memberId, "회원 정보 수정", memberId);
        }
        public void InitializeLogTable()
        {
            OpenConnection();
            command = new MySqlCommand(Constants.LOG_INITIALIZATION, connection);
            command.ExecuteNonQuery();
        }
        public void DeleteFromLogList(string date)
        {
            OpenConnection();
            command = new MySqlCommand(Constants.DELETION_FROM_LOG_LIST, connection);
            command.Parameters.Add(new MySqlParameter("@date", date));
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void StartNonQuery(string user, string menu, string content)
        {
            OpenConnection();
            command = new MySqlCommand(Constants.UPDATE_TO_LOG, connection);
            command.Parameters.Add(new MySqlParameter("@user", user));
            command.Parameters.Add(new MySqlParameter("@menu", menu));
            command.Parameters.Add(new MySqlParameter("@content", content));
            command.Parameters.Add(new MySqlParameter("@date", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
            command.ExecuteNonQuery();
        }
    }
}
