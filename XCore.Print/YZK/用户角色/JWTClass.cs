using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XCore.Print.Common;

namespace XCore.Print.YZK.用户角色
{
    public class JWTClass
    {
        /// <summary>
        /// 生成JWT字符串.
        /// </summary>
        public static void Fun01()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "6"));
            claims.Add(new Claim(ClaimTypes.Name, "yzk"));
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim("PassPort", "E90000082"));
            string key = "fasdfad&9045dafz222#fadpio@0232";
            DateTime expires = DateTime.Now.AddDays(1);
            byte[] secBytes = Encoding.UTF8.GetBytes(key);
            var secKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims,
                expires: expires, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            // Console.WriteLine(jwt);
            // Fun02(jwt);
            Fun03(jwt);
        }

        /// <summary>
        /// 将Base64格式的JWT字符串分割为3份.
        /// </summary>
        /// <param name="jwt"></param>
        public static void Fun02(string jwt)
        {
             string[] segments = jwt.Split('.');
             string head = Utils.Base64URLToBase64(segments[0]);
             string payload = Utils.Base64URLToBase64(segments[1]);
             string signature = segments[2];
            Console.WriteLine("--------head--------");
            Console.WriteLine(head);
            Console.WriteLine("--------payload--------");
            Console.WriteLine(payload);
            Console.WriteLine("--------signature--------");
            Console.WriteLine(signature);

        }

        /// <summary>
        /// 由于头部和负载信息是公开的，所以需要对签名进行验证，这样能防止信息被篡改.
        /// </summary>
        /// <param name="jwt"></param>
        public static void Fun03(string jwt)
        {
            string secKey = "fasdfad&9045dafz222#fadpio@0232";
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters valParam = new TokenValidationParameters();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey));
            valParam.IssuerSigningKey = securityKey;
            valParam.ValidateIssuer = false;
            valParam.ValidateAudience = false;

            // 使用ValidateToken方法验证签名是否有效
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt, valParam, out SecurityToken secToken);
             foreach (var claim in claimsPrincipal.Claims)
             {
                 Console.WriteLine($"{claim.Type}={claim.Value}");
             }
        }
    }
}
