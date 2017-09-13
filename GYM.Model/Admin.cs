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

        [Display(Name = "账号"), MaxLength(32)]
        public string Account { get; set; }

        [Display(Name = "密码"), MaxLength(32)]
        public string Password { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public AdminCode Type { get; set; } = AdminCode.Coach;

        [NotMapped]
        public string TypeStr { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像"), MaxLength(512)]
        public string HeadImgUrl { get; set; }
      
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public SexCode Sex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码"),MaxLength(32)]
        public string Mobile { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Display(Name = "真实姓名"), MaxLength(32)]
        public string RealName { get; set; }


        /// <summary>
        /// 教练id
        /// </summary>
        [MaxLength(32)]
        public string CoachID { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public float Discount { get; set; } = 1;
    }

}
