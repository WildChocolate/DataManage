using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL
{
    public class DataVerifyStepRepo:BaseRepo<tbl_DataVerifyStep>,IDataVerifyStepRepo
    {
        public IQueryable<V_DataVerifyStep> GetVModels(Expression<Func<V_DataVerifyStep, bool>> wherelambda)
        {
            var entities = DBContextFactory.CreateDB().Set<V_DataVerifyStep>();
            return entities.Where(wherelambda);
        }
    }
}
