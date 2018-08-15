﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manage.BLLContainer;
using Manage.Model;
using Manage.IBLL.Dto;
using Manage.IBLL;
using Manage.Common;
using Web.ControllerExt;
using System.Linq.Expressions;
using System.Text;
using Manage.Common.Condition;


namespace Web.Controllers
{
    public class UserController : PowerController
    {
        //
        // GET: /User/
        /// <summary>
        /// 在User控制器中，有两个与菜单操作有送的action,如果还有对其他菜单的控制的，要在这里加入对应action名称的数组
        /// </summary>
        string[] menusAction = new string[] { "ManageQuery", "ChangePassword", "Index" };
        protected override string[] MenusAction
        {
            get { return menusAction; }
        }

        public ActionResult Index()
        {
            if (CanRead) 
            {
                var userinfo = new UserInfo() {Sex=userDto.User.C_Sex, Name=userDto.User.C_Name, LoginName=userDto.User.C_LoginName, PassWord=userDto.User.C_PassWord};
                return View("IndexNoLayout",userinfo);
                return View(userinfo);
            }
            else
            {
                return GotoErrorPage("您无权查看此页，请联系管理员开通");
            }
        }
        public ActionResult GetUserHeadImage(int key=0)
        {
            if (key != 0)
            {
                var service = Container.GetService<IUserService>();
                var user = service.GetModels(u => u.keyid == key).FirstOrDefault();
                if (user.C_Photo == null)
                {
                    if (user.C_Sex == true)
                    {
                        return File("../Images/head_boy.png", "image/png");
                    }
                    else
                    {
                        return File("../Images/head_girl.png", "image/png");
                    }
                }
                else
                {
                    return File(user.C_Photo, "image/png");
                }
                
            }
            else
            {
                return new EmptyResult() ;
            }
        }
        [HttpPost]
        public ActionResult Index(tbl_User user)
        {
            user = new tbl_User();
            user.C_Name = "ekko";
            user.C_Enabled = true;
            user.C_CreatedDate = DateTime.Now;
            user.C_LoginName = "ekko.xu";
            user.C_PassWord = "Xzc94";
            var bll = Container.GetUserService();
            var result = bll.Add(user);
            return View();
        }

        public ActionResult ChangePassword()
        {
            if (CanRead)
                return View("ChangePasswordNoLayout");
            else
                return GotoErrorPage();
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldpassword, string newpassword)
        {
            var x = new ChangedPasswordStatus{ Status = false};
            if (CanUpdated)
            {
                
                var currentuser = Session["user"] as UserDto;
                if (oldpassword != newpassword)
                {
                    if (currentuser.User.C_PassWord == oldpassword)
                    {
                        var userservice = Container.GetService<IUserService>();
                        var result = userservice.ChangePassword(currentuser.User.keyid.ToString(), newpassword);
                        x.Status = result;
                    }
                }
                else
                    x.Status = true;
            }
            else
            {
                x.Message = "您没有修改的权限，请联系管员开通";
                x = new ChangedPasswordStatus{ Status = false, Url = "/Error/Index?Message="+x.Message };
                //return GotoErrorPage("您没有修改的权限，请联系管员开通");
            }
            return Content(x.ToJson());
        }
        public ActionResult ManageQuery()
        {
            
            if (userDto != null)
            {
                if (CanRead)
                    return View();
            }
            ViewBag.Message = "你无权查看此页，请联系管理员开通";
            return View("Error");
        }
        public ActionResult ManageExecute(int Key=0)
        {
            

            if (userDto != null)
            {
                if (CanRead)
                {
                    var userinfo = new UserInfo();
                    if (Key == 0)
                        ViewBag.Operation = "添加用户";
                    else
                    {
                        var service = Container.GetService<IUserService>();
                        var user = service.GetModels(u=>u.keyid==Key).FirstOrDefault()??new tbl_User();
                        userinfo = UserInfo.ConvertUserInfo(user);
                        ViewBag.Operation = "修改用户";
                    }
                    return View(userinfo);
                }
            }
            ViewBag.Message = "你无权查看此页，请联系管理员开通";
            return View("Error");
        }
        [HttpPost]
        public ActionResult Manage(UserInfo userinfo)
        {
            //key大于0 说明是修改操作
            var service = Container.GetService<IUserService>();
            var data = new AjaxResult();

            if (userinfo.Key > 0)
            {
                var user = service.GetModels(u => u.keyid == userinfo.Key).FirstOrDefault();
                if (user != null)
                {
                    user.C_Name = userinfo.Name;
                    user.C_LoginName=userinfo.LoginName;
                    user.C_PassWord=userinfo.PassWord;
                    user.C_Sex = userinfo.Sex;
                    if (userinfo.Photo != null)
                    {
                        var buff = new byte[userinfo.Photo.InputStream.Length];
                        userinfo.Photo.InputStream.Read(buff, 0, buff.Length);
                        user.C_Photo = buff;
                    }
                    user.C_UpdatedDate = DateTime.Now;
                    var result = service.Update(user);
                    
                    data.State=result;
                    if(data.State)
                        data.Message="修改成功";
                    else
                        data.Message="修改失败";
                    return Content(data.ToJson());
                }
                else
                {
                    return Content(new AjaxResult() { State = false, Message = "修改失败，不存在的用户", Data = "" }.ToJson());
                }
            }
            else
            {
                var user = new tbl_User();
                user.C_Name = userinfo.Name;
                user.C_LoginName = userinfo.LoginName;
                user.C_PassWord = userinfo.PassWord;
                user.C_CreatedDate = DateTime.Now;
                user.C_Sex = userinfo.Sex;
                if (userinfo.Photo != null)
                {
                    var buff = new byte[userinfo.Photo.ContentLength];
                    userinfo.Photo.InputStream.Read(buff, 0, buff.Length);
                    user.C_Photo = buff;
                }
                else
                    user.C_Photo = null;

                var result = service.Add(user);
                data.State = result;
                if (data.State)
                    data.Message = "添加成功";
                else
                    data.Message = "添加失败";
                return Content(data.ToJson());
            }
        }
        [HttpPost]
        public ActionResult GetAllUser(int pageSize, int pageIndex)
        {
            var service = Container.GetService<IUserService>();

            var usergrid = service.SearchUserInfos(pageSize, pageIndex,u=> true);

            //var userinfos = UserInfo.ConvertUserInfos(usergrid.OriginRows);
            return Json(usergrid, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SearchUser(UserCondition uiCondition)
        {
            var size = Convert.ToInt32(uiCondition.pageSize);
            var idx = Convert.ToInt32(uiCondition.pageIndex);
            var sql = @"SELECT [keyid],[C_Name] ,[C_LoginName],[C_PassWord],[C_Sex],[C_Enabled],[C_CreatedDate],[C_UpdatedDate] FROM [DATA_MANAGE].[dbo].[tbl_User] ";
            var where = new StringBuilder(" where 1=1 ");
            if (!string.IsNullOrWhiteSpace(uiCondition.Name))
            {
                where.AppendFormat(" and C_Name='{0}'", uiCondition.Name);
            }
            if (!string.IsNullOrWhiteSpace(uiCondition.DateFrom))
            {
                where.AppendFormat(" and C_CreatedDate>'{0}'", uiCondition.DateFrom);
            }
            if (!string.IsNullOrWhiteSpace(uiCondition.DateTo))
            {
                where.AppendFormat(" and C_CreatedDate<'{0}'", uiCondition.DateTo);
            }
            try
            {
                var service = Container.GetService<IUserService>();
                var obj = new tbl_User();
                var infos = service.GetModelsByPage<tbl_User_NoPhoto>(size, idx, sql + where).ToList();

                var userinfos = UserInfo.ConvertUserInfos(infos);

                var cnt = service.GetTableCount(where.ToString());
                service.GetModelsByPage(10, 2, true, u => u.keyid, u => 1 == 1).Skip(10 * 1).Take(10);
                var usergrid = new Manage.Common.DataGrid.UserDataGrid() { total = cnt, rows = userinfos };
                return Json(usergrid);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult(){ Message=ex.Message});
            }
        }

    }
}