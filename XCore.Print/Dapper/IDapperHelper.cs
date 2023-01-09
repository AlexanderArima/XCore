using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace XCore.Print.Dapper
{
    interface IDapperHelper
    {
        public IDbConnection dbConnection { get; set; }

        public string connectionString { get; set; }

        IDbConnection OpenCurrentDbConnection();

        IEnumerable<dynamic> Query<T>(string sql, object param = null);

        int Execute(string sql, object param = null);
    }
}
