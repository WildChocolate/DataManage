using Manage.IBLL;
using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL
{
    public class DataVerifyStepService:BaseService<tbl_DataVerifyStep>,IDataVerifyStepService
    {
        public override void SetDAL()
        {
            Dal = DALContainer.Container.GetRepository<IDataVerifyStepRepo>();
        }

        public IQueryable<V_DataVerifyStep> GetVModels(long Datakey)
        {
            var vsrepo = Dal as IDataVerifyStepRepo;
            return vsrepo.GetVModels(vf => vf.DataKey == Datakey);
        }
    }
}
