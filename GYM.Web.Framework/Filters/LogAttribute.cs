
using GYM.Core.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace GYM.Web.Framework.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LogAttribute : ActionFilterAttribute
    {
        private LogCode logCode;

        public LogAttribute(LogCode logCode)
        {
            this.logCode = logCode;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var controller = filterContext.Controller as BaseController;
            //controller.Client.LogCode = logCode;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}