using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IDAL
{
    public interface IDataVerifyStepRepo : IBaseRepo<tbl_DataVerifyStep>
    {
        IQueryable<V_DataVerifyStep> GetVModels(Expression<Func<V_DataVerifyStep, bool>> wherelambda);
    }
}
