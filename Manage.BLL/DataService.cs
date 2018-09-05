using Manage.Common.Condition;
using Manage.IBLL;
using Manage.IBLL.Dto;
using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Manage.Common;
using Manage.DALContainer;
using Manage.Common.DataGrid;


namespace Manage.BLL
{
    public class DataService:BaseService<tbl_Data>, IDataService
    {

        public override void SetDAL()
        {
            Dal = DALContainer.Container.GetRepository<IDataRepo>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto">需要用到当前用户的很多信息，所以直接把这个对象递过来</param>
        /// <returns></returns>
        public DataVerifyGrid GetDataVerifyGrid(DataVerifyStepPager pager, UserDto userDto)
        {
            //拼接查询条件
            Expression<Func<tbl_Data, bool>> wherelambda = d => d.C_VerifyState==pager.State && d.C_DataTypeId==pager.DataTypeKey;
            if(!string.IsNullOrWhiteSpace(pager.Name))
            {
                wherelambda=wherelambda.And(d => d.C_Name.Contains(pager.Name));
            }
            if (!string.IsNullOrWhiteSpace(pager.DateFrom))
            {
                wherelambda=wherelambda.And(d => d.C_CreatedDate > Convert.ToDateTime(pager.DateFrom));
            }
            if (!string.IsNullOrWhiteSpace(pager.DateTo))
            {
                wherelambda=wherelambda.And(d => d.C_CreatedDate < Convert.ToDateTime(pager.DateTo));
            }
            //1  首先，找出所有审核中的文件记录，默认是只有前一步审核通过才会进入下一步，所以选择同个一个dataid的最后一条记录
            var dataVerifyRepo = Container.GetRepository<IDataVerifyStepRepo>();
            //这个是数据与步骤表的简化版本，只选择两列,这里C_UserId代表
            var dataSteps = dataVerifyRepo.GetModels(df => df.C_State == pager.State).Select(df => new {Key= df.C_DataId, Step=df.C_StepId }).Distinct().ToList();
            //这里把所有的待审核dataid取出

            var verifyDataKeys = dataSteps.Select(ds => ds.Key ).Distinct().ToList();
            //2 根据上面的dataids 找出对应的data
            List<tbl_Data> dataItems = null;
            var dataRepo = Container.GetRepository<IDataRepo>();
            if (verifyDataKeys != null && verifyDataKeys.Count() > 0)
            {
                wherelambda = wherelambda.And(d=> verifyDataKeys.Contains(d.keyid));
                dataItems = dataRepo.GetModelsByPage(pager.pageSize, pager.pageIndex, true, d => d.keyid, wherelambda).ToList();
                //var items = dataRepo.GetModelsByPage(pager.pageSize, pager.pageIndex, true, d => d.keyid, wherelambda).ToList();
                //dataItems = (from k in unVerifyDataKeys
                //             from d in items
                //             where k == d.keyid
                //             select new tbl_Data
                //             {
                //                 keyid = d.keyid,
                //                 C_UserId = d.C_UserId,
                //                 C_DataTypeId = d.C_DataTypeId,
                //                 C_Name = d.C_Name,
                //                 C_Description = d.C_Description,
                //                 C_Path = d.C_Path,
                //                 C_UpdatedDate = d.C_UpdatedDate,
                //                 C_CreatedDate = d.C_CreatedDate,
                //                 C_VerifyState = d.C_VerifyState
                //             }).ToList();
            }
            //3 先把data 转换成DataVerifyInfo,但这个转换不完全，只转了与显示相关的属性，还有也操作相关的要在外面另外添加
            var infos = DataVerifyInfo.ConvertToDataVerifyInfos(dataItems);
            var flowStepRepo = Container.GetRepository<IFlowStepRepo>();
            //从后往前遍历，因为包含删除操作，只有这样才不会影响剩余元素
            for (var idx = infos.Count-1; idx >= 0; idx--)
            {
                var info = infos[idx];
                //找到当前的最新的步骤 id，info.Key就是相应的data 的keyid
                var currentStep = dataSteps.Where(ds => ds.Key == info.Key).Max(ds => ds.Step);
                if (currentStep > 0)
                {
                    var currentStepRole = flowStepRepo.GetModels(fs => fs.keyid == currentStep).FirstOrDefault().C_RoleId;
                    //再通过 步骤id 找到相应的流程
                    var allSteps = flowStepRepo.GetRoleVerifyStep(vs => vs.stepid == currentStep);
                    var allStepRoles = allSteps.Select(s => s.StepRole).ToList();//这里找出这个流程的所有角色

                    var userRoleKeys = userDto.Roles.Select(r => r.keyid).ToList();
                    //在查询状态为通过的单的时候,直接设置为无法审核
                    if (pager.State)
                    {
                        info.Verify = "<a>无法操作</a>";
                    }
                    else
                    {
                        //如果这个用户的角色列表包含当前的 步骤角色，那么这个用户对这个data 拥有审核权限
                        if (userRoleKeys.Contains(currentStepRole))
                        {
                            info.Verify = string.Format("<a href='{0}'>审核</a>", "DataVerify?Key=" + info.Key);
                        }
                        else if (userRoleKeys.Intersect(allStepRoles) != null)//如果当前用户的所有角色中与此流程对应的角色有交集,则此记录对他可见，但不能审核
                        {
                            info.Verify = "<a>无法操作</a>";

                        }
                        else//如果当前用户的所有角色中与此流程对应的角色没有交集，他对这个数据没有审核权限，连给看都不行
                        {
                            infos.Remove(info);
                            wherelambda.And(d => d.keyid != info.Key);
                        }
                    }
                }
            }
            var grid = new DataVerifyGrid();
            grid.rows = infos;
            //total 并不完全正确，有可能与实际的不符
            grid.total = dataSteps.Where(ds => verifyDataKeys.Contains(ds.Key)).GroupBy(ds=> ds.Key).Count();
            return grid;
        }

    }
}
