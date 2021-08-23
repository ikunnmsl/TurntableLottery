using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using TurntableLottery.IService;
using TurntableLottery.Model;

namespace TurntableLottery.Service
{
    public class EntityService : BaseDB, IEntityService
    {
        public SqlSugarClient db = GetClient();
        public bool CreateEntity(string entityName, string filePath)
        {
            try 
            {
                db.DbFirst.IsCreateAttribute().Where(entityName).CreateClassFile(filePath, "TurntableLottery.Entity");
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
