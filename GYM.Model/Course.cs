﻿
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
    /// 课程
    /// </summary>
    [Table("Course")]
    public class Course : BaseEntity
    {

        /// <summary>
        /// 推荐人姓名
        /// </summary>
        [Required(ErrorMessage = "课程不能为空")]
        [MaxLength(32)]
        public string Name { get; set; }

        /// <summary>
        /// 课程分类
        /// </summary>
        [Required(ErrorMessage = "课程分类不能为空")]
        [MaxLength(256)]
        public string CourseCategoryIDS { get; set; }

        /// <summary>
        /// 课程分类
        /// </summary>
        [NotMapped]
        public string CourseCategoryNameS { get; set; }


        /// <summary>
        /// 训练结果
        /// </summary>
        [Display(Name = "训练结果")]
        public string TrainResult { get; set; }
        /// <summary>
        /// 适合人群
        /// </summary>
        [Display(Name = "适合人群")]
        public string FitPeople { get; set; }
        /// <summary>
        /// 问题和回答
        /// </summary>
        [Display(Name = "问题和回答")]
        public string FAQ { get; set; }


        /// <summary>
        /// 图片地址集合 ，隔开
        /// </summary>
        [MaxLength(1024)]
        [Display(Name = "图片地址集合 ，隔开")]
        public string Pictures { get; set; }
        

        /// <summary>
        /// 原价
        /// </summary>
        public decimal Price { get; set; }


        /// <summary>
        /// 会员折扣价
        /// </summary>
        public decimal DiscountPrice { get; set; }
    }
}
