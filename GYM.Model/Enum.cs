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
        [Description("教练")]
        Coach = 1,

        [Description("管理员")]
        Admin = 2,

        [Description("超级管理员")]
        SuperAdmin = 3
    }

    /// <summary>
    /// 后台用户类型
    /// </summary>
    public enum SexCode
    {
        [Description("未知")]
        UnKnow = 0,

        [Description("男")]
        Man = 1,

        [Description("女")]
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
