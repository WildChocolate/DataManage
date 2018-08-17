using Manage.BLLContainer;
using Manage.Common.Condition;
using Manage.Common.DataGrid;
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
    public class MenuController : PowerController
    {
        string[] menusAction = { "IndexQuery" };
        protected override string[] MenusAction
        {
            get { return menusAction; }
        }
        //
        // GET: /Menu/
        public ActionResult IndexQuery()
        {
            if (CanRead)
            {
                @ViewBag.Operation = "菜单查询";
                return View();
            }
            else
            {
                ViewBag.Message = CannotReadText;
                return View("Error");
            }
        }
        /// <summary>
        /// Name DateFrom  DateTo 三个条件为基础的条件，有其他条件可以在 相应的子类中扩展
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public ActionResult SearchRole(RolePager pager)
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
                          on t1.C_parentMenu=t2.keyid";
            var where = new StringBuilder(" where 1=1 ");
            if (!string.IsNullOrWhiteSpace(pager.Name))
            {
                where.AppendLine("and Name like '%"+pager.Name+"%'");
            }
            if (!string.IsNullOrWhiteSpace(pager.DateFrom))
            {
                where.AppendLine(" and C_CreatedDate>'"+pager.DateFrom+"'");
            }
            if (!string.IsNullOrWhiteSpace(pager.DateTo))
            {
                where.AppendLine(" and C_CreatedDate<'" + pager.DateTo + "'");
            }
            try
            {
                var service = Container.GetService<IMenuService>();
                var menudtos = service.GetModelsByPage<MenuDto>(pager.pageSize, pager.pageIndex, sql+where);
                var menuinfos = MenuInfo.ConvertToMenuInfos(menudtos);
                var grid = new MenuDataGrid();
                grid.rows = menuinfos;
                grid.total = service.GetTableCount(where.ToString());
                return Json(grid);
            }
            catch(Exception ex)
            {
                ViewBag.Operation = ex.Message;
                return View("Error");
            }
        }
        public ActionResult GetParentMenus()
        {
            var service = Container.GetService<IMenuService>();
            var menus = service.GetModels(m => m.C_ParentMenu == 0).Select(m => new { m.keyid, m.C_Name});
            return Json(menus);
        }
        public ActionResult IndexExecute(int Key)
        {
            var service  = Container.GetService<IMenuService>();
            var info = new MenuInfo() { Key=Key};
            if (CanRead)
            {
                
                try
                {
                    if (Key > 0)
                    {
                        var dto = service.GetMenuDtoByKey(Key);
                        if (dto != null)
                        {
                            ViewBag.Operation = "菜单修改";
                            info = MenuInfo.ConvertToMenuInfo(dto);
                            return View(info);
                        }
                        else
                        {
                            ViewBag.Message = "不存在此菜单,请重新检查";
                        }
                    }
                    else
                    {
                        ViewBag.Operation = "添加菜单";
                        return View(info);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = CannotReadText;
            }
            return View("Error");
        }
        [HttpPost]
        public ActionResult Manage(MenuInfo info)
        {
            var service = Container.GetService<IMenuService>();
            var result = new Manage.Common.AjaxResult() {};
            try
            {
                if (info.Key == 0)
                {
                    if (!(info.Name.Length > 0))
                    {
                        result.State = false;
                        result.Message = "菜单名不能为空";
                    }
                    else
                    {
                        var menu = new tbl_Menu();
                        menu.C_Name = info.Name;
                        menu.C_Description = info.Description;
                        menu.C_ParentMenu = info.ParentKey;
                        menu.C_Controller = info.Controller;
                        menu.C_Action = info.Action;
                        menu.C_CreatedDate = DateTime.Now;
                        result.State = service.Add(menu);
                        if (result.State)
                        {
                            result.Message = "修改成功";
                        }
                        else
                            result.Message = "修改失败";
                    }
                }
                else
                {
                    var menu = service.GetModels(m => m.keyid == info.Key).FirstOrDefault();
                    menu.C_Name = info.Name;
                    menu.C_Description = info.Description;
                    menu.C_ParentMenu = info.ParentKey;
                    menu.C_Controller = info.Controller;
                    menu.C_Action = info.Action;
                    menu.C_UpdatedDate = DateTime.Now;
                    if (menu != null)
                    {
                        result.State = service.Update(menu);
                        if (result.State)
                            result.Message = "修改成功";
                        else
                            result.Message = "修改失败";
                    }
                    else
                        result.Message = "错误，不存在此菜单，修改失败";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
    }
}