using Manage.IBLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.ControllerExt
{
    public abstract class BasicController : Controller
    {
        protected UserDto userDto
        {
            get{

                return Session.Contents["user"] as UserDto;
            }
            set
            {
                HttpContext.Session["user"] = value;
            }
        }
        
        protected ActionResult GotoErrorPage(string message="您无权限查看此页，请联系管理员开通")
        {
            ViewBag.Message = message;
            return View("Error");
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //如果没有登录或者过期时间到了，就自动回到登录页
            if (userDto == null)
            {
                Response.Redirect("/Login/Index", false);
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}