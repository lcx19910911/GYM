using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM.Web.Framework.Filters
{
    /// <summary>
    /// 过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AdminLoginFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseAdminController;

            var areaName = filterContext.RouteData.DataTokens["area"];
            var actionName = filterContext.RouteData.Values["Action"].ToString();
            var controllerName = filterContext.RouteData.Values["Controller"].ToString();
            string requestUrl = filterContext.HttpContext.Request.Url.ToString();

            if (areaName == null)
            {
                RedirectResult redirectResult = new RedirectResult("/login/index?redirecturl=" + requestUrl);
                filterContext.Result = redirectResult;
            }
            if (controller.LoginAdmin == null)
            {
                if (!controllerName.Equals("login", StringComparison.OrdinalIgnoreCase))
                {
                    RedirectResult redirectResult = new RedirectResult("/admin/login/index?redirecturl=" + requestUrl);
                    filterContext.Result = redirectResult;
                }
            }
        }
    }
}