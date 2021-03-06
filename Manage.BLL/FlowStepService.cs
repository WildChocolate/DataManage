﻿using Manage.IBLL;
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

        public IQueryable<FlowStepInfo> GetVFlowStepModels(System.Linq.Expressions.Expression<Func<V_Verify_Step, bool>> wherelambda)
        {
            var fsdal = Dal as IFlowStepRepo;
            return fsdal.GetVFlowStepModels(wherelambda).Select(vfs => new FlowStepInfo { Key=vfs.StepId, Name=vfs.StepName, StepRole=vfs.StepRole, VerifyKey=vfs.VerifyId, Description=vfs.StepDescription, Step=vfs.StepNumber });
        }


        public IQueryable<V_Role_Data_Verify_Step> GetRoleVerifyStepsByRole(int RoleKey, int DataTypeKey)
        {
            var fsdal = Dal as IFlowStepRepo;
            return fsdal.GetRoleVerifyStep(rvf => rvf.RoleId == RoleKey && rvf.DataTypeId == DataTypeKey);
        }
    }
}
