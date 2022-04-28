﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Connector
    {
        private static MySqlConnection connection;

        public static MySqlConnection GetConnection()
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