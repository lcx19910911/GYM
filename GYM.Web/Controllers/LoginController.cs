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

namespace GYM.Web.Controllers
{
    public class LoginController : BaseUserController
    {

        public IUserService IUserService;

        public LoginController(IUserService _IUserService)
        {
            this.IUserService = _IUserService;
        }

        
        // GET: Login
        public ActionResult Index()
        {

            if (Request.UserAgent.IsNotNullOrEmpty() && Request.UserAgent.ToLower().Contains("micromessenger"))
            {
                WeixinLoginAction();
            }
            return View();
        }


        public void WeixinLoginAction()
        {
            string code = this.Request.QueryString["code"];


            //LogHelper.WriteInfo("MeetID-  " + this.MeetID);
            //本地没有member cookies
            if (this.LoginUser==null)
            {
                //请求回来
                if (!string.IsNullOrEmpty(code))
                {
                    var url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + Params.WeixinAppId + "&secret=" + Params.WeixinAppSecret + "&code=" + code + "&grant_type=authorization_code";

                  
                    string responseResult = WebHelper.GetPage(url);
                    
                    if (responseResult.Contains("access_token"))
                    {
                        JObject obj2 = JsonConvert.DeserializeObject(responseResult) as JObject;

                        var access_token = obj2["access_token"].ToString();
                        string openId = obj2["openid"].ToString();
                        var user = IUserService.FindByOpenId(openId);
                        if (user == null)
                        {
                            string userResponseResult = WebHelper.GetPage("https://api.weixin.qq.com/sns/userinfo?access_token=" + access_token + "&openid=" + openId + "&lang=zh_CN");
                            JObject obj3 = JsonConvert.DeserializeObject(userResponseResult) as JObject;
                            if (obj3!=null)
                            {
                                var model = new Model.User()
                                {
                                    ID = Guid.NewGuid().ToString("N"),
                                    NickName = obj3["nickname"].ToString(),
                                    OpenId = obj3["openid"].ToString(),
                                    Sex = obj3["sex"].GetInt(),
                                    Province = obj3["province"].ToString(),
                                    City = obj3["city"].ToString(),
                                    Country = obj3["country"].ToString(),
                                    HeadImgUrl = obj3["headimgurl"].ToString()
                                };
                                IUserService.Add(model);
                                this.LoginUser = new Core.Model.LoginUser(model);
                                this.Response.Redirect("/home/index");
                            }
                            else
                            {
                                this.Response.Redirect("/base/_404");
                            }
                        }
                        else
                        {
                            this.LoginUser = new Core.Model.LoginUser(user);
                            this.Response.Redirect("/home/index");
                        }

                    }
                    else
                    {
                        this.Response.Redirect("/base/_404");
                    }
                }
                else if (!string.IsNullOrEmpty(this.Request.QueryString["state"]))
                {
                    this.Response.Redirect("https://" +Params.DomianName);
                }
                else
                {
                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Params.WeixinAppId + "&redirect_uri=" + HttpUtility.UrlEncode(this.Request.Url.ToString().Replace(":" + this.Request.Url.Port.ToString(), "")) + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
                    this.Response.Redirect(url);
                }

            }

        }


        public string GetApplicationPath()
        {
            string applicationPath = "/";
            if (Request.RequestContext != null)
            {
                try
                {
                    applicationPath = Request.ApplicationPath;
                }
                catch
                {
                    applicationPath = AppDomain.CurrentDomain.BaseDirectory;
                }
            }
            if (applicationPath == "/")
            {
                return string.Empty;
            }
            return applicationPath.ToLower(CultureInfo.InvariantCulture);
        }



        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Quit()
        {
            this.LoginUser = null;
            return View("Index");
        }
    }
}