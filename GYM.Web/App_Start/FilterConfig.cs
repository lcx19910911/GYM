using GYM.Web.Framework.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GYM.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {         
            filters.Add(new TimerAttribute());
            filters.Add(new ExceptionFilterAttribute());     

        }
    }
}
