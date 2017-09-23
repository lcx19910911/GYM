
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
    /// 课程表
    /// </summary>
    public class SyllabusService : BaseService<Syllabus>, ISyllabusService
    {
        public SyllabusService()
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
        public PageList<Syllabus> GetPageList(int pageIndex, int pageSize,string storeName, string coachName, DateTime? createdTimeStart, DateTime? createdTimeEnd)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.Syllabus.Where(x => !x.IsDelete);
                if (storeName.IsNotNullOrEmpty())
                {
                    var storeIDList = db.Store.Where(x => !x.IsDelete && x.Name.Contains(storeName)).Select(x => x.ID).ToList();
                    query = query.Where(x => storeIDList.Contains(x.StoreID));
                }
                if (coachName.IsNotNullOrEmpty())
                {
                    var coachIDList = db.Coach.Where(x => !x.IsDelete && x.Name.Contains(coachName)).Select(x => x.ID).ToList();
                    query = query.Where(x => coachIDList.Contains(x.StoreID));
                }
                if (createdTimeStart != null)
                {
                    query = query.Where(x => x.StartTime >= createdTimeStart);
                }
                if (createdTimeEnd != null)
                {
                    createdTimeEnd = createdTimeEnd.Value.AddDays(1);
                    query = query.Where(x => x.StartTime < createdTimeEnd);
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.StartTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var storeIdList = list.Select(x => x.StoreID).ToList();
                var coachIdList = list.Select(x => x.CoachID).ToList();
                var storeDic = db.Store.Where(x => storeIdList.Contains(x.ID)).ToDictionary(x => x.ID);
                var coachDic = db.Coach.Where(x => coachIdList.Contains(x.ID)).ToDictionary(x => x.ID);
                list.ForEach(x =>
                {
                    if (x.CoachID.IsNotNullOrEmpty() && coachDic.ContainsKey(x.CoachID))
                    {
                        x.CoachName = coachDic[x.CoachID]?.Name;
                    }
                    if (x.StoreID.IsNotNullOrEmpty() && storeDic.ContainsKey(x.StoreID))
                    {
                        x.StoreName = storeDic[x.StoreID]?.Name;
                    }
                });

                return CreatePageList(list, pageIndex, pageSize, count);

            }
        }


        /// <summary>
        /// 获取教练的课程
        /// </summary>
        /// <returns></returns>
        public List<Syllabus> GetListByParam(DateTime? searchTime,string courseId="", string coachId = "", string storeId = "")
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.Syllabus.Where(x => !x.IsDelete);
                if (searchTime != null)
                {
                    var searchTimeEnd = searchTime.Value.AddDays(1);
                    query.Where(x => x.CreatedTime > searchTime.Value && x.CreatedTime < searchTimeEnd);
                }
                if (coachId.IsNotNullOrEmpty()&&coachId!="-1")
                {
                    query = query.Where(x => x.CoachID == coachId);
                }
                if (storeId.IsNotNullOrEmpty() && coachId != "-1")
                {
                    query = query.Where(x => x.StoreID == storeId);
                }
                if (courseId.IsNotNullOrEmpty() && coachId != "-1")
                {
                    query = query.Where(x => x.CourseID == courseId);
                }
                return query.OrderBy(x => x.CreatedTime).ToList();
            }
        }
        public WebResult<bool> AddSyllabusList(List<Syllabus> model)
        {
            using (DbRepository db = new DbRepository())
            {
                if (model==null||model.Count==0)
                {
                    return Result(false,Core.Code.ErrorCode.sys_param_format_error);
                }
                db.Syllabus.AddRange(model);
                if (db.SaveChanges() > 0)
                {
                    return Result(true);
                }
                else
                {
                    return Result(false, ErrorCode.sys_fail);
                }
            }
        }
        public List<ZTreeNode> GetTimeZTree(string courseId, string cocahId, string storeId)
        {
            List<ZTreeNode> ztreeNodes = new List<ZTreeNode>();
            using (DbRepository db = new DbRepository())
            {
                var query = db.Syllabus.AsQueryable();
                if (storeId.IsNotNullOrEmpty() && storeId != "-1")
                {
                    query = query.Where(x => x.StoreID == storeId);
                }
                if (cocahId.IsNotNullOrEmpty() && cocahId != "-1")
                {
                    query = query.Where(x => x.CoachID == cocahId);
                }
                if (courseId.IsNotNullOrEmpty() && storeId != "-1")
                {
                    query = query.Where(x => x.CourseID == courseId);
                }
                var timeList = query.Where(x => !x.IsDelete).Select(x => x.EndTime).ToList();
                var time = DateTime.Now;
                if (timeList.Count != 0)
                {
                    var maxTime = timeList.Max();
                    if (maxTime > time)
                    {
                        time = maxTime.AddDays(1);
                    }
                }
                for (int i = 0; i < 30; i++)
                {
                    ztreeNodes.Add(new ZTreeNode()
                    {
                        name = time.AddDays(i).ToString("yyyy-MM-dd"),
                        value = (i+1).ToString(),
                    });
                }

                return ztreeNodes;
            }
        }
        public WebResult<bool> UpdateSyllabusList(List<Syllabus> model,List<string> deleteList)
        {
            using (DbRepository db = new DbRepository())
            {
                if (model == null || model.Count == 0)
                {
                    return Result(false, Core.Code.ErrorCode.sys_param_format_error);
                }              

                var code = ErrorCode.sys_success;
                model.ForEach(x =>
                {
                    if (x.ID.IsNotNullOrEmpty())
                    {
                        var oldPrice = db.Syllabus.Find(x.ID);
                        if (oldPrice == null)
                        {
                            code = ErrorCode.sys_param_format_error;
                        }
                        else
                        {
                            oldPrice.CoachID = x.CoachID;
                            oldPrice.StoreID = x.StoreID;
                            oldPrice.CourseID = x.CourseID;

                            oldPrice.StartTime = x.StartTime;
                            oldPrice.EndTime = x.EndTime;
                            oldPrice.PeopleLimit = x.PeopleLimit;
                        }
                    }
                    else
                    {
                        db.Syllabus.Add(x);
                    }
                });
                if (deleteList != null && deleteList.Count > 0)
                {
                    deleteList.ForEach(x =>
                    {
                        var oldEntity = db.Syllabus.Find(x);
                        if (oldEntity != null)
                        {
                            oldEntity.IsDelete = true;
                        }
                    });
                }
                if (code != ErrorCode.sys_success)
                {
                    return Result(false, code);
                }
                if (db.SaveChanges() > 0)
                {
                    return Result(true);
                }
                else
                {
                    return Result(false, ErrorCode.sys_fail);
                }
            }
        }
    }
}
