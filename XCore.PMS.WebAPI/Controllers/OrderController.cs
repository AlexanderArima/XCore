using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using XCore.PMS.WebAPI.Model;
using XCore.PMS.WebAPI.Model_ORM;
using XCore.PMS.WebAPI.VO.Order;

namespace XCore.PMS.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly db_hotelContext _context;
        private readonly IMemoryCache _cache;

        public OrderController(db_hotelContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        /// <summary>
        /// 获取预定列表.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ReceiveList<AppointVO>>> GetAppointList(int index, int size)
        {
            try
            {
                // 获取或创建一个名为GetAppointList的缓存键值对
                var item = await this._cache.GetOrCreateAsync("GetAppointList",
                    async (e) => 
                    {
                        // 设置缓存过期时间的两种策略
                        e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);    // 固定缓存（一到过期时间，缓存就过期）
                        e.SlidingExpiration = TimeSpan.FromSeconds(10);    // 滑动缓存（在缓存期间如果命中，会续期）

                        // 查询数据库
                        var list = await _context.TOrders.Where(m => m.Status == "1").ToListAsync();
                        var count = list.Count;
                        list = list.Skip((index) * size).Take(size).ToList();
                        List<AppointVO> list_appoint = new List<AppointVO>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            var item = list.ElementAt(i);
                            AppointVO model = new AppointVO();
                            model.Xm = item.Xm;
                            model.Zjhm = item.Zjhm;
                            model.Zjlx = item.Zjlx;
                            model.Sex = Convert.ToInt32(item.Sex);
                            model.Roomid = item.Roomid;
                            model.Id = item.Id;
                            model.Appointtime = item.Appointtime;
                            model.Ywx = item.Ywx;
                            model.Ywm = item.Ywm;
                            model.Type = item.Type;
                            model.Gj = item.Gj;
                            list_appoint.Add(model);
                        }

                        ReceiveList<AppointVO> result = new ReceiveList<AppointVO>();
                        result.code = 0;
                        result.data = new ReceiveList<AppointVO>.Data()
                        {
                            index = index,
                            count = count,
                            list = list_appoint,
                        };

                        return result;
                    });

                return item;
            }
            catch(Exception ex)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        /// <summary>
        /// 获取入住列表.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ReceiveList<TOrder>>> GetCheckinList(int index, int size)
        {
            try
            {
                var list = await _context.TOrders.Where(m => m.Status == "2").ToListAsync();
                var count = list.Count;
                list = list.Skip((index - 1) * size).Take(size).ToList();
                ReceiveList<TOrder> result = new ReceiveList<TOrder>();
                result.code = 0;
                result.data = new ReceiveList<TOrder>.Data()
                {
                    index = index,
                    count = count,
                    list = list,
                };

                return result;
            }
            catch (Exception ex)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        /// <summary>
        /// 获取订单详情.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>缓存60s的数据</returns>
        [HttpGet]
        [ResponseCache(Duration = 60)]
        [AllowAnonymous]
        public async Task<ActionResult<ReceiveObject<TOrder>>> GetOrder(int id)
        {
            try
            {
                var tOrder = await _context.TOrders.FindAsync(id);
                if (tOrder == null)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "未查询到这条订单"
                    });
                }

                return Ok(new ReceiveObject<TOrder>()
                {
                    code = 0,
                    data = tOrder,
                });
            }
            catch(Exception ex)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        /// <summary>
        /// 修改订单，退房，续住操作可以参考它
        /// </summary>
        /// <param name="tOrder">订单对象.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(TOrder tOrder)
        {
            try
            {
                var obj = _context.TOrders.Where(m => m.Id == tOrder.Id);
                if (obj == null)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "系统异常"
                    });
                }

                _context.Update<TOrder>(tOrder);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "修改成功"
                });
            }
            catch (Exception ex)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        /// <summary>
        /// 旅客预定
        /// </summary>
        /// <param name="tOrder"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TOrder>> Appoint(TOrder tOrder)
        {
            try
            {
                tOrder.Appointtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tOrder.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tOrder.Status = "1";
                _context.TOrders.Add(tOrder);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "添加成功"
                });
            }
            catch (Exception ex)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        /// <summary>
        /// 旅客入住.
        /// </summary>
        /// <param name="tOrder"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TOrder>> Checkin(TOrder tOrder)
        {
            try
            {
                tOrder.Checkintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tOrder.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tOrder.Status = "2";
                _context.TOrders.Add(tOrder);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "添加成功"
                });
            }
            catch (Exception ex)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        /// <summary>
        /// 退房.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<TOrder>> Checkout(int id)
        {
            try
            {
                var obj = _context.TOrders.Where(m => m.Id == id).SingleOrDefault();
                if (obj == null)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "系统异常"
                    });
                }

                if(obj.Status != "2")
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "订单不是入住状态"
                    });
                }

                obj.Status = "3";
                obj.Checkouttime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _context.Update<TOrder>(obj);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "修改成功"
                });
            }
            catch (Exception ex)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        /// <summary>
        /// 换房.
        /// </summary>
        /// <param name="id">订单id</param>
        /// <param name="roomid">房间id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<TOrder>> ChangeRoom(int id, string roomid)
        {
            try
            {
                var obj = _context.TOrders.Where(m => m.Id == id).SingleOrDefault();
                if (obj == null)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "系统异常"
                    });
                }

                if (obj.Status != "2")
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "订单不是入住状态"
                    });
                }

                obj.Roomid = roomid;
                _context.Update<TOrder>(obj);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "修改成功"
                });
            }
            catch (Exception ex)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        /// <summary>
        /// 删除预订单.
        /// </summary>
        /// <param name="id">订单id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<TOrder>> DeleteAppoint(int id)
        {
            try
            {
                var obj = _context.TOrders.Where(m => m.Id == id).SingleOrDefault();
                if (obj == null)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "系统异常"
                    });
                }

                if (obj.Status != "1")
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "订单不是预订状态"
                    });
                }

                obj.Status = "4";
                _context.Update<TOrder>(obj);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "修改成功"
                });
            }
            catch (Exception ex)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        private bool TOrderExists(int id)
        {
            return _context.TOrders.Any(e => e.Id == id);
        }
    }
}
