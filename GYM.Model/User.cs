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

        [Column("OpenId", TypeName = "char"), MaxLength(32)]
        public string OpenId { get; set; }
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
        /// 国家
        /// </summary>
        [Display(Name = "国家"), Column("Country", TypeName = "varchar"), MaxLength(32)]
        public string Country { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [Display(Name = "省份"), Column("Province", TypeName = "varchar"), MaxLength(32)]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [Display(Name = "城市"), Column("City", TypeName = "varchar"), MaxLength(32)]
        public string City { get; set; }
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
