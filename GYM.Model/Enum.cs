using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Model
{
    /// <summary>
    /// 后台用户类型
    /// </summary>
    public enum AdminCode
    {
        Coach = 1,

        Admin = 2,

        SuperAdmin = 3
    }

    /// <summary>
    /// 后台用户类型
    /// </summary>
    public enum SexCode
    {
        UnKnow = 0,

        Man = 1,

        Women = 2
    }

    /// <summary>
    /// 字典分组
    /// </summary>
    public enum GroupCode
    {
        None = 0,
        /// <summary>
        /// 地区
        /// </summary>
        [Description("地区")]
        Area = 1,
 
    }
}
