using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UserController(IConfiguration configuration, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            this._configuration = configuration;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        /// <summary>
        /// 获取注册用户的验证码.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetToken(string userName, string emailName)
        {
            var flag = await roleManager.RoleExistsAsync("admin");
            if (flag == false)
            {
                // 如果admin角色不存在，则创建它
                Role role = new Role()
                {
                    Name = "admin"
                };

                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded == false)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "创建角色失败"
                    });
                }
            }

            User user = await this.userManager.FindByNameAsync(userName);
            if (user == null)
            {
                // 如果该用户不存在，则创建它
                user = new User()
                {
                    UserName = userName,
                    Email = emailName,
                    EmailConfirmed = false,
                    CreateTime = DateTime.Now
                };

                var result = this.userManager.CreateAsync(user);
                if (result.Result.Succeeded == false)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "创建用户失败"
                    });
                }
            }

            // 创建一个令牌用于确认邮件的有效性
            var result_confirm = this.userManager.GenerateEmailConfirmationTokenAsync(user);
            return Ok(new ReceiveObject<string>()
            {
                code = 999999,
                data = string.Format("已向邮箱{0}发送验证码，验证码为{1}", emailName, result_confirm.Result)
            });
        }

        /// <summary>
        /// 创建用户.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> CreateUser(string userName, string password, string emailName, string token)
        {
            var flag = await roleManager.RoleExistsAsync("admin");
            if(flag == false)
            {
                // 如果admin角色不存在，则创建它
                Role role = new Role()
                {
                    Name = "admin"
                };

                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded == false)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "创建角色失败"
                    });
                }
            }

            User user = await this.userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "请先获取邮箱验证码"
                });
            }
            else
            {
                // 该用户已创建
                // 验证邮件令牌的有效性
                var result = this.userManager.ConfirmEmailAsync(user, token);
                if (result.Result.Succeeded == false)
                {
                    return Ok(new ReceiveObject<string>()
                    {
                        code = 999999,
                        msg = "邮箱验证码无效"
                    });
                }

                user.EmailConfirmed = true;
                result = this.userManager.UpdateAsync(user);
                result = this.userManager.AddPasswordAsync(user, password);
            }

            // 给该用户赋予admin角色权限
            var result_list = this.userManager.GetRolesAsync(user);
            if(result_list.Result.Count(m => m.Equals("admin")) > 0)
            {
                // 已经给该用户赋予角色权限
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "成功"
                });
            }

            var result_role = this.userManager.AddToRoleAsync(user, "admin");
            if (result_role.Result.Succeeded == false)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "给用户赋予角色失败"
                });
            }
            else
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "成功"
                });
            }
        }

        /// <summary>
        /// 登录（使用Identity框架）.
        /// </summary>
        /// <param name="username">用户名.</param>
        /// <param name="password">密码.</param>
        /// <returns></returns>
        [AllowAnonymous] //允许匿名访问
        [HttpGet]
        public async Task<ActionResult> LoginAlpha(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = $"用户名不存在{username}"
                });
            }

            if (await userManager.IsLockedOutAsync(user))
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "用户名已锁定"
                });
            }

            var success = await userManager.CheckPasswordAsync(user, password);
            if (success)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "成功"
                });
            }
            else
            {
                await userManager.AccessFailedAsync(user);
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "用户名或密码错误"
                });
            }
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
