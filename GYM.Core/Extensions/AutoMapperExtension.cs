
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GYM.Core.Model;
using GYM.Model;

namespace GYM.Core.Core
{
    public static class AutoMapperExtension
    {



        /// <summary>
        /// 自动映射
        /// </summary>
        /// <typeparam name="TSource">目标类型</typeparam>
        /// <param name="source">原始对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns>映射结果</returns>
        public static TSource AutoMap<TSource>(this TSource source, TSource destination) where TSource : BaseEntity
        {

            List<TSource> sourceList = new List<TSource>() { source };
            List<TSource> destinationList = new List<TSource>() { destination };

            return sourceList.AutoMap<TSource>(destinationList).First();
        }

        /// <summary>
        /// 自动映射
        /// </summary>
        /// <typeparam name="TSource">目标类型</typeparam>
        /// <param name="source">原始对象</param>
        /// <returns>映射结果</returns>
        public static TSource AutoMap<TSource>(this TSource source) where TSource : BaseEntity
        {
            List<TSource> list = new List<TSource>() { source };
            return list.AutoMap<TSource>().First();
        }

        /// <summary>
        /// 自动映射列表
        /// </summary>
        /// <typeparam name="TSource">原始类型</typeparam>
        /// <typeparam name="TSource">目标类型</typeparam>
        /// <param name="sourceList">原始列表</param>
        /// <returns></returns>
        public static List<TSource> AutoMap<TSource>(this List<TSource> sourceList, List<TSource> destinationList = null) where TSource : BaseEntity
        {
            Dictionary<object, TSource> targetDictionary = new Dictionary<object, TSource>();
            if (destinationList != null && destinationList.Count > 0)
            {
                //如果有目标原始值时的操作

                foreach (var source in sourceList)
                {
                    //创建autoMapper映射

                    AutoMapper.Mapper.CreateMap<TSource, TSource>().BeforeMap((x, y) => {
                        var oldEntity =y as  TSource;
                        x.ID = oldEntity.ID;
                        x.IsDelete = oldEntity.IsDelete;
                        x.CreatedTime = oldEntity.CreatedTime;
                        x.UpdatedTime = DateTime.Now;
                    });

                    //使用autoMapper映射                
                    int index = sourceList.IndexOf(source);
                    if (destinationList.Count > index)
                    {
                        TSource target = AutoMapper.Mapper.Map<TSource, TSource>(source, destinationList[index]);
                        targetDictionary.Add(source, target);
                    }
                    else
                    {

                        TSource target = AutoMapper.Mapper.Map<TSource, TSource>(source);
                        targetDictionary.Add(source, target);
                    }
                }
            }
            else
            {

                foreach (var source in sourceList)
                {
                    //创建autoMapper映射
                     AutoMapper.Mapper.CreateMap<TSource, TSource>();

                    //使用autoMapper映射                
                    TSource target = AutoMapper.Mapper.Map<TSource, TSource>(source);
                    targetDictionary.Add(source, target);
                }
            }
           
            return targetDictionary.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// 自动映射分页列表
        /// </summary>
        /// <typeparam name="TSource">原始类型</typeparam>
        /// <typeparam name="TSource">目标类型</typeparam>
        /// <param name="source">原始对象</param>
        /// <returns></returns>
        public static PageList<TSource> AutoMap<TSource>(this PageList<TSource> source) where TSource : BaseEntity
        {
            PageList<TSource> list = new PageList<TSource>(source.List.AutoMap<TSource>(), source.PageIndex, source.PageSize, source.RecordCount);
            return list;
        }

    }
}
