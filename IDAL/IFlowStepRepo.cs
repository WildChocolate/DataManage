using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IDAL
{
    public interface IFlowStepRepo : IBaseRepo<tbl_FlowStep>
    {
        IQueryable<V_Verify_Step> GetVFlowStepModels(Expression<Func<V_Verify_Step, bool>> wherelambda);
        IQueryable<V_Role_Data_Verify_Step> GetRoleVerifyStep(Expression<Func<V_Role_Data_Verify_Step, bool>> wherelambda);
    }
}
