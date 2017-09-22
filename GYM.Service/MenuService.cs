
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
using GYM.Domain;

namespace GYM.Service
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuService : BaseService<Menu>, IMenuService
    {
        public MenuService()
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
        public PageList<Menu> GetPageList(int pageIndex, int pageSize, string name)
        {
            using (DbRepository db = new DbRepository())
            {
                var query = db.Menu.Where(x => !x.IsDelete);
                if (name.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.Name.Contains(name));
                }
                var count = query.Count();
                var list = query.OrderByDescending(x => x.Sort).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                list.ForEach(x =>
                {
                });

                return CreatePageList(list, pageIndex, pageSize, count);

            }
        }

        /// <summary>
        /// 获取ZTree子节点
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <param name="groups">分组数据</param>
        /// <returns></returns>
        public List<ZTreeNode> GetZTreeChildren()
        {
            List<ZTreeNode> ztreeNodes = new List<ZTreeNode>();
            using (DbRepository db = new DbRepository())
            {
                var group = db.Menu.AsQueryable().Where(x => !x.IsDelete).AsNoTracking().OrderByDescending(x => x.Sort).GroupBy(x => x.ParentID).ToList();

               return GetZTreeChildren(null, group);
            }
        }


        /// <summary>
        /// 获取ZTree子节点
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <param name="groups">分组数据</param>
        /// <returns></returns>
        private List<ZTreeNode> GetZTreeChildren(string parentId, List<IGrouping<string, Menu>> groups)
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

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<SelectItem> GetSelectList()
        {
            using (DbRepository db = new DbRepository())
            {
                return db.Menu.Where(x => !x.IsDelete).OrderByDescending(x=>x.Sort).Select(x => new SelectItem()
                {
                    Text = x.Name,
                    Value = x.ID,
                }).ToList(); ;
            }
        }


        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> GetUserMenu(string parentId)
        {
            List<MenuItem> menuList = new List<MenuItem>();
            using (DbRepository db = new DbRepository())
            {
                var group = db.Menu.Where(x => !x.IsDelete).AsQueryable().AsNoTracking().OrderByDescending(x => x.Sort).GroupBy(x => x.ParentID).ToList();
                return Get_UserMenu(parentId, group);
            }
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        private List<MenuItem> Get_UserMenu(string parentId, List<IGrouping<string, Menu>> groups)
        {
            List<MenuItem> menuList = new List<MenuItem>();
            var group = groups.FirstOrDefault(x => x.Key == parentId);
            if (group != null)
            {
                menuList = group.OrderByDescending(x => x.Sort).Select(
                    x =>
                    {
                        if (this.Client.LoginAdmin.IsSuperAdmin || (this.Client.LoginAdmin.MenuIDStr.Contains(x.ID)))
                        {
                            return new MenuItem()
                            {
                                ClassName = x.ClassName,
                                Name = x.Name,
                                Link = x.Link,
                                Children = Get_UserMenu(x.ID, groups)
                            };
                        }
                        else
                            return null;

                    }).ToList();
            }
            return menuList;
        }




        /// <summary>
        /// 根据页面Flag值判断是否有权限
        /// </summary>
        /// <param name="menuFlag">页面flag值</param>
        /// <param name="url">相对路径</param>
        /// <returns></returns>
        public bool IsHavePage(string url)
        {
            using (DbRepository db = new DbRepository())
            {
                var menu = db.Menu.Where(x => !x.IsDelete).AsQueryable().Where(x => !string.IsNullOrEmpty(x.Link) && x.Link.Contains(url)).FirstOrDefault();
                if (menu == null)
                {
                    return true;
                }

                if (this.Client.LoginAdmin.IsSuperAdmin||this.Client.LoginAdmin.MenuIDStr.Contains(menu.ID))
                {
                    return true;
                }
                else
                    return false;
            }
        }

    }
}
