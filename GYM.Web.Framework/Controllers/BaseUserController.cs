
using GYM.Core;
using GYM.Core.Code;
using GYM.Core.Extensions;
using GYM.Core.Model;
using GYM.Web.Framework.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM.Web.Framework
{
    [UserLoginFilter]
    public class BaseUserController : BaseController
    {


        private LoginUser _loginUser = null;

        public LoginUser LoginUser
        {
            get
            {

                return _loginUser != null ? _loginUser : LoginHelper.GetCurrentUser();
            }
            set {
                LoginHelper.CreateUser(value, Params.UserCookieName);
            }
        }
    }
}