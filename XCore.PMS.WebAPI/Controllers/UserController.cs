using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.WebAPI.Model;
using XCore.PMS.WebAPI.Model_ORM;

namespace XCore.PMS.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// 登录.
        /// </summary>
        /// <param name="username">用户名.</param>
        /// <param name="password">密码.</param>
        /// <returns></returns>
        [AllowAnonymous] //允许匿名访问
        [HttpGet]
        public IActionResult Login(string username, string password)
        {
            //1、验证用户名和密码
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "用户名或密码不能为空"
                });
            }

            using (db_hotelContext context = new db_hotelContext())
            {
                var flag = context.TUsers.Any(m => m.Username == username);
                if (flag == false)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "用户名不存在"
                    });
                }

                flag = context.TUsers.Any(m => m.Username == username && m.Password == password);
                if (flag == false)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "密码错误"
                    });
                }
            }

            //2、创建jwt（header、payload、signiture）
            //2.1、创建header
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;//存储使用算法

            //2.2、创建payload
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "BigBox777"), //自定义数据部分,这里使用JWT标准定义的Sub表示该 jwt 所面向的用户
            };

            //2.3、创建signiture
            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:secretKey"]);
            var signingKey = new SymmetricSecurityKey(secretByte);//对密码进行加密
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm); //验证加密后的私钥

            //2.4、创建jwt
            var token = new JwtSecurityToken(
                issuer: "donesoft.cn",  // 谁发布的
                audience: "donesoft.cn",  // 发布给谁用
                claims,  // payload数据
                notBefore: DateTime.UtcNow,  // 发布时间
                expires: DateTime.UtcNow.AddDays(1),  // 有效1天
                signingCredentials  // 数字签名
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);  // 转成字符串token

            //3、返回200 OK，回传jwt
            return Ok(new ReceiveObject<string>()
            {
                code = 0,
                data = tokenString
            });
        }
    }
}
