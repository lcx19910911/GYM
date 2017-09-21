
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
    /// 用户关系
    /// </summary>
    public class UserRelationService : BaseService<UserRelation>, IUserRelationService
    {
        public UserRelationService()
        {
            base.ContextCurrent = HttpContext.Current;
        }
        
       

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        public PageList<UserRelation> GetPageList(int pageIndex, int pageSize, string name, string relationName)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.UserRelation.Where(x => !x.IsDelete);
                if (name.IsNotNullOrEmpty())
                {
                    var selectUserIdList = db.User.Where(x => !x.IsDelete && x.RealName.Contains(name)).Select(x => x.ID).ToList();

                    query = query.Where(x => selectUserIdList.Contains(x.UserID)|| selectUserIdList.Contains(x.RelationID));
                }
                if (relationName.IsNotNullOrEmpty())
                {
                    var selectUserIdList = db.User.Where(x => !x.IsDelete && x.RealName.Contains(relationName)).Select(x => x.ID).ToList();

                    query = query.Where(x => selectUserIdList.Contains(x.UserID) || selectUserIdList.Contains(x.RelationID));
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var userIdList = list.Select(x => x.UserID).ToList();
                userIdList.AddRange(list.Select(x => x.RelationID).ToList());
                var userDic = db.User.Where(x => userIdList.Contains(x.ID)).ToDictionary(x => x.ID);
                var relationIdList = list.Select(x => x.RelationID).ToList();
                var relationDic = db.Relation.Where(x => relationIdList.Contains(x.ID)).ToDictionary(x => x.ID);
                list.ForEach(x =>
                {
                    if (x.UserID.IsNotNullOrEmpty() && userDic.ContainsKey(x.UserID))
                    {
                        x.UserName = userDic[x.UserID].RealName;
                    }
                    if (x.RelationUserID.IsNotNullOrEmpty() && userDic.ContainsKey(x.RelationUserID))
                    {
                        x.RelationUserName = userDic[x.RelationUserID].RealName;
                    }
                    if (x.RelationID.IsNotNullOrEmpty() && relationDic.ContainsKey(x.RelationID))
                    {
                        x.RelationName = relationDic[x.RelationID].Name;
                    }
                });

                return CreatePageList(list, pageIndex, pageSize, count);

            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<UserRelation> GetList(string userId)
        {
            using (DbRepository db = new DbRepository())
            {
                return db.UserRelation.Where(x => !x.IsDelete&&(x.UserID==userId|| x.RelationUserID == userId)).OrderByDescending(x=>x.CreatedTime).ToList();
            }
        }

        public UserRelation Get(string userId, string relationUserId)
        {
            using (DbRepository db = new DbRepository())
            {
                return db.UserRelation.Where(x => !x.IsDelete && (x.UserID == userId&&x.RelationUserID==relationUserId|| x.UserID == relationUserId && x.RelationUserID == userId)).FirstOrDefault();
            }
        }
    }
}
