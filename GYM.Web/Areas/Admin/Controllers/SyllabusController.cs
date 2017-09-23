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
    public class SyllabusController : BaseAdminController
    {
        public ISyllabusService ISyllabusService;

        public SyllabusController(ISyllabusService _ISyllabusService)
        {
            this.ISyllabusService = _ISyllabusService;
        }
        public ViewResult Index()
        {
            return View();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public JsonResult Add(List<Syllabus> entity)
        {
            ModelState.Remove("ID");
            ModelState.Remove("CreatedTime");
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("IsDelete");
            ModelState.Remove("SyllabusID");
            if (ModelState.IsValid)
            {             
                return JResult(ISyllabusService.AddSyllabusList(entity));
            }
            else
            {
                return ParamsErrorJResult(ModelState);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public JsonResult Update(List<Syllabus> entity,List<string> deleteList)
        {
            ModelState.Remove("CreatedTime");
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("IsDelete");
            if (ModelState.IsValid)
            { 
                var result = ISyllabusService.UpdateSyllabusList(entity, deleteList);
                return JResult(result);
            }
            else
            {
                return ParamsErrorJResult(ModelState);
            }
        }


        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="key"> 搜索项</param>
        /// <param name="value">搜索项</param>
        /// <returns></returns>
        public ActionResult GetPageList(int pageIndex, int pageSize, string storeName, string coachName, DateTime? createdTimeStart, DateTime? createdTimeEnd)
        {
            return JResult(ISyllabusService.GetPageList(pageIndex, pageSize, storeName, coachName, createdTimeStart, createdTimeEnd));
        }


        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Find(string ID)
        {
            var model = ISyllabusService.Find(ID);        
            return JResult(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult Delete(string ID)
        {
            return JResult(ISyllabusService.Delete(ID));
        }

        public ActionResult GetTimeZTree(string courseId, string cocahId, string storeId)
        {
            return JResult(ISyllabusService.GetTimeZTree(courseId, cocahId, storeId));
        }
    }
}