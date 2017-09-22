using GYM.Core.Helper;
using GYM.Core.Extensions;
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
    public class DataDictionaryController : BaseAdminController
    {
        public IDataDictionaryService IDataDictionaryService;

        public DataDictionaryController(IDataDictionaryService _IDataDictionaryService)
        {
            this.IDataDictionaryService = _IDataDictionaryService;
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
        public JsonResult Add(DataDictionary entity)
        {
            ModelState.Remove("ID");
            ModelState.Remove("CreatedTime");
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("IsDelete");
            if (ModelState.IsValid)
            {
                if (IDataDictionaryService.IsExits(x => x.Value == entity.Value))
                {
                    return JResult(Core.Code.ErrorCode.system_name_already_exist, "");
                }
                entity.CreatedTime = entity.UpdatedTime = DateTime.Now;
                if (entity.Key.IsNullOrEmpty())
                {
                    entity.ID = entity.Key = Guid.NewGuid().ToString("N");
                }
                var result = IDataDictionaryService.Add(entity);
                if (result > 0)
                {
                    CacheHelper.Clear();
                }
                return JResult(result);
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
        public JsonResult Update(DataDictionary entity)
        {
            ModelState.Remove("CreatedTime");
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("IsDelete");
            if (ModelState.IsValid)
            {
                var model = IDataDictionaryService.Find(entity.ID);
                if (model == null || (model != null && model.IsDelete))
                {
                    return DataErorrJResult();
                }
                if (IDataDictionaryService.IsExits(x => x.Value == entity.Value && x.ID != entity.ID))
                {
                    return JResult(Core.Code.ErrorCode.system_key_already_exist, "");
                }
                model.Value = entity.Value;
                model.Remark = entity.Remark;
                model.Key = entity.Key;
                var result = IDataDictionaryService.Update(entity);
                if (result > 0)
                {
                    CacheHelper.Clear();
                }
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
        public ActionResult GetPageList(int pageIndex, int pageSize, string key, string value, GroupCode group)
        {
            return JResult(IDataDictionaryService.GetPageList(pageIndex, pageSize, "", group, key, value));
        }


        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Find(string ID)
        {
            return JResult(IDataDictionaryService.Find(ID));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult Delete(string ID)
        {
            var result = IDataDictionaryService.Delete(ID);
            if (result > 0)
            {
                CacheHelper.Clear();
            }
            return JResult(result);
        }



        /// <summary>
        /// 获取下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSelectItem(string value="")
        {
            return JResult(IDataDictionaryService.GetSelectList(GroupCode.Source,value));
        }
    }
}