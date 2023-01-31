using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCore.PMS.WebAPI.Model;
using XCore.PMS.WebAPI.Model_ORM;

namespace XCore.PMS.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly db_hotelContext _context;

        public RoomController(db_hotelContext context)
        {
            _context = context;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TRoom>>> GetList()
        {
            return await _context.TRooms.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<TRoom>> GetRoom(int id)
        {
            var tRoom = await _context.TRooms.FindAsync(id);

            if (tRoom == null)
            {
                return NotFound();
            }

            return tRoom;
        }

        /// <summary>
        /// 修改房间.
        /// </summary>
        /// <param name="tRoom"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateRoom(TRoom tRoom)
        {
            try
            {
                var obj = _context.TRooms.Where(m => m.Id == tRoom.Id);
                if(obj == null)
                {
                    return Ok(new ReceiveObject()
                    {
                        code = 999999,
                        msg = "系统异常"
                    });
                }

                _context.Update<TRoom>(tRoom);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject()
                {
                    code = 0,
                    msg = "修改成功"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ReceiveObject()
                {
                    code = 999999,
                    msg = "系统异常"
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TRoom>> AddRoom(TRoom tRoom)
        {
            try
            {
                _context.TRooms.Add(tRoom);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject()
                {
                    code = 0,
                    msg = "添加成功"
                });
            }
            catch(Exception ex)
            {
                return Ok(new ReceiveObject()
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
                    return Ok(new ReceiveObject()
                    {
                        code = 0,
                        msg = "房间号不存在"
                    });
                }

                _context.TRooms.Remove(tRoom);
                await _context.SaveChangesAsync();
                return Ok(new ReceiveObject()
                {
                    code = 0,
                    msg = "删除成功"
                });
            }
            catch (Exception ex)
            {
                return Ok(new ReceiveObject()
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
