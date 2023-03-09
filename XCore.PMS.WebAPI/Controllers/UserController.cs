using log4net.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger ,IConfiguration configuration, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            this._logger = logger;
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
        public async Task<ActionResult> GetRegisterToken(string userName, string emailName)
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
        [AllowAnonymous]
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
                // 验证通过，返回令牌
                var claims = new List<Claim>();
                ClaimsIdentity claimsAlpha = new ClaimsIdentity();
                claimsAlpha.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claimsAlpha.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                var roles = await userManager.GetRolesAsync(user);
                foreach (var item in roles)
                {
                    claimsAlpha.AddClaim(new Claim(ClaimTypes.Role, item));
                }

                var jwtOption = _configuration.GetSection("JWT").Get<JWTOptions>();
                string jwtToken = BuildToken(claimsAlpha, jwtOption);
                return Ok(new ReceiveObject<string>()
                {
                    code = 0,
                    msg = "成功",
                    data = jwtToken
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
        /// 创建Token.
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="jwtOption"></param>
        /// <returns></returns>
        private string BuildToken(ClaimsIdentity claims, JWTOptions jwtOption)
        {
            string key = jwtOption.SigningKey;
            DateTime expires = DateTime.Now.AddSeconds(jwtOption.ExpireSeconds);
            byte[] secBytes = Encoding.UTF8.GetBytes(key);
            var secKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims.Claims,
                expires: expires, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return jwt;
        }

        /// <summary>
        /// 获得当前的用户信息.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public OkObjectResult GetUserInfo()
        {
            var user = this.User;
            var id = user.FindFirst(m => m.Type == ClaimTypes.NameIdentifier);
            var name = user.FindFirst(m => m.Type == ClaimTypes.Name);
            var list_role = user.FindAll(m => m.Type == ClaimTypes.Role);
            var result = string.Format("编号：{0}，用户名：{1}，角色：{2}", id, name, string.Join(',', list_role));
            return Ok(new ReceiveObject<string>()
            {
                code = 0,
                data = result
            });
        }

        /// <summary>
        /// 获取重置密码的验证码.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetResetToken(string emailName)
        {
            var user = await userManager.FindByEmailAsync(emailName);
            if(user == null)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "该邮箱未注册"
                });
            }

            // 使用GeneratePasswordResetTokenAsync获取令牌的默认字符串很长且复杂，但由于在配置文件中设置options.Tokens.PasswordResetTokenProvider为TokenOptions.DefaultEmailProvide，获得的字符串为简短的6位数字
            string token = await userManager.GeneratePasswordResetTokenAsync(user);
            return Ok(new ReceiveObject<string>()
            {
                code = 0,
                msg = string.Format("往邮箱{0}，发送了令牌{1}", emailName, token)
            });
        }

        /// <summary>
        /// 重置密码.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ResetPassword(string emailName, string token, string newPassword)
        {
            var user = await userManager.FindByEmailAsync(emailName);
            if(user == null)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "该邮箱未注册"
                });
            }

            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            if(result.Succeeded == false)
            {
                return Ok(new ReceiveObject<string>()
                {
                    code = 999999,
                    msg = "密码重置失败"
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
            var secretByte = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").Get<JWTOptions>().SigningKey);
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
