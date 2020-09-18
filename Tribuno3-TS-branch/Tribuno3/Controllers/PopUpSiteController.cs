using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tribuno3.Controllers
{
    public class PopUpSiteController : Controller
    {
        // GET: PopUpSite
        public ActionResult Index()
        {
            return PartialView("_PopUpDecisao");
        }

        public ActionResult _PopUpDecisao()
        {
            return PartialView();
        }
    }
}