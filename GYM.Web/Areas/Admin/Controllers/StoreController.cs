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
    public class StoreController : BaseAdminController
    {
        public IStoreService IStoreService;
        public IDataDictionaryService IDataDictionaryService;

        public StoreController(IStoreService _IStoreService, IDataDictionaryService _IDataDictionaryService)
        {
            this.IStoreService = _IStoreService;
            this.IDataDictionaryService = _IDataDictionaryService;
        }
        public ViewResult Index()
        {
            return View(IDataDictionaryService.GetSelectList(GroupCode.Area,""));
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult Add(Store entity)
        {
            ModelState.Remove("ID");
            ModelState.Remove("CreatedTime");
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("IsDelete");
            if (ModelState.IsValid)
            {
                if (IStoreService.IsExits(x => x.Name == entity.Name && x.CityCode == entity.CityCode))
                {
                    return JResult(Core.Code.ErrorCode.store_city__namealready_exist, "");
                }
                entity.CreatedTime = entity.UpdatedTime = DateTime.Now;
                var result = IStoreService.Add(entity);
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
        public JsonResult Update(Store entity)
        {
            ModelState.Remove("CreatedTime");
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("IsDelete");
            if (ModelState.IsValid)
            {
                var model = IStoreService.Find(entity.ID);
                if (model == null || (model != null && model.IsDelete))
                {
                    return DataErorrJResult();
                }
                
                if (IStoreService.IsExits(x => x.Name == entity.Name && x.CityCode == entity.CityCode&&x.ID!=entity.ID))
                {
                    return JResult(Core.Code.ErrorCode.store_city__namealready_exist, "");
                }

                model.Name = entity.Name;
                model.CityCode = entity.CityCode;
                model.Address = entity.Address;
                model.Introduce = entity.Introduce;
                model.Notice = entity.Notice;
                model.Pictures = entity.Pictures;
                var result = IStoreService.Update(model);
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
        public ActionResult GetPageList(int pageIndex, int pageSize, string areaCode, string name)
        {
            return JResult(IStoreService.GetPageList(pageIndex, pageSize, areaCode, name));
        }


        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Find(string ID)
        {
            return JResult(IStoreService.Find(ID));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult Delete(string ID)
        {
            return JResult(IStoreService.Delete(ID));
        }



        /// <summary>
        /// 获取地区下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSelectItem()
        {
            return JResult(IStoreService.GetSelectList());
        }
    }
}