using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Model
{
    public class BaseEntity
    {
        [Key]
        [Column("ID", TypeName = "char"), MaxLength(32)]
        public string ID { get; set; }


        public bool IsDelete { get; set; } = false;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Required]
        public System.DateTime CreatedTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [Required]
        public System.DateTime UpdatedTime { get; set; }
    }
}
