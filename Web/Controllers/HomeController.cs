using Manage.BLLContainer;
using Manage.IBLL;
using Manage.IBLL.Dto;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.ControllerExt;

namespace Web.Controllers
{
    public class HomeController : BasicController
    {
        public ActionResult Index()
        {
                return View("IndexNoLayout");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Aside()
        {
            var userDto = Session["user"] as UserDto;
            if (userDto != null)
            {
                var menus = userDto.Menus;
                var menuservice = Container.GetService<IMenuService>();
                //将所有的一级菜单找出来，
                var allParent = menuservice.GetModels(m=>m.C_ParentMenu==0);
                var ids = allParent.Select(ap => ap.keyid);
                var parents = new List<tbl_Menu>();
                //遍历所有一级菜单,如果当前用户的二级菜单包含在里面，则将当前一级菜单取出
                menus.ForEach(m => {
                    if (ids.Contains(m.C_ParentMenu))
                    {
                        parents.Add(allParent.Where(ap=>ap.keyid==m.C_ParentMenu).First());
                    }
                });
                //生成一级菜单的分部视图
                return PartialView("/views/Home/PartialAside.cshtml", parents.Distinct());
            }
            return PartialView("/views/Home/PartialAside.cshtml", null);
        }
        public ActionResult GetMenu(int keyid)
        {
            var userDto = Session["user"] as UserDto;

            if (userDto != null) {
                var children = userDto.Menus.Where(m => m.C_ParentMenu == keyid);

                if (children != null)
                {
                    return PartialView("/views/Home/PartialMenu.cshtml", children);
                }
            }
            return PartialView("/views/Home/PartialMenu.cshtml", null);
        }
    }
}