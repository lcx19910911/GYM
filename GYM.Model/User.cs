namespace GYM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 微信用户
    /// </summary>
    [Table("User")]
    public partial class User : BaseEntity
    {
        [MaxLength(32)]
        public string OpenId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称"), MaxLength(32)]
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像"), MaxLength(512)]
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        [Display(Name = "国家"), MaxLength(32)]
        public string Country { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [Display(Name = "省份"), MaxLength(32)]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [Display(Name = "城市"), MaxLength(32)]
        public string City { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public SexCode Sex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码"),MaxLength(32)]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Display(Name = "真实姓名"), MaxLength(32)]
        public string RealName { get; set; }

        /// <summary>
        /// 是否是会员
        /// </summary>
        public bool IsMember { get; set; } = false;

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal Balance { get; set; } = 0;
        /// <summary>
        /// 总充值
        /// </summary>
        public decimal TotalRecharge { get; set; } = 0;
    }
}
