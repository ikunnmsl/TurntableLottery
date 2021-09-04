using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurntableLottery.Entity;
using TurntableLottery.IService.Admin;

namespace TurntableLottery.WebApi.Controllers
{
    [Route("api/[controller]/action")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;
        public AccountController(IAccountService account)
        {
            _account = account;
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetStudentById(Guid id)
        {
            return new JsonResult(_account.Get(id));
        }
       
    }
}
