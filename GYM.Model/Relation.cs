
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
    /// 关系
    /// </summary>
    [Table("Relation")]
    public class Relation : BaseEntity
    {
        /// <summary>
        /// 关系名称
        /// </summary>
        [Required(ErrorMessage = "关系名称不能为空")]
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
