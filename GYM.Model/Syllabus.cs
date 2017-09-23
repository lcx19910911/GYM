
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
    /// 课程表
    /// </summary>
    [Table("Syllabus")]
    public class Syllabus : BaseEntity
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
        /// 教练员
        /// </summary>
        [Required(ErrorMessage = "教练员不能为空")]
        [MaxLength(32)]
        public string CoachID { get; set; }

        /// <summary>
        /// 教练员
        /// </summary>
        [NotMapped]
        public string CoachName { get; set; }

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
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        public System.DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public System.DateTime EndTime { get; set; }



        /// <summary>
        /// 报名人数限制
        /// </summary>
        public int PeopleLimit { get; set; } = 1;
        /// <summary>
        /// 已报名人数
        /// </summary>
        public int JoinCount { get; set; } = 0;


    }
}
