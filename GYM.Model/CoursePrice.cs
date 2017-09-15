
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
    /// 课程价格
    /// </summary>
    [Table("CoursePrice")]
    public class CoursePrice : BaseEntity
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        [Required(ErrorMessage = "课程名称不能为空")]
        [MaxLength(32)]
        public string CourseID { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [NotMapped]
        public string CourseName { get; set; }

        /// <summary>
        /// 单项名称
        /// </summary>
        [Required(ErrorMessage = "单项名称不能为空")]
        [MaxLength(32)]
        public string Name { get; set; }


        /// <summary>
        /// 原价
        /// </summary>
        [MaxLength(512)]
        public decimal Price { get; set; }


        /// <summary>
        /// 会员折扣价
        /// </summary>
        [MaxLength(512)]
        public decimal DiscountPrice { get; set; }

    }
}
