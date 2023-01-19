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

namespace XCore.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [AllowAnonymous] //允许匿名访问
        [HttpGet]
        public ActionResult<string> Login(string username, string password)
        {
            //1、验证用户名和密码
            //2、创建jwt（header、payload、signiture）
            //2.1、创建header
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;//存储使用算法

            //2.2、创建payload
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "BigBox777"), //自定义数据部分,这里使用JWT标准定义的Sub表示该 jwt 所面向的用户
                new Claim(ClaimTypes.Role,"Admin"), //加入角色认证信息
                new Claim(ClaimTypes.Role,"NormalUser") //加入角色认证信息
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
                expires: DateTime.UtcNow.AddMinutes(10),  // 有效期10分钟
                signingCredentials  // 数字签名
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);  // 转成字符串token

            //3、返回200 OK，回传jwt
            return Ok(tokenString);
        }
    }
}
