using System;
using System.Collections.Generic;
using System.Text;

namespace TurntableLottery.Entity
{
    public class Account
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 是否作废
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime RegistrationDate { get; set; }
    }
}
