
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
    /// 用户关系
    /// </summary>
    [Table("UserRelation")]
    public class UserRelation : BaseEntity
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
        /// 关系用户
        /// </summary>
        [Required(ErrorMessage = "关系用户不能为空")]
        [MaxLength(32)]
        public string RelationUserID { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [NotMapped]
        public string RelationUserName { get; set; }

        /// <summary>
        /// 关联关系
        /// </summary>
        [Required(ErrorMessage = "关系用户不能为空")]
        [MaxLength(32)]
        public string RelationID { get; set; }

        /// <summary>
        /// 关联关系
        /// </summary>
        [NotMapped]
        public string RelationName { get; set; }

        [MaxLength(128)]
        public string Remark { get; set; }

    }
}
