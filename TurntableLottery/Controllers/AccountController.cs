using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurntableLottery.Entity;
using TurntableLottery.IService.Admin;

namespace TurntableLottery.Controllers
{
    /// <summary>
    /// 用户模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="account"></param>
        public AccountController(IAccountService account)
        {
            _account = account;
        }
        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetStudentPageList(int pageIndex = 1, int pageSize = 10)
        {
            return new JsonResult(_account.GetPageList(pageIndex, pageSize));
        }
        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetAccountById(Guid id)
        {
            return new JsonResult(_account.Get(id));
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(Account entity = null)
        {
            if (entity == null)
                return new JsonResult("参数为空");
            return new JsonResult(_account.Add(entity));
        }
        /// <summary>
        /// 编辑学生
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonResult Update(Account entity = null)
        {
            if (entity == null)
                return new JsonResult("参数为空");
            return new JsonResult(_account.Update(entity));
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonResult Dels(dynamic[] ids = null)
        {
            if (ids.Length == 0)
                return new JsonResult("参数为空");
            return new JsonResult(_account.Dels(ids));
        }
    }
}

