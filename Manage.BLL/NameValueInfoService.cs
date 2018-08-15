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
    public class NameValueInfoService : BaseService<tbl_NameValueInfo>, INameValueInfoService
    {
        public override void SetDAL()
        {
            Dal = DALContainer.Container.GetRepository<INameValueInfoRepo>();
        }
    }
}
