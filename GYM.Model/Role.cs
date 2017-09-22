
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Model
{
    [Table("Role")]
    public class Role : BaseEntity
    {

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [MaxLength(32)]
        public string Name { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public float Discount { get; set; } = 1;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        [Column("Remark", TypeName = "varchar")]
        public string Remark { get; set; }


        /// <summary>
        /// 页面权限
        /// </summary>
        [Column("MenuIDStr", TypeName = "text")]
        public string MenuIDStr { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        [Column("OperateStr", TypeName = "text")]
        public string OperateStr { get; set; }
    }
}
