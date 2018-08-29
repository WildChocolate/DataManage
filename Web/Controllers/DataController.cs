using Manage.BLLContainer;
using Manage.Common.Condition;
using Manage.IBLL;
using Manage.IBLL.Dto;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Web.ControllerExt;
using Manage.Common;
using Manage.Common.DataGrid;
using System.IO;

namespace Web.Controllers
{
    public class DataController : PowerController
    {
        string[] menusAction = { "TextQuery" };
        protected override string[] MenusAction
        {
            get { return menusAction; }
        }
        // GET: Data
        public ActionResult TextQuery()
        {
            if (!CanRead)
            {
                return GotoErrorPage(CannotReadText);
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult SearchTextData(DataInfoPager pager)
        {
            var service = Container.GetService<IDataService>();
            Expression<Func<tbl_Data, bool>> wherelambda = d=> d.C_UserId==userDto.User.keyid&&d.C_DataTypeId==1;
            if(!string.IsNullOrWhiteSpace(pager.Name))
            {
                wherelambda.And(d=> d.C_Name.Contains(pager.Name));
            }
            if(!string.IsNullOrWhiteSpace(pager.DateFrom))
            {
                wherelambda.And(d=> d.C_CreatedDate> Convert.ToDateTime(pager.DateFrom));
            }
            if(!string.IsNullOrWhiteSpace(pager.DateTo))
            {
                wherelambda.And(d=> d.C_CreatedDate<Convert.ToDateTime(pager.DateTo));
            }
            var items = service.GetModels(wherelambda).ToList();
            var infos = DataInfo.ConvertToDataInfos(items);
            var cnt = service.GetTableCount(wherelambda);
            //返回表格
            var grid = new DataInfoGrid();
            grid.rows = infos;
            grid.total = cnt;
            return Json(grid);
        }
        public ActionResult TextExecute(long Key)
        {
            ViewBag.DataKey = Key;
            if(!CanRead)
            {
                return GotoErrorPage(CannotReadText);
            }
            var service = Container.GetService<IDataService>();
            var info = new DataInfo();
            ViewBag.Operation = "上传";
            if (Key > 0)
            {
                ViewBag.Operation = "编辑";
                info = DataInfo.ConvertToDataInfo(service.GetModels(d => d.C_UserId == userDto.User.keyid && d.keyid == Key).FirstOrDefault());
            }
            return View(info);
        }
        [HttpPost]
        public ActionResult TextManage(DataInfo info,HttpPostedFileBase file)
        {
            
            var data = new tbl_Data();
            var service = Container.GetService<IDataService>();
            var result = new AjaxResult();
            if (file.ContentType != "text/plain")
            {
                result.State = false;
                result.Message = "文件类型错误，应该为文本文件";
                return Json(result);
            }
            if (file.ContentLength > 1024 * 1024 * 10)
            {
                result.State = false;
                result.Message = "文件长度超过限制，最大为10M";
                return Json(result);
            }
            
            if (info.Key > 0)
            {
                if (!CanUpdated)
                {
                    result.State = false;
                    result.Message = CannotUpateText;
                    return Json(result);
                }
                data = service.GetModels(d => d.C_UserId == userDto.User.keyid && d.keyid == info.Key).FirstOrDefault();
                if (data != null)
                {
                    data.C_Name = info.Name;
                    data.C_Description = info.Description;
                    if (!string.IsNullOrEmpty(data.C_Path))
                    {
                        System.IO.File.Delete(data.C_Path);
                    }
                    var path=Server.MapPath("~/file/text");
                    if(!Directory.Exists(path)){
                        Directory.CreateDirectory(path);
                    }
                    var filePath = path + "\\" + data.C_Name +"_"+ Guid.NewGuid() + ".txt";
                    file.SaveAs(filePath);
                    data.C_Path = filePath;
                    data.C_UpdatedDate = DateTime.Now;
                }
                result.State = service.Update(data);
                if (result.State)
                    result.Message = "修改成功";
                else
                    result.Message = "修改失败";
            }
            else
            {
                if (!CanCreated)
                {
                    result.State = false;
                    result.Message =CannotAddText;
                    return Json(result);
                }
                data.C_UserId = userDto.User.keyid;
                data.C_DataTypeId = 1;
                data.C_Name = info.Name;
                data.C_Description = info.Description;
                if (!string.IsNullOrEmpty(data.C_Path))
                {
                    System.IO.File.Delete(data.C_Path);
                }
                var path = Server.MapPath("~/file/text");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var filePath = path + "\\" + data.C_Name + "_" + Guid.NewGuid() + ".txt";
                file.SaveAs(filePath);
                data.C_Path = filePath;
                data.C_CreatedDate = DateTime.Now;
                result.State = service.Add(data);
                if (result.State)
                {
                    result.Message = "添加成功";
                    //添加成功后要添加流程步骤的审核
                    var mainRole = userDto.Roles.FirstOrDefault().keyid;
                    var flowStepService = Container.GetService<IFlowStepService>();
                    //这里只需要取到第一个步骤
                    var firstStep = flowStepService.GetRoleVerifyStepsByRole(mainRole, (int)DataTypeEnum.Text).FirstOrDefault();
                    var VerfiyStepService = Container.GetService<IDataVerifyStepService>();
                    var item = new tbl_DataVerifyStep();
                    item.C_DataId = data.keyid;
                    item.C_State = false;
                    item.C_StepId = firstStep!=null?firstStep.stepid:0;
                    item.C_UserId = 0;
                    item.C_CreatedDate = DateTime.Now;
                    VerfiyStepService.Add(item);
                }
                else
                    result.Message = "添加失败";
            }

            return Json(result);
        }
    }
}