using GYM.Core.Helper;
using GYM.IService;
using GYM.Model;
using GYM.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM.Web.Areas.Admin.Controllers
{
    public class RechargeController : BaseAdminController
    {
        public IRechargeService IRechargeService;

        public RechargeController(IRechargeService _IRechargeService)
        {
            this.IRechargeService = _IRechargeService;
        }
        public ViewResult Index()
        {
            return View();
        }

    


        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="key"> 搜索项</param>
        /// <param name="value">搜索项</param>
        /// <returns></returns>
        public ActionResult GetPageList(int pageIndex, int pageSize, string userName, PayState? stete, DateTime? createdTimeStart, DateTime? createdTimeEnd)
        {
            return JResult(IRechargeService.GetPageList(pageIndex, pageSize, userName,stete, createdTimeStart, createdTimeEnd));
        }


        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Find(string ID)
        {
            return JResult(IRechargeService.Find(ID));
        }
    }
}