using Manage.BLLContainer;
using Manage.Common.Condition;
using Manage.Common.DataGrid;
using Manage.IBLL;
using Manage.IBLL.Dto;
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
                return View();
            else
            {
                ViewBag.Message = CannotReadText;
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult SearchRole(RoleCondition condition)
        {
            var service = Container.GetService<IRoleService>();
            var size = Convert.ToInt32(condition.pageSize);
            var idx = Convert.ToInt32(condition.pageIndex);
            var where = new StringBuilder(" where 1=1 ");
            if (!string.IsNullOrWhiteSpace(condition.Name))
            {
                where.AppendLine("and C_Name like '%" + condition.Name + "%'");
            }
            if (!string.IsNullOrWhiteSpace(condition.DateFrom))
            {
                where.AppendLine("and C_CreatedDate>'" + condition.DateFrom + "' ");
            }
            if (!string.IsNullOrWhiteSpace(condition.DateTo))
            {
                where.AppendLine("and C_CreatedDate<'" + condition.DateTo + "' ");
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

            var rolegrid = new RoleDataGrid();
            rolegrid.rows = RoleInfo.ConvertToRoleInfos(roles);
            rolegrid.total = service.GetTableCount(where.ToString());
            return Json(rolegrid);
        }
        string[] menusaction = {"IndexQuery" };
        protected override string[] MenusAction
        {
            get { return menusaction; }
        }
    }
}