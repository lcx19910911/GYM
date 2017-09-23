
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
    public interface ICourseService : IBaseService<Course>
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        PageList<Course> GetPageList(int pageIndex, int pageSize, string name,string storeName,string coachName,DateTime? createdTimeStart, DateTime? createdTimeEnd);

        List<SelectItem> GetSelectList();

        List<Course> GetListByParam(DateTime? searchTime, string coachId="", string storeId="");

        WebResult<bool> AddCourse(Course model);
        WebResult<bool> UpdateCourse(Course model);
    }
}
