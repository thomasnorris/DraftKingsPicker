using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftKingsUpdater
{
    public class MySQLService
    {
        private MySqlConnection _conn;

        public MySQLService()
        {
            try
            {
                var connBuilder = new MySqlConnectionStringBuilder();

                connBuilder.Add("Data Source", ConfigurationManager.AppSettings.Get("MySQLHost"));
                connBuilder.Add("Database", ConfigurationManager.AppSettings.Get("MySQLDB"));
                connBuilder.Add("User Id", ConfigurationManager.AppSettings.Get("MySQLUser"));
                connBuilder.Add("Password", ConfigurationManager.AppSettings.Get("MySQLPass"));
                connBuilder.Add("Port", ConfigurationManager.AppSettings.Get("MySQLPort"));

                _conn = new MySqlConnection(connBuilder.ConnectionString);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void OpenConnection()
        {
            _conn.Open();
        }

        public void CloseConnection()
        {
            _conn.Close();
        }

        public int ExecuteStoredProc(string procName, List<MySqlParameter> args, bool openAndCloseConn = false)
        {
            var command = _conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procName;

            foreach(var arg in args)
            {
                command.Parameters.Add(arg);
            }

            return ExecuteNonCommand(command, openAndCloseConn);
        }

        public MySqlDataReader ExecuteStringQuery(string query, bool openAndCloseConn = false)
        {
            var command = _conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = query;

            return ExecuteCommand(command, openAndCloseConn);
        }

        private int ExecuteNonCommand(MySqlCommand command, bool openAndCloseConn = false)
        {
            if (openAndCloseConn)
                _conn.Open();

            command.Connection = _conn;

            var rows_affected = command.ExecuteNonQuery();

            if (openAndCloseConn)
                _conn.Close();

            return rows_affected;
        }

        private MySqlDataReader ExecuteCommand(MySqlCommand command, bool openAndCloseConn)
        {
            if (openAndCloseConn)
                _conn.Open();

            command.Connection = _conn;

            var res = command.ExecuteReader();

            if (openAndCloseConn)
                _conn.Close();

            return res;
        }
    }
}
