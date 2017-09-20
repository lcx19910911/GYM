
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
    /// 教练
    /// </summary>
    public class AdminInviteService : BaseService<AdminInvite>, IAdminInviteService
    {
        public AdminInviteService()
        {
            base.ContextCurrent = HttpContext.Current;
        }

        public WebResult<bool> ValiteNO(string no, string courseId, DbRepository db)
        {
            if (no.IsNullOrEmpty() || courseId.IsNullOrEmpty())
            {
                return Result(false, ErrorCode.sys_param_format_error);
            }
                var inviteModel = db.AdminInvite.FirstOrDefault(x => x.InviteNO == no && x.CourseID == courseId && x.StartTime < DateTime.Now && x.EndTime > DateTime.Now);
                if (inviteModel == null)
                {
                    return Result(false, ErrorCode.invite_code_error);
                }
                if (inviteModel.IsUsed)
                {
                    return Result(false, ErrorCode.code_had_used);
                }
                inviteModel.IsUsed = true;

                return Result(true);
        }


        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        public PageList<AdminInvite> GetPageList(int pageIndex, int pageSize, string adminId, string courseName)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.AdminInvite.Where(x => !x.IsDelete);
                if (adminId.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.AdminID.Contains(adminId));
                }
                if (courseName.IsNotNullOrEmpty())
                {
                    var courseIDList = db.Course.Where(x => !x.IsDelete && x.Name.Contains(courseName)).Select(x => x.ID).ToList();
                    query = query.Where(x => courseIDList.Contains(x.CourseID));
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var storeDic = db.Store.ToDictionary(x => x.ID);


                return CreatePageList(list, pageIndex, pageSize, count);

            }
        }

    }
}
