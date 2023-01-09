using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.配置系统.EF基础
{
    class Book
    {
        public long ID { get; set; }

        public string Title { get; set; }

        public DateTime PubTime { get; set; }

        public double Price { get; set; }

        public string AuthorName { get; set; }
    }
}
