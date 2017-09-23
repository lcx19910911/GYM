
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
    public interface ISyllabusService : IBaseService<Syllabus>
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        PageList<Syllabus> GetPageList(int pageIndex, int pageSize,string storeName,string coachName,DateTime? createdTimeStart, DateTime? createdTimeEnd);

        List<Syllabus> GetListByParam(DateTime? searchTime,string courseId="", string coachId="", string storeId="");

        WebResult<bool> AddSyllabusList(List<Syllabus> model);
        WebResult<bool> UpdateSyllabusList(List<Syllabus> model, List<string> deleteList);

        List<ZTreeNode> GetTimeZTree(string courseId, string cocahId, string storeId);
    }
}
