using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ControllerExt
{
    //有些控制器不须要权限控件，比如主页控制，只要用用户能登录进去就可以看到
    public abstract class PowerController:BasicController
    {
        /// <summary>
        /// 与当前控制器关联的菜单的主action名，相当于入口点方法，一个控制器可能包含对多个菜单的操作，用来在后面确定与当前控制器关联的菜单，以做权限检查
        /// </summary>
        protected abstract string[] MenusAction
        {
            get;
        }
        string tipsFormat = "您没有{0}权限，请联系管理员开通";
        protected virtual string CannotReadText
        {
            get { return string.Format(tipsFormat,"浏览"); }
        }
        protected virtual string CannotUpateText
        {
            get { return string.Format(tipsFormat, "修改"); }
        }
        protected virtual string CannotDeleteText
        {
            get { return string.Format(tipsFormat, "删除"); }
        }
        protected virtual string CannotAddText
        {
            get { return string.Format(tipsFormat, "添加"); }
        }
        protected bool CanRead
        {
            get { return (bool)(ViewBag.CanRead??false); }
            private set
            {
                ViewBag.CanRead = value;
            }
        }
        protected bool CanUpdated
        {
            get { return (bool)(ViewBag.CanUpdated??false); }
            private set
            {
                ViewBag.CanUpdated = value;
            }
        }
        protected bool CanDeleted
        {
            get { return (bool)(ViewBag.CanDeleted&&false); }
            private set
            {
                ViewBag.CanDeleted = value;
            }
        }
        protected bool CanCreated
        {
            get { return (bool)(ViewBag.CanCreated&&false); }
            private set
            {
                ViewBag.CanCreated = value;
            }
        }
        /// <summary>
        /// 一个控制器有多个菜单，要把当前菜单记录下来
        /// </summary>

        string MenuAction
        {
            get { return Session["action"] + string.Empty; }
            set { Session["action"] = value; }
        }
        
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //在第一次调用主action的时候，就是打开菜单的时候，对权限进行检查
            var method = filterContext.HttpContext.Request.HttpMethod;
            if (userDto != null)
            {
                //当前action 为包含在入口点中，并且为get方法，说明点击了菜单项
                if (MenusAction.Contains(filterContext.ActionDescriptor.ActionName) && method.ToUpper()=="GET")
                {
                    MenuAction = filterContext.ActionDescriptor.ActionName;
                }
                var p = userDto.GetPower(filterContext.Controller, MenuAction);
                CanRead = p.C_CanRead;
                CanUpdated = p.C_CanUpdated;
                CanDeleted = p.C_CanDelted;
                CanCreated = p.C_CanCreated;
            }
            //if (MenusAction.Contains(filterContext.ActionDescriptor.ActionName))
            //{
            //    MenuAction = filterContext.ActionDescriptor.ActionName;
            //    if (userDto != null)
            //    {
            //        var p = userDto.GetPower(filterContext.Controller, filterContext.ActionDescriptor.ActionName);
            //        CanRead = p.C_CanRead;
            //        CanUpdated = p.C_CanUpdated;
            //        CanDeleted = p.C_CanDelted;
            //        CanCreated = p.C_CanCreated;
            //    }
            //}
            //else
            //{
            //    if (userDto != null)
            //    {
            //        var p = userDto.GetPower(filterContext.Controller, MenuAction);
            //        CanRead = p.C_CanRead;
            //        CanUpdated = p.C_CanUpdated;
            //        CanDeleted = p.C_CanDelted;
            //        CanCreated = p.C_CanCreated;
            //    }
            //}
        }
    }
}