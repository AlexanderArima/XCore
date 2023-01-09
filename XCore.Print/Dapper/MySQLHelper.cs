using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace XCore.Print.Dapper
{
    public class MySQLHelper : IDapperHelper
    {
        public IDbConnection dbConnection { get; set; }

        public string connectionString { get; set; }

        public MySqlConnection OpenCurrentDbConnection(string connectionString)
        {
            this.connectionString = connectionString;
            dbConnection = OpenCurrentDbConnection();
            return dbConnection as MySqlConnection;
        }

        public IDbConnection OpenCurrentDbConnection()
        {
            if (dbConnection == null)
            {
                dbConnection = new MySqlConnection(connectionString);
            }

            //判断连接状态
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            return dbConnection;
        }

        public IEnumerable<dynamic> Query<T>(string sql, object param = null)
        {
            return dbConnection.Query(sql, param);
        }

        public int Execute(string sql, object param = null)
        {
            return dbConnection.Execute(sql, param);
        }
    }
}
