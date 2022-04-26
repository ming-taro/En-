using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Connector
    {
        private static Connector instance = null;
        private static MySqlConnection connection;
        public static Connector GetInstance()
        {
            if (instance == null)
            {
                instance = new Connector();
            }

            return instance;
        }

        public MySqlConnection GetConnection()
        {
            try
            {
                connection = new MySqlConnection(Constants.SERVER + Constants.PORT + Constants.DATABASE + Constants.ID + Constants.PASSWORD);
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return connection;
        }

    }
}
