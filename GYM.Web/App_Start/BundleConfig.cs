using System.Web;
using System.Web.Optimization;

namespace GYM.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region 样式

            bundles.Add(new StyleBundle("~/Content/Admin").Include(
                "~/Areas/Admin/Content/css/amazeui.css",
                "~/Areas/Admin/Scripts/tipso/css/tipso.min.css",
                "~/Areas/Admin/Content/admin.css",
                "~/Areas/Admin/Scripts/My97DatePicker/skin/WdatePicker.css",
                "~/Areas/Admin/Content/zTreeStyle/zTreeStyle.css"
                ));
            #endregion

            #region 脚本
            bundles.Add(new ScriptBundle("~/Scripts/Login").Include(
    "~/Areas/Admin/Scripts/jquery-1.10.2.js",
    "~/Areas/Admin/Scripts/amazeui.js",
    "~/Areas/Admin/Scripts/Nuoya/nuoya.core.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Admin").Include(
    "~/Areas/Admin/Scripts/jquery-1.10.2.js",
               "~/Areas/Admin/Scripts/jquery.form.js",
               "~/Areas/Admin/Scripts/amazeui.min.js",
               "~/Areas/Admin/Scripts/jquery-validation/js/jquery.validate.js",
               "~/Areas/Admin/Scripts/tipso/js/tipso.js",

               "~/Areas/Admin/Scripts/Nuoya/nuoya.core.js",
               "~/Areas/Admin/Scripts/Nuoya/nuoya.grid.js",
               "~/Areas/Admin/Scripts/Nuoya/nuoya.form.js",
               "~/Areas/Admin/Scripts/Nuoya/nuoya.other.js",

               "~/Areas/Admin/Scripts/My97DatePicker/WdatePicker.js",
               "~/Areas/Admin/Scripts/My97DatePicker/config.js",
               "~/Areas/Admin/Scripts/My97DatePicker/lang/zh-cn.js",
               "~/Areas/Admin/Scripts/My97DatePicker/calendar.js",

               "~/Areas/Admin/Scripts/file_upload_plug-in.js",
               "~/Areas/Admin/Scripts/jquery.ztree.all-3.5.min.js",
               "~/Areas/Admin/Scripts/ztree-select.js"
               ));

            #endregion
            BundleTable.EnableOptimizations = false;
        }
    }
}
