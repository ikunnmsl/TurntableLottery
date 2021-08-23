using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurntableLottery.Bussiness;

namespace TurntableLottery.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : Controller
    {
        private readonly EntityBLL _entityBLL;
        private readonly IWebHostEnvironment _webhostEnvironment;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entityBLL"></param>
        /// <param name="webhostEnvironment"></param>
        public EntityController(EntityBLL entityBLL, IWebHostEnvironment webhostEnvironment)
        {
            _entityBLL = entityBLL;
            _webhostEnvironment = webhostEnvironment;
        }

        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateEntity(string entityName = null)
        {
            if (entityName == null)
                return Json("参数为空");
            return Json(_entityBLL.CreateEntity(entityName, _webhostEnvironment.ContentRootPath));
        }

    }
}
