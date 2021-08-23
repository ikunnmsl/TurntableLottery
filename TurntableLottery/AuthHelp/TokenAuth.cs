using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TurntableLottery.Token;
using TurntableLottery.Token.Model;

namespace TurntableLottery.AuthHelp
{
    public class TokenAuth
    {
        /// <summary>
        /// http委托
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public TokenAuth(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;
            //检测是否包含'Authorization'请求头，如果不包含返回context进行下一个中间件，用于访问不需要认证的API
            if (!headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }
            var tokenStr = headers["Authorization"];
            try
            {
                string jwtStr = tokenStr.ToString().Substring("Bearer ".Length).Trim();
                if (!TurntableLotteryMemoryCache.Exists(jwtStr))
                {
                    return httpContext.Response.WriteAsync("非法请求");
                }
                TokenModel tm = (TokenModel)TurntableLotteryMemoryCache.Get(jwtStr);
                List<Claim> lc = new List<Claim>();
                Claim c = new Claim(tm.Sub + "Type", tm.Sub);
                lc.Add(c);
                ClaimsIdentity identity = new ClaimsIdentity(lc);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                httpContext.User = claimsPrincipal;

                return _next(httpContext);
            }
            catch (Exception)
            {
                return httpContext.Response.WriteAsync("token验证异常");
            }
        }
    }
}
