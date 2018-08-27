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
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.ControllerExt;

namespace Web.Controllers
{
    public class RoleController : PowerController
    {
        string[] menusaction = { "IndexQuery" };
        protected override string[] MenusAction
        {
            get { return menusaction; }
        }
        //
        // GET: /Role/
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
        public ActionResult IndexExecute(int Key = 0)
        {
            if (CanRead)
            {
                var roleinfo = new RoleInfo() { Key=Key};
                if (Key == 0)
                {
                    ViewBag.Operation = "添加角色";
                    return View(roleinfo);
                }
                else
                {
                    ViewBag.Operation="修改角色";
                    var service = Container.GetService<IRoleService>();
                    var roledto = service.GetRoleDtoByKey(Key);
                    if (roledto != null)
                    {
                        roleinfo = RoleInfo.ConvertToRoleInfo(roledto);
                        return View(roleinfo);
                    }
                    else
                    {
                        ViewBag.Message = "不存在此角色";
                        return View("Error");
                    }
                }
            }
            else
            {
                ViewBag.Message = CannotReadText;
                return View("Error");
            }
        }

        public ActionResult GetRoles(int Key = 0)
        {
            var service = Container.GetService<IRoleService>();
            
            if (Key == 0)
            {
                var roles = service.GetModels(r=> true).ToList().Select(item => new { Key=item.keyid,Name=item.C_Name });
                return Json(roles, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var roles = service.GetModels(r => r.keyid != Key).ToList().Select(item => new { Key = item.keyid, Name = item.C_Name });
                return Json(roles, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public ActionResult SearchRole(RolePager pager)
        {
            var service = Container.GetService<IRoleService>();
            var size = Convert.ToInt32(pager.pageSize);
            var idx = Convert.ToInt32(pager.pageIndex);
            var where = new StringBuilder(" where 1=1 ");
            if (!string.IsNullOrWhiteSpace(pager.Name))
            {
                where.AppendLine("and C_Name like '%" + pager.Name + "%'");
            }
            if (!string.IsNullOrWhiteSpace(pager.DateFrom))
            {
                where.AppendLine("and C_CreatedDate>'" + pager.DateFrom + "' ");
            }
            if (!string.IsNullOrWhiteSpace(pager.DateTo))
            {
                where.AppendLine("and C_CreatedDate<'" + pager.DateTo + "' ");
            }
            var sql = @"SELECT TOP 1000 t1.[keyid]
                          ,[C_Name]
                          ,ParentName
                          ,[C_ParentRole]
                          ,[C_CreatedDate]
                          ,[C_UpdatedDate]
                      FROM [DATA_MANAGE].[dbo].[tbl_Role] as t1
                      left join (select keyid, C_Name as ParentName from tbl_Role) as t2
                      on t1.C_ParentRole=t2.keyid";
            var roles = service.GetModelsByPage<RoleDto>(size, idx, sql + where);

            var rolegrid = new RoleGrid();
            rolegrid.rows = RoleInfo.ConvertToRoleInfos(roles);
            rolegrid.total = service.GetTableCount(where.ToString());
            return Json(rolegrid);
        }
        [HttpPost]
        public ActionResult Manage(RoleInfo info)
        {
            var service = Container.GetService<IRoleService>();
            var data = new AjaxResult() ;
            try
            {
                if (info.Key > 0)
                {
                    var role = service.GetModels(r => r.keyid == info.Key).FirstOrDefault();
                    if (role != null)
                    {
                        role.C_Name = info.Name;
                        role.C_Description = info.Description;
                        role.C_ParentRole = info.ParentKey;
                        role.C_UpdatedDate = DateTime.Now;
                        var success = service.Update(role);
                        if (success)
                        {
                            data.Message = "修改成功";
                        }
                        else
                        {
                            data.Message = "修改失败";
                        }
                    }
                    else
                        data.Message = "不存在的角色";
                }
                else
                {
                    var role = new tbl_Role();
                    role.C_Name = info.Name;
                    role.C_Description = info.Description;
                    role.C_ParentRole = info.ParentKey;
                    role.C_CreatedDate = DateTime.Now;
                    var success = service.Add(role);

                    if (success)
                    {
                        data.Message = "添加成功";
                    }
                    else
                    {
                        data.Message = "添加失败";
                    }
                }
            }
            catch(Exception ex){
                data.Message = ex.Message;
            }
            return Json(data);
        }
        public ActionResult RoleFlow(int Key)
        {
            if (!CanRead)
                return GotoErrorPage(CannotReadText);
            var service = Container.GetService<IRoleService>();
            ViewBag.RoleName = service.GetModels(r => r.keyid == Key).FirstOrDefault().C_Name+string.Empty;
            
            return View();
        }
    }
}