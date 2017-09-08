
using GYM.Core;
using GYM.Core.Model;
using GYM.Web.Framework.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GYM.Web.Framework
{
    [AdminLoginFilter]
    public class BaseAdminController : BaseController
    {
        private LoginUser _loginAdmin = null;

        public LoginUser LoginAdmin
        {
            get
            {

                return _loginAdmin != null ? _loginAdmin : LoginHelper.GetCurrentAdmin();
            }
            set
            {
                LoginHelper.CreateUser(value, Params.AdminCookieName);
            }
        }
    }
}