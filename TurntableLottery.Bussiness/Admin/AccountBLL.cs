using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TurntableLottery.Entity;
using TurntableLottery.IService.Admin;
using TurntableLottery.Model;

namespace TurntableLottery.Bussiness.Admin
{
    public class AccountBLL
    {
        private readonly IAccountService _accountService;

        public AccountBLL(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Account GetById(Guid id)
        {
            return _accountService.Get(id);
        }
        public async Task<Account> GetAccount(string AccountCode, string PassWord)
        {
            return await _accountService.GetAccout(AccountCode, PassWord);
        }
        public TableModel<Account> GetPageList(int pageIndex, int pageSize)
        {
            return _accountService.GetPageList(pageIndex, pageSize);
        }

        public MessageModel<Account> Add(Account entity)
        {
            if (_accountService.Add(entity))
                return new MessageModel<Account> { Success = true, Msg = "操作成功" };
            else
                return new MessageModel<Account> { Success = false, Msg = "操作失败" };
        }

        public MessageModel<Account> Update(Account entity)
        {
            if (_accountService.Update(entity))
                return new MessageModel<Account> { Success = true, Msg = "操作成功" };
            else
                return new MessageModel<Account> { Success = false, Msg = "操作失败" };
        }

        public MessageModel<Account> Dels(dynamic[] ids)
        {
            if (_accountService.Dels(ids))
                return new MessageModel<Account> { Success = true, Msg = "操作成功" };
            else
                return new MessageModel<Account> { Success = false, Msg = "操作失败" };
        }
    }
}
