using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCore.EF.DataBaseFirst.Models;

namespace XCore.EF.DataBaseFirst
{
    public class TestHelper
    {
        /// <summary>
        /// 添加一条数据.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public async static void Insert(string name)
        {
            using (var context = new db_hotelContext())
            {
                var test = new TTest()
                {
                    Name = name
                };

                context.TTests.Add(test);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 删除一条数据.
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (var context = new db_hotelContext())
            {
                var test = new TTest()
                {
                    Id = id,
                };

                context.TTests.Remove(test);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 修改数据.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public static void Update(int id ,string name)
        {
            using (var context = new db_hotelContext())
            {
                var test = new TTest()
                {
                    Id = id,
                    Name = name,
                };

                context.TTests.Update(test);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 查询所有数据.
        /// </summary>
        public static void QueryAll()
        {
            using (var context = new db_hotelContext())
            {
                foreach (var item in context.TTests)
                {
                    Console.WriteLine($"Id：{item.Id}，Name：{item.Name}");
                }
            }
        }

        /// <summary>
        /// 查询单条数据.
        /// </summary>
        public static void QueryByID(int id)
        {
            using (var context = new db_hotelContext())
            {
                var result = context.TTests.Where(m => m.Id == id);
                foreach (var item in result)
                {
                    Console.WriteLine($"使用IQueryable查询，Id：{item.Id}，Name：{item.Name}");
                }
            }

            using (var context = new db_hotelContext())
            {
                IEnumerable<TTest> result = context.TTests;
                foreach (var item in result.Where(m => m.Id == id))
                {
                    Console.WriteLine($"使用IEnumerable查询，Id：{item.Id}，Name：{item.Name}");
                }
            }
        }

        /// <summary>
        /// 分组查询总数.
        /// </summary>
        public static void QueryCount()
        {
            using (var context = new db_hotelContext())
            {
                var result = context.TTests.GroupBy(m => m.Name).Select(g => new
                {
                    Name = g.Key,
                    Count = g.Count(),
                });
                foreach (var item in result)
                {
                    Console.WriteLine($"Name：{item.Name}，数量：{item.Count}");
                }
            }
        }

        /// <summary>
        /// 分组查询总数.
        /// </summary>
        public async static void QueryLong()
        {
            using (var context = new db_hotelContext())
            {
                var result = await context.TTests.LongCountAsync();
                Console.WriteLine("LongCountAsync返回的是Long类型的Count数：" + result);
            }
        }
    }
}
