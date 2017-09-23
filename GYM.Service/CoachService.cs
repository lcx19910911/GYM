
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
    public class CoachService : BaseService<Coach>, ICoachService
    {
        public CoachService()
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
        public PageList<Coach> GetPageList(int pageIndex, int pageSize, string name, string phone, string idCard)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.Coach.Where(x => !x.IsDelete);
                if (name.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.Name.Contains(name));
                }
                if (phone.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.Mobile.Contains(phone));
                }
                if (idCard.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.IDCard.Contains(idCard));
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                //var storeDic = db.Store.ToDictionary(x => x.ID);
                list.ForEach(x =>
                {
                    //if (x.StoreID.IsNotNullOrEmpty() && storeDic.ContainsKey(x.StoreID))
                    //{
                    //    x.StoreName = storeDic[x.StoreID].Name;
                    //}
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
                return db.Coach.Where(x => !x.IsDelete).Select(x => new SelectItem()
                {
                    Text = x.Name,
                    Value = x.ID,
                }).ToList(); ;
            }
        }

        public WebResult<bool> AddCoach(Coach model)
        {
            if (IsExits(x => x.Mobile == model.Mobile))
            {
                return Result(false, ErrorCode.system_phone_already_exist);
            }
            using (var db = new DbRepository())
            {
                if (db.Admin.Any(x => x.Account == model.Mobile))
                {
                    return Result(false, ErrorCode.system_phone_already_exist);
                }
                model.ID = Guid.NewGuid().ToString("N");
                db.Coach.Add(model);

                var admin = new Admin()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    Type = AdminCode.Coach,
                    CoachID = model.ID,
                    Account = model.Mobile,
                    Mobile = model.Mobile,
                    Sex = model.Sex,
                    Password = CryptoHelper.MD5_Encrypt("123456"),
                    RealName = model.Name,
                };

                var result = db.SaveChanges();
                if (result > 0)
                {
                    return Result(true);
                }
                else
                {
                    return Result(false, ErrorCode.sys_fail);
                }
            }
        }


        public WebResult<bool> UpdateCoach(Coach entity)
        {
            if (IsExits(x => x.Mobile == entity.Mobile && x.ID != entity.ID))
            {
                return Result(false, ErrorCode.system_phone_already_exist);
            }
            using (var db = new DbRepository())
            {
                var model = db.Coach.Find(entity.ID);
                if (model == null)
                {
                    return Result(false, ErrorCode.sys_param_format_error);
                }
                if (db.Admin.Any(x => x.Account == model.Mobile && x.CoachID != entity.ID))
                {
                    return Result(false, ErrorCode.system_phone_already_exist);
                }
                model.HeadImgUrl = entity.HeadImgUrl;
                model.Name = entity.Name;
                model.Pictures = entity.Pictures;
                model.Mobile = entity.Mobile;
                model.Sex = entity.Sex;
                model.Address = entity.Address;
                model.IDCard = entity.IDCard;
                model.BasicSalary = entity.BasicSalary;
                model.EntryDate = entity.EntryDate;
                model.QuitTime = entity.QuitTime;
                model.IsQuit = entity.IsQuit;
                model.TrainYears = entity.TrainYears;
                //model.StoreID = entity.StoreID;
                model.Introduce = entity.Introduce;


                var admin = db.Admin.Where(x => x.CoachID == model.ID).FirstOrDefault();
                if (admin == null)
                {
                    return Result(false, ErrorCode.sys_param_format_error);
                }

                admin.Account = model.Mobile;
                admin.RealName = model.Name;
                admin.Sex = model.Sex;

                var result = db.SaveChanges();
                if (result > 0)
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
