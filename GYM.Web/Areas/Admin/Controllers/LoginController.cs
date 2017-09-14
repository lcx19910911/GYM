using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using GYM.Web.Framework;
using GYM.IService;
using GYM.Core.Extensions;
using GYM.Core.Web;
using GYM.Core;
using GYM.Core.Model;
using GYM.Core.Code;
using GYM.Core.Helper;

namespace GYM.Web.Areas.Admin.Controllers
{
    public class LoginController : BaseAdminController
    {

        public IAdminService IAdminService;

        public LoginController(IAdminService _IAdminService)
        {
            this.IAdminService = _IAdminService;
        }

        
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        #region 验证码
        /// <summary>
        /// 验证码
        /// </summary>
        public void ValidateCode()
        {
            ValidateCodeGenertor v = ValidateCodeGenertor.Default;
            CacheHelper.Remove("code" + this.IP);
            var code = v.CreateValidateCode();
            CacheHelper.Set<string>("code" + this.IP, code, CacheTimeOption.SixMinutes);
            var tuple = new Tuple<ValidateCodeGenertor, string>(v, code);
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.ClearContent();
            HttpContext.Response.ContentType = "image/Jpeg";
            HttpContext.Response.BinaryWrite(tuple.Item1.CreateImageStream(tuple.Item2));    //  输出图片
        }
        #endregion

        /// <summary>
        /// 登录提交
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param> 
        /// <returns></returns>
        public JsonResult Submit(string account, string password,string code)
        {
            var result = IAdminService.Login(account, password, code);
            if (result.Success)
            {

                if (result.Result == null)
                    return JResult(new WebResult<bool> { Code = ErrorCode.sys_fail, Result = false, Append = "账号密码错误" });
                else
                {
                    if (result.Result.IsDelete)
                    {
                        return JResult(new WebResult<bool> { Code = ErrorCode.sys_fail, Result = false, Append = "该账户已被删除" });
                    }
                    else
                    {
                        this.LoginAdmin = new Core.Model.LoginUser(result.Result);
                    }
                }
                
            }
            return JResult(result);

        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Quit()
        {
            this.LoginAdmin = null;
            return View("Index");
        }
    }
}