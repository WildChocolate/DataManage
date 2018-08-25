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
using Manage.IBLL.Dto;
using Manage.Common.DataGrid;

namespace Web.Controllers
{
    public class VerifyFlowController : PowerController
    {
        string[] menusAction = { "IndexQuery" };
        protected override string[] MenusAction
        {
            get { return menusAction; }
        }
        //
        // GET: /VerifyFlow/
        public ActionResult IndexQuery()
        {
         
            if(!CanRead){
                return GotoErrorPage(CannotReadText);
            }
            return View();
        }
        [HttpPost]
        public ActionResult SearchVerifyFlow(VerifyFlowPager pager)
        {
            Expression<Func<tbl_VerifyFlow, bool>> namelambda = null;
            if (!string.IsNullOrWhiteSpace(pager.Name))
            {
                namelambda = vf => vf.C_Name.Contains( pager.Name);
            }
            Expression<Func<tbl_VerifyFlow, bool>> datefromlambda = null;
            if (!string.IsNullOrWhiteSpace(pager.DateFrom ))
            {
                datefromlambda = vf => vf.C_UpdatedDate > DateTime.Parse(pager.DateFrom);
            }
            Expression<Func<tbl_VerifyFlow, bool>> datetolambda = null;
            if (!string.IsNullOrWhiteSpace(pager.DateTo ))
            {
                datetolambda = vf => vf.C_CreatedDate < DateTime.Parse(pager.DateTo);
            }
            Expression<Func<tbl_VerifyFlow, bool>> wherelambda = vf=> true;
            if (namelambda != null)
            {
                wherelambda = wherelambda.And(namelambda);
            }
            if (datefromlambda != null)
                wherelambda = wherelambda.And(datefromlambda);
            if (datetolambda != null)
                wherelambda = wherelambda.And(datetolambda);
            var service = Container.GetService<IVerifyFlowService>();
            var verifyflowlist = VerifyFlowInfo.ConvertToVerifyFlows( service.GetModelsByPage(pager.pageSize, pager.pageIndex, true, vf => vf.keyid, wherelambda).AsEnumerable().ToList());
            var count = service.GetTableCount(wherelambda);
            var grid = new VerifyFlowGrid();
            grid.rows = verifyflowlist;
            grid.total = count;
            return Json(grid);
        }
        public ActionResult IndexExecute(int Key)
        {
            if(!CanRead)
            {
                return GotoErrorPage(CannotReadText);
            }
            else
            {
                var info = new VerifyFlowInfo();
                if (Key > 0)
                {
                    var service = Container.GetService<IVerifyFlowService>();
                    var model = service.GetModels(vf => vf.keyid == Key).FirstOrDefault();
                    info.Key = Key;
                    if(model!=null)
                    {
                        info.Name = model.C_Name;
                        info.Description = model.C_Description;
                        info.UpdatedDate = model.C_UpdatedDate.ToString();
                        info.CreatedDate = model.C_CreatedDate!=null?model.C_CreatedDate.ToString():"";
                    }
                }
                return View(info);
            }
        }
        [HttpPost]
        public ActionResult Manage(VerifyFlowInfo info)
        {
            var service = Container.GetService<IVerifyFlowService>();
            var result = new AjaxResult();
            if (info.Key > 0)
            {
                if(!CanUpdated)
                {
                    return GotoErrorPage(CannotUpateText);
                }
                var item = service.GetModels(vf => vf.keyid == info.Key).FirstOrDefault();
                if (item == null)
                {
                    return GotoErrorPage("不存在此流程");
                }
                else
                {
                    item.C_Name = info.Name;
                    item.C_Description = info.Description;
                    item.C_UpdatedDate = DateTime.Now;
                }
                var updated = service.Update(item);
                result.State = updated;
                if (updated)
                {
                    result.Message = "修改成功";
                }
                else
                {
                    result.Message = "修改失败";
                }
            }
            else{
                if(!CanCreated)
                {
                    return GotoErrorPage(CannotAddText);
                }
                var item = new tbl_VerifyFlow();
                item.C_Name = info.Name;
                item.C_Description = info.Description;
                item.C_CreatedDate = DateTime.Now;
                var added = service.Add(item);
                if (added)
                {
                    result.Message = "添加成功";
                }
                else
                {
                    result.Message = "添加失败";
                }
            }
            return Json(result);
        }
        public ActionResult FlowStep(int Key)
        {
            if (!CanRead)
            {
                return GotoErrorPage(CannotReadText);
            }
            var infos= new List<FlowStepInfo>();
            ViewBag.VerifyKey = Key;

            if (Key > 0) 
            {
                var service = Container.GetService<IFlowStepService>();
                infos = service.GetVFlowStepModels(vfs => vfs.VerifyId == Key ).ToList();
                var flowservice = Container.GetService<IVerifyFlowService>();
                ViewBag.FlowName = flowservice.GetModels(f => f.keyid == Key).Select(f=>f.C_Name).FirstOrDefault();
            }
            return View(infos);
        }
        public ActionResult GetRoleCombo()
        {
            var rolecontroller = new RoleController();
            return rolecontroller.GetRoles();
        }
        [HttpPost]
        public ActionResult StepManage(string FlowStepsJson)
        {
            var infos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FlowStepInfo>>(FlowStepsJson);
            var deletedKey = infos.Select(i => i.VerifyKey).FirstOrDefault();
            var service = Container.GetService<IFlowStepService>();
            var deleted_rows = service.GetModels(fs => fs.C_VerifyId == deletedKey).ToList();
            var deleted = true;
            //
            var result = new AjaxResult();
            if (deleted_rows != null && deleted_rows.Count>0)
            {
                deleted = service.RemoveRange(deleted_rows);
            }
            //如果清除成功才进行下一步
            var updated = false;
            if (deleted)
            {
                var list = FlowStepInfo.ConvertTotblFlowSteps(infos);
                updated=service.AddRange(list);
            }
            else
            {
                result.Message = "清除原有记录失败，设置失败";
            }
            result.State = updated;
            if (result.State)
            {
                result.Message = "设置成功";
            }
            else
            {
                result.Message = "设置失败";
            }
            return Json(result);
        }
    }
}