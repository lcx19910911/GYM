using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM.Web.Framework.Filters
{
    /// <summary>
    /// 用户过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UserLoginFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseUserController;

            var areaName = filterContext.RouteData.DataTokens["area"];
            var actionName = filterContext.RouteData.Values["Action"].ToString();
            var controllerName = filterContext.RouteData.Values["Controller"].ToString();
            var actionMethodList = filterContext.Controller.GetType().GetMethods();
            string requestUrl = filterContext.HttpContext.Request.Url.ToString();

            if (areaName != null)
            {
                    RedirectResult redirectResult = new RedirectResult("/admin/login/index?redirecturl=" + requestUrl);
                    filterContext.Result = redirectResult;
            }

            if (controller.LoginUser == null)
            {
                if (!controllerName.Equals("login", StringComparison.OrdinalIgnoreCase))
                {
                    var actionMethod = actionMethodList.Where(x => x.Name.Equals(actionName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (actionMethod != null)
                    {
                        if (actionMethod.ReturnType.Name == "ViewResult" || actionMethod.ReturnType.Name == "ActionResult")
                        {
                            RedirectResult redirectResult = new RedirectResult("/login/index?redirecturl=" + requestUrl);
                            filterContext.Result = redirectResult;
                        }
                        else if (actionMethod.ReturnType.Name == "JsonResult")
                        {
                            JsonResult jsonResult = new JsonResult();
                            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                            filterContext.RequestContext.HttpContext.Response.StatusCode = 9999;
                            filterContext.Result = jsonResult;
                        }
                    }
                }
            }
        }
    }
}