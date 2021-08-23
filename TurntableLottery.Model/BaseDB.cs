using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurntableLottery.Configuration;

namespace TurntableLottery.Model
{
    public class BaseDB
    {
        public static SqlSugarClient GetClient()
        {
            SqlSugarClient db = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = AppSettingsConstVars.DbSqlConnection,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true
                }
            );
            db.Aop.OnLogExecuting = (sql, pars) => {
                Console.WriteLine(sql+"\r\n"+db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName,it=>it.Value)));
                Console.WriteLine();
            };
           return db;
        }
    }
}
