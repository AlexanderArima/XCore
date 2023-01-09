using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using XCore.Print.Dapper;
using Dapper;

namespace XCore.Print.ORM
{
    public class UseMySQL
    {
        public static void MainFunc()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<MySQLHelper>();
            using (var provider = services.BuildServiceProvider())
            {
                var helper = provider.GetRequiredService<MySQLHelper>();
                var connection = helper.OpenCurrentDbConnection("server=localhost;port=3306;userid=root;pwd=123456;database=db_hotel");
                var list = helper.Query<User>("SELECT * FROM t_user");
            }
        }
    }
}
