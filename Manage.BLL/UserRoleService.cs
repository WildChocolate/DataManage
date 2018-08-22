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
    public class UserRoleService : BaseService<tbl_User_Role>, IUserRoleService
    {
        public override void SetDAL()
        {
            Dal = DALContainer.Container.GetRepository<IUserRoleRepo>();
        }

        public IQueryable<V_User_Role> GetModels(System.Linq.Expressions.Expression<Func<V_User_Role, bool>> wherelambda)
        {
            var urdal = Dal as IUserRoleRepo;
            return urdal.GetModels(wherelambda);
        }


        public bool UpdateUserRoles(int userid, int[] Roles)
        {
            var urdal = Dal as IUserRoleRepo;
            return urdal.UpdateUserRoles(userid,Roles);
        }
    }
}
