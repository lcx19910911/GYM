
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
    /// 配置类
    /// </summary>
    public class DataDictionaryService : BaseService<DataDictionary>, IDataDictionaryService
    {
        public DataDictionaryService()
        {
            base.ContextCurrent = HttpContext.Current;
        }

        string dictionaryKey = CacheHelper.RenderKey(Params.Cache_Prefix_Key, "DataDictionary");


        public Dictionary<GroupCode, Dictionary<string, DataDictionary>> CacheDic()
        {

            return CacheHelper.Get<Dictionary<GroupCode, Dictionary<string, DataDictionary>>>(dictionaryKey, () =>
            {
                var listss = new List<string>();
                using (var db = new DbRepository())
                {
                    var dic = db.DataDictionary.GroupBy(x => x.GroupCode).ToDictionary(x => x.Key, x => x.ToList());
                    return dic.ToDictionary(x => x.Key,
                        x => {
                            var returnDic = new Dictionary<string, DataDictionary>();
                            x.Value.ForEach(y =>
                            {
                                if (!returnDic.ContainsKey(y.Key))
                                {
                                    returnDic.Add(y.Key, y);
                                }
                            });
                            return returnDic;
                        });
                }
            });

        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="title">标题 - 搜索项</param>
        /// <returns></returns>
        public PageList<DataDictionary> GetPageList(int pageIndex, int pageSize, string parentKey, GroupCode code, string name, string value)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.DataDictionary.Where(x => !x.IsDelete && x.GroupCode == code);
                if (parentKey.IsNullOrEmpty())
                {
                    query = query.Where(x => string.IsNullOrEmpty(x.ParentKey));
                }
                else if(parentKey!="-1")
                {
                    query = query.Where(x => x.ParentKey.Equals(parentKey.Trim()));
                }
                if (name.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.Key.Contains(name));
                }
                if (value.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.Value.Contains(value));
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                list.ForEach(x =>
                {

                });

                return CreatePageList(list, pageIndex, pageSize, count);

            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="value">地区编码</param>
        /// <returns></returns>
        public List<SelectItem> GetSelectList(GroupCode code,string value)
        {
            var areas = CacheDic()[code].Values.OrderByDescending(x => x.Sort).ToList().AsQueryable();
            if (!string.IsNullOrEmpty(value) && !value.Equals("-1"))
                areas = areas.Where(x => !string.IsNullOrEmpty(x.ParentKey) && x.ParentKey.Trim().Equals(value));
            else
                areas = areas.Where(_ => string.IsNullOrEmpty(_.ParentKey));
            var alist = areas.ToList();
            var list = alist.Select(x => new SelectItem() { Value = x.Key, Text = x.Value }).ToList();
            list.Insert(0, new SelectItem() { Value = "-1", Text = "点击选择..." });
            return list;
        }
    }
}
