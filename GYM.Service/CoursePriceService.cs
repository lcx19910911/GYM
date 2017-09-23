
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
    /// 课程价格
    /// </summary>
    public class CoursePriceService : BaseService<CoursePrice>, ICoursePriceService
    {
        public CoursePriceService()
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
        //public PageList<CoursePrice> GetPageList(int pageIndex, int pageSize, string name, string courseName)
        //{
        //    using (DbRepository db = new DbRepository())
        //    {
        //        var query = db.CoursePrice.Where(x => !x.IsDelete);
        //        if (name.IsNotNullOrEmpty())
        //        {
        //            query = query.Where(x => x.ThingName.Contains(name));
        //        }
        //        if (courseName.IsNotNullOrEmpty())
        //        {
        //            var courseIDList = db.Course.Where(x => !x.IsDelete && x.Name.Contains(courseName)).Select(x => x.ID).ToList();
        //            query = query.Where(x => courseIDList.Contains(x.CourseID));
        //        }
        //        var count = query.Count();
        //        var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //        list.ForEach(x =>
        //        {
        //        });

        //        return CreatePageList(list, pageIndex, pageSize, count);

        //    }
        //}



        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        //public List<CoursePrice> GetListByCourseID(string courseID)
        //{
        //    using (DbRepository db = new DbRepository())
        //    {
        //        return db.CoursePrice.Where(x => !x.IsDelete&&x.CourseID==courseID).OrderByDescending(x=>x.ThingName).ToList(); ;
        //    }
        //}
    }
}
