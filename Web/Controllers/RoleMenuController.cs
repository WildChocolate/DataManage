using Manage.BLLContainer;
using Manage.Common;
using Manage.Common.Condition;
using Manage.Common.DataGrid;
using Manage.IBLL;
using Manage.IBLL.Dto;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ControllerExt;

namespace Web.Controllers
{
    public class RoleMenuController : PowerController
    {
        string[] menusAction = { "IndexQuery"};
        protected override string[] MenusAction
        {
            get { return menusAction; }
        }
        // GET: /RoleMenu/
        public ActionResult IndexQuery()
        {
            if (CanRead)
            {
                return View();
            }
            else
            {
                ViewBag.Message = CannotReadText;
                return View("Error");
            }
            
        }
        [HttpPost]
        public ActionResult GetRoleGrid(RolePager pager)
        {
            var rolecontroller = new RoleController();
            return rolecontroller.SearchRole(pager);
        }
        public ActionResult GetRoleMenus(int roleid)
        {
            var service = Container.GetService<IRoleMenuService>();
            var list = service.GetVModels(rm => rm.RoleId == roleid).ToList();
            return Json(RoleMenuInfo.ConvertToRoleMenuInfos( list ));
        }
        public ActionResult IndexExecute(int RoleKey)
        {
            if (CanRead)
            {
                ViewBag.RoleKey = RoleKey;
                var service = Container.GetService<IRoleService>();
                var role = service.GetModels(r => r.keyid == RoleKey).FirstOrDefault();
                if (role != null)
                {
                    ViewBag.RoleName = role.C_Name;
                }
                return View();
            }
            else
            {
                ViewBag.Message = CannotReadText;
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult GetRoleMenuGrid(int RoleKey)
        {
            var sql = @"SELECT t1.[keyid]
                              ,[C_Name]
                              ,[C_Description]
                              ,[C_ParentMenu]
                              , ParentMenu
                              ,[C_Controller]
                              ,[C_Action]
                              ,[C_CreatedDate]
                              ,[C_UpdatedDate]
                          FROM [DATA_MANAGE].[dbo].[tbl_Menu] t1
                          left join (select keyid, C_Name as ParentMenu from tbl_menu) as t2
                          on t1.C_parentMenu=t2.keyid where C_ParentMenu<>0";
            
            var service = Container.GetService<IMenuService>();
            var list = service.GetModelsByPage<MenuDto>(1000, 1, sql).ToList() ;//得到所有菜单

            var rmservice = Container.GetService<IRoleMenuService>();
            var rolemenus = rmservice.GetVModels(rm => rm.RoleId == RoleKey).ToList();//得到与当前角色对应的菜单
            var currentmenus = rolemenus.Select(rm=> rm.MenuId);//选出当前角色拥有的菜单id列表


            var rolemenuList = new List<RoleMenu>(list.Count);
            foreach (var item in list)
            {
                var new_rm = new RoleMenu();
                new_rm.Key = item.keyid;
                new_rm.Menu = item.C_Name;
                new_rm.ParentMenu = item.ParentMenu;
                //如果当前角色拥有这个菜单，就要对权限进行检测
                if (currentmenus.Contains(item.keyid))
                {
                    var targetRolemenu = rolemenus.Where(rm => rm.MenuId == item.keyid).FirstOrDefault();
                    if (targetRolemenu != null)
                    {
                        new_rm.CanCreated = targetRolemenu.C_CanCreated;
                        new_rm.CanDeleted = targetRolemenu.C_CanDelted;
                        new_rm.CanReaded = targetRolemenu.C_CanRead;
                        new_rm.CanUpdated = targetRolemenu.C_CanUpdated;
                    }
                }
                rolemenuList.Add(new_rm);
            }
            var rolemenugrid = new RoleMenuGrid();

            rolemenugrid.total = rolemenuList.Count;
            rolemenugrid.rows = rolemenuList;
            return Json(rolemenugrid);
        }
        [HttpPost]
        public ActionResult Manage(int RoleKey, string RoleMenusJsonString)
        {
            if(!CanUpdated||!CanCreated)
            {
                ViewBag.Message = CannotUpateText + " 并且 " + CannotAddText;
                return View("Error");
            }
            var rolemenuList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoleMenu>>(RoleMenusJsonString);
            var service = Container.GetService<IRoleMenuService>();
            var list = new List<tbl_Role_Menu>();
            foreach (var item in rolemenuList)
            {
                var trm = new tbl_Role_Menu();
                trm.C_RoleId = RoleKey;
                trm.C_MenuId = item.Key;
                trm.C_CanCreated = item.CanCreated;
                trm.C_CanDelted = item.CanDeleted;
                trm.C_CanRead = item.CanReaded;
                trm.C_CanUpdated = item.CanUpdated;
                trm.C_CreatedDate = DateTime.Now;
                list.Add(trm);
            }
            var rm_deleteds = service.GetModels(rm => rm.C_RoleId == RoleKey);
            foreach (var rm in rm_deleteds)
            {
                service.Delete(rm);
            }
            var added = service.AddRange(list);

            var ajaxresult = new AjaxResult() { State=added};
            if (added)
            {
                ajaxresult.Message = "修改成功";
            }
            else
                ajaxresult.Message = "修改失败";
            return Json(ajaxresult);
        }
        
    }
}