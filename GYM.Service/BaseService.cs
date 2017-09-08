using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Linq.Expressions;
using GYM.IService;
using GYM.Model;
using GYM.Core.Model;
using GYM.Core.Code;
using GYM.Core.Extensions;
using GYM.DB;
using GYM.Core.Core;

namespace GYM.Service
{
    public class BaseService<T>: IBaseService<T> where T:BaseEntity
    {
        /// <summary>
        /// 请求context
        /// </summary>
        public HttpContext ContextCurrent { get; set; }

        public WebClient Client
        {
            get
            {
                return new WebClient(ContextCurrent);
            }
        }

        public WebResult<Y> Result<Y>(Y model) 
        {
            return Result<Y>(model, ErrorCode.sys_success);
        }

        public WebResult<Y> Result<Y>(Y model, ErrorCode code)
        {
            return new WebResult<Y> { Code = code, Result = model, Append = code.GetDescription() };
        }

        public WebResult<Y> Result<Y>(Y model, string append)
        {
            return new WebResult<Y> { Code = ErrorCode.sys_fail, Result = model, Append = append };
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="source">实体</param>
        /// <returns>影响条数</returns>
        public int Add(T source)
        {
            using (DbRepository db = new DbRepository())
            {
                var addEntity = source.AutoMap<T>();
                db.Entry(addEntity).State = System.Data.Entity.EntityState.Added;
                if (db.SaveChanges() > 0)
                    return 1;
                else
                    return 0;
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="IDs">ID，多个id用逗号分隔</param>
        /// <returns>影响条数</returns>
        public int Delete(string IDs)
        {
            using (DbRepository db = new DbRepository())
            {
                //按逗号分隔符分隔开得到ID列表
                var IDArray = IDs.Split(',');
                //遍历ID列表逐个删除
                foreach (var ID in IDArray)
                {
                    DbSet<T> dbSet = db.Set<T>();
                    var entity = dbSet.Find(ID.GetInt());
                    if (entity != null)
                    {
                        entity.IsDelete = true;
                    }
                }
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响条数</returns>
        public int Delete(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                return 0;
            using (DbRepository db = new DbRepository())
            {
                DbSet<T> dbSet = db.Set<T>();
                var list = dbSet.Where(predicate).ToList();
                list.ForEach(entity =>
                {
                    entity.IsDelete = true;
                });
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 获取用户所有的会议
        /// </summary>
        /// <returns></returns>
        public bool IsExits(Expression<Func<T, bool>> predicate)
        {
            using (DbRepository db = new DbRepository())
            {
                DbSet<T> dbSet = db.Set<T>();
                return  dbSet.Where(predicate).Any();
            }
        }

        public T Find(object ID)
        {
            using (DbRepository db = new DbRepository())
            {
                DbSet<T> dbSet = db.Set<T>();
                return dbSet.Find(ID);
            }
        }

        public T Find(Expression<Func<T, bool>> predicate) 
        {
            using (DbRepository db = new DbRepository())
            {
                DbSet<T> dbSet = db.Set<T>();
                return dbSet.Where(x => !x.IsDelete).Where(predicate).FirstOrDefault();
            }
        }


        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> predicate) 
        {
            using (DbRepository db = new DbRepository())
            {
                DbSet<T> dbSet = db.Set<T>();
                return dbSet.Where(x => x.IsDelete).Where(predicate).Count();
            }
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            using (DbRepository db = new DbRepository())
            {
                DbSet<T> dbSet = db.Set<T>();
                return dbSet.ToList();
            }
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> predicate=null,int takeCount=0)
        {
            using (DbRepository db = new DbRepository())
            {
                DbSet<T> dbSet = db.Set<T>();
                var query = dbSet.Where(x => !x.IsDelete);
                if (predicate != null)
                {
                    if(takeCount==0)
                        return query.Where(predicate).ToList();
                    else//.OrderByDescending(x => x.CreatedTime)
                        return query.Where(predicate).Take(takeCount).ToList();
                }
                else
                {
                    if (takeCount == 0)
                        return query.ToList();
                    else
                        return query.Take(takeCount).ToList();
                }
            }
        }       

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="source">实体</param>
        public int Update(T source,object  ID=null)
        {
            using (DbRepository db = new DbRepository())
            {
                DbSet<T> dbSet = db.Set<T>();
                var sourceEntity =ID==null?dbSet.Find(source.ID): dbSet.Find(ID);
                if (sourceEntity != null)
                {
                    source.AutoMap<T>(sourceEntity);
                }
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 创建分页列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">查询对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageList<T> CreatePageList(IQueryable<T> queryable, int pageIndex, int pageSize) 
        {
            int recordCount = 0;
            recordCount = queryable.Count();
            List<T> list = queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PageList<T>(list, pageIndex, pageSize, recordCount);

        }

        /// <summary>
        /// 创建分页列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">查询对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageList<T> CreatePageList(List<T> list, int pageIndex, int pageSize, int recordCount)
        {
             return new PageList<T>(list, pageIndex, pageSize, recordCount);
        }

        /// <summary>
        /// 创建分页列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">查询对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> CreateList(IQueryable<T> queryable, int pageIndex, int pageSize) 
        {
           List<T> list = queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
           return list;
        }
    }
}
