
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GYM.Core.Model;
using GYM.Model;
using GYM.Core;

namespace GYM.IService
{
    public interface IUserRelationService : IBaseService<UserRelation>
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        PageList<UserRelation> GetPageList(int pageIndex, int pageSize, string userName,string relationName);

        List<UserRelation> GetList(string userId);

        UserRelation Get(string userId, string relationUserId);
    }
}
