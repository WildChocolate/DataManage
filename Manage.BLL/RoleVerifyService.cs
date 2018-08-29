using Manage.DALContainer;
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
    public class RoleVerifyService:BaseService<tbl_RoleVerify>, IRoleVerifyService
    {

        public override void SetDAL()
        {
            Dal = Dal = Container.GetRepository<IRoleVerifyRepo>();
        }
    }
}
