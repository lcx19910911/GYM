using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Linq.Expressions;
using System.Net.Http;
using GYM.Core.Model;

namespace GYM.IService
{
    public interface IBaseService<T>
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="source">实体</param>
        /// <returns>影响条数</returns>
        int Add(T source);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="IDs">ID，多个id用逗号分隔</param>
        /// <returns>影响条数</returns>
        int Delete(string IDs);

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响条数</returns>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        bool IsExits(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 查找单个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 查找单个
        /// </summary>
        /// <param name="ID">主键值</param>
        /// <returns></returns>
        T Find(object ID);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();
        

        /// <summary>
        /// 查找集合
        /// </summary>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> predicate, int takeCount = 0);


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="source">实体</param>
        int Update(T source,object ID=null);


        /// <summary>
        /// 创建分页列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">查询对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageList<T> CreatePageList(IQueryable<T> queryable, int pageIndex, int pageSize);

        /// <summary>
        /// 创建分页列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">查询对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageList<T> CreatePageList(List<T> list, int pageIndex, int pageSize, int recordCount);

        /// <summary>
        /// 创建分页列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">查询对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<T> CreateList(IQueryable<T> queryable, int pageIndex, int pageSize);
    }
}
