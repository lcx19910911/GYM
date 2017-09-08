using GYM.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM.Web.Framework.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class TimerAttribute : ActionFilterAttribute
    {
        private long ticks = 0;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var time = Environment.TickCount - ticks;
            if (time > 2000)
            {
                LogHelper.WriteCustom(string.Format("本次请求耗时 {0} 秒.", (double)time / 1000), @"TimeOut\");
            }

            base.OnActionExecuted(filterContext);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ticks = Environment.TickCount;

            base.OnActionExecuting(filterContext);
        }
    }
}