using System;
using System.Collections.Generic;
using System.Text;
using TurntableLottery.IService;
using TurntableLottery.Model;

namespace TurntableLottery.Bussiness
{
    public class EntityBLL
    {
        private readonly IEntityService _entity;
        public EntityBLL(IEntityService entity)
        {
            _entity = entity;
        }

        public MessageModel<string> CreateEntity(string entityName, string contentRootPath)
        {
            string[] arr = contentRootPath.Split('\\');
            string baseFileProvider = "";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                baseFileProvider += arr[i];
                baseFileProvider += "\\";
            }
            string filePath = baseFileProvider + "TurntableLottery.Entity";
            if (_entity.CreateEntity(entityName, filePath))
            {
                return new MessageModel<string> { Success = true, Msg = "生成成功" };
            }
            else
            {
                return new MessageModel<string> { Success = false, Msg = "生成失败" };
            }
        }
    }
}
