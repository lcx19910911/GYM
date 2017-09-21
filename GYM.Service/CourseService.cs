
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
    /// 课程
    /// </summary>
    public class CourseService : BaseService<Course>, ICourseService
    {
        public CourseService()
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
        public PageList<Course> GetPageList(int pageIndex, int pageSize, string name, string storeName, string coachName, DateTime? createdTimeStart, DateTime? createdTimeEnd)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.Course.Where(x => !x.IsDelete);
                if (name.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.Name.Contains(name));
                }
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
                var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var storeIdList = list.Select(x => x.StoreID).ToList();
                var coachIdList = list.Select(x => x.CoachID).ToList();
                var storeDic = db.Store.Where(x => storeIdList.Contains(x.ID)).ToDictionary(x=>x.ID);
                var coachDic = db.Coach.Where(x => coachIdList.Contains(x.ID)).ToDictionary(x => x.ID);
                var categoryIdsList = list.Select(x => x.CourseCategoryIDS).ToList();
                var categoryIdList = new List<string>();
                categoryIdsList.ForEach(x => categoryIdList.AddRange(x.Split(',')));
                var categoryDic = db.CourseCategory.Where(x => categoryIdList.Contains(x.ID)).ToDictionary(x=>x.ID);
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
                    if (x.CourseCategoryIDS.IsNotNullOrEmpty())
                    {
                        x.CourseCategoryIDS.Split(',').ToList().ForEach(y=> {
                            if (categoryDic.ContainsKey(y))
                            {
                                x.CourseCategoryNameS += categoryDic[y].Name+" ";
                            }
                        });
                    }
                });

                return CreatePageList(list, pageIndex, pageSize, count);

            }
        }



        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<SelectItem> GetSelectList()
        {
            using (DbRepository db = new DbRepository())
            {
                return db.Course.Where(x => !x.IsDelete).OrderBy(x=>x.StartTime).Select(x => new SelectItem()
                {
                    Text = x.Name,
                    Value = x.ID,
                }).ToList(); ;
            }
        }


        /// <summary>
        /// 获取教练的课程
        /// </summary>
        /// <returns></returns>
        public List<Course> GetListByParam(DateTime? searchTime, string coachId = "", string storeId = "")
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.Course.Where(x => !x.IsDelete);
                if (searchTime != null)
                {
                    var searchTimeEnd = searchTime.Value.AddDays(1);
                    query.Where(x => x.StartTime > searchTime.Value && x.StartTime < searchTimeEnd);
                }
                if (coachId.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.CoachID == coachId);
                }
                if (storeId.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.StoreID == storeId);
                }
                return db.Course.Where(x => !x.IsDelete&&x.CoachID==coachId).OrderBy(x => x.StartTime).ToList();
            }
        }
        public WebResult<bool> AddCourse(Course model, List<CoursePrice> priceList)
        {
            using (DbRepository db = new DbRepository())
            {
                if (model==null||priceList == null || (priceList != null && priceList.Count == 0))
                {
                    return Result(false,Core.Code.ErrorCode.course_no_had_price);
                }
                if (db.Course.Any(x => x.Name == model.Name && x.CoachID == x.CoachID))
                {
                    return Result(false,Core.Code.ErrorCode.store_coach_cours_ralready_exist);
                }
                var countDic = priceList.GroupBy(x => x.ThingName).ToDictionary(x => x.Key, x => x.Count());
                if (countDic.Values.Any(x => x > 1))
                {
                    return Result(false, Core.Code.ErrorCode.courseprice_name_extis);
                }
                model.ID = Guid.NewGuid().ToString("N");
                db.Course.Add(model);
                priceList.ForEach(x => { x.CourseID = model.ID; });
                db.CoursePrice.AddRange(priceList);
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

        public WebResult<bool> UpdateCourse(Course model, List<CoursePrice> priceList)
        {
            using (DbRepository db = new DbRepository())
            {
                if (model == null || priceList == null || (priceList != null && priceList.Count == 0))
                {
                    return Result(false, Core.Code.ErrorCode.course_no_had_price);
                }
                if (db.Course.Any(x => x.Name == model.Name && x.CoachID == model.CoachID&&x.ID!=model.ID))
                {
                    return Result(false, Core.Code.ErrorCode.store_coach_cours_ralready_exist);
                }
                var countDic = priceList.GroupBy(x => x.ThingName).ToDictionary(x => x.Key, x => x.Count());
                if (countDic.Values.Any(x => x > 1))
                {
                    return Result(false, Core.Code.ErrorCode.courseprice_name_extis);
                }

                var oldModel = db.Course.Find(model.ID);
                if (oldModel == null)
                {
                    return Result(false, Core.Code.ErrorCode.sys_param_format_error);
                }

                oldModel.Name = model.Name;
                oldModel.Pictures = model.Pictures;
                oldModel.StartTime = model.StartTime;
                oldModel.EndTime = model.EndTime;
                oldModel.StoreID = model.StoreID;
                oldModel.CoachID = model.CoachID;
                oldModel.CourseCategoryIDS = model.CourseCategoryIDS;
                oldModel.FAQ = model.FAQ;
                oldModel.FitPeople = model.FitPeople;
                oldModel.TrainResult = model.TrainResult;
                oldModel.PeopleLimit = model.PeopleLimit;                

                var code = ErrorCode.sys_success;
                var priceIDList = db.CoursePrice.Where(x => !x.IsDelete && x.CourseID == model.ID).Select(x => x.ID).ToList();
                priceList.ForEach(x => {
                    if (x.ID.IsNotNullOrEmpty())
                    {
                        var oldPrice = db.CoursePrice.Find(x.ID);
                        if (oldPrice == null)
                        {
                            code = ErrorCode.sys_param_format_error;
                        }
                        else
                        {
                            oldPrice.ThingName = x.ThingName;
                            oldPrice.Price = x.Price;
                            oldPrice.DiscountPrice = x.DiscountPrice;
                        }
                    }
                    else
                    {
                        x.CourseID = model.ID;
                        db.CoursePrice.Add(x);
                    }
                });
              
                priceIDList.Where(x => !priceList.Select(y => y.ID).Contains(x)).ToList().ForEach(x =>
                {
                    var oldPrice = db.CoursePrice.Find(x);
                    if (oldPrice != null)
                    {
                        oldPrice.IsDelete = true;
                    }
                });
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
