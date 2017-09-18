
using System;
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
    public class AdminService : BaseService<Admin>, IAdminService
    {
        public AdminService()
        {
            base.ContextCurrent = HttpContext.Current;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public WebResult<Admin> Login(string name, string password,string code)
        {
            using (DbRepository entities = new DbRepository())
            {
                var realCode = CacheHelper.Get<string>("validate_code" + Client.IP);
                if (code.IsNullOrEmpty() || !code.Equals(code, StringComparison.InvariantCultureIgnoreCase))
                {
                    CacheHelper.Remove("validate_code" + Client.IP);
                    return Result(new Admin() { }, ErrorCode.verification_time_out);
                }
                string md5Password = Core.Util.CryptoHelper.MD5_Encrypt(password);

                return Result(Find(x => x.Account == name && x.Password == md5Password &&!x.IsDelete));
            }
        }

        /// <summary>
        /// 编辑管理用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WebResult<bool> Manager(Admin model)
        {
            var user = Find(model.ID);
            if (user == null)
            {
                int id=Add(model);
                if (id > 0)
                {
                    LoginHelper.CreateUser(new LoginUser(model),Params.AdminCookieName);
                    return Result(true);
                }
                else
                    return Result(false, ErrorCode.sys_fail);
            }
            else
            {

                if (IsExits(x => x.Account == model.Account && x.ID != model.ID))
                    return Result(false, ErrorCode.user_account_already_exist);
              
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
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        public PageList<Admin> GetPageList(int pageIndex, int pageSize, string name, string phone)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.Admin.Where(x => !x.IsDelete&&x.Type!=AdminCode.SuperAdmin);
                if (name.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.RealName.Contains(name));
                }
                if (phone.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.Mobile.Contains(phone));
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var roleIDList = list.Select(x => x.RoleID).ToList();
                var roleDic = db.Role.Where(x => roleIDList.Contains(x.ID)).ToDictionary(x => x.ID);
                list.ForEach(x =>
                {
                    x.TypeStr = x.Type.GetDescription();
                    if (x.RoleID.IsNotNullOrEmpty() && roleDic.ContainsKey(x.RoleID))
                    {
                        x.RoleName = roleDic[x.RoleID]?.Name;
                    }
                });

                return CreatePageList(list, pageIndex, pageSize, count);

            }
        }


        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns> 
        public WebResult<bool> ChangePassword(string oldPassword, string newPassword, string cfmPassword, string id)
        {
            if (newPassword.IsNullOrEmpty() || cfmPassword.IsNullOrEmpty() || id.IsNullOrEmpty())
            {
                return Result(false, ErrorCode.sys_param_format_error);
            }
            if (!cfmPassword.Equals(newPassword))
            {
                return Result(false, ErrorCode.user_password_notequal);

            }
            var user = Find(id);
            if (user == null)
                return Result(false, ErrorCode.user_not_exit);
            if (oldPassword == "")
            {
                oldPassword = CryptoHelper.MD5_Encrypt(oldPassword);
                if (!user.Password.Equals(oldPassword))
                    return Result(false, ErrorCode.user_password_nottrue);
            }
            newPassword = CryptoHelper.MD5_Encrypt(newPassword);
            user.Password = newPassword;
            Update(user);
            return Result(true);
        }


    }
}
