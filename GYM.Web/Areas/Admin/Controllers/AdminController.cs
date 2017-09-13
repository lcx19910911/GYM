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
    /// <summary>
    /// 管理员管理
    /// </summary>
    public class AdminController : BaseAdminController
    {
        public IAdminService IAdminService;

        public AdminController(IAdminService _IAdminService)
        {
            this.IAdminService = _IAdminService;
        }
        // GET: 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();

        }
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="name">名称 - 搜索项</param>
        /// <param name="no">编号 - 搜索项</param>
        /// <returns></returns>
        public ActionResult GetPageList(int pageIndex, int pageSize, string name, string mobile)
        {
            return JResult(IAdminService.GetPageList(pageIndex, pageSize, name, mobile));
        }


        /// <summary>
        /// 增加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(GYM.Model.Admin entity)
        {
            ModelState.Remove("ID");
            ModelState.Remove("UpdaterID");
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("CreatedTime");
            if (ModelState.IsValid)
            {
                if (IAdminService.IsExits(x => x.Account == entity.Account))
                {
                    return JResult(Core.Code.ErrorCode.user_account_already_exist);
                }
                entity.CreatedTime = entity.UpdatedTime = DateTime.Now;
                var result = IAdminService.Add(entity);
                return JResult(result);
            }
            else
            {
                return ParamsErrorJResult(ModelState);
            }
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(GYM.Model.Admin model)
        {
            ModelState.Remove("UpdaterID");
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("CreatedTime");
            ModelState.Remove("NewPassword");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                var result = WebService.Update_User(model);
                return JResult(result);
            }
            else
            {
                return ParamsErrorJResult(ModelState);
            }
        }

        /// <summary>
        /// 操作权限
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="OperateFlag"></param>
        /// <returns></returns>

        public ActionResult UpdateOperate(string ID, long OperateFlag)
        {
            return JResult(WebService.Update_UserOperate(ID, OperateFlag));
        }

        /// <summary>
        /// 报名点权限
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="enteredPointIDStr"></param>
        /// <returns></returns>
        public ActionResult UpdateEnteredPointIDStr(string ID, string enteredPointIDStr)
        {
            return JResult(WebService.Update_UserEnteredPointIDStr(ID, enteredPointIDStr));
        }

        /// <summary>
        /// 菜单权限
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="enteredPointIDStr"></param>
        /// <returns></returns>
        public ActionResult UpdateMenuIDStr(string ID, string menuIDStr)
        {
            return JResult(WebService.Update_MenuIDStr(ID, menuIDStr));
        }
        



        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string ids)
        {
            return JResult(WebService.Delete_User(ids));
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <returns></returns>
        public ActionResult Find(string id)
        {
            return JResult(WebService.Find_User(id));
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <returns></returns>
        public ActionResult Enable(string ids)
        {
            return JResult(WebService.Enable_User(ids));
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <returns></returns>
        public ActionResult Quit(string id,DateTime quitTime)
        {
            return JResult(WebService.User_Quit(id, quitTime));
        }

        

        /// <summary>
        /// 禁用
        /// </summary>
        /// <returns></returns>
        public ActionResult Disable(string ids)
        {
            return JResult(WebService.Disable_User(ids));
        }

        /// <summary>
        /// 获取用户选择项
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSelectItem()
        {
            return JResult(WebService.Get_UserSelectItem());
        }
    }
}