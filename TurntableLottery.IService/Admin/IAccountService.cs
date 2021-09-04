using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TurntableLottery.Entity;
using TurntableLottery.Model;

namespace TurntableLottery.IService.Admin
{
    public interface IAccountService
    {
        #region base
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        TableModel<Account> GetPageList(int pageIndex, int pageSize);
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Account Get(Guid id);
        /// <summary>
        /// 根据账号密码获取
        /// </summary>
        /// <param name="AccountCode"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        Task<Account> GetAccount(string AccountCode, string PassWord);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(Account entity);
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(Account entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Dels(dynamic[] ids);
        #endregion
    }
}
