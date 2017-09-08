namespace GYM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 管理员
    /// </summary>
    [Table("Admin")]
    public partial class Admin : BaseEntity
    {

        [Display(Name = "账号"), Column("Account", TypeName = "varchar"), MaxLength(32)]
        public string Account { get; set; }

        [Display(Name = "密码"), Column("Password", TypeName = "varchar"), MaxLength(32)]
        public string Password { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public AdminType Type { get; set; } = AdminType.Coach;

        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称"), Column("NickName", TypeName = "varchar"), MaxLength(32)]
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像"), Column("HeadImgUrl", TypeName = "varchar"), MaxLength(512)]
        public string HeadImgUrl { get; set; }
      
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        [Column("Sex", TypeName = "int")]
        public int Sex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码"), Column("MobilePhone", TypeName = "varchar"),MaxLength(32)]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Display(Name = "真实姓名"), Column("RealName", TypeName = "varchar"), MaxLength(32)]
        public string RealName { get; set; }
        
    }

}
