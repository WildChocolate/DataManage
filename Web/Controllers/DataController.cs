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
    /// <summary>
    /// 
    /// </summary>
    public class DataController : PowerController
    {
        string[] menusAction = { "TextQuery", "DocxQuery", "ExcelQuery", "PDFQuery" };
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
            ViewBag.DataTypeKey = (int)DataTypeEnum.Text;
            return View("DataQuery");
        }
        public ActionResult DocxQuery()
        {
            if (!CanRead)
            {
                return GotoErrorPage(CannotReadText);
            }
            ViewBag.DataTypeKey = (int)DataTypeEnum.Word;
            return View("DataQuery");
        }
        public ActionResult ExcelQuery()
        {
            if (!CanRead)
            {
                return GotoErrorPage(CannotReadText);
            }
            ViewBag.DataTypeKey = (int)DataTypeEnum.Excel;
            return View("DataQuery");
        }
        /// <summary>
        /// PDF这个就不用上面的页面了，独立出来，用Bootstrap+vue试试
        /// </summary>
        /// <returns></returns>

        public ActionResult PDFQuery()
        {
            if (!CanRead)
            {
                return GotoErrorPage(CannotReadText);
            }
            ViewBag.DataTypeKey = (int)DataTypeEnum.PDF;
            return View();
        }
        public ActionResult PDFExecute(int Key)
        {
            if (!CanRead)
            {
                return GotoErrorPage(CannotReadText);
            }
            var service = Container.GetService<IDataService>();
            var info = new DataInfo() { DataTypeKey=(int)DataTypeEnum.PDF};
            ViewBag.Operation = "上传";
            if (Key > 0)
            {
                ViewBag.Operation = "编辑";
                info = DataInfo.ConvertToDataInfo(service.GetModels(d => d.C_UserId == userDto.User.keyid && d.keyid == Key).FirstOrDefault());
            }
            return View(info);
        }
        [HttpPost]
        public ActionResult SearchData(DataInfoPager pager)
        {
            var service = Container.GetService<IDataService>();
            Expression<Func<tbl_Data, bool>> wherelambda = d=> d.C_UserId==userDto.User.keyid&&d.C_DataTypeId==pager.DataTypeKey;
            if(!string.IsNullOrWhiteSpace(pager.Name))
            {
                wherelambda=wherelambda.And(d => d.C_Name.Contains(pager.Name));
            }
            if(!string.IsNullOrWhiteSpace(pager.DateFrom))
            {
                wherelambda=wherelambda.And(d => d.C_CreatedDate > Convert.ToDateTime(pager.DateFrom));
            }
            if(!string.IsNullOrWhiteSpace(pager.DateTo))
            {
                wherelambda=wherelambda.And(d => d.C_CreatedDate < Convert.ToDateTime(pager.DateTo));
            }
            var items = service.GetModelsByPage(pager.pageSize, pager.pageIndex, true, d=> d.keyid, wherelambda).ToList();
            var infos = DataInfo.ConvertToDataInfos(items);
            var cnt = service.GetTableCount(wherelambda);
            //返回表格
            var grid = new DataInfoGrid();
            grid.rows = infos;
            grid.total = cnt;
            //if (pager.DataTypeKey == (int)DataTypeEnum.PDF) 
            //{
            //    return Json(infos);
            //}
            return Json(grid);
        }
        public ActionResult DataExecute(long Key, int DataTypeKey)
        {
            ViewBag.DataKey = Key;
            ViewBag.DataTypeKey = DataTypeKey;
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
        public ActionResult DataManage(DataInfo info,HttpPostedFileBase file)
        {
            
            var data = new tbl_Data();
            var service = Container.GetService<IDataService>();
            var result = new AjaxResult() { State=true};
            var filetuple = CheckFileType(info, file, result);

            var suffix = filetuple.Item1;//文件后缀
            var path = filetuple.Item2;
            
            if (result.State == false)
            {
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
                    if (file != null)
                    {
                        if (!string.IsNullOrEmpty(data.C_Path))
                        {
                            System.IO.File.Delete(data.C_Path);
                        }

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        var filePath = path + "\\" + data.C_Name + "_" + Guid.NewGuid() + suffix;
                        file.SaveAs(filePath);
                        data.C_Path = filePath;
                    }
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
                data.C_DataTypeId = info.DataTypeKey;
                data.C_Name = info.Name;
                data.C_Description = info.Description;
                if (file != null)
                {
                    if (!string.IsNullOrEmpty(data.C_Path))
                    {
                        System.IO.File.Delete(data.C_Path);
                    }

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var filePath = path + "\\" + data.C_Name + "_" + Guid.NewGuid() + suffix;
                    file.SaveAs(filePath);
                    data.C_Path = filePath;
                }
                data.C_CreatedDate = DateTime.Now;
                result.State = service.Add(data);
                if (result.State)
                {
                    result.Message = "添加成功";
                    //添加成功后要添加流程步骤的审核,这是个简化处理，本来一个人有多个角色，但这里取第一个，来确定需要走的流程
                    var mainRole = userDto.Roles.FirstOrDefault().keyid;
                    var flowStepService = Container.GetService<IFlowStepService>();
                    //这里只需要取到流程中的第一个步骤
                    var firstStep = flowStepService.GetRoleVerifyStepsByRole(mainRole, info.DataTypeKey).FirstOrDefault();
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
        private Tuple<string, string> CheckFileType(DataInfo info, HttpPostedFileBase file, AjaxResult result)
        {
            if (file == null) {
                return new Tuple<string, string>(string.Empty, string.Empty);
            }
            if (file.ContentLength > 1024 * 1024 * 10)
            {
                result.State = false;
                result.Message = "文件长度超过限制，最大为10M";
                return new Tuple<string, string>(string.Empty, string.Empty);
            }
            var path = Server.MapPath("~/file/text");//文件路径
            var suffix = "";//文件后缀
            if ((DataTypeEnum)info.DataTypeKey == DataTypeEnum.Text)
            {
                if (file.ContentType != "text/plain")
                {
                    result.State = false;
                    result.Message = "文件类型错误，应该为文本文件";
                }
                suffix = ".txt";
                path = Server.MapPath("~/file/text");
            }
            else if ((DataTypeEnum)info.DataTypeKey == DataTypeEnum.Word)
            {
                if (file.ContentType != "application/msword")
                {
                    result.State = false;
                    result.Message = "文件类型错误，应该为word文档";
                }
                suffix = ".doc";
                path = Server.MapPath("~/file/word");
            }
            else if ((DataTypeEnum)info.DataTypeKey == DataTypeEnum.Excel)
            {
                if (file.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    result.State = false;
                    result.Message = "文件类型错误，应该为excel文件";
                }
                suffix = ".xlsx";
                path = Server.MapPath("~/file/excel");
            }
            else if ((DataTypeEnum)info.DataTypeKey == DataTypeEnum.PDF)
            {
                if (file.ContentType != "application/pdf")
                {
                    result.State = false;
                    result.Message = "文件类型错误，应该为excel文件";
                }
                suffix = ".pdf";
                path = Server.MapPath("~/file/pdf");
            }
            else
            {
                result.State = false;
                result.Message = "不合法的文件类型";
            }
            return  new Tuple<string, string>(suffix, path);
        }
    }
}