
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
    /// 用户私教
    /// </summary>
    [Table("UserCourse")]
    public class UserCourse : BaseEntity
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Required(ErrorMessage = "用户不能为空")]
        [MaxLength(32)]
        public string UserID { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [NotMapped]
        public string UserName { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        [Required(ErrorMessage = "课程不能为空")]
        [MaxLength(32)]
        public string CourseID { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        [NotMapped]
        public string CourseName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        public System.DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public System.DateTime EndTime { get; set; }


        public bool HadSign { get; set; } = false;

        /// <summary>
        /// 签到时间
        /// </summary>
        [Display(Name = "开始时间")]
        public System.DateTime? SignTime { get; set; }

    }
}
