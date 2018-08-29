using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ControllerExt;

namespace Web.Controllers
{
    public class DataVerifyController : PowerController
    {
        string[] menusAction = { "TxtIndex" };
        protected override string[] MenusAction
        {
            get { throw new NotImplementedException(); }
        }
        // GET: DataVerify
        public ActionResult TxtIndex()
        {
            if (!CanRead)
                return GotoErrorPage(CannotReadText);
            return View();
        }

        
    }
}