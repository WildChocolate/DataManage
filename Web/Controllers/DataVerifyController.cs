using Manage.BLLContainer;
using Manage.Common;
using Manage.Common.Condition;
using Manage.IBLL;
using Manage.IBLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ControllerExt;

namespace Web.Controllers
{
    public class DataVerifyController : PowerController
    {
        string[] menusAction = { "TxtQuery" };
        protected override string[] MenusAction
        {
            get { return menusAction; }
        }
        // GET: DataVerify
        public ActionResult TxtQuery()
        {
            if (!CanRead)
                return GotoErrorPage(CannotReadText);
            ViewBag.DataTypeKey=(int)DataTypeEnum.Text;
            return View();
        }
        public ActionResult SearchDataVerify(DataVerifyStepPager pager)
        {
            var service = Container.GetService<IDataService>();
            
            var grid = service.GetDataVerifyGrid(pager,userDto);
            return Json(grid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DataVerifyDetails(long Key)
        {
            if (!CanRead)
                return GotoErrorPage(CannotReadText);
            //获取步骤详情
            var service = Container.GetService<IDataVerifyStepService>();
            var cnt = service.GetVModels(Key).Count();
            var items = service.GetVModels(Key).ToList();
            var infos = DataVerifyStepInfo.ConvertToDataVerifyStepInfos(items);
            return View(infos);
        }
        [HttpGet]
        public ActionResult DataVerify(long Key)
        {
            if (!CanRead)
            {
                return GotoErrorPage(CannotReadText);
            }
            var service = Container.GetService<IDataService>();
            var detailInfo = new DataVerifyDetailInfo();
            var dataitem = service.GetModels(d => d.keyid == Key).FirstOrDefault();
            if (dataitem != null)
            {
                detailInfo.Data = DataInfo.ConvertToDataInfo(dataitem);
                var stepservice = Container.GetService<IDataVerifyStepService>();
                var steps = stepservice.GetVModels(Key).ToList();
                detailInfo.Steps = DataVerifyStepInfo.ConvertToDataVerifyStepInfos(steps);
            }
            return View(detailInfo);
        }
        [HttpPost]
        public ActionResult DataVerify(DataVerifyStepInfo info)
        {
            var result = new AjaxResult();
            if(CanUpdated&&CanCreated){
                if (info.Key > 0)
                {
                    var service = Container.GetService<IDataVerifyStepService>();
                    var item = service.GetModels(vf => vf.C_DataId == info.Key).AsEnumerable().LastOrDefault();
                    if (item != null)
                    {
                        item.C_UserId = userDto.User.keyid;
                        item.C_Advice = info.Advice;
                        item.C_State = info.State;
                        item.C_UpdatedDate = DateTime.Now;
                        result.State = service.Update(item);
                    }
                    else
                    {
                        result.Message = "审核失败，不存在的步骤!!!";
                    }
                    if (result.State)
                    {
                        result.Message = "操作成功";
                    }
                    else
                    {
                        result.Message = "操作失败";
                    }
                }
                else
                {
                    result.Message = "dataKey不能小0";
                }
            }
            else{
                result.Message="你没有审核权限，请联系管理员开通，审核权限： 添加+修改";
            }
            return Json(result);
        }
    }
}