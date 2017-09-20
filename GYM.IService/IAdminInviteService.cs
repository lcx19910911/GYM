
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GYM.Core.Model;
using GYM.Model;
using GYM.DB;

namespace GYM.IService
{
    /// <summary>
    /// 邀请码
    /// </summary>
    public interface IAdminInviteService : IBaseService<AdminInvite>
    {
        WebResult<bool> ValiteNO(string no,string courseId, DbRepository db);

        PageList<AdminInvite> GetPageList(int pageIndex, int pageSize, string adminId, string courseName);
    }
}
