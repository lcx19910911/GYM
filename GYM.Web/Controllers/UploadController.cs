
using GYM.Core.Extensions;
using GYM.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM.Web.Controllers
{
    [AllowAnonymous]
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult UploadImage(string mark)
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file != null)
            {
                string path = UploadHelper.Save(file, mark);
                return Content(path);
            }
            else
                return Content("");
        }

        // GET: Upload
        public ActionResult UploadFile(string mark)
        {
            var fileList = Request.Files;
            string name = "";
            string path = UploadHelper.Save(Request.Files[0], mark, out name);
            return Content(new { Name = name, Path = path }.ToJson());

        }
    }
}