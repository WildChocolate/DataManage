using Manage.IBLL;
using Manage.IBLL.Dto;
using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL
{
    public class FlowStepService : BaseService<tbl_FlowStep>, IFlowStepService
    {
        public override void SetDAL()
        {
            Dal = DALContainer.Container.GetRepository<IFlowStepRepo>();
        }

        public IQueryable<FlowStepInfo> GetVFlowStepModels(System.Linq.Expressions.Expression<Func<V_Role_Data_Verify_Step, bool>> wherelambda)
        {
            var fsdal = Dal as IFlowStepRepo;
            return fsdal.GetVFlowStepModels(wherelambda).Select(vfs => new FlowStepInfo { Key=vfs.stepid, Name=vfs.StepName, StepRole=vfs.StepRole, VerifyKey=vfs.VerifyId, Description=vfs.StepDesction, Step=vfs.StepNumber });
        }
    }
}
