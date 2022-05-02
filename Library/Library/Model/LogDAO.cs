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
        public static LogDAO GetInstance()
        {
            if (logDAO == null)
            {
                logDAO = new LogDAO();
            }
            return logDAO;
        }
        public void AddToRentalList(MySqlCommand command, MySqlConnection connection)
        {
            command = new MySqlCommand(Constants.DECREASE_IN_BOOK_QUANTITY, connection); 
            command.Parameters.Add(new MySqlParameter("@bookId", bookId));
            command.ExecuteNonQuery();
        }
    }
}
