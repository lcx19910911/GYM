
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Model
{
    /// <summary>
    /// 教练员
    /// </summary>
    [Table("Coach")]
    public class Coach : BaseEntity
    {

        /// <summary>
        /// 教练员姓名
        /// </summary>
        [Required(ErrorMessage = "教练员姓名不能为空")]
        [MaxLength(32)]
        [Column("Name", TypeName = "varchar")]
        public string Name { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [Display(Name = "身份证号码")]
        [MaxLength(32)]
        [Required(ErrorMessage = "身份证号码不能为空")]
        [Column("IDCard", TypeName = "varchar")]
        [RegularExpression(@"(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$)", ErrorMessage = "身份证号码格式不正确")]
        public string IDCard { get; set; }

        /// <summary>
        /// 身份证地址
        /// </summary>
        [MaxLength(512)]
        [Display(Name = "地址")]
        [Column("Address", TypeName = "varchar")]
        public string Address { get; set; }


        /// <summary>
        /// 性别
        /// </summary>        
        public SexCode Sex { get; set; }


        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [MaxLength(11)]
        [RegularExpression(@"((\d{11})$)", ErrorMessage = "手机格式不正确")]
        public string Mobile { get; set; }

        /// <summary>
        /// 底薪
        /// </summary>
        public decimal BasicSalary { get; set; }
      

        /// <summary>
        /// 教龄
        /// </summary>
        public int? TrainYears { get; set; }


        /// <summary>
        /// 入职时间
        /// </summary>
        public Nullable<DateTime> EntryDate { get; set; }



        /// <summary>
        /// 介绍
        /// </summary>
        [MaxLength(512)]
        [Display(Name = "介绍")]
        public string Introduce { get; set; }

        /// <summary>
        /// 图片地址集合 ，隔开
        /// </summary>
        [MaxLength(1024)]
        [Display(Name = "图片地址集合 ，隔开")]
        public string Pictures { get; set; }


        /// <summary>
        /// 所属门店
        /// </summary>
        [Required(ErrorMessage = "所属门店不能为空")]
        [MaxLength(32)]
        public string StoreID { get; set; }

        /// <summary>
        /// 所属门店
        /// </summary>
        [NotMapped]
        public string StoreName { get; set; }

        /// <summary>
        /// 离职时间
        /// </summary>
        [Display(Name = "离职时间")]
        public Nullable<DateTime> QuitTime { get; set; }
        /// <summary>
        /// 是否离职
        /// </summary>
        [Display(Name = "是否离职")]
        public bool IsQuit { get; set; } = false;
    }
}
