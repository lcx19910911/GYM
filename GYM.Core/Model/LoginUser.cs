
using GYM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Core.Model
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public class LoginUser
    {
        public LoginUser(User user)
        {
            this.ID = user.ID;
            this.Account = user.NickName;
            this.IsAdmin = false;
            this.HeadImgUrl = user.HeadImgUrl;
        }

        public LoginUser(Admin admin)
        {

            this.ID = admin.ID;
            this.Account = admin.Account;
            this.IsAdmin = true;
        }

        public LoginUser()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string HeadImgUrl { get; set; }


        public string Token { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
