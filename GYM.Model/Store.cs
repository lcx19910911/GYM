
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
    /// 门店
    /// </summary>
    [Table("Store")]
    public class Store : BaseEntity
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        [Required(ErrorMessage = "门店名称不能为空")]
        [MaxLength(32)]
        [Display(Name= "门店名称")]
        public string Name { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        [MaxLength(6)]
        public string CityCode { get; set; }

        /// <summary>
        /// 城市称
        /// </summary>
        [NotMapped]
        public string CityName { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(128)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [MaxLength(128)]
        [Display(Name = "经度")]
        public string Longitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [MaxLength(128)]
        [Display(Name = "经度")]
        public string Latitude { get; set; }


        /// <summary>
        /// 介绍
        /// </summary>
        [MaxLength(512)]
        [Display(Name = "介绍")]
        public string Introduce { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "注意事项")]
        public string Notice { get; set; }


        /// <summary>
        /// 图片地址集合 ，隔开
        /// </summary>
        [MaxLength(1024)]
        [Display(Name = "图片地址集合 ，隔开")]
        public string Pictures { get; set; }
    }
}
