
using GYM.Core;
using GYM.Core.Code;
using GYM.Core.Extensions;
using GYM.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM.Web.Framework
{
    public class BaseController : Controller
    {

        public BaseController()
        {
        }

        //
        // GET: /Web/
        public ActionResult JError()
        {
            return JResult(false);
        }

        public ActionResult _404()
        {
            return View();
        }

        public ActionResult OAuthExpired()
        {
            return View("OAuthExpired");
        }

        

        public ActionResult _505()
        {
            return View();
        }

        /// <summary>
        /// 返回部分视图的错误页
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PartialError()
        {
            return null;
        }

        #region Json返回

        /// <summary>
        /// 返回异常编号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected internal JsonResult JResult(ErrorCode code)
        {
            return Json(new
            {
                Code = code,
                ErrorDesc = code.GetDescription()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回异常编号附带自定义消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="appendMsg"></param>
        /// <returns></returns>
        protected internal JsonResult JResult(ErrorCode code, string appendMsg)
        {
            return Json(new
            {
                Code = code,
                ErrorDesc = code.GetDescription(),
                Append = appendMsg
            }, JsonRequestBehavior.AllowGet);
        }


        protected internal JsonResult JResult<T>(T model)
        {
            return Json(new
            {
                Code = ErrorCode.sys_success,
                Result = model
            }, JsonRequestBehavior.AllowGet);
        }

        protected internal JsonResult JResult<T>(WebResult<T> model)
        {
            if (model.OccurError)
            {
                return JResult(model.Code, model.Append);
            }
            return Json(new
            {
                Code = ErrorCode.sys_success,
                Result = model,
            }, JsonRequestBehavior.AllowGet);
        }



        #endregion

        protected internal ViewResult View<T>(WebResult<T> model)
        {
            if (model.OccurError)
            {
                return View("Error");
            }
            return View(model.Result);
        }

        protected internal ActionResult ReLogin()
        {
            return RedirectToAction("Login", "Home");
        }

        private string ip = null;
        /// <summary>
        /// 当前IP
        /// </summary>
        public string IP
        {
            get
            {
                if (ip.IsNullOrEmpty())
                {
                    ip = Request.UserHostAddress;
                    try
                    {
                        if (!ip.IsNullOrEmpty() && ip.StartsWith("10.", StringComparison.Ordinal))
                        {
                            ip = Request.ServerVariables["HTTP_X_REAL_IP"].Split(',')[0].Trim();
                        }
                    }
                    catch { }
                }
                return ip;
            }
        }
    }
}