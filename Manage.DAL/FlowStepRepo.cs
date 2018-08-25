using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL
{
    public class FlowStepRepo:BaseRepo<tbl_FlowStep>, IFlowStepRepo
    {
        public IQueryable<V_Role_Data_Verify_Step> GetVFlowStepModels(System.Linq.Expressions.Expression<Func<V_Role_Data_Verify_Step, bool>> wherelambda)
        {
            var db = DBContextFactory.CreateDB();
            var entities = db.Set<V_Role_Data_Verify_Step>();
            return entities.Where(wherelambda);
        }
    }
}
