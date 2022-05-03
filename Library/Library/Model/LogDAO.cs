using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LogDAO
    {
        private static LogDAO logDAO;
        private string connectionString;
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
        public void AddToRentalList(string memberId, string bookName)
        {
            string query = Constants.UPDATE_TO_LOG;
            StartNonQuery(query, memberId, "도서 대출", bookName);
        }
        public void DeleteFromRentalList(string memberId, string bookName)
        {
            string query = Constants.UPDATE_TO_LOG;
            StartNonQuery(query, memberId, "도서 반납", bookName);
        }

        public void StartNonQuery(string query, string user, string menu, string content)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Add(new MySqlParameter("@user", user));
            command.Parameters.Add(new MySqlParameter("@menu", menu));
            command.Parameters.Add(new MySqlParameter("@content", content));
            command.Parameters.Add(new MySqlParameter("@date", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
            command.ExecuteNonQuery();
        }
    }
}
