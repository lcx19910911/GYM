using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYM.Core.Config
{
    public static class UploadPathConfig
    {
        /// <summary>
        /// 获取保存路径
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        public static string GetUploadPath(string mark)
        {
            switch (mark)
            {
                case "scratchcard":
                    return ScratchCardUploadPath();
                default:
                    return OtherUploadPath();
            }
        }

        /// <summary>
        /// 获取商品上传文件保存路径
        /// </summary>
        /// <returns></returns>
        private static string ScratchCardUploadPath()
        {
            return "/Upload/ScratchCard/";
        }

        /// <summary>
        /// 获取客户其它类型文件上传的保存路径
        /// </summary>
        /// <returns></returns>
        private static string OtherUploadPath()
        {

            return "/Upload/Other/";
        }
    }
}