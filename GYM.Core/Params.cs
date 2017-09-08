using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Security;
using GYM.Core.Helper;

namespace GYM.Core
{
    public class Params
    {
        /// <summary>
        /// 时间戳有效时间c
        /// </summary>
        public const int TimspanExpiredMinutes = 60;
        /// <summary>
        /// token失效时间
        /// </summary>
        public const int ExpiredDays = 7;

        public static readonly string SecretKey =CustomHelper.GetValue("SecretKey");
        public static readonly string DomianName = CustomHelper.GetValue("Company_SiteUrl");
        /// <summary>
        /// 登陆cookie
        /// </summary>
        public static readonly string UserCookieName = "website_user";
        /// <summary>
        /// 登陆cookie
        /// </summary>
        public static readonly string UserTokenCookieName = "token";
        /// 登陆cookie
        /// </summary>
        public static readonly string AdminCookieName = "website_admin";


        /// 跟平台通信密钥
        /// </summary>
        public static readonly string WeixinAppSecret = CustomHelper.GetValue("Weixin_AppSecret");

        /// <summary>
        /// 平台地址
        /// </summary>
        public static readonly string WeixinAppId = CustomHelper.GetValue("Weixin_AppId");
    }
}
