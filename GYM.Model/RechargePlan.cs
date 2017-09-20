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
    /// 充值方案
    /// </summary>
    [Table("RechargePlan")]
    public class RechargePlan : BaseEntity
    {

        [Required,MaxLength(32)]
        public string Name { get; set; }
             
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal Money { get; set; }
        
        /// <summary>
        /// 到账数值
        /// </summary>
        public decimal RealRecharge { get; set; }
    }

}
