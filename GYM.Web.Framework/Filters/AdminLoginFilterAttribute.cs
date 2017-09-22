using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM.IService;
using GYM.Core.Model;
using GYM.Core.Code;
using GYM.Service;

namespace GYM.Web.Framework.Filters
{
    /// <summary>
    /// 过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AdminLoginFilterAttribute : ActionFilterAttribute
    {
        public AdminLoginFilterAttribute()
        { }
        IMenuService IMenuService=new MenuService();
        IOperateService IOperateService = new OperateService();

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

            if (controller.LoginAdmin != null)
            {
                var url = filterContext.HttpContext.Request.RawUrl;
                if (!controller.LoginAdmin.IsSuperAdmin)
                {
                    if (filterContext.HttpContext.Request.HttpMethod == "Get")
                    {
                        if (!IMenuService.IsHavePage(url))
                        {
                            filterContext.Result = new RedirectResult("/Admin/Home/Index");
                        }
                    }
                    else
                    {
                        if (!IOperateService.IsHaveAuthority(url))
                        {
                            var result = new WebResult<bool> { Code = ErrorCode.sys_user_role_error, Result = false };
                            JsonResult jsonResult = new JsonResult();
                            jsonResult.Data = result;
                            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                            filterContext.Result = jsonResult;
                        }
                    }
                }
            }
            else
            {
                if (!controllerName.Equals("Login", StringComparison.CurrentCultureIgnoreCase))
                {
                    RedirectResult redirectResult = new RedirectResult("/Admin/login/index?redirecturl=" + requestUrl);
                    filterContext.Result = redirectResult;
                }
            }
        }
    }
}