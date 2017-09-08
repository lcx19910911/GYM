using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Core.Attributes
{
    public class EnumAutoMapperAttribute : Attribute
    {
        /// <summary>
        /// 来源属性
        /// </summary>
        public string SourceProperty { get; set; }

        /// <summary>
        /// 枚举类
        /// </summary>
        public Type EnumType { get; set; }

        public EnumAutoMapperAttribute(string sourceProperty, Type enumType)
        {
            SourceProperty = sourceProperty;
            EnumType = enumType;
        }
    }
}
