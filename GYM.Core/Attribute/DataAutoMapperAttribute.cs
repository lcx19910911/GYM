using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Core.Attributes
{
    public class DataAutoMapperAttribute : Attribute
    {
        /// <summary>
        /// 来源属性
        /// </summary>
        public string SourceMatchProperty { get; set; }
        /// <summary>
        /// 目标表名
        /// </summary>
        public Type TargetEntityType { get; set; }
        /// <summary>
        /// 目标匹配属性名
        /// </summary>
        public string TargetMatchProperty { get; set; }
        /// <summary>
        /// 目标值属性
        /// </summary>
        public string TargetValueProperty { get; set; }


        /// <summary>
        /// 自动映射特性类构造函数
        /// </summary>
        /// <param name="sourceMatchProperty">来源属性</param>
        /// <param name="targetEntityType">目标表名</param>
        /// <param name="targetMatchProperty">目标属性名</param>
        /// <param name="targetValueProperty">目标值属性</param>
        public DataAutoMapperAttribute(string sourceMatchProperty, Type targetEntityType, string targetMatchProperty, string targetValueProperty)
        {
            this.SourceMatchProperty = sourceMatchProperty;
            this.TargetEntityType = targetEntityType;
            this.TargetMatchProperty = targetMatchProperty;
            this.TargetValueProperty = targetValueProperty;
        }
    }
}
