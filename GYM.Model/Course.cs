
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
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public string Remark { get; set; }
    }
}
