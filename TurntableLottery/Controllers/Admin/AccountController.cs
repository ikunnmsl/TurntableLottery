﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurntableLottery.Bussiness.Admin;
using TurntableLottery.Entity;

namespace TurntableLottery.Controllers.Admin
{
    /// <summary>
    /// 用户模块
    /// </summary>
    [Route("api/Admin/[controller]")]
    [ApiController]
    [Authorize(Policy="Admin")]
    public class AccountController :  Controller
    {
        private readonly AccountBLL _accountBLL;
        public AccountController(AccountBLL accountBLL)
        {
            _accountBLL = accountBLL;
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
            return Json(_accountBLL.GetPageList(pageIndex, pageSize));
        }
        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetStudentById(Guid id)
        {
            return Json(_accountBLL.GetById(id));
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
                return Json("参数为空");
            return Json(_accountBLL.Add(entity));
        }
        /// <summary>
        /// 编辑学生
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Student")]
        public JsonResult Update(Account entity = null)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(_accountBLL.Update(entity));
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
                return Json("参数为空");
            return Json(_accountBLL.Dels(ids));
        }
    }
}

