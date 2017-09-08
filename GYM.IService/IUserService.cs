
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GYM.Core.Model;
using GYM.Model;

namespace GYM.IService
{
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        PageList<User> GetPageList(int pageIndex, int pageSize, string name,string phone);

        WebResult<bool> Manager(User model);


        User FindByOpenId(string openid);


    }
}
