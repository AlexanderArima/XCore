using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.配置系统.读取出对象
{
    /// <summary>
    /// 使用配置的类.
    /// </summary>
    class UseConfig
    {
        private readonly IOptionsSnapshot<DBSetting> opt_db;

        private readonly IOptionsSnapshot<SmtpSetting> opt_smtp;

        public UseConfig(IOptionsSnapshot<DBSetting> opt_db, IOptionsSnapshot<SmtpSetting> opt_smtp)
        {
            this.opt_db = opt_db;
            this.opt_smtp = opt_smtp;
        }

        public void Test()
        {
            var db = opt_db.Value;  // 通过IOptionsSnapshop<T>的Value属性获取值
            Console.WriteLine($"数据库：{db.DbType}，{db.ConnectionString}");

            var smtp = opt_smtp.Value;
            Console.WriteLine($"数据库：{smtp.Server}，{smtp.UserName}，{smtp.Password}");
        }
    }
}
