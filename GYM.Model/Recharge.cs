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
    /// 充值记录
    /// </summary>
    [Table("Recharge")]
    public class Recharge : BaseEntity
    {
        [Required,MaxLength(32)]
        public string UserID { get; set; }

        [NotMapped]
        public string UserName { get; set; }
        
        public decimal? BeforeBalance { get; set; }
        
        public decimal Amount { get; set; }
        
        public decimal? AfterBalance { get; set; }
        [MaxLength(32)]
        public string ThirdOrderID { get; set; }
        
        public PayType Type { get; set; } = PayType.Alipay;
        [NotMapped]
        public string TypeStr { get; set; }

        public PayState State { get; set; } = PayState.WaitPay;
        [NotMapped]
        public string StateStr { get; set; }

        /// <summary>
        /// 到账时间
        /// </summary>
        [Display(Name = "到账时间")]
        public System.DateTime? SuccessTime { get; set; }
    }

}
