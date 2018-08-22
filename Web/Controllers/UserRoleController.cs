using Manage.BLLContainer;
using Manage.Common;
using Manage.Common.Condition;
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
    public class UserRoleController : PowerController
    {
        //
        // GET: /UserRole/
        public ActionResult IndexQuery()
        {
            if (!CanRead)
            {
                ViewBag.Message = CannotReadText;
                return View("Error");
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetUserGrid (UserPager pager)
        {
            var size = Convert.ToInt32(pager.pageSize);
            var idx = Convert.ToInt32(pager.pageIndex);
            var sql = @"SELECT [keyid],[C_Name] ,[C_LoginName],[C_PassWord],[C_Sex],[C_Enabled],[C_CreatedDate],[C_UpdatedDate] FROM [DATA_MANAGE].[dbo].[tbl_User] ";
            var where = new StringBuilder(" where 1=1 ");
            if (!string.IsNullOrWhiteSpace(pager.Name))
            {
                where.AppendFormat(" and C_Name='{0}'", pager.Name);
            }
            if (!string.IsNullOrWhiteSpace(pager.DateFrom))
            {
                where.AppendFormat(" and C_CreatedDate>'{0}'", pager.DateFrom);
            }
            if (!string.IsNullOrWhiteSpace(pager.DateTo))
            {
                where.AppendFormat(" and C_CreatedDate<'{0}'", pager.DateTo);
            }
            try
            {
                var service = Container.GetService<IUserService>();
                var obj = new tbl_User();
                var infos = service.GetModelsByPage<tbl_User_NoPhoto>(size, idx, sql + where).ToList();

                var userinfos = UserInfo.ConvertUserInfos(infos);

                var cnt = service.GetTableCount(where.ToString());
                service.GetModelsByPage(10, 2, true, u => u.keyid, u => 1 == 1).Skip(10 * 1).Take(10);
                var usergrid = new Manage.Common.DataGrid.UserGrid() { total = cnt, rows = userinfos };
                return Json(usergrid);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult() { Message = ex.Message });
            }
        }
        public ActionResult GetUserRoles(int userid)
        {
            var urservice = Container.GetService<IUserRoleService>();
            var roles = urservice.GetModels(ur => ur.UserId == userid).ToList();
            return Json(UserRoleInfo.ConvertToUserRoleInfos(roles));
        }
        public ActionResult IndexExecute(int userid)
        {
            if (!CanRead)
            {
                ViewBag.Message = CannotReadText;
                return View("Error");
            }
            var service = Container.GetService<IUserService>();
            var user = service.GetModels(u => u.keyid == userid).FirstOrDefault();
            var userinfo = UserInfo.ConvertUserInfo(user);
            return View(userinfo);
        }
        string[] menusAction = {"IndexQuery" };
        protected override string[] MenusAction
        {
            get { return menusAction; }
        }
        public ActionResult GetExecuteRolesByUserId(int userid)
        {
            var service = Container.GetService<IRoleService>();
            //所有的角色
            var allRoles = service.GetModels(r => true).Select(r => new { Key=r.keyid, Name=r.C_Name}).ToList();

            //当前用户拥有的角色的Id
            var urservice = Container.GetService<IUserRoleService>();
            var selectRoleIds = urservice.GetModels(ur => ur.UserId == userid).Select(ur =>  ur.RoleId ).ToList();

            var selected = allRoles.Where(r => selectRoleIds.Contains(r.Key));
            var unselected = allRoles.Except(selected);//得到差集，即未选的集合
            return Json(new { selected = selected, unselected = unselected });
        }
        [HttpPost]
        public ActionResult Manage(int UserId, string Roles="") 
        {
            if (CanUpdated)
            {
                var roleids = Array.ConvertAll(Roles.Split(','), int.Parse);

                if (UserId > 0)
                {
                    var service = Container.GetService<IUserRoleService>();
                    var updated = service.UpdateUserRoles(UserId, roleids);
                    var result = new AjaxResult() { State = updated };
                    if (updated)
                    {
                        result.Message = "修改成功";
                    }
                    else
                        result.Message = "修改失败";
                    return Json(result);
                }
                else
                {
                    ViewBag.Message = "不存在的用户";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.Message = CannotUpateText;
                return View("Error");
            }
        }
        
    }
}