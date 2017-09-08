using GYM.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}