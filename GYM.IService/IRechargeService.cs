﻿
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
    public interface IRechargeService : IBaseService<Recharge>
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        PageList<Recharge> GetPageList(int pageIndex, int pageSize, string userName, PayState? stete, DateTime? createdTimeStart, DateTime? createdTimeEnd);


        WebResult<bool> CreateRecharge(Recharge model);

        WebResult<bool> SuccessRecharge(string id,string thirdOrderId);

        WebResult<bool> FailedRecharge(string id);
    }
}
