using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL
{
    public interface IDataVerifyStepService:IBaseService<tbl_DataVerifyStep>
    {
        IQueryable<V_DataVerifyStep> GetVModels(long Datakey); 
    }
}
