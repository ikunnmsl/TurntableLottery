using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurntableLottery.Entity;
using TurntableLottery.IService.Admin;
using TurntableLottery.Model;

namespace TurntableLottery.Service.Admin
{
    public class AccountService : BaseDB, IAccountService
    {
        //public SimpleClient<Account> _sdb;
        //public AccountService(SimpleClient<Account> sdb)
        //{
        //    _sdb = sdb;
        //}

        public SimpleClient<Account> _sdb = new SimpleClient<Account>(BaseDB.GetClient());
        public bool Add(Account entity)
        {
            return _sdb.Insert(entity);
        }

        public bool Dels(dynamic[] ids)
        {
            return _sdb.DeleteByIds(ids);
        }

        public Account Get(Guid id)
        {
            return _sdb.GetById(id);
        }

        public Task<Account> GetAccout(string AccountCode, string PassWord)
        {
            return _sdb.GetSingleAsync(a => a.AccountCode == AccountCode && a.PassWord == PassWord);
        }

        public TableModel<Account> GetPageList(int pageIndex, int pageSize)
        {
            PageModel p = new PageModel() { PageIndex = pageIndex, PageSize = pageSize };
            Expression<Func<Account, bool>> ex = (it => 1 == 1);
            List<Account> data = _sdb.GetPageList(ex, p);
            TableModel<Account> t = new TableModel<Account>();
            t.Code = 0;
            t.Count = p.TotalCount;
            t.Data = data;
            t.Msg = "成功";
            return t;
        }

        public bool Update(Account entity)
        {
            return _sdb.Update(entity);
        }
    }
}
