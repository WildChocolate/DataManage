using Manage.BLLContainer;
using Manage.Common;
using Manage.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string logout="")
        {
            if (logout == "")
            {
                return View();
            }
            else if (logout == "true")
            {
                Session.Contents.Remove("user");
                return View();
            }
            else
                return new EmptyResult();
        }

        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
        

        public ActionResult CheckLogin(string username, string password)
        {
            try
            {
                var service = Container.GetService<IUserService>();
                var user = service.CheckLogin(username, password);
                if (user != null) 
                {
                    Session["user"] = user;
                    Session.Timeout = 20;
                    return Content(new { key = user.User.keyid }.ToJson());
                }
                else
                {
                    return Content(new { key = 0, message = "帐号或密码不正确" }.ToJson());
                }
            }
            catch (Exception ex)
            {
                return Content(new { key = 0, message = ex.Message }.ToJson());
            }
        }
	}
}