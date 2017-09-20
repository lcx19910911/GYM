
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
    /// 管理员邀请码
    /// </summary>
    [Table("AdminInvite")]
    public class AdminInvite : BaseEntity
    {
        /// <summary>
        /// 邀请码
        /// </summary>
        [Required(ErrorMessage = "邀请码不能为空")]
        [MaxLength(6)]
        public string InviteNO { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public float Discount { get; set; } = 1;

        /// <summary>
        /// 管理员
        /// </summary>
        [Required(ErrorMessage = "管理员不能为空")]
        [MaxLength(32)]
        public string AdminID { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        [NotMapped]
        public string AdminName { get; set; }

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

        public bool IsUsed { get; set; } = false;
    }
}
