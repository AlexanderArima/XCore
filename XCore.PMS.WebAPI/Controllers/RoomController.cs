﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using XCore.PMS.WebAPI.Model;
using XCore.PMS.WebAPI.Model_ORM;
using XCore.PMS.WebAPI.VO.Room;

namespace XCore.PMS.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly db_hotelContext _context;

        private readonly IDistributedCache _distributedCache;

        public RoomController(db_hotelContext context, IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult> GetList()
        {
            GetListVO model = new GetListVO();
            try
            {
                var list = await _context.TRooms.ToListAsync();
                model.code = 0;
                model.data = list;
            }
            catch(Exception ex)
            {
                model.code = 999999;
                model.msg = "系统异常";
            }

            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult<GetRoomVO>> GetRoom(int id)
        {
            var tRoom = await _context.TRooms.FindAsync(id);
            if (tRoom == null)
            {
                return NotFound();
            }

            GetRoomVO model = new GetRoomVO();
            model.code = 0;
            model.data = new TRoom();
            model.data.Name = tRoom.Name;
            model.data.Id = tRoom.Id;
            model.data.Type = tRoom.Type;
            model.data.Status = tRoom.Status;
            model.data.Number = tRoom.Number;
            return model;
        }

        /// <summary>
        /// 获取房间总数.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetCount()
        {
            ReceiveObject<string> model = new ReceiveObject<string>();
            try
            {
                // 从缓存数据库中取值
                var count = this._distributedCache.GetString("GetCount");
                if(count == null)
                {
                    // 如果取不到值，则从数据库中查询，并写入到缓存中.
                    var a = await _context.TRooms.CountAsync();
                    count = a.ToString();
                    var opt = new DistributedCacheEntryOptions();
                    opt.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    this._distributedCache.SetString("GetCount", count, opt);
                }
             
                model.code = 0;
                model.data = count;
            }
            catch (Exception ex)
            {
                model.code = 999999;
                model.msg = "系统异常";
            }

            return Ok(model);
        }

        /// <summary>
        /// 修改房间.
        /// </summary>
        /// <param name="tRoom"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateRoom(UpdateRoomVO room)
        {
            try
            {
                var obj = _context.TRooms.Where(m => m.Id == room.Id);
                if(obj == null)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "系统异常"
                    });
                }

                var tRoom = await _context.TRooms.FindAsync(room.Id);
                if (tRoom == null)
                {
                    return NotFound();
                }

                tRoom.Id = room.Id;
                tRoom.Name = room.Name;
                tRoom.Type = room.Type;
                tRoom.Status = room.Status;
                _context.Update<TRoom>(tRoom);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "修改成功"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TRoom>> AddRoom(AddRoomVO model)
        {
            try
            {
                var list = await _context.TRooms.ToListAsync();
                var flag = list.Exists(m => m.Name == model.Name);
                if(flag)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "该房间名已存在"
                    });
                }
                TRoom tRoom = new TRoom()
                {
                    Name = model.Name,
                    Type = model.Type,
                    Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Deleteflag = 0,
                    Status = "0",
                    Number = 0
                };

                _context.TRooms.Add(tRoom);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "添加成功"
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

        [HttpGet]
        public async Task<ActionResult<TRoom>> DeleteRoom(int id)
        {
            try
            {
                var tRoom = await _context.TRooms.FindAsync(id);
                if (tRoom == null)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 0,
                        msg = "房间号不存在"
                    });
                }

                _context.TRooms.Remove(tRoom);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "删除成功"
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

        private bool RoomExists(int id)
        {
            return _context.TRooms.Any(e => e.Id == id);
        }
    }
}
