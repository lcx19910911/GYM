
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
    /// 充值方案
    /// </summary>
    public class RechargeService : BaseService<Recharge>, IRechargeService
    {
        public RechargeService()
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
        public PageList<Recharge> GetPageList(int pageIndex, int pageSize, string userName, PayState? stete, DateTime? createdTimeStart, DateTime? createdTimeEnd)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.Recharge.Where(x => !x.IsDelete);

                if (userName.IsNotNullOrEmpty())
                {
                    var userIDList = db.User.Where(x => !x.IsDelete && x.RealName.Contains(userName)).Select(x => x.ID).ToList();
                    query = query.Where(x => userIDList.Contains(x.UserID));
                }
                if (createdTimeStart != null)
                {
                    query = query.Where(x => x.CreatedTime >= createdTimeStart);
                }
                if (createdTimeEnd != null)
                {
                    createdTimeEnd = createdTimeEnd.Value.AddDays(1);
                    query = query.Where(x => x.CreatedTime < createdTimeEnd);
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                var selectUserIDList = list.Select(x => x.UserID).ToList();
                var userDic = db.User.Where(x => !x.IsDelete && selectUserIDList.Contains(x.ID)).ToDictionary(x => x.ID);
                list.ForEach(x =>
                {
                    if (x.UserID.IsNullOrEmpty() && userDic.ContainsKey(x.UserID))
                    {
                        x.UserName = userDic[x.UserID]?.RealName;
                    }
                    x.TypeStr = x.Type.GetDescription();
                    x.StateStr = x.State.GetDescription();
                });
                return CreatePageList(list, pageIndex, pageSize, count);

            }
        }

        /// <summary>
        /// 创建充值记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WebResult<bool> CreateRecharge(Recharge model)
        {
            if (model == null && model.Amount <= 0)
            {
                return Result(false, ErrorCode.sys_param_format_error);
            }
            using (var db = new DbRepository())
            {
                model.UserID = Client.LoginUser.ID;
                return Result(true);
            }
        }


        /// <summary>
        /// 支付成功
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WebResult<bool> SuccessRecharge(string id, string thirdOrderId)
        {
            if (id.IsNullOrEmpty()|| thirdOrderId.IsNullOrEmpty())
            {
                return Result(false, ErrorCode.sys_param_format_error);
            }
            using (var db = new DbRepository())
            {
                var rechargeModel = db.Recharge.Find(id);
                if (rechargeModel == null || rechargeModel.IsDelete || rechargeModel.State != PayState.WaitPay)
                {
                    return Result(false, ErrorCode.sys_param_format_error);
                }
                rechargeModel.State = PayState.Success;
                rechargeModel.SuccessTime = DateTime.Now;
                rechargeModel.ThirdOrderID = thirdOrderId;

                var user = db.User.Find(rechargeModel.UserID);
                if (user == null || user.IsDelete)
                {
                    return Result(false, ErrorCode.sys_param_format_error);
                }

                rechargeModel.BeforeBalance = user.Balance;
                rechargeModel.AfterBalance = user.Balance + rechargeModel.Amount;

                user.Balance += rechargeModel.Amount;
                user.TotalRecharge += rechargeModel.Amount;

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

        /// <summary>
        /// 充值失败
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WebResult<bool> FailedRecharge(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return Result(false, ErrorCode.sys_param_format_error);
            }
            using (var db = new DbRepository())
            {
                var rechargeModel = db.Recharge.Find(id);
                if (rechargeModel == null || rechargeModel.IsDelete || rechargeModel.State != PayState.WaitPay)
                {
                    return Result(false, ErrorCode.sys_param_format_error);
                }
                rechargeModel.State = PayState.Failed;

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
