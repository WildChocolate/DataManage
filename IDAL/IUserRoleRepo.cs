using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IDAL
{
    public interface IUserRoleRepo : IBaseRepo<tbl_User_Role>
    {
        IQueryable<V_User_Role> GetModels(Expression<Func<V_User_Role, bool>> wherelambda);
        bool UpdateUserRoles(int userid, int[] Roles);
    }
}
