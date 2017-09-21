
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
    /// 部门
    /// </summary>
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        public DepartmentService()
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
        public PageList<Department> GetPageList(int pageIndex, int pageSize, string name)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.Department.Where(x => !x.IsDelete);
                if (name.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.Name.Contains(name));
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var departmentIdList = list.Select(x => x.ParentID).ToList();
                var departmentDic = db.Department.Where(x => departmentIdList.Contains(x.ID)).ToDictionary(x => x.ID);
                list.ForEach(x=>
                {
                    if (!string.IsNullOrEmpty(x.ParentID) && departmentDic.ContainsKey(x.ParentID))
                    {
                        x.ParentName = departmentDic[x.ParentID]?.Name;
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
                return db.Department.Where(x => !x.IsDelete).Select(x => new SelectItem()
                {
                    Text = x.Name,
                    Value = x.ID,
                }).ToList(); ;
            }
        }

        public List<ZTreeNode> GetZTreeChildren()
        {
         List<ZTreeNode> ztreeNodes = new List<ZTreeNode>();
            using (DbRepository db = new DbRepository())
            {
                var group = db.Department.Where(x =>!x.IsDelete).OrderByDescending(x => x.Sort).GroupBy(x => x.ParentID).ToList();
                return GetZTreeChildren("", group);
            }
        }

        /// <summary>
        /// 获取ZTree子节点
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <param name="groups">分组数据</param>
        /// <returns></returns>
        private List<ZTreeNode> GetZTreeChildren(string parentId, List<IGrouping<string, Department>> groups)
        {
            List<ZTreeNode> ztreeNodes = new List<ZTreeNode>();
            var group = groups.FirstOrDefault(x => x.Key == parentId);
            if (group != null)
            {
                ztreeNodes = group.Select(
                    x => new ZTreeNode()
                    {
                        name = x.Name,
                        value = x.ID,
                        children = GetZTreeChildren(x.ID, groups)
                    }).ToList();
            }
            return ztreeNodes;
        }
    }
}
