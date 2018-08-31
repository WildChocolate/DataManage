using Manage.Common.Condition;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Web.ControllerExt;
using Manage.Common;
using Manage.BLLContainer;
using Manage.IBLL;
using Manage.Common.DataGrid;
using Manage.IBLL.Dto;

namespace Web.Controllers
{
    public class DataTypeController : PowerController
    {
        string[] menusAction = { "IndexQuery"};
        protected override string[] MenusAction
        {
            get { return menusAction; }
        }
        //
        // GET: /DataType/
        public ActionResult IndexQuery()
        {
            if (!CanRead)
                return GotoErrorPage(CannotReadText);
            return View();
        }
        [HttpPost]
        public ActionResult SearchDataType(DataTypePager pager)
        {
            //构建查询条件
            Expression<Func<tbl_DataType, bool>> wherelambda = dt => true;
            if (!string.IsNullOrWhiteSpace(pager.Name))
            {
                wherelambda=wherelambda.And(dt => dt.C_Name.Contains(pager.Name));
            }
            if (!string.IsNullOrWhiteSpace(pager.DateFrom))
            {
                wherelambda=wherelambda.And(dt => dt.C_CreatedDate > Convert.ToDateTime(pager.DateFrom));
            }
            if (!string.IsNullOrWhiteSpace(pager.DateTo))
            {
                wherelambda=wherelambda.And(dt => dt.C_CreatedDate < Convert.ToDateTime(pager.DateFrom));
            }
            var service = Container.GetService<IDataTypeService>();
            var items = service.GetModelsByPage(pager.pageSize, pager.pageIndex, true, d=> d.keyid, wherelambda).ToList();
            var cnt = service.GetTableCount(wherelambda);
            var grid = new DataTypeGrid();
            grid.rows = DataTypeInfo.ConvertoToDataTypeInfos(items);
            grid.total = cnt;
            return Json(grid);
        }
        public ActionResult IndexExecute(int Key)
        {
            if (!CanRead)
                return GotoErrorPage(CannotAddText);
            var info = new DataTypeInfo() { Key=Key};
            var service = Container.GetService<IDataTypeService>();
            if (Key > 0)
            {
                ViewBag.Operation = "修改";
                var item = service.GetModels(dt => dt.keyid == Key).FirstOrDefault();
                info = DataTypeInfo.ConvertoToDataTypeInfo(item);
            }
            ViewBag.Operation = "添加";
            return View(info);
        }
        [HttpPost]
        public ActionResult DataTypeManage(DataTypeInfo info)
        {
            var service = Container.GetService<IDataTypeService>();
            var result = new AjaxResult();
            if (info.Key > 0)
            {
                var item = service.GetModels(dt => dt.keyid == info.Key).FirstOrDefault();
                if (item == null)
                {
                    result.Message = "修改失败，不存在的对象 ";
                    return Json(result);
                }
                item.C_Name = info.Name;
                item.C_Description = info.Description;
                item.C_UpdatedDate = DateTime.Now;
                result.State=service.Update(item);
                if (result.State)
                {
                    result.Message = "修改成功";
                }
                else
                    result.Message = "修改失败";
            }
            else
            {
                var item = new tbl_DataType();
                item.C_Name = info.Name;
                item.C_Description = info.Description;
                item.C_CreatedDate = DateTime.Now;
                result.State=service.Add(item);
                if (result.State)
                {
                    result.Message = "添加成功";
                }
                else
                    result.Message = "添加失败";
            }
            return Json(result);
        }
    }
}