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
    public class RoleService : BaseService<tbl_Role>, IRoleService
    {
        public override void SetDAL()
        {
            Dal = DALContainer.Container.GetRepository<IRoleRepo>();
        }

        public T GetRoleDtoByKey<T>(int Key)
        {
            var userdal = Dal as IRoleRepo;
            return userdal.GetRoleByKey<T>(Key);
        }
    }
}
