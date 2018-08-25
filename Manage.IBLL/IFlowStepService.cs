using Manage.IBLL.Dto;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL
{
    public interface IFlowStepService : IBaseService<tbl_FlowStep>
    {
        IQueryable<FlowStepInfo> GetVFlowStepModels(Expression<Func<V_Role_Data_Verify_Step, bool>> wherelambda); 
    }
}
