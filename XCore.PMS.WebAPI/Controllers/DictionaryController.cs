using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using XCore.PMS.WebAPI.Model;
using XCore.PMS.WebAPI.Model_ORM;
using XCore.PMS.WebAPI.VO.Dictionary;

namespace XCore.PMS.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DictionaryController : ControllerBase
    {
        private readonly db_hotelContext _context;

        public DictionaryController(db_hotelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 根据字典类型返回对应的数据字典.
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GetListVO>> GetList(string typeid)
        {
            GetListVO model = new GetListVO();
            try
            {
                var list = await _context.TDictionaries.Where(m => m.Typeid == typeid).ToListAsync();
                model.data = list;
                model.code = 0;
                model.msg = null;
                return model;
            }
            catch(Exception ex)
            {
                model.code = 999999;
                model.msg = "系统异常";
                return model;
            }
        }

    }
}
