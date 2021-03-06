using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TurntableLottery.IService.Admin;
using TurntableLottery.Model;
using TurntableLottery.Model.FromBody;
using TurntableLottery.Token;
using TurntableLottery.Token.Model;
using TurntableLottery.Utils.Helper;

namespace TurntableLottery.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService _account;
        private readonly PermissionRequirement _permissionRequirement;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permissionRequirement"></param>
        /// <param name="account"></param>

        public LoginController(IAccountService account, PermissionRequirement permissionRequirement)
        {
            _account = account;
            _permissionRequirement = permissionRequirement;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> LoginTest([FromBody] FMLogin model)
        {
            if (string.IsNullOrEmpty(model.userName) || string.IsNullOrEmpty(model.password))
            {
                return new JsonResult("用户名或密码不能为空");
            }
            model.password = MD5Helper.MD5Encrypt32(model.password);

            var account = await _account.GetAccount(model.userName, model.password);
            if (account != null)
            {
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                        new Claim(ClaimTypes.GivenName, account.NickName),
                        new Claim(ClaimTypes.Name, account.AccountCode),
                        new Claim(JwtRegisteredClaimNames.Jti, account.Id.ToString()),
                        new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_permissionRequirement.Expiration.TotalSeconds).ToString()) };
                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                var token = TurnTableLotteryToken.IssueJWT(claims.ToArray(), _permissionRequirement);

                return new JsonResult(token);
            }
            else
            {
                return new JsonResult("账户密码错误");
            }
            
        }
    }
}
