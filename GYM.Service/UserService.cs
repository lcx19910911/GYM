
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GYM.Core;
using GYM.Core.Code;
using GYM.Core.Extensions;
using GYM.Core.Helper;
using GYM.Core.Model;
using GYM.Core.Web;
using GYM.DB;
using GYM.IService;
using GYM.Model;

namespace GYM.Service
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public class UserService : BaseService<User>, IUserService
    {
        public UserService()
        {
            base.ContextCurrent = HttpContext.Current;
        }
        

        /// <summary>
        /// 编辑管理用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WebResult<bool> Manager(User model)
        {
            var user = Find(model.ID);
            if (user == null)
            {
                model.CreatedTime = DateTime.Now; ;
                int id=Add(model);
                if (id > 0)
                {
                    LoginHelper.CreateUser(new LoginUser(model), Params.UserCookieName);
                    return Result(true);
                }
                else
                    return Result(false, ErrorCode.sys_fail);
            }
            else
            {
                
                user.NickName = model.NickName;
                user.RealName = model.RealName;
                int result=Update(user);
                if (result > 0)
                {
                    return Result(true);
                }
                else
                    return Result(false, ErrorCode.sys_fail);
            }

        }



        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public User FindByOpenId(string openId)
        {
            if (string.IsNullOrEmpty(openId))
                return null;
            return Find(x => x.OpenId == openId);
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        public PageList<User> GetPageList(int pageIndex, int pageSize, string name,string phone,bool isMember)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.User.Where(x => !x.IsDelete&&x.IsMember==isMember);
                if (name.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.NickName.Contains(name));
                }
                if (phone.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.MobilePhone.Contains(phone));
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                list.ForEach(x =>
                {
                });

                return CreatePageList(list, pageIndex, pageSize, count);

            }
        }
    }
}
