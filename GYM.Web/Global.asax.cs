using GYM.Core;
using GYM.Core.Util;
using GYM.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GYM.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            LogHelper.WriteCustom(string.Format("Application_Start At {0} \r\n", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")), @"Application\", false);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            Database.SetInitializer<DbRepository>(null);
            ///预加载
            using (var dbcontext = new DbRepository())
            {
                dbcontext.Database.CreateIfNotExists();
                var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
                if (!dbcontext.Admin.Where(x => x.Type == Model.AdminType.SuperAdmin).Any())
                {
                    dbcontext.Admin.Add(new Model.Admin()
                    {
                        CreatedTime = DateTime.Now,
                        Account = "admin",
                        Type = Model.AdminType.SuperAdmin,
                        Password = CryptoHelper.MD5_Encrypt("123456"),
                        RealName = "超级管理员",
                        NickName = "超级管理员",
                        HeadImgUrl = "",
                    });
                    dbcontext.SaveChanges();
                }
            }
        }



        protected void Application_End(object sender, EventArgs e)
        {
            var runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);
            if (runtime != null)
            {
                string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
                string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
                ApplicationShutdownReason shutDownReason = (ApplicationShutdownReason)runtime.GetType().InvokeMember("_shutdownReason", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
                LogHelper.WriteCustom(string.Format("Application_End:shutDownMessage:\r\n{0}\r\nshutDownStack:\r\n{1}\r\nshutDownReason:\r\n{2}\r\n", shutDownMessage, shutDownStack, shutDownReason.ToString()), @"Application\", false);
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception is ThreadAbortException)
            {
                Thread.ResetAbort();
                HttpContext.Current.ClearError();
                return;
            }
            var httpException = exception as HttpException;

            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                LogHelper.WriteCustom(httpException.ToString(), "404Error\\");
                Server.ClearError();
                Response.Clear();
                Response.Redirect("/base/_404");
            }
            else
            {
                LogHelper.WriteException("Application Error.", Server.GetLastError());
                Server.ClearError();
                Response.Clear();
                Response.Redirect("/base/_500");
            }

        }


        //解决不同浏览器的上传问题
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                string user_cookie_name = Params.UserCookieName;
                string token_cookie_name = Params.UserTokenCookieName;

                if (HttpContext.Current.Request.Cookies[user_cookie_name] != null)
                {
                    RefreshCookie(user_cookie_name);
                }
                if (HttpContext.Current.Request.Cookies[token_cookie_name] != null)
                {
                    RefreshCookie(token_cookie_name);
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// 修改cookie
        /// </summary>
        /// <param name="cookie_name">cookie名</param>
        private void RefreshCookie(string cookie_name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (null != cookie)
            {
                cookie.Expires = DateTime.Now.AddHours(1);
                HttpContext.Current.Request.Cookies.Set(cookie);
            }
        }

    }
}