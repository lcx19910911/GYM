using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Model
{
    [Table("DataDictionary")]
    public class DataDictionary:BaseEntity
    {

        /// <summary>
        /// 父级ID
        /// </summary>
        [MaxLength(32)]
        public string ParentKey { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        [Required(ErrorMessage = "选择分类")]
        public GroupCode GroupCode { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        [MaxLength(32)]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "值不能为空")]
        [MaxLength(32)]
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        [NotMapped]
        public string ParentName { get; set; }
    }
}
